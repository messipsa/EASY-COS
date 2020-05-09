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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window6.xaml
    /// </summary>
    public partial class Employes : UserControl
    {
        private static string nom_emp_;
        private static string prenom_emp_;
        private static string matricule_emp_;
        private static string num_sec_seoc_emp_;
        private static string grade_emp_;
        private static string etat_emp_;
        private static string ccp_emp_;
        private static string cle_ccp_emp_;
        private static string tel_emp_;
        private static string date_naiss_emp_;
        private static string date_recru_emp_;
        private static string service_;
        private static string email_;
        public Employes()
        {
            InitializeComponent();
            actualiser();
        }

        public class employe
        {
            public string Id { get; set; }
            public String Matricule { get; set; }
            public String Nom { get; set; }
            public String Prenom { get; set; }
            public String Num_sec_soc { get; set; }
            public String Date_naissance { get; set; }
            public String Grade { get; set; }
            public String Date_recrutement { get; set; }
            public String Etat { get; set; }
            public String CCP { get; set; }
            public String Cle_ccp { get; set; }
            public String Tel { get; set; }            
            public String Service { get; set; }
            public String Email { get; set; }
        }

        private void actualiser_click(object sender, RoutedEventArgs e)
        {
            liste_employés.ItemsSource = null;
            List<employe> source = new List<employe>();
            source.Clear();
            foreach (Employé liste in responsable.liste_employes.Values)
            {
                employe emp = new employe();
                emp.Id = liste.Cle.ToString();
                emp.Matricule = liste.Matricule;
                emp.Nom = liste.Nom;
                emp.Prenom = liste.Prenom;
                emp.Num_sec_soc = liste.sec_soc;
                emp.Date_naissance = liste.Date_naissance.ToString();
                emp.Grade = liste.Grade;
                emp.Etat = liste.etats;
                emp.CCP = liste.compte_ccp;
                emp.Cle_ccp = liste.Cle_ccp;
                emp.Tel = liste.tel;
                emp.Service = liste.Service;
                emp.Email = liste.Email;
                source.Add(emp);
            }
            liste_employés.ItemsSource = source;
        }
        private void actualiser()
        {
            liste_employés.ItemsSource = null;
            List<employe> source = new List<employe>();
            source.Clear();
            foreach (Employé liste in responsable.liste_employes.Values)
            {
                employe emp = new employe();
                emp.Id = liste.Cle.ToString();
                emp.Matricule = liste.Matricule;
                emp.Nom = liste.Nom;
                emp.Prenom = liste.Prenom;
                emp.Num_sec_soc = liste.sec_soc;
                emp.Date_naissance = liste.Date_naissance.ToString();
                emp.Grade = liste.Grade;
                emp.Etat = liste.etats;
                emp.CCP = liste.compte_ccp;
                emp.Cle_ccp = liste.Cle_ccp;
                emp.Tel = liste.tel;
                emp.Service = liste.Service;
                emp.Email = liste.Email;
                source.Add(emp);
            }
            liste_employés.ItemsSource = source;
        }

        private void Confirmer_Ajout_emp_Click(object sender, RoutedEventArgs e)
        {
            if (nom_ajout.Text.Equals("") || prenom_ajout.Text.Equals("") || matricule.Text.Equals("") || num_sec_social.Text.Equals("") || grade.Text.Equals("") || etat.Text.Equals("") || ccp.Text.Equals("") || cle_ccp.Text.Equals("") || telephone.Text.Equals("") || date_naiss.SelectedDate.Equals(null) || date_prem.SelectedDate.Equals(null))
            {
                Remarquee.Visibility = Visibility.Visible;
                DoubleAnimation a = new DoubleAnimation();
                a.From = 1.0; a.To = 0.0;
                a.Duration = new Duration(TimeSpan.FromSeconds(5));
                Remarquee.BeginAnimation(OpacityProperty, a);
            }
            else
            {
                nom_emp_ = nom_ajout.Text;
                prenom_emp_ = prenom_ajout.Text;
                matricule_emp_ = matricule.Text;
                num_sec_seoc_emp_ = num_sec_social.Text;
                grade_emp_ = grade.Text;
                etat_emp_ = etat.Text;
                ccp_emp_ = ccp.Text;
                cle_ccp_emp_ = cle_ccp.Text;
                tel_emp_ = telephone.Text;
                date_naiss_emp_ = date_naiss.SelectedDate.ToString();
                date_recru_emp_ = date_prem.SelectedDate.ToString();
                service_ = Service.Text;
                responsable.Creer_employe(matricule.Text, nom_ajout.Text, prenom_ajout.Text, num_sec_social.Text, DateTime.Parse(date_naiss.SelectedDate.ToString()), grade.Text, DateTime.Parse(date_prem.SelectedDate.ToString()), etat.Text, ccp.Text, cle_ccp.Text, telephone.Text, service_, email_);

                Grid_Ajout_employe.Visibility = Visibility.Hidden; Grid_Ajout_employe.IsEnabled = false;
                liste_employés.Visibility = Visibility.Visible; liste_employés.IsEnabled = true;
                actualiser();
            }
        }
        private void Annuler_Ajout_emp_Click(object sender, RoutedEventArgs e)
        {
            Grid_Ajout_employe.Visibility = Visibility.Hidden; Grid_Ajout_employe.IsEnabled = false;
            liste_employés.Visibility = Visibility.Visible; liste_employés.IsEnabled = true;
        }

        private void ajouter_employe(object sender, RoutedEventArgs e)
        {
            liste_employés.Visibility = Visibility.Hidden; liste_employés.IsEnabled = false;
            Grid_Ajout_employe.Visibility = Visibility.Visible; Grid_Ajout_employe.IsEnabled = true;
        }
    }
}
