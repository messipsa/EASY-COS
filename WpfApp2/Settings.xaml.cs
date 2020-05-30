using Microsoft.Win32;
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
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        public ImageSource user_photo;
        public string user_name;
        public string user_Password;

        public static bool mode_archivage = false;
        public static string durée_avant_archivage_d = "0";
        public static string durée_avant_archivage_m = "1";
        public static bool envoi_notif = false;
        public static bool mode_envoi = false;

        public Window2(TextBox psuedo_show, PasswordBox pass, Image img)
        {
            InitializeComponent();

            if (mode_archivage)
            { jours_archv.Visibility = Visibility.Visible; mois_archv.Visibility = Visibility.Visible; label_d.Visibility = Visibility.Visible; }
            else { jours_archv.Visibility = Visibility.Hidden; mois_archv.Visibility = Visibility.Hidden; label_d.Visibility = Visibility.Hidden; }

            if (envoi_notif)
            { toogle_mode_envoi.Visibility = Visibility.Visible; auto.Visibility = Visibility.Visible; man.Visibility = Visibility.Visible; label_m.Visibility = Visibility.Visible; }
            else { toogle_mode_envoi.Visibility = Visibility.Hidden; auto.Visibility = Visibility.Hidden; man.Visibility = Visibility.Hidden; label_m.Visibility = Visibility.Hidden; }

            user_name = psuedo_show.Text;
            user_Password = pass.Password;
            user_photo = img.Source;

            toogle_mode_archive.IsChecked = mode_archivage;
            jours_archv.Text = durée_avant_archivage_d;
            mois_archv.Text = durée_avant_archivage_m;
            tooggle_envoi_notif.IsChecked = envoi_notif;
            toogle_mode_envoi.IsChecked = mode_envoi;
        }


        private void Confirmer_changement_Click(object sender, RoutedEventArgs e)
        {
            if ((mot_de_passe_nouveau_confirmation_Valide.Visibility == Visibility.Visible) && (mot_de_passe_actuel_Valide.Visibility == Visibility.Visible))
            {
                user_Password = mot_de_passe_nouveau_confirmation.Password;
                mot_de_passe_actuel.Password = null;
                mot_de_passe_nouveau.Password = null;
                mot_de_passe_nouveau_confirmation = null;
                mot_de_passe_actuel_Invalide.Visibility = Visibility.Hidden;
                mot_de_passe_actuel_Valide.Visibility = Visibility.Hidden;
                mot_de_passe_nouveau_confirmation_Invalide.Visibility = Visibility.Hidden;
                mot_de_passe_nouveau_confirmation_Valide.Visibility = Visibility.Hidden;
                mot_de_passe_modification.Visibility = Visibility.Collapsed;
            }
            else if (mot_de_passe_actuel_Invalide.Visibility == Visibility.Visible)
            {
                DoubleAnimation c = new DoubleAnimation();
                c.From = 1.0; c.To = 0.0;
                c.Duration = new Duration(TimeSpan.FromSeconds(4));
                mot_de_passe_actuel_Invalide.BeginAnimation(OpacityProperty, c);

                if (mot_de_passe_nouveau_confirmation_Invalide.Visibility == Visibility.Visible)
                {
                    DoubleAnimation k = new DoubleAnimation();
                    k.From = 1.0; k.To = 0.0;
                    k.Duration = new Duration(TimeSpan.FromSeconds(4));
                    mot_de_passe_nouveau_confirmation_Invalide.BeginAnimation(OpacityProperty, k);
                }
            }
            else if (mot_de_passe_nouveau_confirmation_Invalide.Visibility == Visibility.Visible)
            {
                DoubleAnimation k = new DoubleAnimation();
                k.From = 1.0; k.To = 0.0;
                k.Duration = new Duration(TimeSpan.FromSeconds(4));
                mot_de_passe_nouveau_confirmation_Invalide.BeginAnimation(OpacityProperty, k);
                if (mot_de_passe_actuel_Invalide.Visibility == Visibility.Visible)
                {
                    DoubleAnimation c = new DoubleAnimation();
                    c.From = 1.0; c.To = 0.0;
                    c.Duration = new Duration(TimeSpan.FromSeconds(4));
                    mot_de_passe_actuel_Invalide.BeginAnimation(OpacityProperty, c);
                }
            }
        }

        private void Annuler_changement_Click(object sender, RoutedEventArgs e)
        {

            mot_de_passe_actuel.Password = null;
            mot_de_passe_nouveau.Password = null;
            mot_de_passe_nouveau_confirmation.Password = null;
            mot_de_passe_actuel_Invalide.Visibility = Visibility.Hidden;
            mot_de_passe_actuel_Valide.Visibility = Visibility.Hidden;
            mot_de_passe_nouveau_confirmation_Invalide.Visibility = Visibility.Hidden;
            mot_de_passe_nouveau_confirmation_Valide.Visibility = Visibility.Hidden;
            mot_de_passe_modification.Visibility = Visibility.Collapsed;

        }

        private void mot_de_passe_nouveau_confirmation_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (mot_de_passe_nouveau.Password.Equals(mot_de_passe_nouveau_confirmation.Password) && (mot_de_passe_nouveau.Password.Length != 0))
            {
                mot_de_passe_nouveau_confirmation_Valide.Visibility = Visibility.Visible;
                mot_de_passe_nouveau_confirmation_Invalide.Visibility = Visibility.Hidden;
            }
            else
            {
                mot_de_passe_nouveau_confirmation_Valide.Visibility = Visibility.Hidden;
                mot_de_passe_nouveau_confirmation_Invalide.Visibility = Visibility.Visible;
            }
        }

        private void back_Menu_Click(object sender, RoutedEventArgs e)
        {

            mode_archivage = toogle_mode_archive.IsChecked.Value;
            durée_avant_archivage_d = jours_archv.Text;
            durée_avant_archivage_m = mois_archv.Text;
            envoi_notif = tooggle_envoi_notif.IsChecked.Value;
            mode_envoi = toogle_mode_envoi.IsChecked.Value;

            grid_settings.Visibility = Visibility.Hidden;
            MainWindow menu = new MainWindow();
            menu.media.Close();
            menu.welcome.Close();
            menu.welcome.Visibility = Visibility.Hidden;
            menu.Menu.Visibility = Visibility.Visible;
            // menu.media.Visibility = Visibility.Hidden;
            menu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            menu.Show();
            //probleme kbirrrrrrr

            menu.Password_show.Password = user_Password;
            menu.Pseudo_show.Text = user_name;
            if (user_photo != null)
            {
                menu.image_info.Source = user_photo;
            }

        }

        private void Confirmer_changement_Nom_utilisateur_Click(object sender, RoutedEventArgs e)
        {
            if ((mot_de_passe_Valide.Visibility == Visibility.Visible) && (Nom_utilisateur_nouveau.Text != null))
            {
                user_name = Nom_utilisateur_nouveau.Text;
                mot_de_passe.Password = null;
                Nom_utilisateur_nouveau.Text = null;
                mot_de_passe_Valide.Visibility = Visibility.Hidden;
                mot_de_passe_Invalide.Visibility = Visibility.Hidden;
                Pseudo_modification.Visibility = Visibility.Collapsed;
            }
            else
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 1.0; anim.To = 0.0;
                anim.Duration = new Duration(TimeSpan.FromSeconds(4));
                mot_de_passe_Invalide.BeginAnimation(OpacityProperty, anim);
            }

        }

        private void Annuler_changement_Nom_utilisateur_Click(object sender, RoutedEventArgs e)
        {
            mot_de_passe.Password = null;
            Nom_utilisateur_nouveau.Text = null;
            mot_de_passe_Valide.Visibility = Visibility.Hidden;
            mot_de_passe_Invalide.Visibility = Visibility.Hidden;
            Pseudo_modification.Visibility = Visibility.Collapsed;
        }

        private void mot_de_passe_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (mot_de_passe.Password.Equals(user_Password))
            {
                mot_de_passe_Invalide.Visibility = Visibility.Hidden;
                mot_de_passe_Valide.Visibility = Visibility.Visible;
            }
            else
            {
                mot_de_passe_Invalide.Visibility = Visibility.Visible;
                mot_de_passe_Valide.Visibility = Visibility.Hidden;
            }
        }

        private void mot_de_passe_actuel_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (mot_de_passe_actuel.Password.Equals(user_Password))
            {
                mot_de_passe_actuel_Valide.Visibility = Visibility.Visible;
                mot_de_passe_actuel_Invalide.Visibility = Visibility.Hidden;
            }
            else
            {
                mot_de_passe_actuel_Valide.Visibility = Visibility.Hidden;
                mot_de_passe_actuel_Invalide.Visibility = Visibility.Visible;
            }
        }

        private void Label_mot_de_passe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mot_de_passe_modification.Visibility = Visibility.Visible;
        }

        private void Label_nom_utilisateur_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Pseudo_modification.Visibility = Visibility.Visible;
        }

        private void Label_photo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Image_modification.Visibility = Visibility.Visible;
            default_picture.Visibility = Visibility.Visible;
        }

        private void Label_aide_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label_aide.Foreground = Brushes.BlueViolet;
        }

        private void upload_image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                profil.Source = new BitmapImage(fileUri);

                confirmer_upload_image.Visibility = Visibility.Visible;
                upload_image.Visibility = Visibility.Hidden;
            }

        }

        private void annuler_upload_image_Click(object sender, RoutedEventArgs e)
        {
            profil.Source = null;
            default_picture.Visibility = Visibility.Hidden;
            Image_modification.Visibility = Visibility.Collapsed;
        }

        private void confirmer_upload_image_Click(object sender, RoutedEventArgs e)
        {
            user_photo = profil.Source;
            profil_confirmation.Visibility = Visibility.Visible;
            DoubleAnimation k = new DoubleAnimation();
            k.From = 1.0;
            k.To = 0.0;
            k.Duration = new Duration(TimeSpan.FromSeconds(5));
            k.Completed += new EventHandler(fin_label);
            profil_confirmation.BeginAnimation(OpacityProperty, k);

        }
        private void fin_label(object sender, EventArgs e)
        {
            default_picture.Visibility = Visibility.Hidden;
            Image_modification.Visibility = Visibility.Collapsed;
            profil.Source = null;
        }

        private void toogle_mode_archive_Checked(object sender, RoutedEventArgs e)
        {
            mode_archivage = toogle_mode_archive.IsChecked.Value;

            if (mode_archivage)
            { jours_archv.Visibility = Visibility.Visible; mois_archv.Visibility = Visibility.Visible; label_d.Visibility = Visibility.Visible; }
            else { jours_archv.Visibility = Visibility.Hidden; mois_archv.Visibility = Visibility.Hidden; label_d.Visibility = Visibility.Hidden; }
        }

        private void tooggle_envoi_notif_Checked(object sender, RoutedEventArgs e)
        {
            envoi_notif = tooggle_envoi_notif.IsChecked.Value;

            if (envoi_notif)
            { toogle_mode_envoi.Visibility = Visibility.Visible; auto.Visibility = Visibility.Visible; man.Visibility = Visibility.Visible; label_m.Visibility = Visibility.Visible; }
            else { toogle_mode_envoi.Visibility = Visibility.Hidden; auto.Visibility = Visibility.Hidden; man.Visibility = Visibility.Hidden; label_m.Visibility = Visibility.Hidden; }

        }

        private void tooggle_envoi_notif_Unchecked(object sender, RoutedEventArgs e)
        {
            envoi_notif = tooggle_envoi_notif.IsChecked.Value;

            if (envoi_notif)
            { toogle_mode_envoi.Visibility = Visibility.Visible; auto.Visibility = Visibility.Visible; man.Visibility = Visibility.Visible; label_m.Visibility = Visibility.Visible; }
            else { toogle_mode_envoi.Visibility = Visibility.Hidden; auto.Visibility = Visibility.Hidden; man.Visibility = Visibility.Hidden; label_m.Visibility = Visibility.Hidden; }

        }

        private void toogle_mode_archive_Unchecked(object sender, RoutedEventArgs e)
        {
            mode_archivage = toogle_mode_archive.IsChecked.Value;

            if (mode_archivage)
            { jours_archv.Visibility = Visibility.Visible; mois_archv.Visibility = Visibility.Visible; label_d.Visibility = Visibility.Visible; }
            else { jours_archv.Visibility = Visibility.Hidden; mois_archv.Visibility = Visibility.Hidden; label_d.Visibility = Visibility.Hidden; }
        }
    }
}