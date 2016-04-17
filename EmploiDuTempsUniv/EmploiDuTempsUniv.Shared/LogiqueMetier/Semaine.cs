using EmploiDuTempsUniv.Sauvegarde;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EmploiDuTempsUniv.LogiqueMetier
{
	public class Semaine
	{
		private static Semaine instance;

		private List<Jour> jours = new List<Jour>();

		private Sauvable saveManager;

		public List<Jour> Jours
		{
			get
			{
				return this.jours;
			}
			set
			{
				this.jours = value;
			}
		}

		static Semaine()
		{
		}

		private Semaine(Sauvable s)
		{
			for (int i = 0; i < 7; i++)
			{
				this.jours.Add(new Jour((Jour.day)i));
			}
			this.saveManager = s;
		}

		public Task charger()
		{
			return this.saveManager.readPlanning();
		}

		public static Semaine getInstance()
		{
			if (Semaine.instance == null)
			{
				Semaine.instance = new Semaine(new Serializer());
			}
			return Semaine.instance;
		}

		public Jour getJour(Jour.day j)
		{
			return this.jours.ElementAt<Jour>((int)j);
		}

		public void retireMatiere(Matiere m)
		{
			foreach (Jour jour in this.jours)
			{
				jour.retireMatiere(m);
			}
		}

		public async void save()
		{
			await this.saveManager.writePlanning(this.jours);
		}
	}
}