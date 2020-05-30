﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static bool isRun = false;
        private static readonly object syncLock = new object();
        public Window1()
        {
            Lecture_BDD();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;   
            InitializeComponent();
            //intro.Play();
            verification.Close();
            verification.Visibility = Visibility.Hidden;
            DoubleAnimation a = new DoubleAnimation();
            a.From = 0.0; a.To = 1.0;
            a.Duration = new Duration(TimeSpan.FromSeconds(3));
            //Droit.BeginAnimation(OpacityProperty, a);

        }
        static string user_name="admin";
        static string user_paswword="admin";

        DispatcherTimer a = new DispatcherTimer();
        private void timer_tick(object sender, EventArgs e)
        {
            verification.Stop();
            a.Stop();
            Connexion_Grid.Visibility = Visibility.Hidden;
            MainWindow window = new MainWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Password_show.Password = user_paswword;
            window.Pseudo_show.Text = user_name;
            window.Show();

        }
        void Loading()
        {
            verification.Position = new TimeSpan(0, 0, 2);
            verification.Play();
            verification.Visibility = Visibility.Visible;
            a.Tick += timer_tick;
            a.Interval = new TimeSpan(0, 0, 4);
            a.Start();
        }

        private void checked_MediaEnded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Se_Connecter_Click(object sender, RoutedEventArgs e)
        { 
            if(Nom_utilisateur.Text.Equals(user_name) && mot_de_passe.Password.Equals(user_paswword))
            {
                Loading();
            }
            else
            {
                SystemSounds.Hand.Play();
                error.Visibility = Visibility.Visible;

                DoubleAnimation b = new DoubleAnimation();
                b.From = 1.0; b.To = 0.0;
                b.Duration = new Duration(TimeSpan.FromSeconds(5));
                error.BeginAnimation(OpacityProperty, b);
            }
        }

        private void Arrét_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void intro_MediaEnded(object sender, RoutedEventArgs e)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(1));

            ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation1.From = new Thickness(0, 0, 0, 0);
            myDoubleAnimation1.To = new Thickness(-300, 0, 0, 0);
            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);

            Storyboard.SetTarget(myDoubleAnimation1, connexion_grid);

            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(Grid.MarginProperty));
            sb.Begin();
        }


        public void Lecture_BDD()
        {
            lock (syncLock)
            {
                if (!isRun)
                {
                    responsable.initialiser_dictionnaire_employes();
                    responsable.initialiser_dictionnaire_archive();
                    responsable.initialiser_dictionnaire_types_prets();
                    responsable.initialiser_dictionnaire_pret_remboursable();
                    responsable.initialiser_dictionnaire_pret_non_remboursable();
                    responsable.charger_montant_tresor();
                    responsable.initialisation_archive_auto();
                    isRun = true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}