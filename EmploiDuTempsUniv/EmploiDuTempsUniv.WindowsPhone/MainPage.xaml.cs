using EmploiDuTempsUniv.LogiqueMetier;
using System;
using System.Globalization;
using Windows.Devices.Sensors;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace EmploiDuTempsUniv
{
    public sealed partial class MainPage : Page
    {
        private bool first = true;

        private SimpleOrientationSensor sensor;

        private Creneau.parite semaineCourante;

        private PivotItem samedi;

        private PivotItem dimanche;

        public MainPage()
        {
            this.InitializeComponent();
            base.NavigationCacheMode = NavigationCacheMode.Required;
            this.sensor = SimpleOrientationSensor.GetDefault();
        }

        private void addCClick(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(CreationCreneau));
        }

        private void addMClick(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(CreationMatiere));
        }

        private void aProposClick(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(APropos));
        }

        public void eltClick()
        {
            this.edit.Visibility = Visibility.Visible;
            this.delete.Visibility = Visibility.Visible;
        }

        private void gerer_Click(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(GestionMatieres));
        }


        private void initPivotItem()
        {
            System.TimeSpan timeSpan;
            System.DateTime now = System.DateTime.Now;
            int num = 0;
            switch ((int)now.DayOfWeek)
            {
                case 0:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Vendredi).fin();
                        if (!(now.TimeOfDay < timeSpan) && (now.Hour != timeSpan.Hours || now.Minute > timeSpan.Minutes) || ApplicationData.Current.RoamingSettings.Values["samedi"] == null || !(bool)ApplicationData.Current.RoamingSettings.Values["samedi"])
                        {
                            break;
                        }
                        num = 6;
                        break;
                    }
                case 1:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Lundi).fin();
                        if (!(now.TimeOfDay > timeSpan) && (now.Hour != timeSpan.Hours || now.Minute <= timeSpan.Minutes))
                        {
                            break;
                        }
                        num = 1;
                        break;
                    }
                case 2:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Mardi).fin();
                        if (now.TimeOfDay < timeSpan || now.Hour == timeSpan.Hours && now.Minute <= timeSpan.Minutes)
                        {
                            num = 1;
                            break;
                        }
                        else
                        {
                            num = 2;
                            break;
                        }
                    }
                case 3:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Mercredi).fin();
                        if (now.TimeOfDay < timeSpan || now.Hour == timeSpan.Hours && now.Minute <= timeSpan.Minutes)
                        {
                            num = 2;
                            break;
                        }
                        else
                        {
                            num = 3;
                            break;
                        }
                    }
                case 4:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Jeudi).fin();
                        if (now.TimeOfDay < timeSpan || now.Hour == timeSpan.Hours && now.Minute <= timeSpan.Minutes)
                        {
                            num = 3;
                            break;
                        }
                        else
                        {
                            num = 4;
                            break;
                        }
                    }
                case 5:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Vendredi).fin();
                        if (!(now.TimeOfDay < timeSpan) && (now.Hour != timeSpan.Hours || now.Minute > timeSpan.Minutes))
                        {
                            break;
                        }
                        num = 4;
                        break;
                    }
                case 6:
                    {
                        timeSpan = Semaine.getInstance().getJour(Jour.day.Vendredi).fin();
                        if (!(now.TimeOfDay < timeSpan) && (now.Hour != timeSpan.Hours || now.Minute > timeSpan.Minutes) || ApplicationData.Current.RoamingSettings.Values["samedi"] == null || !(bool)ApplicationData.Current.RoamingSettings.Values["samedi"])
                        {
                            break;
                        }
                        num = 5;
                        break;
                    }
            }
            this.Pivot.SelectedIndex = num;
        }

        private void modifierClick(object sender, RoutedEventArgs e)
        {
            CreneauIHM selectedCreneau = CreneauIHM.getSelectedCreneau();
            if (selectedCreneau != null)
            {
                CreneauIHM.resetClick();
                this.edit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.delete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                base.Frame.Navigate(typeof(CreationCreneau), selectedCreneau.getCreneau());
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Creneau.parite _parite;
            if (this.first)
            {
                await Matieres.getInstance().charger();
                await Semaine.getInstance().charger();
                if (Semaine.getInstance().Jours.Count < 6)
                {
                    Semaine.getInstance().Jours.Add(new Jour(Jour.day.Samedi));
                    Semaine.getInstance().Jours.Add(new Jour(Jour.day.Dimanche));
                }
                else if (Semaine.getInstance().Jours.Count == 6)
                {
                    Semaine.getInstance().Jours.Add(new Jour(Jour.day.Dimanche));
                }
                foreach (Jour jour in Semaine.getInstance().Jours)
                {
                    foreach (Creneau creneaux in jour.Creneaux)
                    {
                        Matieres instance = Matieres.getInstance();
                        if (!instance.estPresent(creneaux.Matiere.toString()))
                        {
                            continue;
                        }
                        creneaux.Matiere = instance.getMatiere(creneaux.Matiere.toString());
                    }
                }
                this.first = false;
                this.initPivotItem();
                int weekOfYear = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                MainPage mainPage = this;
                _parite = (weekOfYear % 2 == 0 ? Creneau.parite.Paire : Creneau.parite.Impaire);
                mainPage.semaineCourante = _parite;
            }
            if (Matieres.getInstance().ListeMatieres.Count != 0)
            {
                this.addCreneau.Visibility = Visibility.Visible;
                this.gestion.Visibility = Visibility.Visible;
            }
            else
            {
                this.addCreneau.Visibility = Visibility.Collapsed;
                this.gestion.Visibility = Visibility.Collapsed;
            }
            CreneauIHM.deleteAll();
            for (int i = 0; i < 7; i++)
            {
                foreach (Creneau creneau in Semaine.getInstance().getJour((Jour.day)i).Creneaux)
                {
                    if (creneau.Parite != Creneau.parite.PaireImpaire && creneau.Parite != this.semaineCourante)
                    {
                        continue;
                    }
                    switch (i)
                    {
                        case 0:
                            {
                                CreneauIHM creneauIHM = new CreneauIHM(creneau, this.Lundi);
                                continue;
                            }
                        case 1:
                            {
                                CreneauIHM creneauIHM1 = new CreneauIHM(creneau, this.Mardi);
                                continue;
                            }
                        case 2:
                            {
                                CreneauIHM creneauIHM2 = new CreneauIHM(creneau, this.Mercredi);
                                continue;
                            }
                        case 3:
                            {
                                CreneauIHM creneauIHM3 = new CreneauIHM(creneau, this.Jeudi);
                                continue;
                            }
                        case 4:
                            {
                                CreneauIHM creneauIHM4 = new CreneauIHM(creneau, this.Vendredi);
                                continue;
                            }
                        case 5:
                            {
                                CreneauIHM creneauIHM5 = new CreneauIHM(creneau, this.Samedi);
                                continue;
                            }
                        case 6:
                            {
                                CreneauIHM creneauIHM6 = new CreneauIHM(creneau, this.Dimanche);
                                continue;
                            }
                        default:
                            {
                                continue;
                            }
                    }
                }
            }
            this.parite.Label = "Afficher la semaine ";
            if (this.semaineCourante != Creneau.parite.Paire)
            {
                AppBarButton appBarButton = this.parite;
                appBarButton.Label = string.Concat(appBarButton.Label, "paire");
            }
            else
            {
                AppBarButton appBarButton1 = this.parite;
                appBarButton1.Label = string.Concat(appBarButton1.Label, "impaire");
            }
            this.semaineTB.Text = string.Concat("Semaine ", this.semaineCourante.ToString());
            this.samedi = this.SamediPivotItem;
            this.dimanche = this.DimanchePivotItem;
            if (this.Pivot.Items.Count > 5)
            {
                this.Pivot.Items.Remove(this.SamediPivotItem);
                this.Pivot.Items.Remove(this.DimanchePivotItem);
            }
            if (ApplicationData.Current.RoamingSettings.Values["samedi"] != null && (bool)ApplicationData.Current.RoamingSettings.Values["samedi"])
            {
                this.Pivot.Items.Add(this.samedi);
            }
            if (ApplicationData.Current.RoamingSettings.Values["dimanche"] != null && (bool)ApplicationData.Current.RoamingSettings.Values["dimanche"])
            {
                this.Pivot.Items.Add(this.dimanche);
            }
            if (e.Parameter != null && (object)e.Parameter.GetType() == (object)typeof(Jour.day))
            {
                int parameter = 0;
                if ((int)e.Parameter < 5)
                {
                    parameter = (int)e.Parameter;
                }
                else if ((Jour.day)e.Parameter == Jour.day.Samedi && this.Pivot.Items.Contains(this.samedi) || (Jour.day)e.Parameter == Jour.day.Dimanche && !this.Pivot.Items.Contains(this.samedi) && this.Pivot.Items.Contains(this.dimanche))
                {
                    parameter = 5;
                }
                else if ((Jour.day)e.Parameter == Jour.day.Dimanche && this.Pivot.Items.Contains(this.samedi) && this.Pivot.Items.Contains(this.dimanche))
                {
                    parameter = 6;
                }
                this.Pivot.SelectedIndex = parameter;
            }
        }

        private void Page_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CreneauIHM.resetClick();
            this.edit.Visibility = Visibility.Collapsed;
            this.delete.Visibility = Visibility.Collapsed;
        }

        private void parametresClick(object sender, RoutedEventArgs e)
        {
            base.Frame.Navigate(typeof(Parametres));
        }

        private void parite_Click(object sender, RoutedEventArgs e)
        {
            if (this.semaineCourante != Creneau.parite.Paire)
            {
                this.semaineCourante = Creneau.parite.Paire;
            }
            else
            {
                this.semaineCourante = Creneau.parite.Impaire;
            }
            base.Frame.Navigate(typeof(MainPage));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreneauIHM.resetClick();
            this.edit.Visibility = Visibility.Collapsed;
            this.delete.Visibility = Visibility.Collapsed;
        }

        private void retirerClick(object sender, RoutedEventArgs e)
        {
            CreneauIHM.getSelectedCreneau().deleteCreneau();
            Semaine.getInstance().save();
            this.edit.Visibility = Visibility.Collapsed;
            this.delete.Visibility = Visibility.Collapsed;
        }
    }
}