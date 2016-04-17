using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace EmploiDuTempsUniv
{
	public sealed partial class ColorDialog : ContentDialog
	{
        /// <summary>
        /// Color selected by the user, null if he pressed the backbutton
        /// </summary>
		public SolidColorBrush SelectedColor;

        public bool IsOpened { get; private set; }

        /// <summary>
        /// Instantiate a Color picker dialog
        /// </summary>
		public ColorDialog()
        {
            IsOpened = false;

            //Event handlers
            this.Opened += (ContentDialog c, ContentDialogOpenedEventArgs args) =>
            {
                IsOpened = true;
            };

            this.Closed += (ContentDialog c, ContentDialogClosedEventArgs args) =>
            {
                IsOpened = false;
            };

            //Initialisation de la page
            this.InitializeComponent();

            //Application du contexte de données (liste de couleurs)
            this.picker.DataContext = ColorContext.getContext();

            //Taille de la popup
            Grid height = this.grille;
			Windows.Foundation.Rect bounds = Window.Current.Bounds;
			height.Height = bounds.Height - 100;
		}

        /// <summary>
        /// Handler on grid tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid originalSource = e.OriginalSource as Grid;
			if (originalSource != null)
			{
				this.SelectedColor = (SolidColorBrush)originalSource.Background;
				base.Hide();
			}
		}
	}
}