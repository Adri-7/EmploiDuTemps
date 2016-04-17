using EmploiDuTempsUniv.LogiqueMetier;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.Storage;

namespace EmploiDuTempsUniv
{
	public class ElementContext
	{
		private Creneau c;
        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

		public Creneau Creneau
		{
			get
			{
				return this.c;
			}
		}

		public string Debut
		{
			get
			{
				return this.c.Debut.ToString("hh\\:mm");
			}
		}

		public string Duree
		{
			get
			{
				return this.c.Duree.ToString("hh\\hmm");
			}
		}

		public string Fin
		{
			get
			{
				return this.c.Fin.ToString("hh\\:mm");
			}
		}

		public SolidColorBrush Fond
		{
			get
			{
				return new SolidColorBrush(this.c.Matiere.CouleurFond);
			}
		}

		public SolidColorBrush Impaire
		{
			get
			{
				if (this.c.Parite != EmploiDuTempsUniv.LogiqueMetier.Creneau.parite.PaireImpaire && this.c.Parite != EmploiDuTempsUniv.LogiqueMetier.Creneau.parite.Impaire)
				{
					return new SolidColorBrush();
				}
				return Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
			}
		}

		public string Intitule
		{
			get
			{
				return this.c.Matiere.Intitule;
			}
		}

		public EmploiDuTempsUniv.LogiqueMetier.Jour.day Jour
		{
			get
			{
				return this.c.Jour;
			}
		}

		public SolidColorBrush Paire
		{
			get
			{
				if (this.c.Parite != EmploiDuTempsUniv.LogiqueMetier.Creneau.parite.PaireImpaire && this.c.Parite != EmploiDuTempsUniv.LogiqueMetier.Creneau.parite.Paire)
				{
					return new SolidColorBrush();
				}
				return Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
			}
		}

		public SolidColorBrush Police
		{
			get
			{
				return new SolidColorBrush(this.c.Matiere.CouleurPolice);
			}
		}

		public string Prof
		{
			get
			{
				string str = "";
				str = (roamingSettings.Values["mode"] == null || !(bool)roamingSettings.Values["mode"] ? this.c.Matiere.Prof : this.c.Prof);
				if (!string.IsNullOrEmpty(str))
				{
					return str;
				}
				return "n/a";
			}
		}

		public string Salle
		{
			get
			{
				return this.c.Salle;
			}
		}

		public string Type
		{
			get
			{
				return this.c.typeComplet();
			}
		}

		public ElementContext(EmploiDuTempsUniv.LogiqueMetier.Creneau creneau)
		{
			this.c = creneau;
		}

		public void supprimeCreneau()
		{
			Semaine.getInstance().getJour(this.c.Jour).retireCreneau(this.c);
		}
	}
}