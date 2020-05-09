﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.IO;
using System.Globalization;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string montant;

        public class pret_ac
        {
            public String Nom { get; set; }
            public String Prenom { get; set; }
            public String N_Pv { get; set; }
            public String description { get; set; }            
            public String Date_paiement { get; set; }
            public String Montant_Prét { get; set; }
        }



        public MainWindow()
        {
            InitializeComponent();
            media.Play();
            /*welcome.Position = new TimeSpan(0, 0, 0);
            welcome.Pause();*/
            Loading();
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new Accueil());
            CompteCosTotal.Text = responsable.tresor.ToString() + " DA";
            CompteCosTotal.Text = responsable.tresor.ToString() + " DA";

            /*Montant cos restant*/
            montant_restant2.Text = responsable.tresor.ToString() + " DA";

            #region nombre de prets remboursable            



            double montant_t_r = 0;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                montant_t_r = montant_t_r + liste.Value.Montant;
            }
            double montant_t_r_n = 0;
            foreach (KeyValuePair<int, pret_non_remboursable> liste in responsable.liste_pret_non_remboursables)
            {
                montant_t_r_n = montant_t_r_n + liste.Value.Montant;
            }
            nbr_prt_total2.Text = (montant_t_r + montant_t_r_n).ToString() + "DA";

            nbr_dons2.Text = montant_t_r_n.ToString() + "DA";

            double mon_res = 0;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                mon_res = mon_res + liste.Value.Somme_remboursée;
            }
            nbr_prt_socials2.Text = mon_res.ToString() + "DA"; ;

            #endregion



            #region nombre de prets en cours de suivi

            int cpt2 = 0;
            foreach (KeyValuePair<int, pret_remboursable> pret in responsable.liste_pret_remboursable)
            {
                if (pret.Value.Somme_remboursée < pret.Value.Montant)
                {
                    cpt2++;
                }
            }

            nbr_prt_cours_suivi2.Text = cpt2.ToString();

            #endregion

            #region date de prelevement la plus proche

            var listeDates = new List<DateTime>();
            foreach (KeyValuePair<int, pret_remboursable> pret in responsable.liste_pret_remboursable)
            {
                if (pret.Value.Debordement == -1 && pret.Value.isPere())
                {
                    listeDates.Add(pret.Value.Date_actuelle);
                }
                else
                {
                    pret_remboursable p = pret.Value;
                    while (p.Debordement != -1)
                    {
                        p = p.getFils();
                    }

                    listeDates.Add(p.Date_actuelle);
                }
            }

            DateTime smallest = listeDates.Min();
            date_prelev_plus_proch2.Text = smallest.ToString("d", CultureInfo.CreateSpecificCulture("fr-FR"));

            #endregion
            methode_prelevement.Items.Add("Paiement Standard.");
            methode_prelevement.Items.Add("Paiement Standard (sur plusieurs mois).");
            methode_prelevement.Items.Add("Paiement Anticipé.");
            methode_prelevement.Items.Add("Paiement Différé.");
            methode_prelevement.Items.Add("Effacement des Dettes");
            methode_prelevement.Items.Add("Paiement spéciale");
            actualiser();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateText.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }, this.Dispatcher);

            DispatcherTimer timer1 = new DispatcherTimer(new TimeSpan(100000), DispatcherPriority.Normal, delegate
            {
                montant_restant2.Text = responsable.tresor.ToString() + " DA";
                CompteCosTotal.Text = responsable.tresor.ToString() + " DA";
            }, this.Dispatcher);
        }

        DispatcherTimer a = new DispatcherTimer();

        private void timer_tick(object sender, EventArgs e)
        {
            a.Stop();
            media.Visibility = Visibility.Hidden;
            welcome.Play();
        }

        void Loading()
        {
            a.Tick += timer_tick;
            a.Interval = new TimeSpan(0, 0, 2);
            a.Start();
        }

        private void deconnexion(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window1 connexion = new Window1();
            connexion.connexion_grid.Margin = new Thickness(-300, 0, 0, 0);
            // connexion.intro.Close();
            connexion.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            connexion.Show();
        }

        private void listMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listMenu.SelectedIndex;

            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new Accueil());
                    break;
                case 1:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new Suivi_Prét());
                    break;
                case 2:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new dons());
                    break;
                case 3:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new NouveauPrêts());
                    break;
                case 4:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new stat());
                    break;
                case 5:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new Archivage());
                    break;
                case 6:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new Bilan());
                    break;
                case 7:
                    Grid_Principale.Children.Clear();
                    Grid_Principale.Children.Add(new Employes());
                    break;
                default:
                    break;
            }

        }

        private void MoveCursorMenu(int index)
        {
            Transition.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (91 + (70 * index)), 0, 0);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Paramétres_Click(object sender, RoutedEventArgs e)
        {
            //grid_gnrl.Children.Clear();
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new Accueil());
            listMenu.SelectedIndex = 0;
            grid_gnrl.Children.Add(new Window2(Pseudo_show, Password_show, image_info));
            //setting.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //setting.Show(); 
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            media.Position = new TimeSpan(0, 0, 1);
            media.Play();
        }

        private void profil_user_MouseEnter(object sender, MouseEventArgs e)
        {
            pop_compte.Visibility = Visibility.Visible;
        }

        private void prelevement_MouseLeave(object sender, MouseEventArgs e)
        {
            pop_prelevement.Visibility = Visibility.Hidden;
        }

        private void prelevement_MouseEnter(object sender, MouseEventArgs e)
        {
            pop_prelevement.Visibility = Visibility.Visible;
        }

        private void profil_user_MouseLeave(object sender, MouseEventArgs e)
        {
            pop_compte.Visibility = Visibility.Hidden;
        }

        private void excel_MouseEnter(object sender, MouseEventArgs e)
        {
            pop_excel.Visibility = Visibility.Visible;
        }
        private void excel_MouseLeave(object sender, MouseEventArgs e)
        {
            pop_excel.Visibility = Visibility.Hidden;
        }

        private void export_click(object sender, RoutedEventArgs e)
        {
            responsable.export_prêts_remboursable();
        }

        private void welcome_MediaEnded(object sender, RoutedEventArgs e)
        {
            welcome.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
        }

        private void reduction_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void description_Click(object sender, RoutedEventArgs e)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));

            ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation1.From = new Thickness(0, 0, -250, 0);
            myDoubleAnimation1.To = new Thickness(0, 0, 0, 0);
            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);

            Storyboard.SetTarget(myDoubleAnimation1, Description_grid);

            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Grid.MarginProperty));
            sb.Begin();
        }

        private void back_description_Click(object sender, RoutedEventArgs e)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));

            ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation1.From = new Thickness(0, 0, 0, 0);
            myDoubleAnimation1.To = new Thickness(0, 0, -250, 0);
            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);

            Storyboard.SetTarget(myDoubleAnimation1, Description_grid);

            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Grid.MarginProperty));
            sb.Begin();
        }

        private void On_graph_Click(object sender, RoutedEventArgs e)
        {
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new Statistiques());
        }

        private void On_list_Click(object sender, RoutedEventArgs e)
        {
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new Window3());
        }

        public void Display_Detail_Click(object sender, RoutedEventArgs e)
        {
            if (border_memer.Height == 330)
            {
                Duration duration = new Duration(TimeSpan.FromSeconds(0.5));

                DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();

                myDoubleAnimation1.Duration = duration;
                myDoubleAnimation1.From = 330;
                myDoubleAnimation1.To = 0;
                Storyboard sb = new Storyboard();
                sb.Duration = duration;

                sb.Children.Add(myDoubleAnimation1);

                Storyboard.SetTarget(myDoubleAnimation1, border_memer);

                Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Border.HeightProperty));
                sb.Begin();

                Icon_Display_Detail.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowDownBoldCircleOutline;
                Display_Detail.ToolTip = "Afficher Le Detail";

            }
            else
            {
                border_memer.Visibility = Visibility.Visible;
                Detail_slimimer.Visibility = Visibility.Visible;
                Icon_Display_Detail.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowUpBoldCircleOutline;
                Display_Detail.ToolTip = "Cacher Le Detail";
                Duration duration = new Duration(TimeSpan.FromSeconds(0.5));

                DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();

                myDoubleAnimation1.Duration = duration;
                myDoubleAnimation1.From = 0;
                myDoubleAnimation1.To = 330;
                Storyboard sb = new Storyboard();
                sb.Duration = duration;

                sb.Children.Add(myDoubleAnimation1);

                Storyboard.SetTarget(myDoubleAnimation1, border_memer);

                Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Border.HeightProperty));
                sb.Begin();



            }

        }

        private void Entrer_Montant_Tresor_Click(object sender, RoutedEventArgs e)
        {
            Grid_Principale.Visibility = Visibility.Hidden;
            Grid_Principale.IsEnabled = false;
            Ajouter_Montant_Tresor.Visibility = Visibility.Visible;
            Ajouter_Montant_Tresor.IsEnabled = true;
        }

        private void Annuler_tresor_Click(object sender, RoutedEventArgs e)
        {
            Ajouter_Montant_Tresor.Visibility = Visibility.Hidden;
            Ajouter_Montant_Tresor.IsEnabled = false;
            Grid_Principale.Visibility = Visibility.Visible;
            Grid_Principale.IsEnabled = true;
        }

        private void Confirmer_tresor_Click(object sender, RoutedEventArgs e)
        {
            double value, value0 = 0;
            if (Tresor_Annee.Text == "" || !double.TryParse(Tresor_Annee.Text, out value))
            {
                NotAccepted.Visibility = Visibility.Visible;
                DoubleAnimation a = new DoubleAnimation();
                a.From = 1.0;
                a.To = 0.0;
                a.Duration = new Duration(TimeSpan.FromSeconds(5));
                NotAccepted.BeginAnimation(OpacityProperty, a);

            }
            else
            {

                double.TryParse(Tresor_Annee.Text, out value0);
                double value1 = value0 + responsable.tresor;
                CompteCosTotal.Text = value1.ToString() + " DA";
                Accepted.Visibility = Visibility.Visible;
                DoubleAnimation a = new DoubleAnimation();
                a.From = 1.0;
                a.To = 0.0;
                a.Duration = new Duration(TimeSpan.FromSeconds(5));
                Accepted.BeginAnimation(OpacityProperty, a);
                Grid_Principale.Visibility = Visibility.Visible;
                Grid_Principale.IsEnabled = true;
                Ajouter_Montant_Tresor.Visibility = Visibility.Hidden;
                Ajouter_Montant_Tresor.IsEnabled = false;
            }
        }
     




        private void prelevement_click(object sender, RoutedEventArgs e)
        {
            prelevement.Visibility = Visibility.Visible; prelevement.IsEnabled = true;
        }

        private void montant_prelevement_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            montant_prelevement.Text = "";
            pret_ac st = Donnée_pret_ac.SelectedItem as pret_ac;
            pret_remboursable pret = null;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
                {
                    pret = liste.Value;
                }
            }
            if (methode_prelevement.SelectedItem.ToString().Equals("Paiement Standard."))
            {
                nb_mois.Visibility = Visibility.Hidden;
                nb_mois_saisi.Visibility = Visibility.Hidden;
                m.Visibility = Visibility.Hidden;
                montant_prelevement.IsReadOnly = true;
                MainWindow.montant = "      " + (pret.Montant / pret.Durée).ToString();
            }
            else
            {
                if (methode_prelevement.SelectedItem.ToString().Equals("Paiement Standard (sur plusieurs mois)."))
                {
                    nb_mois.Visibility = Visibility.Visible;
                    nb_mois_saisi.Visibility = Visibility.Visible;
                    m.Visibility = Visibility.Visible;
                    double nb_mois_ = Double.Parse(nb_mois_saisi.Text);
                    double montant_multip = (pret.Montant / (double)pret.Durée) * nb_mois_;
                    montant_prelevement.IsReadOnly = true;
                    MainWindow.montant = "      " + montant_multip.ToString();
                }
                else
                {
                    if (methode_prelevement.SelectedItem.ToString().Equals("Paiement Anticipé."))
                    {
                        nb_mois.Visibility = Visibility.Hidden;
                        nb_mois_saisi.Visibility = Visibility.Hidden;
                        m.Visibility = Visibility.Hidden;
                        montant_prelevement.IsReadOnly = true;
                        MainWindow.montant = "      " + (pret.Montant - pret.Somme_remboursée).ToString();
                    }
                    else
                    {
                        if (methode_prelevement.SelectedItem.ToString().Equals("Paiement Différé."))
                        {
                            nb_mois.Visibility = Visibility.Hidden;
                            nb_mois_saisi.Visibility = Visibility.Hidden;
                            m.Visibility = Visibility.Hidden;
                            montant_prelevement.IsReadOnly = true;
                            MainWindow.montant = "      0";
                        }
                        else
                        {
                            if (methode_prelevement.SelectedItem.ToString().Equals("Effacement des Dettes"))
                            {
                                nb_mois.Visibility = Visibility.Hidden;
                                nb_mois_saisi.Visibility = Visibility.Hidden;
                                m.Visibility = Visibility.Hidden;
                                montant_prelevement.IsReadOnly = true;
                                MainWindow.montant = "      0";
                            }
                            else
                            {
                                if (methode_prelevement.SelectedItem.ToString().Equals("Paiement spéciale"))
                                {
                                    nb_mois.Visibility = Visibility.Hidden;
                                    nb_mois_saisi.Visibility = Visibility.Hidden;
                                    m.Visibility = Visibility.Hidden;
                                    montant_prelevement.IsReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void confirmer_Prélèvement_click(object sender, RoutedEventArgs e)
        {
            pret_ac st = Donnée_pret_ac.SelectedItem as pret_ac;
            pret_remboursable pret = null;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
                {
                    pret = liste.Value;
                }
            }
            double montant_prelevé = 0;
            if (methode_prelevement.Text.Equals("Paiement Standard."))
            {
                montant_prelevé = (pret.Montant / pret.Durée) * Int32.Parse(nb_mois_saisi.Text);
                responsable.paiement_standard(pret.Cle);
                actualiser();
                int mois = pret.Date_actuelle.Month - 1;
                MessageBoxResult result = MessageBox.Show("Prélèvement fait avec succès !", "Notification Prélèvement", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);                
            }
            else
            {
                montant_prelevé = (pret.Montant / pret.Durée);
                if (methode_prelevement.Text.Equals("Paiement Standard (sur plusieurs mois)."))
                {
                    responsable.paiement_plusieurs_mois(pret.Cle, Int32.Parse(nb_mois_saisi.Text));
                    actualiser();
                    int mois = pret.Date_actuelle.Month - 1;
                    double d = pret.Etat[mois - 2];
                    MessageBoxResult result = MessageBox.Show("Prélèvement fait avec succès !", "Notification Prélèvement", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                }
                else
                {
                    if (methode_prelevement.Text.Equals("Paiement Anticipé."))
                    {
                        double d = pret.Montant - pret.Somme_remboursée;
                        montant_prelevé = d;
                        responsable.paiement_anticipé(pret.Cle);
                        actualiser();
                        int mois = pret.Date_actuelle.Month - 1;
                        MessageBoxResult result = MessageBox.Show("Prélèvement fait avec succès !", "Notification Prélèvement", MessageBoxButton.OK);
                    }
                    else
                    {
                        if (methode_prelevement.Text.Equals("Paiement Différé."))
                        {
                            responsable.retardement_paiement(pret.Cle);
                            actualiser();
                            int mois = pret.Date_actuelle.Month - 1;
                            MessageBoxResult result = MessageBox.Show("Retardement effectué avec succès !", "Notification Retardement", MessageBoxButton.OK);
                        }
                        else
                        {
                            if (methode_prelevement.Text.Equals("Effacement des Dettes"))
                            {
                                responsable.effacement_dettes(pret.Cle);
                                actualiser();
                                int mois = pret.Date_actuelle.Month;
                                MessageBoxResult result = MessageBox.Show("Effacement des dettes fait avec succès !", "Notification Effacement des dettes", MessageBoxButton.OK);
                            }
                            else
                            {
                                if (methode_prelevement.Text.Equals("Paiement spéciale"))
                                {
                                    double montant = Double.Parse(montant_prelevement.Text);
                                    responsable.paiement_spécial(pret.Cle, montant);
                                    actualiser();
                                    int mois = pret.Date_actuelle.Month - 1;
                                    MessageBoxResult result = MessageBox.Show("Prélèvement fait avec succès !", "Notification Prélèvement", MessageBoxButton.OK);
                                }
                            }
                        }
                    }
                }
            }

            if (Window2.envoi_notif)
            {
                if (Window2.mode_envoi)
                {
                    if (!pret.Employé.Email.Equals(""))
                        responsable.Envoi_mail(pret, montant_prelevé);
                    else
                    {
                        WpfTutorialSamples.Dialogs.InputDialogSample input = new WpfTutorialSamples.Dialogs.InputDialogSample(pret, montant_prelevé, "Veuillez entrer le mail de l'employé :", "mail@esi.dz");
                        input.ShowActivated = true;
                        input.Show();
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Voulez vous envoyer une notification E-mail ?", "Notification E-mail", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            if (!pret.Employé.Email.Equals(""))
                                responsable.Envoi_mail(pret, montant_prelevé);
                            else
                            {
                                WpfTutorialSamples.Dialogs.InputDialogSample input = new WpfTutorialSamples.Dialogs.InputDialogSample(pret, montant_prelevé, "Veuillez entrer le mail de l'employé :", "mail@esi.dz");
                                input.ShowActivated = true;
                                input.Show();
                            }
                            break;
                        case MessageBoxResult.No:
                            MessageBox.Show("La notification sera pas envoyé", "Notification E-mail", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }

        }

        private void retourner_suivi_click(object sender, RoutedEventArgs e)
        {
            prelevement.Visibility = Visibility.Hidden; prelevement.IsEnabled = false;
        }


        private void affiche_montant_click(object sender, RoutedEventArgs e)
        {
            pret_ac st = Donnée_pret_ac.SelectedItem as pret_ac;
            pret_remboursable pret = null;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
                {
                    pret = liste.Value;
                }
            }
            if (methode_prelevement.Text.Equals("Paiement Standard (sur plusieurs mois)."))
            {
                double nb_mois_ = Double.Parse(nb_mois_saisi.Text);
                double montant_multip = (pret.Montant / (double)pret.Durée) * nb_mois_;
                MainWindow.montant = "      " + montant_multip.ToString();
            }
            montant_prelevement.Text = MainWindow.montant;
        }

        private void excel_click(object sender, RoutedEventArgs e)
        {
            responsable.export_prêts_remboursable();
        }

        private void actualiser_click(object sender, RoutedEventArgs e)
        {
            remarque_prelevements_ancien.Visibility = Visibility.Hidden;
            Donnée_pret_ac.ItemsSource = null;
            List<pret_ac> source = new List<pret_ac>();
            source.Clear();
            foreach (pret_remboursable pret in responsable.liste_pret_remboursable.Values)
            {
                if(pret.isPere())
                {
                    if(pret.Debordement == -1)
                    {
                        if ((pret.Date_actuelle.Month < DateTime.Now.Month && pret.Somme_remboursée < pret.Montant))
                        {
                            remarque_prelevements_ancien.Visibility = Visibility.Visible;
                            pret_ac p = new pret_ac();
                            p.Nom = pret.Employé.Nom;
                            p.Prenom = pret.Employé.Prenom;
                            p.N_Pv = pret.Num_pv.ToString();
                            p.description = pret.Type_Pret.Description;
                            p.Date_paiement = pret.Date_actuelle.ToString();
                            p.Montant_Prét = pret.Montant.ToString();
                            source.Add(p);
                        }
                        else
                        {
                            if (pret.Date_actuelle.Month == DateTime.Now.Month)
                            {
                                pret_ac p = new pret_ac();
                                p.Nom = pret.Employé.Nom;
                                p.Prenom = pret.Employé.Prenom;
                                p.N_Pv = pret.Num_pv.ToString();
                                p.description = pret.Type_Pret.Description;
                                p.Date_paiement = pret.Date_actuelle.ToString();
                                p.Montant_Prét = pret.Montant.ToString();
                                source.Add(p);
                            }
                        }
                    }
                    else
                    {
                        pret_remboursable fils = null;
                        retry:;
                        fils = pret.getFils();
                        if (fils.Debordement != -1)
                        {
                            goto retry;
                        }
                        if ((fils.Date_actuelle.Month < DateTime.Now.Month && fils.Somme_remboursée < fils.Montant))
                        {
                            remarque_prelevements_ancien.Visibility = Visibility.Visible;
                            pret_ac p = new pret_ac();
                            p.Nom = fils.Employé.Nom;
                            p.Prenom = fils.Employé.Prenom;
                            p.N_Pv = fils.Num_pv.ToString();
                            p.description = fils.Type_Pret.Description;
                            p.Date_paiement = fils.Date_actuelle.ToString();
                            p.Montant_Prét = fils.Montant.ToString();
                            source.Add(p);
                        }
                        else
                        {
                            if (fils.Date_actuelle.Month == DateTime.Now.Month)
                            {
                                pret_ac p = new pret_ac();
                                p.Nom = fils.Employé.Nom;
                                p.Prenom = fils.Employé.Prenom;
                                p.N_Pv = fils.Num_pv.ToString();
                                p.description = fils.Type_Pret.Description;
                                p.Date_paiement = fils.Date_actuelle.ToString();
                                p.Montant_Prét = fils.Montant.ToString();
                                source.Add(p);
                            }
                        }
                    }
                }
            }
            Donnée_pret_ac.ItemsSource = source;
        }

        private void actualiser()
        {
            remarque_prelevements_ancien.Visibility = Visibility.Hidden;
            Donnée_pret_ac.ItemsSource = null;
            List<pret_ac> source = new List<pret_ac>();
            source.Clear();
            foreach (pret_remboursable pret in responsable.liste_pret_remboursable.Values)
            {
                if (pret.isPere())
                {
                    if (pret.Debordement == -1)
                    {
                        if ((pret.Date_actuelle.Month < DateTime.Now.Month && pret.Somme_remboursée < pret.Montant))
                        {
                            remarque_prelevements_ancien.Visibility = Visibility.Visible;
                            pret_ac p = new pret_ac();
                            p.Nom = pret.Employé.Nom;
                            p.Prenom = pret.Employé.Prenom;
                            p.N_Pv = pret.Num_pv.ToString();
                            p.description = pret.Type_Pret.Description;
                            p.Date_paiement = pret.Date_actuelle.ToString();
                            p.Montant_Prét = pret.Montant.ToString();
                            source.Add(p);
                        }
                        else
                        {
                            if (pret.Date_actuelle.Month == DateTime.Now.Month)
                            {
                                pret_ac p = new pret_ac();
                                p.Nom = pret.Employé.Nom;
                                p.Prenom = pret.Employé.Prenom;
                                p.N_Pv = pret.Num_pv.ToString();
                                p.description = pret.Type_Pret.Description;
                                p.Date_paiement = pret.Date_actuelle.ToString();
                                p.Montant_Prét = pret.Montant.ToString();
                                source.Add(p);
                            }
                        }
                    }
                    else
                    {
                        pret_remboursable fils = null;
                    retry:;
                        fils = pret.getFils();
                        if (fils.Debordement != -1)
                        {
                            goto retry;
                        }
                        if ((fils.Date_actuelle.Month < DateTime.Now.Month && fils.Somme_remboursée < fils.Montant))
                        {
                            remarque_prelevements_ancien.Visibility = Visibility.Visible;
                            pret_ac p = new pret_ac();
                            p.Nom = fils.Employé.Nom;
                            p.Prenom = fils.Employé.Prenom;
                            p.N_Pv = fils.Num_pv.ToString();
                            p.description = fils.Type_Pret.Description;
                            p.Date_paiement = fils.Date_actuelle.ToString();
                            p.Montant_Prét = fils.Montant.ToString();
                            source.Add(p);
                        }
                        else
                        {
                            if (fils.Date_actuelle.Month == DateTime.Now.Month)
                            {
                                pret_ac p = new pret_ac();
                                p.Nom = fils.Employé.Nom;
                                p.Prenom = fils.Employé.Prenom;
                                p.N_Pv = fils.Num_pv.ToString();
                                p.description = fils.Type_Pret.Description;
                                p.Date_paiement = fils.Date_actuelle.ToString();
                                p.Montant_Prét = fils.Montant.ToString();
                                source.Add(p);
                            }
                        }
                    }
                }
            }
            Donnée_pret_ac.ItemsSource = source;
        }
    }
}