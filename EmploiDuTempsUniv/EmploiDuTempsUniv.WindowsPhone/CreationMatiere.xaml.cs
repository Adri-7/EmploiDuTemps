using EmploiDuTempsUniv.LogiqueMetier;
using System;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
    public sealed partial class CreationMatiere : Page
    {

        private ColorDialog colorDialog;

        public CreationMatiere()
        {
            this.InitializeComponent();
            colorDialog = new ColorDialog();

            ad.AdSdkError += AdMediatorHandlers.AdMediator_Bottom_AdError;
            ad.AdMediatorFilled += AdMediatorHandlers.AdMediator_Bottom_AdFilled;
            ad.AdMediatorError += AdMediatorHandlers.AdMediator_Bottom_AdMediatorError;
            ad.AdSdkEvent += AdMediatorHandlers.AdMediator_Bottom_AdSdkEvent;
            AdMediatorHandlers.InitAdsOptionalSize(ad);
        }

        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(MainPage));
        }

        private async void Color_Click(object sender, RoutedEventArgs e)
        {
            if(colorDialog.IsOpened)
            {
                return;
            }

            Button originalSource = e.OriginalSource as Button;

            this.BottomAppBar.Visibility = Visibility.Collapsed;
            await colorDialog.ShowAsync();
            this.BottomAppBar.Visibility = Visibility.Visible;

            if (colorDialog.SelectedColor != null && originalSource != null)
            {
                originalSource.Background = colorDialog.SelectedColor;
            }
        }

        private void CreerClick(object sender, RoutedEventArgs e)
        {
            SolidColorBrush background = this.FondColor.Background as SolidColorBrush;
            SolidColorBrush solidColorBrush = this.PoliceColor.Background as SolidColorBrush;
            if (Matieres.getInstance().ajouterMatiere(this.intitule.Text, background.Color, solidColorBrush.Color))
            {
                base.Frame.Navigate(typeof(MainPage));
                return;
            }
            this.erreur.Text = "Matière déjà présente";
            this.intitule.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void intituleKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (this.intitule.Text == "")
            {
                this.creer.IsEnabled = false;
                return;
            }
            this.creer.IsEnabled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.RoamingSettings.Values["mode"] != null && !(bool)ApplicationData.Current.RoamingSettings.Values["mode"])
            {
                this.prof.Visibility = Visibility.Visible;
                this.profTB.Visibility = Visibility.Visible;
                return;
            }
            this.prof.Visibility = Visibility.Collapsed;
            this.profTB.Visibility = Visibility.Collapsed;
        }
    }
}