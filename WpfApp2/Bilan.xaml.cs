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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Bilan.xaml
    /// </summary>
    public partial class Bilan : UserControl
    {
        public Bilan()
        {
            InitializeComponent();
            Grid_année.Visibility = Visibility.Visible;

        }
        public class bilann
        {
            public String Nom { get; set; }
            public String Prenom { get; set; }
            public String N_Pv { get; set; }
            public String Type_Prêt { get; set; }
            public String Date_de_Pv { get; set; }
            public String Motif { get; set; }
            public String Date_demande { get; set; }
            public String Montant_Prét_lettre { get; set; }
            public String Montant_Prét { get; set; }
            public String Observation { get; set; }
            public String Etat { get; set; }
            public String Durée { get; set; }

            public String prem_paiement { get; set; }
            public String fin_paiement { get; set; }

            public string sum_rembours { get; set; }
        }
        private void Clik(object sender, RoutedEventArgs e)
        {
            int ann = 0;
            int cpt = 0;

            if (!String.IsNullOrEmpty(an.Text))
            {

                try
                {
                    ann = int.Parse(an.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("L'année entrée est invalide");
                    data_grid.Visibility = Visibility.Hidden;
                    Grid_année.Visibility = Visibility.Visible;
                    cpt++;
                }
                if (cpt == 0)
                {
                    Grid_année.Visibility = Visibility.Hidden;
                    data_grid.Visibility = Visibility.Visible;
                    responsable.remplissage_bilan(ann);
                    List<bilann> source = new List<bilann>();

                    foreach (Prets liste in responsable.bilan)
                    {
                        bilann arch = new bilann();
                        if (liste.GetType() == typeof(pret_remboursable))
                        {
                            if (responsable.liste_pret_remboursable.ContainsValue((pret_remboursable)liste))
                            {
                                arch.Etat = "en cours";
                            }
                            else
                            {
                                arch.Etat = "cloturé";
                            }
                        }
                        else
                        {
                            if (liste.GetType() == typeof(pret_non_remboursable))
                            {
                                if (responsable.liste_pret_non_remboursables.ContainsValue((pret_non_remboursable)liste))
                                {
                                    arch.Etat = "en cours";
                                }
                                else
                                {
                                    arch.Etat = "cloturé";
                                }

                            }

                        }



                        arch.Nom = liste.Employé.Nom;
                        arch.Prenom = liste.Employé.Prenom;
                        arch.N_Pv = liste.Num_pv.ToString();
                        arch.Motif = liste.Motif;
                        arch.Date_demande = liste.Date_demande.ToString();
                        arch.Montant_Prét = liste.Montant.ToString();
                        arch.Montant_Prét_lettre = liste.Montant_lettre;
                        arch.Observation = "";
                        arch.Type_Prêt = liste.Type_Pret.Description.ToString();
                        arch.Date_de_Pv = liste.Date_pv.ToString();
                        arch.prem_paiement = liste.prem_paiment();
                        arch.fin_paiement = liste.fin_paiement();
                        arch.sum_rembours = liste.somme_rembours();
                        if (liste.GetType() == typeof(pret_remboursable))
                        {
                            pret_remboursable p = (pret_remboursable)liste;
                            arch.Durée = p.Durée.ToString();
                        }
                        else
                        {
                            arch.Durée = "0";
                        }

                        source.Add(arch);

                    }
                    Donnée_bilan.ItemsSource = source;
                }

            }
        }
        private void Export(object sender, RoutedEventArgs e)
        {
            responsable.export_bilan();
        }
    }

}