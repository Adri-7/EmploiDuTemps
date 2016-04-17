using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
	public sealed partial class APropos : Page
	{
		private versionContext context;

		public APropos()
		{
			this.InitializeComponent();
			this.context = new versionContext();
			this.Version.DataContext = this.context;

            AdMediator_99DF98.AdSdkError += AdMediatorHandlers.AdMediator_Bottom_AdError;
            AdMediator_99DF98.AdMediatorFilled += AdMediatorHandlers.AdMediator_Bottom_AdFilled;
            AdMediator_99DF98.AdMediatorError += AdMediatorHandlers.AdMediator_Bottom_AdMediatorError;
            AdMediator_99DF98.AdSdkEvent += AdMediatorHandlers.AdMediator_Bottom_AdSdkEvent;
            AdMediatorHandlers.InitAdsOptionalSize(AdMediator_99DF98);

        }

		private void accueilClick(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
		}
    }
}