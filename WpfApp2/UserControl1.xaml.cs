/*using System;
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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            complete_type_pret();
            complete();
        }
        private void complete_type_pret()
        {
            foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
            {
                type_p.Items.Add(liste.Value.Description);
            }
        }

        private void complete()
        {
            foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
            {
                string nom = liste.Value.Nom + " " + liste.Value.Prenom;
                cmbp2.Items.Add(nom);
            }
        }

        private void recherche_Click(object sender, RoutedEventArgs e)
        {
            responsable.liste_filtres.Clear();
            char[] whitespace = new char[] { ' ', '\t' };
            bool remboursable = false;
            int choix = 0;
            bool date1 = false;
            DateTime d_inf = new DateTime();
            bool date2 = false;
            DateTime d_max = new DateTime();
            bool date3 = false;
            DateTime pv_min = new DateTime();
            bool date4 = false;
            DateTime pv_max = new DateTime();
            bool durée1 = false;
            int durée_min = 0;
            bool durée2 = false;
            int durée_max = 0;
            bool somme1 = false;
            double somme_min = 0;
            bool somme2 = false;
            double somme_max = 0;
            bool employé = false;
            bool type = false;

            if (!String.IsNullOrEmpty(type_p.Text))
            {
                type = true;
                foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
                {
                    if (type_p.Text == liste.Value.Description)
                    {
                        responsable.clés_types.Add(liste.Value.Cle);
                        break;
                    }

                }

            }
            if (!String.IsNullOrEmpty(cmbp2.Text))
            {
                employé = true;
                string[] sizes = cmbp2.Text.Split(whitespace);
                foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
                {
                    if (sizes[0] == liste.Value.Nom && sizes[1] == liste.Value.Prenom)
                    {
                        responsable.clés_employés.Add(liste.Value.Cle);
                        break;
                    }

                }
            }




            if (oui.IsChecked == true)
            {
                remboursable = true;
                choix = 1;
            }
            else if (non.IsChecked == true)
            {
                remboursable = true;
                choix = 2;
            }

            //----------------------------------------------

            if (!String.IsNullOrEmpty(min_pm.Text))
            {
                date1 = true;
                d_inf = DateTime.Parse(min_pm.Text);
            }

            //----------------------------------------------

            if (!String.IsNullOrEmpty(max_pm.Text))
            {
                date2 = true;
                d_inf = DateTime.Parse(max_pm.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(min_pv.Text))
            {
                date3 = true;
                pv_min = DateTime.Parse(min_pv.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(max_pv.Text))
            {
                date4 = true;
                pv_max = DateTime.Parse(max_pv.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(dur_min.Text))
            {
                durée1 = true;
                durée_min = int.Parse(dur_min.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(dur_max.Text))
            {
                durée2 = true;
                durée_max = int.Parse(dur_max.Text);
            }
            //--------------------------------------------
            if (!String.IsNullOrEmpty(som_min.Text))
            {
                somme1 = true;
                somme_min = double.Parse(som_min.Text);
            }
            //--------------------------------------------
            if (!String.IsNullOrEmpty(som_max.Text))
            {
                somme2 = true;
                somme_max = double.Parse(som_max.Text);
            }
            responsable.recherche_par_criteres(remboursable, choix, date1, d_inf, date2, d_max, date3, pv_min, date4, pv_max, durée1, durée_min, durée2, durée_max, somme1, somme_min, somme2, somme_max, employé, type);
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new fenetre2());
            //Console.WriteLine(responsable.liste_filtres.Count);
            foreach (KeyValuePair<int, Archive> liste in responsable.liste_filtres)
            {
                Console.WriteLine("{0}---{1}---{2}", liste.Value.Pret.Date_demande, liste.Value.Pret.Date_pv, liste.Value.Pret.Employé.Nom);
            }
        }
    }
}*/
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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            complete_type_pret();
            complete();
        }
        private void complete_type_pret()
        {
            foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
            {
                type_p.Items.Add(liste.Value.Description);
            }
        }

        private void complete()
        {
            foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
            {
                string nom = liste.Value.Nom + " " + liste.Value.Prenom;
                cmbp2.Items.Add(nom);
            }
        }

        private void recherche_Click(object sender, RoutedEventArgs e)
        {
            responsable.liste_filtres.Clear();
            char[] whitespace = new char[] { ' ', '\t' };
            bool remboursable = false;
            int choix = 0;
            bool date1 = false;
            DateTime d_inf = new DateTime();
            bool date2 = false;
            DateTime d_max = new DateTime();
            bool date3 = false;
            DateTime pv_min = new DateTime();
            bool date4 = false;
            DateTime pv_max = new DateTime();
            bool durée1 = false;
            int durée_min = 0;
            bool durée2 = false;
            int durée_max = 0;
            bool somme1 = false;
            double somme_min = 0;
            bool somme2 = false;
            double somme_max = 0;
            bool employé = false;
            bool type = false;

            if (!String.IsNullOrEmpty(type_p.Text))
            {
                type = true;
                foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
                {
                    if (type_p.Text == liste.Value.Description)
                    {
                        responsable.clés_types.Add(liste.Value.Cle);
                        break;
                    }

                }

            }
            if (!String.IsNullOrEmpty(cmbp2.Text))
            {
                employé = true;
                string[] sizes = cmbp2.Text.Split(whitespace);
                foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
                {
                    if (sizes[0] == liste.Value.Nom && sizes[1] == liste.Value.Prenom)
                    {
                        responsable.clés_employés.Add(liste.Value.Cle);
                        break;
                    }

                }
            }




            if (oui.IsChecked == true)
            {
                remboursable = true;
                choix = 1;
            }
            else if (non.IsChecked == true)
            {
                remboursable = true;
                choix = 2;
            }

            //----------------------------------------------

            if (!String.IsNullOrEmpty(min_pm.Text))
            {
                date1 = true;
                d_inf = DateTime.Parse(min_pm.Text);
            }

            //----------------------------------------------

            if (!String.IsNullOrEmpty(max_pm.Text))
            {
                date2 = true;
                d_inf = DateTime.Parse(max_pm.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(min_pv.Text))
            {
                date3 = true;
                pv_min = DateTime.Parse(min_pv.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(max_pv.Text))
            {
                date4 = true;
                pv_max = DateTime.Parse(max_pv.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(dur_min.Text))
            {
                durée1 = true;
                durée_min = int.Parse(dur_min.Text);
            }
            //----------------------------------------------

            if (!String.IsNullOrEmpty(dur_max.Text))
            {
                durée2 = true;
                durée_max = int.Parse(dur_max.Text);
            }
            //--------------------------------------------
            if (!String.IsNullOrEmpty(som_min.Text))
            {
                somme1 = true;
                somme_min = double.Parse(som_min.Text);
            }
            //--------------------------------------------
            if (!String.IsNullOrEmpty(som_max.Text))
            {
                somme2 = true;
                somme_max = double.Parse(som_max.Text);
            }
            responsable.recherche_par_criteres_deux(remboursable, choix, date1, d_inf, date2, d_max, date3, pv_min, date4, pv_max, durée1, durée_min, durée2, durée_max, somme1, somme_min, somme2, somme_max, employé, type);
            Grid_Principale.Children.Clear();
            Grid_Principale.Children.Add(new fenetre2());
            //Console.WriteLine(responsable.liste_filtres.Count);
            /*foreach (KeyValuePair<int, Archive> liste in responsable.liste_filtres)
            {
                Console.WriteLine("{0}---{1}---{2}", liste.Value.Pret.Date_demande, liste.Value.Pret.Date_pv, liste.Value.Pret.Employé.Nom);
            }*/
        }
    }
}
