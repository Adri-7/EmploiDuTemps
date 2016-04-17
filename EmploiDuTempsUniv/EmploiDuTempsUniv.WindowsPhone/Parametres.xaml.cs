using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
	public sealed partial class Parametres : Page
	{
		private const string HELPSEC = "Avec le mode actuel, les types de cours sont CE (Classe Entière) et DG (Demi-Groupe). De plus, le professeur peut être saisi pour chaque matière.";
		private const string HELPSUP = "Avec le mode actuel, les types de cours sont TP, TD et CM. De plus, le professeur peut être saisi pour chaque cours.";

		private bool superieur = true;
		private bool samedi;
		private bool dimanche;

		private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

		public Parametres()
		{
			this.InitializeComponent();

            AdMediator_Parametres.AdSdkError += AdMediatorHandlers.AdMediator_Bottom_AdError;
            AdMediator_Parametres.AdMediatorFilled += AdMediatorHandlers.AdMediator_Bottom_AdFilled;
            AdMediator_Parametres.AdMediatorError += AdMediatorHandlers.AdMediator_Bottom_AdMediatorError;
            AdMediator_Parametres.AdSdkEvent += AdMediatorHandlers.AdMediator_Bottom_AdSdkEvent;
            AdMediatorHandlers.InitAdsOptionalSize(AdMediator_Parametres);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.roamingSettings.Values["mode"] != null)
            {
                this.superieur = (bool)this.roamingSettings.Values["mode"];
            }
            if (this.roamingSettings.Values["samedi"] != null)
            {
                this.samedi = (bool)this.roamingSettings.Values["samedi"];
            }
            if (this.roamingSettings.Values["dimanche"] != null)
            {
                this.dimanche = (bool)this.roamingSettings.Values["dimanche"];
            }
            this.modeTS.IsOn = this.superieur;
            this.modeTS_Toggled(null, null);
            this.samediTS.IsOn = this.samedi;
            this.samediTS_Toggled(null, null);
            this.dimancheTS.IsOn = this.dimanche;
            this.dimancheTS_Toggled(null, null);
        }

        /* --------------------- Events handlers --------------------------- */

        private void accueilClick(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

        private void modeTS_Toggled(object sender, RoutedEventArgs e)
        {
            if (!this.modeTS.IsOn)
            {
                this.superieur = false;
                this.modeHelp.Text = HELPSEC;
            }
            else
            {
                this.superieur = true;
                this.modeHelp.Text = HELPSUP;
            }
            this.roamingSettings.Values["mode"] = this.superieur;
        }

        private void samediTS_Toggled(object sender, RoutedEventArgs e)
		{
			if (!this.samediTS.IsOn)
			{
				this.samedi = false;
			}
			else
			{
				this.samedi = true;
			}
			this.roamingSettings.Values["samedi"] = this.samedi;
		}

		private void dimancheTS_Toggled(object sender, RoutedEventArgs e)
		{
			if (!this.dimancheTS.IsOn)
			{
				this.dimanche = false;
			}
			else
			{
				this.dimanche = true;
			}
			this.roamingSettings.Values["dimanche"] = this.dimanche;
		}
	}
}