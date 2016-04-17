using EmploiDuTempsUniv.LogiqueMetier;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EmploiDuTempsUniv
{
	public sealed partial class GestionMatieres : Page
	{
		private Matiere selectedMatiere;

		private ColorDialog picker = new ColorDialog();

        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

		public GestionMatieres()
		{
            this.InitializeComponent();
			foreach (Matiere listeMatiere in Matieres.getInstance().ListeMatieres)
			{
				this.matiere.Items.Add(listeMatiere.toString());
			}
			this.matiere.SelectedIndex = 0;

            AdMediator_GestionMatiere.AdSdkError += AdMediatorHandlers.AdMediator_Bottom_AdError;
            AdMediator_GestionMatiere.AdMediatorFilled += AdMediatorHandlers.AdMediator_Bottom_AdFilled;
            AdMediator_GestionMatiere.AdMediatorError += AdMediatorHandlers.AdMediator_Bottom_AdMediatorError;
            AdMediator_GestionMatiere.AdSdkEvent += AdMediatorHandlers.AdMediator_Bottom_AdSdkEvent;
            AdMediatorHandlers.InitAdsOptionalSize(AdMediator_GestionMatiere);
        }

		private void annuler_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		private async void Color_Click(object sender, RoutedEventArgs e)
		{
            if(this.picker.IsOpened)
            {
                return;
            }

			Button originalSource = e.OriginalSource as Button;

            this.BottomAppBar.Visibility = Visibility.Collapsed;
			await this.picker.ShowAsync();
            this.BottomAppBar.Visibility = Visibility.Visible;

            if (this.picker.SelectedColor != null && originalSource != null)
			{
				originalSource.Background = this.picker.SelectedColor;
			}
		}

		private void matiere_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (UIElement child in this.form.Children)
			{
				child.Visibility = Visibility.Visible;
			}

			this.Modifier.IsEnabled = true;
			this.Supprimer.IsEnabled = true;

			this.selectedMatiere = Matieres.getInstance().getMatiere((string)this.matiere.SelectedItem);

			this.intitule.Text = this.selectedMatiere.Intitule;
			this.FondColor.Background = new SolidColorBrush(this.selectedMatiere.CouleurFond);
			this.PoliceColor.Background = new SolidColorBrush(this.selectedMatiere.CouleurPolice);

			if (roamingSettings.Values["mode"] == null || roamingSettings.Values["mode"] != null && (bool)roamingSettings.Values[ "mode"])
			{
				this.prof.Visibility = Visibility.Collapsed;
				this.profTB.Visibility = Visibility.Collapsed;
			}
		}

		private void modifier_Click(object sender, RoutedEventArgs e)
		{
			if (this.selectedMatiere != null)
			{
				SolidColorBrush background = this.FondColor.Background as SolidColorBrush;
				SolidColorBrush solidColorBrush = this.PoliceColor.Background as SolidColorBrush;
				this.selectedMatiere.updateMatiere(this.intitule.Text, background.Color, solidColorBrush.Color);
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		private void supprimer_Click(object sender, RoutedEventArgs e)
		{
			if (this.selectedMatiere != null)
			{
				Matieres.getInstance().retireMatiere(this.selectedMatiere);
				base.Frame.Navigate(typeof(MainPage));
			}
		}
	}
}