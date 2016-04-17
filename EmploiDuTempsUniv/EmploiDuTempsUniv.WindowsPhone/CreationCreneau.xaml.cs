using EmploiDuTempsUniv.LogiqueMetier;
using System;
using System.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
	public sealed partial class CreationCreneau : Page
	{
		private Creneau creneau;
        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

		public CreationCreneau()
		{
			this.InitializeComponent();
			TimePicker timePicker = this.debut;
            timePicker.TimeChanged += debut_TimeChanged;

            AdMediator_AjouterCreneau.AdSdkError += AdMediatorHandlers.AdMediator_Bottom_AdError;
            AdMediator_AjouterCreneau.AdMediatorFilled += AdMediatorHandlers.AdMediator_Bottom_AdFilled;
            AdMediator_AjouterCreneau.AdMediatorError += AdMediatorHandlers.AdMediator_Bottom_AdMediatorError;
            AdMediator_AjouterCreneau.AdSdkEvent += AdMediatorHandlers.AdMediator_Bottom_AdSdkEvent;
            AdMediatorHandlers.InitAdsOptionalSize(AdMediator_AjouterCreneau);
        }

		private void ajouter_Click(object sender, RoutedEventArgs e)
		{
			TimeSpan timeSpan = new TimeSpan(0, 29, 0);
            if (this.matiere.SelectedIndex != -1 && this.type.SelectedIndex != -1 && this.jour.SelectedIndex != -1 && this.fin.Time > (this.debut.Time + timeSpan) && this.debut.Time.Hours >= 7 && this.fin.Time.TotalHours <= 22 && !string.IsNullOrWhiteSpace(this.salle.Text))
            {
                string text = null;
                Creneau.parite _parite = this.pariteFromIndex(this.parite.SelectedIndex);
                if (roamingSettings.Values["mode"] != null && (bool)roamingSettings.Values["mode"])
				{
					text = this.prof.Text;
				}
				if (this.creneau != null)
				{
					if (!Semaine.getInstance().getJour((Jour.day)this.jour.SelectedIndex).editerCreneau(this.creneau, Matieres.getInstance().getMatiere(this.matiere.SelectedItem.ToString()), this.debut.Time, this.fin.Time, ((TypeContext)this.type.SelectedItem).RealType, this.salle.Text, text, _parite))
					{
						this.erreur.Text = "Conflit Horaire avec un autre cours";
						return;
					}
					Semaine.getInstance().save();
					base.Frame.GoBack();
					return;
				}
				if (!Semaine.getInstance().getJour((Jour.day)this.jour.SelectedIndex).ajouterCreneau(Matieres.getInstance().getMatiere(this.matiere.SelectedItem.ToString()), this.debut.Time, this.fin.Time, ((TypeContext)this.type.SelectedItem).RealType, this.salle.Text, text, _parite))
				{
					this.erreur.Text = "Conflit Horaire avec un autre cours";
					return;
				}
				Semaine.getInstance().save();
				base.Frame.Navigate(typeof(MainPage));
				return;
			}
			this.erreur.Text = "Veuillez renseigner tous les champs*";
			if (this.matiere.SelectedIndex != -1)
			{
				this.matiereTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.matiereTB.Foreground = new SolidColorBrush(Colors.Red);
			}
			if (this.type.SelectedIndex != -1)
			{
				this.typeTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.typeTB.Foreground = new SolidColorBrush(Colors.Red);
			}
			if (this.jour.SelectedIndex != -1)
			{
				this.jourTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.jourTB.Foreground = new SolidColorBrush(Colors.Red);
			}
			if (this.fin.Time != this.debut.Time)
			{
				this.finTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.finTB.Foreground = new SolidColorBrush(Colors.Red);
				this.erreur.Text = "Veuillez saisir une heure de début et de fin différentes";
			}
			if (this.fin.Time > (this.debut.Time + timeSpan))
			{
				this.finTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.finTB.Foreground = new SolidColorBrush(Colors.Red);
				this.erreur.Text = "Veuillez choisir une durée d'au moins 1/2 heure";
			}
			if (this.debut.Time.Hours < 8 || this.fin.Time.TotalHours > 20)
			{
				this.debutTB.Foreground = new SolidColorBrush(Colors.Red);
				this.erreur.Text = "Heure non valide (Choisir une heure entre 8 et 22h)";
			}
			else
			{
				this.debutTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			if (this.fin.Time >= this.debut.Time)
			{
				this.finTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.finTB.Foreground = new SolidColorBrush(Colors.Red);
				this.erreur.Text = "Heures de fin non valide";
			}
			if (!string.IsNullOrWhiteSpace(this.salle.Text))
			{
				this.salleTB.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
			}
			else
			{
				this.salleTB.Foreground = new SolidColorBrush(Colors.Red);
			}
			this.scrollview.ChangeView(new double?(0), new double?(100), new float?(1f));
		}

		private void annuler_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		private void debut_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
		{
			if (this.fin.Time <= this.debut.Time)
			{
				TimePicker timePicker = this.fin;
				TimeSpan time = this.debut.Time;
				timePicker.Time = time.Add(new TimeSpan(1, 0, 0));
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			this.creneau = null;
			foreach (Matiere listeMatiere in Matieres.getInstance().ListeMatieres)
			{
				this.matiere.Items.Add(listeMatiere.Intitule);
			}
			if (roamingSettings.Values["mode"] == null || (bool)roamingSettings.Values["mode"])
			{
				this.type.DataContext = TypeContext.getSupContext();
				this.prof.Visibility = Visibility.Visible;
				this.profTB.Visibility = Visibility.Visible;
			}
			else
			{
				this.type.DataContext = TypeContext.getSecContext();
				this.prof.Visibility = Visibility.Collapsed;
				this.profTB.Visibility = Visibility.Collapsed;
			}
			if (e.Parameter == null)
			{
				this.type.SelectedIndex = 0;
				this.matiere.SelectedIndex = 0;
                this.jour.SelectedIndex = 0;
                this.parite.SelectedIndex = 0;
			}
			else
			{
				this.creneau = e.Parameter as Creneau;
				this.sousTitre.Text = "Editer un Cours";
				this.selectListItem(this.matiere, this.creneau.Matiere.Intitule);
				this.selectListItem(this.type, this.creneau.Type);
				this.selectListItem(this.jour, (int)this.creneau.Jour);
				this.salle.Text = this.creneau.Salle;
				this.ajouter.Content = "Editer";
				this.debut.Time = this.creneau.Debut;
				this.fin.Time = this.creneau.Fin;
                this.parite.SelectedIndex = (int)this.creneau.Parite;
				if (!string.IsNullOrWhiteSpace(this.creneau.Prof))
				{
					this.prof.Text = this.creneau.Prof;
					return;
				}
			}
		}

		private Creneau.parite pariteFromIndex(int index)
		{
			Creneau.parite _parite = Creneau.parite.PaireImpaire;
			switch (index)
			{
				case 1:
				{
					_parite = Creneau.parite.Paire;
					break;
				}
				case 2:
				{
					_parite = Creneau.parite.Impaire;
					break;
				}
			}
			return _parite;
		}

		private void plus_Click(object sender, RoutedEventArgs e)
		{
			if (this.plus.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
			{
				this.plus.Visibility = Windows.UI.Xaml.Visibility.Visible;
				this.plusButton.Content = "Moins de détails";
				return;
			}
			this.plus.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
			this.plusButton.Content = "Plus de détails";
		}

		private void selectListItem(ComboBox liste, string item)
		{
			for (int i = 0; i < liste.Items.Count; i++)
			{
				if ((string)liste.Items.ElementAt<object>(i) == item)
				{
					liste.SelectedIndex = i;
					return;
				}
			}
		}

		private void selectListItem(ComboBox liste, int item)
		{
			if (item < liste.Items.Count)
			{
				liste.SelectedIndex = item;
			}
		}

		private void selectListItem(ComboBox liste, Creneau.TypeC item)
		{
			foreach (object obj in liste.Items)
			{
				if (((TypeContext)obj).RealType != item)
				{
					continue;
				}
				liste.SelectedItem = obj;
			}
		}
	}
}