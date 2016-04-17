using EmploiDuTempsUniv.Sauvegarde;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI;

namespace EmploiDuTempsUniv.LogiqueMetier
{
	public class Matieres
	{
		private static Matieres instance;

		private Sauvable saveManager;

		private List<Matiere> listeMatieres = new List<Matiere>();

		public List<Matiere> ListeMatieres
		{
			get
			{
				return this.listeMatieres;
			}
			set
			{
				this.listeMatieres = value;
			}
		}

		static Matieres()
		{
		}

		private Matieres(Sauvable s)
		{
			this.saveManager = s;
		}

		public bool ajouterMatiere(string nom, Color couleurF, Color couleurP)
		{
			if (this.estPresent(nom))
			{
				return false;
			}
			this.listeMatieres.Add(new Matiere(nom, couleurF, couleurP));
			this.save();
			return true;
		}

		public Task charger()
		{
			return this.saveManager.readMatieres();
		}

		public bool estPresent(string nom)
		{
			bool flag = false;
			foreach (Matiere listeMatiere in this.listeMatieres)
			{
				if (listeMatiere.Intitule != nom)
				{
					continue;
				}
				flag = true;
				break;
			}
			return flag;
		}

		public static Matieres getInstance()
		{
			if (Matieres.instance == null)
			{
				Matieres.instance = new Matieres(new Serializer());
			}
			return Matieres.instance;
		}

		public Matiere getMatiere(string m)
		{
			Matiere matiere;
			List<Matiere>.Enumerator enumerator = this.listeMatieres.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Matiere current = enumerator.Current;
					if (current.Intitule != m)
					{
						continue;
					}
					matiere = current;
					return matiere;
				}
				return null;
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		public void retireMatiere(string m)
		{
			foreach (Matiere listeMatiere in this.listeMatieres)
			{
				if (listeMatiere.Intitule != m)
				{
					this.save();
				}
				else
				{
					Semaine.getInstance().retireMatiere(listeMatiere);
					this.listeMatieres.Remove(listeMatiere);
					break;
				}
			}
		}

		public void retireMatiere(Matiere m)
		{
			if (m != null)
			{
				Semaine.getInstance().retireMatiere(m);
				this.listeMatieres.Remove(m);
				this.save();
			}
		}

		public async void save()
		{
			await this.saveManager.writeMatieres(this.listeMatieres);
		}
	}
}