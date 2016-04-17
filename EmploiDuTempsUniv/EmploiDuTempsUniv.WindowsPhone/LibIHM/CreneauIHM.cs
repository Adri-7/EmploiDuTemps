using EmploiDuTempsUniv.LogiqueMetier;
using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace EmploiDuTempsUniv
{
	internal class CreneauIHM
	{
		private MainPage page;

		private Grid parent;

		private Creneau creneau;

		private Grid element = new Grid();

		private TextBlock matiere = new TextBlock();

		private TextBlock type = new TextBlock();

		private TextBlock salle = new TextBlock();

		private static List<CreneauIHM> liste;

		private static CreneauIHM selectedCreneau;

		static CreneauIHM()
		{
			CreneauIHM.liste = new List<CreneauIHM>();
			CreneauIHM.selectedCreneau = null;
		}

		public CreneauIHM(Creneau c, Grid _parent)
		{
			CreneauIHM.liste.Add(this);
			this.parent = _parent;
			this.creneau = c;
			this.element.Children.Add(this.matiere);
			this.element.Children.Add(this.type);
			this.element.Children.Add(this.salle);
			this.element.Background = new SolidColorBrush(c.Matiere.CouleurFond);
			TimeSpan debut = this.creneau.Debut;
			int totalHours = (int)((debut.TotalHours - 8) * 50) + 10;
			this.element.Margin = new Thickness(40, (double)totalHours, 20, 0);
			this.element.VerticalAlignment = VerticalAlignment.Top;
			this.matiere.VerticalAlignment = VerticalAlignment.Top;
			this.matiere.Margin = new Thickness(5, 0, 0, 0);
			this.matiere.FontSize = 22;
			this.matiere.FontWeight = FontWeights.ExtraBold;
			this.matiere.Foreground = new SolidColorBrush(c.Matiere.CouleurPolice);
			this.matiere.Text = this.creneau.Matiere.Intitule;
			this.type.VerticalAlignment = VerticalAlignment.Bottom;
			this.type.HorizontalAlignment = HorizontalAlignment.Right;
			this.type.FontSize = 15;
			this.type.FontWeight = FontWeights.Bold;
			this.type.Foreground = new SolidColorBrush(c.Matiere.CouleurPolice);
			this.type.Text = this.creneau.Type.ToString();
			this.type.Margin = new Thickness(5, 0, 5, 2);
			this.salle.VerticalAlignment = VerticalAlignment.Bottom;
			this.salle.HorizontalAlignment = HorizontalAlignment.Left;
			this.salle.FontSize = 15;
			this.salle.FontWeight = FontWeights.Bold;
			this.salle.Foreground = new SolidColorBrush(c.Matiere.CouleurPolice);
			this.salle.Text = this.creneau.Salle;
			this.salle.Margin = new Thickness(5, 0, 5, 2);
			Grid grid = this.element;
			TimeSpan duree = this.creneau.Duree;
			grid.Height = (double)((int)(duree.TotalHours * 50) - 2);
			if (this.element.Height < 48)
			{
				this.type.Visibility = Visibility.Collapsed;
				this.salle.HorizontalAlignment = HorizontalAlignment.Right;
				this.salle.VerticalAlignment = VerticalAlignment.Center;
				this.matiere.FontFamily = new FontFamily("Segoe UI");
				this.matiere.VerticalAlignment = VerticalAlignment.Center;
			}
			this.parent.Children.Add(this.element);
            this.element.Tapped += element_Tapped;
            this.element.Holding += fond_Click;
		}

		public static void deleteAll()
		{
			foreach (CreneauIHM creneauIHM in CreneauIHM.liste)
			{
				if (creneauIHM == null)
				{
					continue;
				}
				creneauIHM.element.Visibility = Visibility.Collapsed;
			}
			CreneauIHM.liste = new List<CreneauIHM>();
		}

		public void deleteCreneau()
		{
			Semaine.getInstance().getJour(this.creneau.Jour).retireCreneau(this.creneau);
			this.element.Visibility = Visibility.Collapsed;
			CreneauIHM.liste.Remove(this);
			CreneauIHM.selectedCreneau = null;
		}

		private void element_Tapped(object sender, TappedRoutedEventArgs e)
		{
			FrameworkElement parent = (FrameworkElement)this.parent.Parent;
			while ((object)parent.GetType() != (object)typeof(MainPage))
			{
				parent = (FrameworkElement)parent.Parent;
			}
			this.page = (MainPage)parent;
			this.page.Frame.Navigate(typeof(ElementPage), this.creneau);
		}

		private void fond_Click(object sender, HoldingRoutedEventArgs e)
		{
			CreneauIHM.resetClick();
			CreneauIHM.selectedCreneau = this;
			PlaneProjection planeProjection = new PlaneProjection()
			{
				GlobalOffsetZ = 75
			};
			this.element.Projection = planeProjection;
			foreach (CreneauIHM creneauIHM in CreneauIHM.liste)
			{
				if ((object)creneauIHM == (object)this)
				{
					continue;
				}
				PlaneProjection planeProjection1 = new PlaneProjection()
				{
					GlobalOffsetZ = -50
				};
				creneauIHM.element.Opacity = 0.7;
				creneauIHM.element.Projection = planeProjection1;
			}
			FrameworkElement parent = (FrameworkElement)this.parent.Parent;
			while ((object)parent.GetType() != (object)typeof(MainPage))
			{
				parent = (FrameworkElement)parent.Parent;
			}
			this.page = (MainPage)parent;
			this.page.eltClick();
			e.Handled = true;
		}

		public Creneau getCreneau()
		{
			return this.creneau;
		}

		public static CreneauIHM getSelectedCreneau()
		{
			return CreneauIHM.selectedCreneau;
		}

		public static void resetClick()
		{
			CreneauIHM.selectedCreneau = null;
			foreach (CreneauIHM creneauIHM in CreneauIHM.liste)
			{
				PlaneProjection planeProjection = new PlaneProjection()
				{
					GlobalOffsetZ = 0
				};
				creneauIHM.element.Opacity = 1;
				creneauIHM.element.Projection = planeProjection;
			}
		}
	}
}