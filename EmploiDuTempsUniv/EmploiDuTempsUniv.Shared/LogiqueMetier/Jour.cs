using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace EmploiDuTempsUniv.LogiqueMetier
{
	[DataContract]
	public class Jour
	{
		private List<Creneau> creneaux = new List<Creneau>();

		[DataMember]
		public List<Creneau> Creneaux
		{
			get
			{
				return this.creneaux;
			}
			set
			{
				this.creneaux = value;
			}
		}

		[DataMember]
		public Jour.day jour
		{
			get;
			set;
		}

		public Jour(Jour.day j)
		{
			this.jour = j;
		}

		public bool ajouterCreneau(Matiere m, TimeSpan debut, TimeSpan fin, Creneau.TypeC t, string salle, Creneau.parite _parite)
		{
			bool flag = true;
			Creneau creneau = new Creneau(m, debut, fin, t, this.jour, salle, _parite);
			foreach (Creneau creneau1 in this.creneaux)
			{
				if (!creneau.Conflit(creneau1))
				{
					continue;
				}
				flag = false;
				break;
			}
			if (flag)
			{
				this.creneaux.Add(creneau);
			}
			return flag;
		}

		public bool ajouterCreneau(Matiere m, TimeSpan debut, TimeSpan fin, Creneau.TypeC t, string salle, string prof, Creneau.parite _parite)
		{
			bool flag = true;
			Creneau creneau = new Creneau(m, debut, fin, t, this.jour, salle, prof, _parite);
			foreach (Creneau creneau1 in this.creneaux)
			{
				if (!creneau.Conflit(creneau1))
				{
					continue;
				}
				flag = false;
				break;
			}
			if (flag)
			{
				this.creneaux.Add(creneau);
			}
			return flag;
		}

		public bool editerCreneau(Creneau creneau, Matiere m, TimeSpan debut, TimeSpan fin, Creneau.TypeC t, string salle, string prof, Creneau.parite _parite)
		{
			bool flag = true;
			Semaine.getInstance().getJour(creneau.Jour).retireCreneau(creneau);
			foreach (Creneau creneau1 in this.creneaux)
			{
				if (!creneau1.Conflit(debut, fin))
				{
					continue;
				}
				flag = false;
				break;
			}
			if (flag)
			{
				creneau.Matiere = m;
				creneau.Debut = debut;
				creneau.Fin = fin;
				creneau.Type = t;
				creneau.Salle = salle;
				creneau.Prof = prof;
				creneau.Parite = _parite;
			}
			Semaine.getInstance().getJour(creneau.Jour).creneaux.Add(creneau);
			return flag;
		}

		public TimeSpan fin()
		{
			TimeSpan timeSpan = new TimeSpan(8, 0, 0);
			foreach (Creneau creneau in this.creneaux)
			{
				if (creneau.Fin <= timeSpan)
				{
					continue;
				}
				timeSpan = creneau.Fin;
			}
			return timeSpan;
		}

		public void retireCreneau(Creneau c)
		{
			if (c != null)
			{
				this.creneaux.Remove(c);
			}
		}

		public void retireMatiere(Matiere m)
		{
			foreach (Creneau creneau in this.creneaux)
			{
				if ((object)creneau.Matiere != (object)m)
				{
					continue;
				}
				this.creneaux.Remove(creneau);
			}
		}

		public string toString()
		{
			return Enum.GetName(typeof(Jour.day), this.jour);
		}

		[Flags]
		public enum day
		{
			Lundi,
			Mardi,
			Mercredi,
			Jeudi,
			Vendredi,
			Samedi,
			Dimanche
		}
	}
}