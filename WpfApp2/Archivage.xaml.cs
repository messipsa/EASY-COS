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
    /// Interaction logic for Archivage.xaml
    /// </summary>
    public partial class Archivage : UserControl
    {
        public Archivage()
        {
            InitializeComponent();

            List<Archives> source = new List<Archives>();
            foreach (KeyValuePair<int, Archive> liste in responsable.liste_archives)
            {
                Archives arch = new Archives();
                arch.Nom = liste.Value.Pret.Employé.Nom;
                arch.Prenom = liste.Value.Pret.Employé.Prenom;
                arch.N_Pv = liste.Value.Pret.Num_pv.ToString();
                arch.Motif = liste.Value.Pret.Motif;
                arch.Date_demande = liste.Value.Pret.Date_demande.ToString();
                arch.Montant_Prét = liste.Value.Pret.Montant.ToString();
                arch.Montant_Prét_lettre = liste.Value.Pret.Montant_lettre;
                arch.Observation = liste.Value.Observations;
                arch.Type_Prêt = liste.Value.Pret.Type_Pret.Description.ToString();
                arch.Date_de_Pv = liste.Value.Pret.Date_pv.ToString();
                if (liste.Value.Durée != -1)
                {
                    arch.Durée = liste.Value.Durée.ToString();
                }
                else
                {
                    arch.Durée = "0";
                }
                source.Add(arch);
            }
            Donnée_Archivage.ItemsSource = source;

            /* foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
             {
                 liste_employes.Items.Add(liste.Key.ToString() + " ) " + liste.Value.Nom + " " + liste.Value.Prenom + ".");
             }
             introduire.Items.Add("Choisir un employe parmis la liste");
             introduire.Items.Add("Créer un nouvel employe");*/
        }

        public class Archives
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
            public String Durée { get; set; }
        }

        private void Filtrer_Click(object sender, RoutedEventArgs e)
        {
            archivees.Children.Clear();
            archivees.Children.Add(new UserControl1());
        }

        private void Enregistrer_excel_click(object sender, RoutedEventArgs e)
        {

        }

        private void Détails_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Confirmer_Filtrage_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Annuler_Filtrage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Recherche_Click(object sender, RoutedEventArgs e)
        {
            data_grid.Visibility = Visibility.Hidden;
            data_grid.IsEnabled = false;
            //grid_rech.Visibility = Visibility.Visible;
            //grid_rech.IsEnabled = true;
        }


    }
}