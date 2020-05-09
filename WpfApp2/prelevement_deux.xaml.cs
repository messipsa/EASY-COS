using System;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour prelevement_deux.xaml
    /// </summary>
    public partial class prelevement_deux : Window
    {
        public static string montant;
        public class pret_ac
        {
            public String Nom { get; set; }
            public String Prenom { get; set; }
            public String N_Pv { get; set; }
            public String description { get; set; }
            public String Date_de_Pv { get; set; }
            public String Date_paiement { get; set; }
            public String Montant_Prét { get; set; }
        }
        public prelevement_deux()
        {
            InitializeComponent();
            methode_prelevement.Items.Add("Paiement Standard.");
            methode_prelevement.Items.Add("Paiement Standard (sur plusieurs mois).");
            methode_prelevement.Items.Add("Paiement Anticipé.");
            methode_prelevement.Items.Add("Paiement Différé.");
            methode_prelevement.Items.Add("Effacement des Dettes");
            methode_prelevement.Items.Add("Paiement spéciale");
            Donnée_pret_ac.ItemsSource = null;
            List<pret_ac> source = new List<pret_ac>();
            source.Clear();
            foreach (pret_remboursable pret in responsable.liste_pret_remboursable.Values)
            {
                if (pret.Date_actuelle.Month == DateTime.Now.Month)
                {
                    pret_ac p = new pret_ac();
                    p.Nom = pret.Employé.Nom;
                    p.Prenom = pret.Employé.Prenom;
                    p.N_Pv = pret.Num_pv.ToString();
                    p.description = pret.Type_Pret.Description;
                    p.Date_de_Pv = pret.Date_pv.ToString();
                    p.Date_paiement = pret.Date_actuelle.ToString();
                    p.Montant_Prét = pret.Montant.ToString();
                    source.Add(p);
                }
            }
            Donnée_pret_ac.ItemsSource = source;
        }
        
        
        private void montant_prelevement_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            montant_prelevement.Text = "";
            pret_ac st = Donnée_pret_ac.SelectedItem as pret_ac;
            pret_remboursable pret = null;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (DateTime.Parse(st.Date_de_Pv).Equals(liste.Value.Date_pv) && Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
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
                if (DateTime.Parse(st.Date_de_Pv).Equals(liste.Value.Date_pv) && Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
                {
                    pret = liste.Value;
                }
            }
            double montant_prelevé = 0;
            if (methode_prelevement.Text.Equals("Paiement Standard."))
            {
                montant_prelevé = (pret.Montant / pret.Durée) * Int32.Parse(nb_mois_saisi.Text);
                responsable.paiement_standard(pret.Cle);
                int mois = pret.Date_actuelle.Month - 1;
                
            }
            else
            {
                montant_prelevé = (pret.Montant / pret.Durée);
                if (methode_prelevement.Text.Equals("Paiement Standard (sur plusieurs mois)."))
                {
                    responsable.paiement_plusieurs_mois(pret.Cle, Int32.Parse(nb_mois_saisi.Text));
                    int mois = pret.Date_actuelle.Month - 1;
                    double d = pret.Etat[mois - 2];
                    
                }
                else
                {
                    if (methode_prelevement.Text.Equals("Paiement Anticipé."))
                    {
                        double d = pret.Montant - pret.Somme_remboursée;
                        montant_prelevé = d;
                        responsable.paiement_anticipé(pret.Cle);
                        int mois = pret.Date_actuelle.Month - 1;
                        
                    }
                    else
                    {
                        if (methode_prelevement.Text.Equals("Paiement Différé."))
                        {
                            responsable.retardement_paiement(pret.Cle);
                            int mois = pret.Date_actuelle.Month - 1;
                            
                        }
                        else
                        {
                            if (methode_prelevement.Text.Equals("Effacement des Dettes"))
                            {
                                responsable.effacement_dettes(pret.Cle);
                                int mois = pret.Date_actuelle.Month;
                                
                            }
                            else
                            {
                                if (methode_prelevement.Text.Equals("Paiement spéciale"))
                                {
                                    double montant = Double.Parse(montant_prelevement.Text);
                                    responsable.paiement_spécial(pret.Cle, montant);
                                    int mois = pret.Date_actuelle.Month - 1;
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
            
        }
    
        
        private void affiche_montant_click(object sender, RoutedEventArgs e)
        {
            pret_ac st = Donnée_pret_ac.SelectedItem as pret_ac;
            pret_remboursable pret = null;
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (DateTime.Parse(st.Date_de_Pv).Equals(liste.Value.Date_pv) && Double.Parse(st.Montant_Prét) == liste.Value.Montant && st.Nom.Equals(liste.Value.Employé.Nom) && st.Prenom.Equals(liste.Value.Employé.Prenom) && Int32.Parse(st.N_Pv) == liste.Value.Num_pv && st.description.Equals(liste.Value.Type_Pret.Description))
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
        
    }
}
