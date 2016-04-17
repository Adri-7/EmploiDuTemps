using EmploiDuTempsUniv.LogiqueMetier;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
	public sealed partial class ElementPage : Page
	{
		public ElementPage()
		{
			this.InitializeComponent();
		}

		private void Modifier_Click(object sender, RoutedEventArgs e)
		{
			ElementContext dataContext = this.LayoutRoot.DataContext as ElementContext;
			base.Frame.Navigate(typeof(CreationCreneau), dataContext.Creneau);
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e.Parameter == null)
			{
				base.Frame.GoBack();
				return;
			}
			ElementContext elementContext = new ElementContext(e.Parameter as Creneau);
			this.LayoutRoot.DataContext = elementContext;
		}

		private void retour_Click(object sender, RoutedEventArgs e)
		{
			ElementContext dataContext = this.LayoutRoot.DataContext as ElementContext;
			base.Frame.Navigate(typeof(MainPage), dataContext.Jour);
		}

		private void supprimer_Click(object sender, RoutedEventArgs e)
		{
			ElementContext dataContext = this.LayoutRoot.DataContext as ElementContext;
			Jour.day jour = dataContext.Jour;
			dataContext.supprimeCreneau();
			base.Frame.Navigate(typeof(MainPage), dataContext.Jour);
		}
	}
}