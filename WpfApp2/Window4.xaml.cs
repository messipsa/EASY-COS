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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window4.xaml
    /// </summary>
    public partial class Window4 : UserControl
    {
        public Window4()
        {
            InitializeComponent();
            actualiser();
        }
        public class employee
        {
            public String Nom { get; set; }
            public String Prenom { get; set; }
            public String etat_social { get; set; }
            public String N_Pv { get; set; }
            public String Type_Prêt { get; set; }
            public String Date_de_Pv { get; set; }
            public String Motif { get; set; }
            public String Date_demande { get; set; }
            public String Date_de_recrutement { get; set; }
            public String Montant_Prét_lettre { get; set; }
            public String Montant_Prét { get; set; }
            public String Date_dernier_paiment { get; set; }
            public String Duree_de_paiment { get; set; }



        }
        private void actualiser()
        {
            double somme = 0;

            Resultats_recherche.ItemsSource = null;
            //  liste_employes.ItemsSource = null;
            // type_pret_datagrid.Items.Clear();
            //Prêt_Type_ajout.ItemsSource = null;


            //responsable.liste_filtres.Add(40, responsable.liste_archives[1]);
            List<employee> source = new List<employee>();
            source.Clear();
            foreach (KeyValuePair<int, Archive> liste in responsable.liste_filtres)
            {

                employee Employe = new employee();
                Employe.Nom = liste.Value.Pret.Employé.Nom;
                Employe.Prenom = liste.Value.Pret.Employé.Prenom;
                Employe.etat_social = liste.Value.Pret.Employé.etats;
                Employe.N_Pv = liste.Value.Pret.Num_pv.ToString();
                Employe.Type_Prêt = liste.Value.Pret.Type_Pret.Description;
                Employe.Date_de_Pv = liste.Value.Pret.Date_pv.ToString();
                Employe.Motif = liste.Value.Pret.Motif;
                Employe.Date_demande = liste.Value.Pret.Date_demande.ToString();
                Employe.Date_de_recrutement = liste.Value.Pret.Employé.Date_prem.ToString();
                Employe.Montant_Prét_lettre = liste.Value.Pret.Montant_lettre;
                Employe.Montant_Prét = liste.Value.Pret.Montant.ToString();
                Employe.Date_dernier_paiment = liste.Value.Date_fin_remboursement.ToString();
                if (liste.Value.Durée < 0) Employe.Duree_de_paiment = "/";
                else Employe.Duree_de_paiment = liste.Value.Durée.ToString();

                source.Add(Employe);

            }
            Resultats_recherche.ItemsSource = source;
            cpt.Text = responsable.liste_filtres.Count.ToString();
            foreach (KeyValuePair<int, Archive> liste in responsable.liste_filtres)
            {
                somme += liste.Value.Pret.Montant;
            }
            somme_total.Text = somme.ToString();

        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Main_Grid.Children.Clear();
            Main_Grid.Children.Add(new Window3());


        }
    }
}