using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Net.Mail;

namespace WpfApp2
{
    public class responsable
    {
        public static double tresor;
        public static int cle_liste_types = 1;              // des attributs statiques permettant de donner des
        public static int cle_liste_pret_remboursable = 1;           // cles uniques aux differents dictionnaires 
        public static int cle_liste_non_remboursable = 1;                 // utilisés en s'incrémentant à chaque ajout
        public static int cle_liste_employe = 1;                             // les clés se sont gérées par nous et pas selon l'introduction
        public static int cle_liste_archive = 1;                                     // de l'utilisateur.
        public static Dictionary<int, Employé> liste_employes = new Dictionary<int, Employé>();//dic num°1
        public static Dictionary<int, Type_pret> liste_types = new Dictionary<int, Type_pret>();//dic num°2
        public static Dictionary<int, Archive> liste_archives = new Dictionary<int, Archive>();//dic num°3
        public static Dictionary<int, pret_remboursable> liste_pret_remboursable = new Dictionary<int, pret_remboursable>();//dic num°4
        public static Dictionary<int, pret_non_remboursable> liste_pret_non_remboursables = new Dictionary<int, pret_non_remboursable>();//dic num°5
        public static Dictionary<int, pret_remboursable> liste_pret_remboursable_provisoire = new Dictionary<int, pret_remboursable>();
        public static Dictionary<int, Archive> liste_archives_provisoire = new Dictionary<int, Archive>();

        public static List<Modification> pile_modifications = new List<Modification>();
        public static List<String> services = new List<String>();
        public static Dictionary<int, Archive> liste_filtres = new Dictionary<int, Archive>();
        public static Dictionary<int, pret_remboursable> liste_filtres_rem = new Dictionary<int, pret_remboursable>();
        public static Dictionary<int, pret_non_remboursable> liste_filtres_non_rem = new Dictionary<int, pret_non_remboursable>();
        public static List<int> clés_employés = new List<int>();
        public static List<int> clés_types = new List<int>();
        public static List<String> choix_service = new List<string>();
        public static Dictionary<int, int> liste_stat_1 = new Dictionary<int, int>();
        public static Dictionary<int, double> list_sup = new Dictionary<int, double>();
        public static Dictionary<int, double> list_inf = new Dictionary<int, double>();
        public static Dictionary<int, double> liste_tresor = new Dictionary<int, double>();

        public static string User_mail = "im_aliousalah@esi.dz";
        public static string User_pwd = "";
        //lecture-----------------------------------------------------------
        public static void initialiser_dictionnaire_employes()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            string commande = "SELECT COUNT(*) FROM employes;";
            int longueur_table = 0;
            cmd.CommandText = commande;
            cmd.ExecuteNonQuery();
            SqlDataReader rdr1 = cmd.ExecuteReader();
            rdr1.Read();
            longueur_table = (int)rdr1.GetValue(0);
            rdr1.Close();
            for (int i = 1; i <= longueur_table; i++)
            {
                commande = "SELECT * FROM employes WHERE cle = " + i.ToString() + " ;";
                cmd.CommandText = commande;
                cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                DateTime date_naiss = DateTime.Parse(rdr.GetValue(4).ToString());
                DateTime date_prem = DateTime.Parse(rdr.GetValue(6).ToString());

                Employé emp = new Employé((int)rdr.GetValue(0), rdr.GetValue(13).ToString(), rdr.GetValue(1).ToString(), rdr.GetValue(2).ToString(), rdr.GetValue(3).ToString(), date_naiss, rdr.GetValue(5).ToString(), date_prem, rdr.GetValue(7).ToString(), rdr.GetValue(8).ToString(), rdr.GetValue(9).ToString(), rdr.GetValue(10).ToString(), rdr.GetValue(12).ToString(), rdr.GetValue(13).ToString());
                rdr.Close();
                responsable.liste_employes.Add(emp.Cle, emp);
            }
            cnx.Close();
        }

        public static void initialiser_dictionnaire_archive()
        {
            int longueur_table = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM archive;";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr1 = cmd.ExecuteReader();
            rdr1.Read();
            longueur_table = (int)rdr1.GetValue(0);
            rdr1.Close();
            cmd.Cancel();
            for (int i = 1; i <= longueur_table; i++)
            {
                SqlCommand commande = cnx.CreateCommand();
                commande.CommandText = "SELECT * FROM archive WHERE cle = " + i.ToString() + " ;";
                commande.ExecuteNonQuery();
                SqlDataReader rdr = commande.ExecuteReader();
                rdr.Read();
                float var = (float)rdr.GetDouble(9);
                if (var != -1.0)
                {
                    int id_employe = (int)rdr.GetValue(1);
                    Employé emp = responsable.liste_employes[id_employe];
                    Dictionary<int, double> mois = new Dictionary<int, double>();
                    mois.Add(0, (double)rdr.GetDouble(9));
                    mois.Add(1, (double)rdr.GetDouble(10));
                    mois.Add(2, (double)rdr.GetDouble(11));
                    mois.Add(3, (double)rdr.GetDouble(12));
                    mois.Add(4, (double)rdr.GetDouble(13));
                    mois.Add(5, (double)rdr.GetDouble(14));
                    mois.Add(6, (double)rdr.GetDouble(15));
                    mois.Add(7, (double)rdr.GetDouble(16));
                    mois.Add(8, (double)rdr.GetDouble(17));
                    mois.Add(9, (double)rdr.GetDouble(18));
                    //
                    int cle_type_pret = (int)rdr.GetValue(2);
                    SqlConnection cnx2 = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
                    cnx2.Open();
                    SqlCommand cmd2 = cnx2.CreateCommand();
                    cmd2.CommandText = "SELECT * FROM type_prets WHERE cle = " + cle_type_pret.ToString() + " ;";
                    cmd2.ExecuteNonQuery();
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    rdr2.Read();
                    Type_pret type = new Type_pret(cle_type_pret, (int)rdr2.GetValue(1), (int)rdr2.GetValue(3), rdr2.GetValue(2).ToString(), (int)rdr2.GetValue(4));
                    rdr2.Close();
                    //
                    DateTime date_pv = DateTime.Parse(rdr.GetValue(22).ToString());
                    DateTime date_demande = DateTime.Parse(rdr.GetValue(3).ToString());
                    DateTime date_premier_paiment = DateTime.Parse(rdr.GetValue(4).ToString());
                    pret_remboursable pret = new pret_remboursable((int)rdr.GetValue(0), emp, type, rdr.GetValue(8).ToString(), (int)rdr.GetValue(20), date_pv, (double)rdr.GetDouble(5), date_demande, rdr.GetValue(6).ToString(), date_premier_paiment, (int)rdr.GetValue(23), 0, mois, (int)rdr.GetValue(21));
                    DateTime date_fin_rembourssement = DateTime.Parse(rdr.GetValue(7).ToString());
                    Archive archive = new Archive((int)rdr.GetValue(0), pret, rdr.GetValue(19).ToString(), date_fin_rembourssement, (int)rdr.GetValue(23));
                    responsable.liste_archives.Add((int)rdr.GetValue(0), archive);
                }
                else if (var == -1.0)
                {
                    int id_employe = (int)rdr.GetValue(1);
                    Employé emp = responsable.liste_employes[id_employe];
                    int cle_type_pret = (int)rdr.GetValue(2);
                    Dictionary<int, double> mois = new Dictionary<int, double>();
                    for (int k = 0; k < 10; k++)
                        mois.Add(k, -1);
                    SqlConnection cnx2 = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
                    cnx2.Open();
                    SqlCommand cmd2 = cnx2.CreateCommand();
                    cmd2.CommandText = "SELECT * FROM type_prets WHERE cle = " + cle_type_pret.ToString() + " ;";
                    cmd2.ExecuteNonQuery();
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    rdr2.Read();
                    Type_pret type = new Type_pret(cle_type_pret, (int)rdr2.GetValue(1), (int)rdr2.GetValue(3), rdr2.GetValue(2).ToString(), (int)rdr2.GetValue(4));
                    rdr2.Close();
                    DateTime date_pv = DateTime.Parse(rdr.GetValue(22).ToString());
                    DateTime date_demande = DateTime.Parse(rdr.GetValue(3).ToString());
                    pret_non_remboursable pret = new pret_non_remboursable((int)rdr.GetValue(0), emp, type, rdr.GetValue(8).ToString(), (int)rdr.GetValue(20), date_pv, (double)rdr.GetValue(5), date_demande, rdr.GetValue(6).ToString());
                    Archive archive = new Archive((int)rdr.GetValue(0), pret, rdr.GetValue(19).ToString(), date_demande, (int)rdr.GetValue(23));
                    responsable.liste_archives.Add((int)rdr.GetValue(0), archive);
                }
                rdr.Close();
            }
            cnx.Close();
        }

        public static void initialiser_dictionnaire_types_prets()
        {
            int longueur_table = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM type_prets;";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr1 = cmd.ExecuteReader();
            rdr1.Read();
            longueur_table = (int)rdr1.GetValue(0);
            rdr1.Close();
            SqlCommand commande = cnx.CreateCommand();
            for (int i = 1; i <= longueur_table; i++)
            {
                commande.CommandText = "SELECT * FROM type_prets WHERE cle = " + i.ToString() + " ;";
                commande.ExecuteNonQuery();
                SqlDataReader rdr = commande.ExecuteReader();
                rdr.Read();
                Type_pret type = new Type_pret((int)rdr.GetValue(0), (int)rdr.GetValue(1), (int)rdr.GetValue(3), rdr.GetValue(2).ToString(), (int)rdr.GetValue(4));
                responsable.liste_types.Add((int)rdr.GetValue(0), type);
                rdr.Close();
            }
            cnx.Close();
        }

        public static void initialiser_dictionnaire_pret_remboursable()
        {
            int longueur_table = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM prets_remboursable;";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr1 = cmd.ExecuteReader();
            rdr1.Read();
            longueur_table = (int)rdr1.GetValue(0);
            rdr1.Close();
            SqlCommand commande = cnx.CreateCommand();
            for (int i = 1; i <= longueur_table; i++)
            {
                commande.CommandText = "SELECT * FROM prets_remboursable WHERE cle = " + i.ToString() + " ;";
                commande.ExecuteNonQuery();
                SqlDataReader rdr = commande.ExecuteReader();
                rdr.Read();
                try
                {
                    Employé emp = responsable.liste_employes[(int)rdr.GetValue(1)];
                    Type_pret type = responsable.liste_types[(int)rdr.GetValue(2)];
                    DateTime date_pv = DateTime.Parse(rdr.GetValue(21).ToString());
                    DateTime date_demande = DateTime.Parse(rdr.GetValue(3).ToString());
                    DateTime date_prem_paiment = DateTime.Parse(rdr.GetValue(5).ToString());
                    Dictionary<int, double> mois = new Dictionary<int, double>();
                    mois.Add(0, (double)rdr.GetDouble(10));
                    mois.Add(1, (double)rdr.GetDouble(11));
                    mois.Add(2, (double)rdr.GetDouble(12));
                    mois.Add(3, (double)rdr.GetDouble(13));
                    mois.Add(4, (double)rdr.GetDouble(14));
                    mois.Add(5, (double)rdr.GetDouble(15));
                    mois.Add(6, (double)rdr.GetDouble(16));
                    mois.Add(7, (double)rdr.GetDouble(17));
                    mois.Add(8, (double)rdr.GetDouble(18));
                    mois.Add(9, (double)rdr.GetDouble(19));
                    pret_remboursable pret = new pret_remboursable((int)rdr.GetInt32(0), emp, type, rdr.GetValue(8).ToString(), (int)rdr.GetInt32(4), date_pv, (double)rdr.GetDouble(6), date_demande, rdr.GetValue(7).ToString(), date_prem_paiment, (int)rdr.GetInt32(22), (int)rdr.GetValue(9), mois, (int)rdr.GetInt32(20));
                    liste_pret_remboursable.Add(pret.Cle, pret);

                }
                catch
                {
                    longueur_table++;
                }

                rdr.Close();

            }
            cnx.Close();
        }

        public static void initialiser_dictionnaire_pret_non_remboursable()
        {
            int longueur_table = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM prets_non_remboursable;";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr1 = cmd.ExecuteReader();
            rdr1.Read();
            longueur_table = (int)rdr1.GetValue(0);
            rdr1.Close();
            SqlCommand commande = cnx.CreateCommand();
            for (int i = 1; i <= longueur_table; i++)
            {
                commande.CommandText = "SELECT * FROM prets_non_remboursable WHERE cle = " + i.ToString() + " ;";
                commande.ExecuteNonQuery();
                SqlDataReader rdr = commande.ExecuteReader();
                rdr.Read();
                try
                {
                    Employé emp = responsable.liste_employes[(int)rdr.GetValue(1)];
                    Type_pret type = responsable.liste_types[(int)rdr.GetValue(8)];
                    DateTime date_pv = DateTime.Parse(rdr.GetValue(7).ToString());
                    DateTime date_demande = DateTime.Parse(rdr.GetValue(2).ToString());
                    pret_non_remboursable pret = new pret_non_remboursable((int)rdr.GetInt32(0), emp, type, rdr.GetValue(6).ToString(), (int)rdr.GetInt32(3), date_pv, (double)rdr.GetDouble(4), date_demande, rdr.GetValue(5).ToString());
                    responsable.liste_pret_non_remboursables.Add((int)rdr.GetInt32(0), pret);
                    pret.Employé.ajouter_pret_non_remboursable_employe(pret);
                }
                catch
                {
                    longueur_table++;
                }
                rdr.Close();
            }
            cnx.Close();
        }

        //affichge-----------------------------------------------------------
        public static void affiche_liste_employes()
        {
            foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
            {
                Console.Write("Clé = " + liste.Key + " ||  ");
                liste.Value.affiche_attribus();
            }
        }

        public static void affiche_liste_type_pret()
        {
            foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Clé = " + liste.Key + " || ");
                liste.Value.affiche_attribus();
            }
        }
        public static void affiche_liste_pret_remboursable()
        {
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Clé = " + liste.Key + " || ");
                liste.Value.affiche_attributs_complets();
            }
        }
        public static void affiche_liste_pret_non_remboursable()
        {
            foreach (KeyValuePair<int, pret_non_remboursable> liste in responsable.liste_pret_non_remboursables)
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Clé = " + liste.Key + " || ");
                liste.Value.affiche_attribus();
            }
        }

        public static void affiche_liste_archive()
        {
            foreach (KeyValuePair<int, Archive> liste in responsable.liste_archives)
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Clé = " + liste.Key + " || ");
                liste.Value.affiche_attribue();
            }
        }


        //ajout-----------------------------------------------------------
        public static void ajouter_employe(Employé b)
        {

            if (!(liste_employes.ContainsValue(b)))
            {
                liste_employes.Add(b.Cle, b);
            }
            else
            {
                Console.WriteLine("pas d'ajout d'employe");
            }
        }
        public static void ajouter_type_pret(Type_pret b)
        {
            int cpt = 0;
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (b.Equals(kvp.Value))
                {
                    Console.WriteLine(b.Cle + " " + b.Description);
                    cpt++;
                }
            }
            if (cpt == 0)
            {
                liste_types.Add(b.Cle, b);
            }
            else
            {
                Console.WriteLine("pas d'ajout de type");
            }
        }
        public static void ajouter_pret_remboursable(pret_remboursable b)
        {

            if (!(liste_pret_remboursable.ContainsValue(b)))
            {
                liste_pret_remboursable.Add(b.Cle, b);
            }
            else
            {
                Console.WriteLine("pas d'ajout de pret remboursable");
            }
        }
        public static void ajouter_pret_non_remboursable(pret_non_remboursable b)
        {

            if (!(liste_pret_non_remboursables.ContainsValue(b)))
            {
                liste_pret_non_remboursables.Add(b.Cle, b);
                responsable.tresor = responsable.tresor - b.Montant;
            }
            else
            {
                Console.WriteLine("pas d'ajout de pret non remboursable");
            }
        }

        public static void ajouter_archive(Archive b)
        {

            if (!(liste_archives.ContainsValue(b)))
            {
                liste_archives.Add(b.Cle, b);
            }
            else
            {
                Console.WriteLine("pas d'ajout d'archive");
            }
        }

        //manipulations des prets-----------------------------------------------------------
        public static void suivi()
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                element.Value.paiement();
            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);
            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        /*public static void retardement_paiement(int cle)
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.retardement();
                }
            }
        }*/

        public static void retardement_paiement(int cle)
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.retardement();
                    element.Value.paiement();
                }
            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);

            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        //clés a affecter-----------------------------------------------------------

        public static int cle_a_affecter_employe()
        {
            int cpt = 1;
            foreach (KeyValuePair<int, Employé> kvp in liste_employes)
            {
                if (kvp.Key >= cpt)
                {
                    cpt = kvp.Key + 1;
                }
            }
            return cpt;
        }
        public static int cle_a_affecter_archive()
        {
            int cpt = 1;
            foreach (KeyValuePair<int, Archive> kvp in liste_archives)
            {
                if (kvp.Key >= cpt)
                {
                    cpt = kvp.Key + 1;
                }
            }
            return cpt;
        }

        public static int cle_a_affecter_pret_remboursable()
        {
            int cpt = 1;
            int cpt2 = 1;
            foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
            {
                if (kvp.Key >= cpt)
                {
                    cpt = kvp.Key + 1;
                }
            }
            foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable_provisoire)
            {
                if (kvp.Key >= cpt)
                {
                    cpt2 = kvp.Key + 1;
                }
            }
            if (cpt > cpt2)
            {
                return cpt;
            }
            return cpt2;
        }
        public static int cle_a_affecter_pret_non_remboursable()
        {
            int cpt = 1;
            foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
            {
                if (kvp.Key >= cpt)
                {
                    cpt = kvp.Key + 1;
                }
            }
            return cpt;
        }
        public static int cle_a_affecter_type_pret()
        {
            int cpt = 1;
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (kvp.Key >= cpt)
                {
                    cpt = kvp.Key + 1;
                }
            }
            return cpt;
        }

        public static int type_a_affecter_type_pret()
        {
            int cpt = 1;
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (kvp.Value.Type_de_pret >= cpt)
                {
                    cpt = kvp.Value.Type_de_pret + 1;
                }
            }
            return cpt;
        }


        //service
        public static void ajout_service()
        {
            foreach (Employé emp in responsable.liste_employes.Values)
            {
                if (!services.Contains(emp.Service))
                {
                    services.Add(emp.Service);
                }

            }
        }

        //methodes de creation-----------------------------------------------------------
        public static void Creer_employe(string matricule, string nom, string prenom, string num_sec_social, DateTime date_naissance, string grade, DateTime date_prem, string etat, string ccp, string cle_ccp, string tel, string service, string mail)
        {
            int cpt = 0;
            int cle = cle_a_affecter_employe();
            Employé p = new Employé(cle, matricule, nom, prenom, num_sec_social, date_naissance, grade, date_prem, etat, ccp, cle_ccp, tel, service, mail);
            foreach (KeyValuePair<int, Employé> kvp in liste_employes)
            {

                if (p.Equals(kvp.Value))
                {
                    cpt++;
                }
            }
            if (cpt == 0)
            {
                liste_employes.Add(p.Cle, p);
            }
            else
            {
                Console.WriteLine("Veuillez insérer un employé valide , Le numéro de sécurité sociale inséré est déja existant ! ");
            }
        }



        public static void Creer_Type_pret(int typepret, int dispo, string descri, int remboursable)
        {
            int cpt = 0;
            int cle = cle_a_affecter_type_pret();
            Type_pret p = new Type_pret(cle, typepret, dispo, descri, remboursable);
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (p.Equals(kvp.Value))
                {
                    cpt++;

                }
            }
            if (cpt == 0)
            {
                liste_types.Add(p.Cle, p);
            }
            else
            {
                Console.WriteLine("Ce type existe déja , veuillez insérer un nouveau!");
            }
        }



        public static void Creer_pret_non_remboursable(int employé, int type, string motif, int num_pv, DateTime date_pv, double montant, DateTime date_demande, string montant_lettre)
        {
            int cle = cle_a_affecter_pret_non_remboursable();
            Employé emp = null;
            Type_pret typ = null;
            foreach (KeyValuePair<int, Employé> kvp in liste_employes)
            {
                if (employé == kvp.Key)
                {
                    emp = kvp.Value;
                }
            }
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (type == kvp.Key)
                {
                    typ = kvp.Value;
                }
            }
            if ((typ == null) || (emp == null))
            {
                if (typ == null)
                {
                    Console.WriteLine("Veillez choisir un type existant ou créer un nouveau ! ");
                }
                if (emp == null)
                {
                    Console.WriteLine("Veillez choisir un employé existant ou créer un nouveau ! ");
                }

            }
            else
            {
                pret_non_remboursable p = new pret_non_remboursable(cle, emp, typ, motif, num_pv, date_pv, montant, date_demande, montant_lettre);
                responsable.tresor = tresor - p.Montant;
                liste_pret_non_remboursables.Add(p.Cle, p);
                p.Employé.ajouter_pret_non_remboursable_employe(p);
            }
        }




        public static void Creer_pret_remboursable(int employé, int type, string motif, int num_pv, DateTime date_pv, double montant, DateTime date_demande, string montant_lettre, DateTime date_premier_paiment, int durée)
        {
            int cle = cle_a_affecter_pret_remboursable();
            Employé emp = null;
            Type_pret typ = null;
            foreach (KeyValuePair<int, Employé> kvp in liste_employes)
            {
                if (employé == kvp.Key)
                {
                    emp = kvp.Value;
                }
            }
            foreach (KeyValuePair<int, Type_pret> kvp in liste_types)
            {
                if (type == kvp.Key)
                {
                    typ = kvp.Value;
                }
            }
            if ((typ == null) || (emp == null))
            {
                if (typ == null)
                {
                    Console.WriteLine("Veillez choisir un type existant ou créer un nouveau ! ");
                }
                if (emp == null)
                {
                    Console.WriteLine("Veillez choisir un employé existant ou créer un nouveau ! ");
                }

            }
            else
            {
                Dictionary<int, double> dico1 = new Dictionary<int, double>();
                for (int i = 0; i < 10; i++)
                {
                    dico1.Add(i, -1);
                }

                pret_remboursable p = new pret_remboursable(cle, emp, typ, motif, num_pv, date_pv, montant, date_demande, montant_lettre, date_premier_paiment, durée, 1, dico1, -1);
                responsable.tresor = responsable.tresor - p.Montant;
                p.Employé.ajouter_pret_remboursable_employe(p);
                liste_pret_remboursable.Add(p.Cle, p);
            }
        }

        //methodes sauvgarde-----------------------------------------------------------

        //methode verification si un element de clé (clé) existe dans le dictionnaire employés : pour savoir si c'est un nouvel element ou un elemnt dej existant
        public static bool Clé_Existante_Employé(int clé)
        {
            bool resultat = false;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM employes ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == clé)
                {
                    resultat = true;
                    break;
                }
            }
            rdr.Close();

            return resultat;
        }

        //methode verification si un element de clé (clé) existe dans le dictionnaire type_pret : pour savoir si c'est un nouvel element ou un elemnt dej existant
        public static bool Clé_Existante_Type_Pret(int clé)
        {
            bool resultat = false;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM type_prets ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == clé)
                {
                    resultat = true;
                    break;
                }
            }
            rdr.Close();

            return resultat;
        }

        //methode verification si un element de clé (clé) existe dans le dictionnaire archive : pour savoir si c'est un nouvel element ou un elemnt dej existant
        public static bool Clé_Existante_archive(int clé)
        {
            bool resultat = false;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM archive ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == clé)
                {
                    resultat = true;
                    break;
                }
            }
            rdr.Close();

            return resultat;
        }

        //methode verification si un element de clé (clé) existe dans le dictionnaire pret remboursable : pour savoir si c'est un nouvel element ou un elemnt dej existant
        public static bool Clé_Existante_pret_remboursable(int clé)
        {
            bool resultat = false;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM prets_remboursable ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == clé)
                {
                    resultat = true;
                    break;
                }
            }
            rdr.Close();

            return resultat;
        }

        //methode verification si un element de clé (clé) existe dans le dictionnaire pret_non_remboursable : pour savoir si c'est un nouvel element ou un elemnt dej existant
        public static bool Clé_Existante_pret_non_remboursable(int clé)
        {
            bool resultat = false;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM prets_non_remboursable ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == clé)
                {
                    resultat = true;
                    break;
                }
            }
            rdr.Close();

            return resultat;
        }

        // methode qui verifie si un element a été supprimé dans le dictionnaire pret_remboursable et si un element est supprimé elle retourne sa clé dans la bdd ,retourne 0 sinon (retourne le premier element si ona plusieurs a supprimmer)
        public static int verif_sup_remboursable()
        {
            int clé_a_sup = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM prets_remboursable ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (!responsable.liste_pret_remboursable.ContainsKey(rdr.GetInt32(0)))
                {
                    clé_a_sup = rdr.GetInt32(0);
                    rdr.Close();
                    break;
                }
            }
            rdr.Close();

            return clé_a_sup;
        }

        // methode qui verifie si un element a été supprimé dans le dictionnaire pret_non_remboursable et si un element est supprimé elle retourne sa clé dans la bdd, retourne 0 sinon (retourne le premier element si ona plusieurs a supprimmer)
        public static int verif_sup_non_remboursable()
        {
            int clé_a_sup = 0;
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");
            cnx.Open();
            SqlCommand cmd_cle = cnx.CreateCommand();
            cmd_cle.CommandText = "SELECT cle FROM prets_non_remboursable ;";
            SqlDataReader rdr = cmd_cle.ExecuteReader();

            while (rdr.Read())
            {
                if (!(responsable.liste_pret_non_remboursables.ContainsKey(rdr.GetInt32(0))))
                {

                    clé_a_sup = rdr.GetInt32(0);
                    Console.WriteLine(clé_a_sup);
                    rdr.Close();
                    break;
                }
            }
            rdr.Close();

            return clé_a_sup;
        }


        //methode sauvgarde de changements dans le dictionnaire employes (ajout et modification)
        public static void sauvgarde_Employe()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS; Integrated Security = True");//"Data Source = (localdb)\\localdb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();


            foreach (KeyValuePair<int, Employé> liste in responsable.liste_employes)
            {

                if (!Clé_Existante_Employé(liste.Key))//ajout
                {
                    cmd.CommandText = "SET IDENTITY_INSERT employes ON;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT employes (cle,nom, prenom, num_securite_sociale, date_naissance, grade, date_prem, etat_sociale, ccp, cle_ccp, tel, matricule,service_travail,email) VALUES(" + liste.Key + ",'" + liste.Value.Nom + "','" + liste.Value.Prenom + "','" + liste.Value.sec_soc + "','" + liste.Value.Date_naissance.ToShortDateString() + "','" + liste.Value.Grade + "','" + liste.Value.Date_prem.ToShortDateString() + "','" + liste.Value.etats + "','" + liste.Value.compte_ccp + "','" + liste.Value.Cle_ccp + "','" + liste.Value.tel + ",'" + liste.Value.Matricule + "','" + liste.Value.Service + "','" + liste.Value.Email + "'); ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //methode sauvgarde de changements dans le dictionnaire type_pret (ajout et modification)
        public static void sauvgarde_Type_pret()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();


            foreach (KeyValuePair<int, Type_pret> liste in responsable.liste_types)
            {

                if (!Clé_Existante_Type_Pret(liste.Key))//ajout
                {
                    cmd.CommandText = "SET IDENTITY_INSERT type_prets ON;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT type_prets (cle,type_du_pret, description_pret, disponibilite, remboursable) VALUES(" + liste.Key + "," + liste.Value.Type_de_pret + ",'" + liste.Value.Description + "'," + liste.Value.Disponibilité + "," + liste.Value.Remboursable + "); ";
                    cmd.ExecuteNonQuery();
                }
                else//modification
                {
                    foreach (Modification element in responsable.pile_modifications)
                    {
                        if (element.Dic_modifié == 2)
                        {
                            if (element.Clé_element_modifié == liste.Key)
                            {
                                cmd.CommandText = "UPDATE type_prets SET disponibilite =" + liste.Value.Disponibilité + " WHERE cle=" + element.Clé_element_modifié + ";";
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }
        }

        //methode sauvgarde de changements dans le dictionnaire archive (ajout seulement)
        public static void sauvgarde_archive()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();


            foreach (KeyValuePair<int, Archive> liste in responsable.liste_archives)
            {
                if (!Clé_Existante_archive(liste.Key))
                {
                    if (liste.Value.Pret.Type_Pret.Remboursable == 1)//ajout de pret remboursable
                    {
                        cmd.CommandText = "SET IDENTITY_INSERT archive ON;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT archive(cle,idetifiant_employe, cle_type_pret, date_demande, date_premier_paiment, montant_pret, montant_pret_lettre, date_fin_remboursement, motif, mois_1, mois_2, mois_3, mois_4, mois_5, mois_6, mois_7, mois_8, mois_9, mois_10, observation, num_pv, debordement,date_pv,long_remboursement) VALUES(" + liste.Key + "," + liste.Value.Pret.Employé.Cle + "," + liste.Value.Pret.Type_Pret.Cle + ",'" + liste.Value.Pret.Date_demande.ToShortDateString() + "','" + ((pret_remboursable)liste.Value.Pret).Date_premier_paiment.ToShortDateString() + "'," + liste.Value.Pret.Montant + ",'" + liste.Value.Pret.Montant_lettre + "','" + liste.Value.Date_fin_remboursement.ToShortDateString() + "','" + liste.Value.Pret.Motif + "'," + ((pret_remboursable)liste.Value.Pret).Etat[0] + "," + ((pret_remboursable)liste.Value.Pret).Etat[1] + "," + ((pret_remboursable)liste.Value.Pret).Etat[2] + "," + ((pret_remboursable)liste.Value.Pret).Etat[3] + "," + ((pret_remboursable)liste.Value.Pret).Etat[4] + "," + ((pret_remboursable)liste.Value.Pret).Etat[5] + "," + ((pret_remboursable)liste.Value.Pret).Etat[6] + "," + ((pret_remboursable)liste.Value.Pret).Etat[7] + "," + ((pret_remboursable)liste.Value.Pret).Etat[8] + "," + ((pret_remboursable)liste.Value.Pret).Etat[9] + ",'" + liste.Value.Observations + "'," + liste.Value.Pret.Num_pv + "," + ((pret_remboursable)liste.Value.Pret).Debordement + ",'" + liste.Value.Pret.Date_pv.ToShortDateString() + "'," + liste.Value.Durée + ");";
                        cmd.ExecuteNonQuery();
                    }
                    else//ajout de pret non remboursable
                    {
                        cmd.CommandText = "SET IDENTITY_INSERT archive ON;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT archive(cle,idetifiant_employe, cle_type_pret, date_demande, date_premier_paiment, montant_pret, montant_pret_lettre, date_fin_remboursement, motif, mois_1, mois_2, mois_3, mois_4, mois_5, mois_6, mois_7, mois_8, mois_9, mois_10, observation, num_pv, debordement,date_pv,long_remboursement) VALUES(" + liste.Key + "," + liste.Value.Pret.Employé.Cle + "," + liste.Value.Pret.Type_Pret.Cle + ",'" + liste.Value.Pret.Date_demande.ToShortDateString() + "',NULL," + liste.Value.Pret.Montant + ",'" + liste.Value.Pret.Montant_lettre + "',NULL,'" + liste.Value.Pret.Motif + "',-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'" + liste.Value.Observations + "'," + liste.Value.Pret.Num_pv + ",NULL,'" + liste.Value.Pret.Date_pv.ToShortDateString() + "',-1);";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //methode sauvgarde de changements dans le dictionnaire pret remboursable (ajout, modification, suppression)
        public static void sauvgarde_pret_remboursable()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            int sup = 0;

            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {

                if (!Clé_Existante_pret_remboursable(liste.Key))//ajout
                {
                    cmd.CommandText = "SET IDENTITY_INSERT prets_remboursable ON;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT prets_remboursable(cle,idetifiant_employe, type_pret, date_demande, num_pv, date_premier_paiment, montant_pret, montant_pret_lettre, motif, en_cours, mois_1, mois_2, mois_3, mois_4, mois_5, mois_6, mois_7, mois_8, mois_9, mois_10, debordement,date_pv,long_remboursement) VALUES(" + liste.Key + "," + liste.Value.Employé.Cle + "," + liste.Value.Type_Pret.Cle + ",'" + liste.Value.Date_demande.ToShortDateString() + "'," + liste.Value.Num_pv + ",'" + liste.Value.Date_premier_paiment.ToShortDateString() + "'," + liste.Value.Montant + ",'" + liste.Value.Montant_lettre + "','" + liste.Value.Motif + "'," + liste.Value.En_cours + "," + liste.Value.Etat[0] + "," + liste.Value.Etat[1] + "," + liste.Value.Etat[2] + "," + liste.Value.Etat[3] + "," + liste.Value.Etat[4] + "," + liste.Value.Etat[5] + "," + liste.Value.Etat[6] + "," + liste.Value.Etat[7] + "," + liste.Value.Etat[8] + "," + liste.Value.Etat[9] + "," + liste.Value.Debordement + ",'" + liste.Value.Date_pv.ToShortDateString() + "'," + liste.Value.Durée + ");";
                    cmd.ExecuteNonQuery();
                }
                else//modification
                {
                    foreach (Modification element in responsable.pile_modifications)
                    {
                        if (element.Dic_modifié == 4)
                        {
                            if (element.Clé_element_modifié == liste.Key)
                            {
                                cmd.CommandText = " UPDATE prets_remboursable SET idetifiant_employe=" + liste.Value.Employé.Cle + " ,type_pret=" + liste.Value.Type_Pret.Cle + " ,date_demande='" + liste.Value.Date_demande.ToShortDateString() + "' ,num_pv=" + liste.Value.Num_pv + " ,date_premier_paiment='" + liste.Value.Date_premier_paiment.ToShortDateString() + "' ,montant_pret=" + liste.Value.Montant + " ,montant_pret_lettre='" + liste.Value.Montant_lettre + "' ,motif='" + liste.Value.Motif + "' ,en_cours=" + liste.Value.En_cours + " ,mois_1=" + liste.Value.Etat[0] + " ,mois_2=" + liste.Value.Etat[1] + " ,mois_3=" + liste.Value.Etat[2] + " ,mois_4=" + liste.Value.Etat[3] + " ,mois_5=" + liste.Value.Etat[4] + " ,mois_6=" + liste.Value.Etat[5] + " ,mois_7=" + liste.Value.Etat[6] + " ,mois_8=" + liste.Value.Etat[7] + " ,mois_9=" + liste.Value.Etat[8] + " ,mois_10=" + liste.Value.Etat[9] + " ,debordement=" + liste.Value.Debordement + " ,date_pv='" + liste.Value.Date_pv.ToShortDateString() + "' ,long_remboursement=" + liste.Value.Durée + " WHERE cle=" + element.Clé_element_modifié + ";";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            sup = verif_sup_remboursable();//supression
            while (sup != 0)//on supprimme un seul element par itteration
            {
                cmd.CommandText = "DELETE FROM prets_remboursable WHERE cle=" + sup + ";";
                cmd.ExecuteNonQuery();
                sup = verif_sup_remboursable();
            }
        }

        //methode sauvgarde de changements dans le dictionnaire pret non remboursable (ajout, modification, suppression)
        public static void sauvgarde_pret_non_remboursable()
        {
            SqlConnection cnx = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = BDD_COS_finale_v2; Integrated Security = True");//"Data Source = (localdb)\\localdb2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            int sup = 0;

            foreach (KeyValuePair<int, pret_non_remboursable> liste in responsable.liste_pret_non_remboursables)
            {

                if (!Clé_Existante_pret_non_remboursable(liste.Key))//ajout
                {
                    cmd.CommandText = "SET IDENTITY_INSERT prets_non_remboursable ON;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT prets_non_remboursable(cle,idetifiant_employe, type_pret, date_demande, num_pv, montant_don, montant_don_lettre, motif, date_pv) VALUES(" + liste.Key + "," + liste.Value.Employé.Cle + "," + liste.Value.Type_Pret.Cle + ",'" + liste.Value.Date_demande.ToShortDateString() + "'," + liste.Value.Num_pv + "," + liste.Value.Montant + ",'" + liste.Value.Montant_lettre + "','" + liste.Value.Motif + "','" + liste.Value.Date_pv.ToShortDateString() + "');";
                    cmd.ExecuteNonQuery();
                }
                else//modification
                {
                    foreach (Modification element in responsable.pile_modifications)
                    {
                        if (element.Dic_modifié == 5)
                        {
                            if (element.Clé_element_modifié == liste.Key)
                            {
                                cmd.CommandText = " UPDATE prets_non_remboursable SET idetifiant_employe=" + liste.Value.Employé.Cle + " ,type_pret=" + liste.Value.Type_Pret.Cle + " ,date_demande='" + liste.Value.Date_demande.ToShortDateString() + "' ,num_pv=" + liste.Value.Num_pv + " ,montant_don=" + liste.Value.Montant + " ,montant_don_lettre='" + liste.Value.Montant_lettre + "' ,motif='" + liste.Value.Motif + "' ,date_pv='" + liste.Value.Date_pv.ToShortDateString() + "' WHERE cle=" + element.Clé_element_modifié + ";";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            sup = verif_sup_non_remboursable();//supression
            while (sup != 0)//on supprimme un seul element par itteration
            {
                cmd.CommandText = "DELETE FROM prets_non_remboursable WHERE cle=" + sup + ";";
                cmd.ExecuteNonQuery();
                sup = verif_sup_non_remboursable();
            }
        }

        //methodes archivage(prototype)-----------------------------------------------------------

        /*
        public static void archiver(Prets pret , string observation = "Aucune observation indroduite par l'utilisateur.")
        {
            if(pret.Type_Pret.Remboursable == 1)
            {
                pret_remboursable pret_remb = (pret_remboursable)pret;
                if(pret_remb.Montant == pret_remb.Somme_remboursée)
                {
                    if(pret_remb.Debordement == -1)
                    {
                        int cle = responsable.cle_a_affecter_archive();
                        Archive archive = new Archive(cle, pret_remb, observation, pret_remb.Date_actuelle, pret_remb.Durée);
                        responsable.liste_archives.Add(cle, archive);
                        responsable.liste_pret_remboursable.Remove(pret_remb.Cle);
                    }
                    else
                    {
                       while(true)
                       {
                            if (pret_remb.Debordement == -1)
                                break;
                            int cle = responsable.cle_a_affecter_archive();
                            Archive archive = new Archive(cle, pret_remb, observation, pret_remb.Date_actuelle, pret_remb.Durée);
                            responsable.liste_archives.Add(cle, archive);
                            responsable.liste_pret_remboursable.Remove(pret_remb.Cle);
                            pret_remb = pret_remb.fils();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Impossible d'archiver un pret en cours de remboursement.");
                }
            }
            else
            {
                pret_non_remboursable pret_non_remb = (pret_non_remboursable)pret;
                int cle = responsable.cle_a_affecter_archive();
                Archive archive = new Archive(cle, pret_non_remb, observation, DateTime.Parse("0000-00-00"), 0);
                responsable.liste_archives.Add(cle, archive);
                responsable.liste_pret_non_remboursables.Remove(pret_non_remb.Cle);
            }
        }

        public static void archiver_tout_pret_remboursable()
        {
            foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
            {
                if (kvp.Value.Montant == kvp.Value.Somme_remboursée)
                {
                    responsable.archiver(kvp.Value);
                }
            }
        }
        public static void archiver_tout_pret_non_remboursable()
        {
            foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
            {
                responsable.archiver(kvp.Value);
            }
        }
    
     
        public static void initialisation()
        {
            string observation = "Aucune observation indroduite par l'utilisateur.";
            foreach (KeyValuePair<int, pret_remboursable> liste in responsable.liste_pret_remboursable)
            {
                if (liste.Value.Somme_remboursée == liste.Value.Montant)
                {
                    if ((int)(liste.Value.Date_actuelle - DateTime.Today).Days == 30) // Archivage automatique des prets remboursés et non-archivés 
                    {
                        Archive archive = new Archive(cle_a_affecter_archive(), liste.Value, observation, liste.Value.Date_actuelle, liste.Value.Durée);
                        responsable.liste_archives.Add(archive.Cle, archive);
                        responsable.liste_pret_remboursable.Remove(liste.Key);
                    }
                }
            }
            foreach (KeyValuePair<int, pret_non_remboursable> liste in responsable.liste_pret_non_remboursables)// Archivage automatique des dons non-archivés 
            {
                if((int)(liste.Value.Date_demande - DateTime.Today).Days == 30)
                {
                    Archive archive = new Archive(cle_a_affecter_archive(), liste.Value, observation, DateTime.Parse("0000-00-00"), 0);
                    responsable.liste_archives.Add(archive.Cle, archive);
                    responsable.liste_pret_remboursable.Remove(liste.Key);
                }
            }
        }

    */

        //methodes archivage-----------------------------------------------------------
        public static void archiver_pret_non_remboursable()//archivage auto apres un mois
        {
            foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
            {
                kvp.Value.archiver();
            }
            foreach (KeyValuePair<int, Archive> kvp in responsable.liste_archives_provisoire)
            {
                responsable.liste_pret_non_remboursables.Remove(kvp.Value.Pret.Cle);
            }
            responsable.liste_archives_provisoire.Clear();
        }
        public static void archiver_manuel_pret_non_remboursable(int cle)//archivage auto apres un mois
        {
            foreach (KeyValuePair<int, pret_non_remboursable> element in responsable.liste_pret_non_remboursables)
            {
                if (cle == element.Key)
                {
                    element.Value.archiver_manuel();
                }
            }
            foreach (KeyValuePair<int, Archive> kvp in responsable.liste_archives_provisoire)
            {
                responsable.liste_pret_non_remboursables.Remove(kvp.Value.Pret.Cle);
            }
            responsable.liste_archives_provisoire.Clear();
        }

        public static void archiver_pret_remboursable()//archivage auto apres un mois
        {
            foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
            {
                kvp.Value.archiver();
            }
            foreach (KeyValuePair<int, Archive> kvp in responsable.liste_archives_provisoire)
            {
                responsable.liste_pret_remboursable.Remove(kvp.Value.Pret.Cle);
            }
            responsable.liste_archives_provisoire.Clear();
        }
        public static void archiver_manuel_pret_remboursable(int cle) //Archiver un pret selon le voeux de l'utisitateur et ce qui est bien pour un pret
                                                                      // qui s'etend sur plusieurs lignes on px citer n'imprt quel ligne pour l'archiver
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {

                if (cle == element.Key)
                {

                    element.Value.children();

                }
            }
            foreach (KeyValuePair<int, Archive> kvp in responsable.liste_archives_provisoire)
            {
                responsable.liste_pret_remboursable.Remove(kvp.Value.Pret.Cle);
            }
            responsable.liste_archives_provisoire.Clear();
        }

        public static void paiement_standard(int cle)
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.paiement();
                }
            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);
            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        public static void paiement_spécial(int cle, double cout)
        {

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.paiement_spécial(cout);
                }
            }

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);

            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        public static void paiement_plusieurs_mois(int cle, int nb_mois)
        {

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.paiement_plusieurs_mois(nb_mois);
                }
            }

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);

            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        public static void effacement_dettes(int cle)
        {
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {

                if (cle == element.Key)
                {

                    element.Value.children_effacement_dettes();

                }
            }
            foreach (KeyValuePair<int, Archive> kvp in responsable.liste_archives_provisoire)
            {
                responsable.liste_pret_remboursable.Remove(kvp.Value.Pret.Cle);
            }
            responsable.liste_archives_provisoire.Clear();

        }

        public static void paiement_anticipé(int cle)
        {

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                if (cle == element.Key)
                {
                    element.Value.paiement_anticipé();
                }
            }

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable_provisoire)
            {
                responsable.liste_pret_remboursable.Add(element.Key, element.Value);

            }
            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {
                responsable.liste_pret_remboursable_provisoire.Remove(element.Key);
            }
        }

        public static void initialisation_archive_auto()
        {
            if (Window2.mode_archivage)
            {
                archiver_pret_remboursable();
                archiver_pret_non_remboursable();
            }
        }

        public static void sauvegarder_montant_tresor()
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                path.Replace('\\', '/');
                path += "/Montant_tresor.txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(responsable.tresor.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
        }


        public static void remplissage_liste_filtres()
        {
            responsable.liste_filtres.Clear();
            foreach (KeyValuePair<int, Archive> kvp in liste_archives)
            {
                responsable.liste_filtres.Add(kvp.Key, kvp.Value);
            }
        }

        public static void filtrer_par_remboursable_ou_non(bool remboursable, int choix)
        {

            if (remboursable == true)
            {
                if (choix == 1)
                {
                    foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                    {
                        if (kvp.Value.Pret.Type_Pret.Remboursable == 0)
                        {
                            liste_filtres.Remove(kvp.Key);
                        }
                    }
                }
                else
                {
                    if (choix == 2)
                    {
                        foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                        {
                            if (kvp.Value.Pret.Type_Pret.Remboursable == 1)
                            {
                                liste_filtres.Remove(kvp.Key);
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Veillez entrer un choix valide!");
                    }

                }
            }
        }


        public static void filtrer_par_date_demande_inf(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Date_demande, d_inf) < 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }


        public static void filtrer_par_date_demande_max(bool date, DateTime d_max)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Date_demande, d_max) > 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }



        public static void filtrer_par_date_pv_inf(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Date_pv, d_inf) < 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }


        public static void filtrer_par_date_pv_max(bool date, DateTime d_max)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Date_pv, d_max) > 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_durée_min(bool durée, int durée_min)
        {

            if (durée == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (kvp.Value.Durée < durée_min)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }


        public static void filtrer_par_durée_max(bool durée, int durée_min)
        {

            if (durée == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (kvp.Value.Durée > durée_min)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }



        public static void filtrer_par_somme_min(bool somme, double somme_min)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (kvp.Value.Pret.Montant < somme_min)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }



        public static void filtrer_par_somme_max(bool somme, double somme_max)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (kvp.Value.Pret.Montant > somme_max)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_employés(bool employé)
        {

            int cpt = 0;
            if (employé == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {
                    foreach (int entier in clés_employés)
                    {
                        if (entier == kvp.Value.Pret.Employé.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_employés.Clear();
        }


        public static void filtrer_par_types(bool type)
        {

            int cpt = 0;
            if (type == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {
                    foreach (int entier in clés_types)
                    {
                        if (entier == kvp.Value.Pret.Type_Pret.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_types.Clear();
        }

        public static void filtrer_par_service(bool service)
        {

            int cpt = 0;
            if (service == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {
                    foreach (String s in choix_service)
                    {
                        if (s.Equals(kvp.Value.Pret.Employé.Service))
                        {
                            cpt++;
                        
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.choix_service.Clear();
        }

        public static void filtrer_par_date_recru_min(bool date, DateTime d_recru_min)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Employé.Date_prem, d_recru_min) < 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_date_recru_max(bool date, DateTime d_recru_max)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (DateTime.Compare(kvp.Value.Pret.Employé.Date_prem, d_recru_max) > 0)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_etat(bool e, string etat)
        {

            if (e == true)
            {
                foreach (KeyValuePair<int, Archive> kvp in liste_archives)
                {

                    if (kvp.Value.Pret.Employé.etats != etat)
                    {
                        liste_filtres.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void recherche_par_criteres(bool remboursable, int choix, bool date1, DateTime d_inf, bool date2, DateTime d_max, bool date3, DateTime pv_min, bool date4, DateTime pv_max, bool durée1, int durée_min, bool durée2, int durée_max, bool somme1, double somme_min, bool somme2, double somme_max, bool employé, bool type, bool e, string etat, bool a, DateTime drmin, bool b, DateTime drmax)
        {
            responsable.remplissage_liste_filtres();
            filtrer_par_remboursable_ou_non(remboursable, choix);
            filtrer_par_date_demande_inf(date1, d_inf);
            filtrer_par_date_demande_max(date2, d_max);
            filtrer_par_date_pv_inf(date3, pv_min);
            filtrer_par_date_pv_max(date4, pv_max);
            filtrer_par_durée_min(durée1, durée_min);
            filtrer_par_durée_max(durée2, durée_max);
            filtrer_par_employés(employé);
            filtrer_par_types(type);
            filtrer_par_somme_min(somme1, somme_min);
            filtrer_par_somme_max(somme2, somme_max);
            filtrer_par_etat(e, etat);
            filtrer_par_date_recru_min(a, drmin);
            filtrer_par_date_recru_max(b, drmax);
        }

        public static void recherche_par_criteres_deux(bool remboursable, int choix, bool date1, DateTime d_inf, bool date2, DateTime d_max, bool date3, DateTime pv_min, bool date4, DateTime pv_max, bool durée1, int durée_min, bool durée2, int durée_max, bool somme1, double somme_min, bool somme2, double somme_max, bool employé, bool type,bool service)
        {
            responsable.remplissage_liste_filtres();
            filtrer_par_remboursable_ou_non(remboursable, choix);
            filtrer_par_date_demande_inf(date1, d_inf);
            filtrer_par_date_demande_max(date2, d_max);
            filtrer_par_date_pv_inf(date3, pv_min);
            filtrer_par_date_pv_max(date4, pv_max);
            filtrer_par_durée_min(durée1, durée_min);
            filtrer_par_durée_max(durée2, durée_max);
            filtrer_par_employés(employé);
            filtrer_par_types(type);
            filtrer_par_service(service);
            filtrer_par_somme_min(somme1, somme_min);
            filtrer_par_somme_max(somme2, somme_max);
        }

        //methodes statistiques-----------------------------------------------------------

        public static void stat_pret_durrée(int année)
        {
            liste_stat_1.Clear();

            for (int i = 1; i < 13; i++)
            {
                liste_stat_1.Add(i, 0);
            }

            if (DateTime.Now.Year >= année)
            {

                foreach (KeyValuePair<int, pret_non_remboursable> liste in liste_pret_non_remboursables)
                {
                    if (liste.Value.Date_demande.Year == année)
                    {
                        foreach (KeyValuePair<int, int> mois in liste_stat_1)
                        {
                            if (mois.Key == liste.Value.Date_demande.Month.GetHashCode())
                            {
                                liste_stat_1[mois.Key]++;
                                break;
                            }
                        }
                    }

                }


                foreach (KeyValuePair<int, pret_remboursable> liste in liste_pret_remboursable)
                {
                    if (liste.Value.Date_demande.Year == année)
                    {
                        foreach (KeyValuePair<int, int> mois in liste_stat_1)
                        {
                            if (mois.Key == liste.Value.Date_demande.Month.GetHashCode())
                            {
                                liste_stat_1[mois.Key]++;
                                break;
                            }
                        }
                    }
                }


                foreach (KeyValuePair<int, Archive> liste in liste_archives)
                {
                    if (liste.Value.Pret.Date_demande.Year == année)
                    {
                        foreach (KeyValuePair<int, int> mois in liste_stat_1)
                        {
                            if (mois.Key == liste.Value.Pret.Date_demande.Month.GetHashCode())
                            {
                                liste_stat_1[mois.Key]++;
                                break;
                            }
                        }
                    }
                }

            }
        }

        public static void stat_type_pret(double montant, int annee)
        {
            list_sup.Clear();
            list_inf.Clear();

            foreach (KeyValuePair<int, Type_pret> liste in liste_types)
            {
                //Console.WriteLine(liste.Value.Type_de_pret);
                if (!list_sup.ContainsKey(liste.Value.Type_de_pret))
                {
                    list_sup.Add(liste.Value.Type_de_pret, 0);
                    list_inf.Add(liste.Value.Type_de_pret, 0);
                }
            }
            foreach (KeyValuePair<int, pret_remboursable> liste in liste_pret_remboursable)
            {
                if (liste.Value.Date_demande.Year == annee)
                {
                    if (liste.Value.Montant <= montant) list_inf[liste.Value.Type_Pret.Type_de_pret] += 1;//si 3eme champ =0 alors le montant du pret est inferieur ou egala celui defini  par le responsable
                    else list_sup[liste.Value.Type_Pret.Type_de_pret] += 1;
                }
            }

            foreach (KeyValuePair<int, pret_non_remboursable> liste in liste_pret_non_remboursables)
            {
                if (liste.Value.Date_demande.Year == annee)
                {
                    if (liste.Value.Montant <= montant) list_inf[liste.Value.Type_Pret.Type_de_pret] += 1;//si 3eme champ =0 alors le montant du pret est inferieur ou egala celui defini  par le responsable
                    else list_sup[liste.Value.Type_Pret.Type_de_pret] += 1;
                }
            }
            foreach (KeyValuePair<int, Archive> liste in liste_archives)
            {
                if (liste.Value.Pret.Date_demande.Year == annee)
                {
                    if (liste.Value.Pret.Montant <= montant) list_inf[liste.Value.Pret.Type_Pret.Type_de_pret] += 1;//si 3eme champ =0 alors le montant du pret est inferieur ou egala celui defini  par le responsable
                    else list_sup[liste.Value.Pret.Type_Pret.Type_de_pret] += 1;
                }
            }

        }




        public static void nouveau_tresor(double montant_tresor)
        {
            tresor += montant_tresor;
            sauvgarder_montant_tresor();

            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string[] paths = path.Split('\\');
                path = "";
                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }
                path += "/Montant_tresor.txt";
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write("\n¦" + montant_tresor.ToString() + "|" + DateTime.Now.ToShortDateString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
        }

        public static void charger_montant_tresor()
        {

            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string[] paths = path.Split('\\');
                path = "";
                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }
                path += "/Montant_tresor.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.BaseStream.Position = 0;
                    string line = sr.ReadLine();
                    tresor = double.Parse(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
        }

        public static void sauvgarder_montant_tresor()
        {
            string line;
            string[] year_split = new string[100];
            bool vide = true;

            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string[] paths = path.Split('\\');
                path = "";
                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }
                path += "/Montant_tresor.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.BaseStream.Seek(0, SeekOrigin.End);
                    if (sr.BaseStream.Position != 0)
                    {
                        vide = false;
                        sr.BaseStream.Position = 0;
                        line = sr.ReadToEnd();
                        year_split = line.Split('¦');
                    }
                }
                using (StreamWriter sw = new StreamWriter(path))
                {
                    if (vide)
                        sw.Write(tresor.ToString());
                    else
                    {
                        year_split[0] = tresor.ToString();
                        line = string.Join("\n¦", year_split);
                        sw.Write(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
        }

        public static void ecriture_modif_tresor()
        {
            DateTime date_affectation_tresor = new DateTime();
            int longeur = 0;
            string[] line_split;
            string[] year_split;

            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string[] paths = path.Split('\\');
                path = "";
                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }
                path += "/Montant_tresor.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();
                    year_split = line.Split('¦');
                    line_split = year_split[year_split.Length - 1].Split('|');
                    date_affectation_tresor = DateTime.Parse(line_split[1]);
                    longeur = line_split.Length;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }

            DateTime date_dernier_affectation;
            if (longeur > 2)
                date_dernier_affectation = date_affectation_tresor.AddDays(7 * (longeur - 1));
            else
                date_dernier_affectation = date_affectation_tresor.AddDays(7);

            while (DateTime.Now > date_dernier_affectation)
            {
                try
                {
                    string path = System.IO.Directory.GetCurrentDirectory();
                    string[] paths = path.Split('\\');
                    path = "";
                    for (int i = 0; i < paths.Length - 3; i++)
                    {
                        path += paths[i] + "/";
                    }
                    path += "/Montant_tresor.txt";
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.Write("|" + tresor.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Le fichier n'a pas pu être lu.");
                    Console.WriteLine(e.Message);
                }

                date_dernier_affectation = date_dernier_affectation.AddDays(7);
            }
        }


        public static void stat_tresor(int année)
        {
            liste_tresor.Clear();

            for (int i = 1; i < 53; i++)
            {
                liste_tresor.Add(i, 0);
            }

            string[] year_split;
            string[] line_split = new string[54];
            bool année_exist = false;

            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string[] paths = path.Split('\\');
                path = "";
                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }
                path += "/Montant_tresor.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();
                    year_split = line.Split('¦');
                    for (int i = 1; i < year_split.Length; i++)
                    {
                        line_split = year_split[i].Split('|');

                        if (DateTime.Parse(line_split[1]).Year == année)
                        {
                            année_exist = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }

            if (année_exist)
            {
                foreach (KeyValuePair<int, double> liste in responsable.liste_tresor.ToList())
                {
                    if (liste.Key < line_split.Length)
                    {
                        if (liste.Key == 1)
                            liste_tresor[liste.Key] = double.Parse(line_split[0]);
                        else
                            liste_tresor[liste.Key] = double.Parse(line_split[liste.Key]);
                    }
                }
            }
        }

        //all what hakim and I did (Excel plus filtre pr rembours et non rembours)


        //-------------------------------hakim + amine ----------------------------------------

        public static void remplissage_liste_filtres_rem()
        {
            responsable.liste_filtres_rem.Clear();
            foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
            {
                responsable.liste_filtres_rem.Add(kvp.Key, kvp.Value);
            }
        }

        public static void remplissage_liste_filtres_non_rem()
        {
            responsable.liste_filtres_non_rem.Clear();
            foreach (KeyValuePair<int, pret_non_remboursable> liste in responsable.liste_pret_non_remboursables)
            {
                responsable.liste_filtres_non_rem.Add(liste.Key, liste.Value);
            }
        }

        public static void filtrer_par_date_demande_inf_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (DateTime.Compare(kvp.Value.Date_demande, d_inf) < 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_demande_inf_non_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (DateTime.Compare(kvp.Value.Date_demande, d_inf) < 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_demande_max_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (DateTime.Compare(kvp.Value.Date_demande, d_inf) > 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_demande_max_non_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (DateTime.Compare(kvp.Value.Date_demande, d_inf) > 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_pv_inf_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (DateTime.Compare(kvp.Value.Date_pv, d_inf) < 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_pv_inf_non_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (DateTime.Compare(kvp.Value.Date_pv, d_inf) < 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_date_pv_max_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (DateTime.Compare(kvp.Value.Date_pv, d_inf) > 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_date_pv_max_non_rem(bool date, DateTime d_inf)
        {

            if (date == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (DateTime.Compare(kvp.Value.Date_pv, d_inf) > 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_durée_min_rem(bool durée, int durée_min)
        {

            if (durée == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (kvp.Value.Durée < durée_min)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_durée_max_rem(bool durée, int durée_min)
        {

            if (durée == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (kvp.Value.Durée > durée_min)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }


        public static void filtrer_par_somme_min_rem(bool somme, double somme_min)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (kvp.Value.Montant < somme_min)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_somme_min_non_rem(bool somme, double somme_min)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (kvp.Value.Montant < somme_min)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_somme_max_rem(bool somme, double somme_min)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {

                    if (kvp.Value.Montant > somme_min)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                }
            }
        }

        public static void filtrer_par_somme_max_non_rem(bool somme, double somme_min)
        {

            if (somme == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {

                    if (kvp.Value.Montant > somme_min)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                }
            }
        }
        public static void filtrer_par_employés_rem(bool employé)
        {

            int cpt = 0;
            if (employé == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {
                    foreach (int entier in clés_employés)
                    {
                        if (entier == kvp.Value.Employé.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_employés.Clear();
        }

        public static void filtrer_par_employés_non_rem(bool employé)
        {

            int cpt = 0;
            if (employé == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {
                    foreach (int entier in clés_employés)
                    {
                        if (entier == kvp.Value.Employé.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_employés.Clear();
        }
        public static void filtrer_par_types_rem(bool type)
        {

            int cpt = 0;
            if (type == true)
            {
                foreach (KeyValuePair<int, pret_remboursable> kvp in liste_pret_remboursable)
                {
                    foreach (int entier in clés_types)
                    {
                        if (entier == kvp.Value.Type_Pret.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres_rem.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_types.Clear();
        }

        public static void filtrer_par_types_non_rem(bool type)
        {

            int cpt = 0;
            if (type == true)
            {
                foreach (KeyValuePair<int, pret_non_remboursable> kvp in liste_pret_non_remboursables)
                {
                    foreach (int entier in clés_types)
                    {
                        if (entier == kvp.Value.Type_Pret.Cle)
                        {
                            cpt++;
                        }
                    }

                    if (cpt == 0)
                    {
                        liste_filtres_non_rem.Remove(kvp.Key);
                    }
                    cpt = 0;
                }
            }
            responsable.clés_types.Clear();
        }

        public static void recherche_par_criteres_rem(bool date1, DateTime d_inf, bool date2, DateTime d_max, bool date3, DateTime pv_min, bool date4, DateTime pv_max, bool durée1, int durée_min, bool durée2, int durée_max, bool somme1, double somme_min, bool somme2, double somme_max, bool employé, bool type)
        {
            responsable.remplissage_liste_filtres_rem();
            filtrer_par_date_demande_inf_rem(date1, d_inf);
            filtrer_par_date_demande_max_rem(date2, d_max);
            filtrer_par_date_pv_inf_rem(date3, pv_min);
            filtrer_par_date_pv_max_rem(date4, pv_max);
            filtrer_par_durée_min_rem(durée1, durée_min);
            filtrer_par_durée_max_rem(durée2, durée_max);
            filtrer_par_employés_rem(employé);
            filtrer_par_types_rem(type);
            filtrer_par_somme_min_rem(somme1, somme_min);
            filtrer_par_somme_max_rem(somme2, somme_max);
        }
        public static void recherche_par_criteres_non_rem(bool date1, DateTime d_inf, bool date2, DateTime d_max, bool date3, DateTime pv_min, bool date4, DateTime pv_max, bool somme1, double somme_min, bool somme2, double somme_max, bool employé, bool type)
        {
            responsable.remplissage_liste_filtres_non_rem();
            filtrer_par_date_demande_inf_non_rem(date1, d_inf);
            filtrer_par_date_demande_max_non_rem(date2, d_max);
            filtrer_par_date_pv_inf_non_rem(date3, pv_min);
            filtrer_par_date_pv_max_non_rem(date4, pv_max);
            filtrer_par_employés_non_rem(employé);
            filtrer_par_types_non_rem(type);
            filtrer_par_somme_min_non_rem(somme1, somme_min);
            filtrer_par_somme_max_non_rem(somme2, somme_max);
        }






       /* public static void export_prêts_remboursable()
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            sheet1.Cells[1, 1] = "numero social";
            sheet1.Cells[1, 2] = "Nom";
            sheet1.Cells[1, 3] = "Prenom";
            sheet1.Cells[1, 4] = "type";
            sheet1.Cells[1, 5] = "motif";
            sheet1.Cells[1, 6] = "num_pv";
            sheet1.Cells[1, 7] = "date_pv";
            sheet1.Cells[1, 8] = "montant";
            sheet1.Cells[1, 9] = "date_demande";
            sheet1.Cells[1, 10] = "montant_lettre";
            sheet1.Cells[1, 11] = "date_premier_paiment";
            sheet1.Cells[1, 12] = "en cours";
            sheet1.Cells[1, 13] = "durée";
            sheet1.Cells[1, 14] = "mois 1";
            sheet1.Cells[1, 15] = "mois 2";
            sheet1.Cells[1, 16] = "mois 3";
            sheet1.Cells[1, 17] = "mois 4";
            sheet1.Cells[1, 18] = "mois 5";
            sheet1.Cells[1, 19] = "mois 6";
            sheet1.Cells[1, 20] = "mois 7";
            sheet1.Cells[1, 21] = "mois 8";
            sheet1.Cells[1, 22] = "mois 9";
            sheet1.Cells[1, 23] = "mois 10";
            sheet1.Cells[1, 24] = "Debordement";

            int i = 2;

            foreach (KeyValuePair<int, pret_remboursable> element in responsable.liste_pret_remboursable)
            {

                sheet1.Cells[i, 1] = element.Value.Employé.sec_soc.ToString();
                sheet1.Cells[i, 2] = element.Value.Employé.Nom;
                sheet1.Cells[i, 3] = element.Value.Employé.Prenom;
                sheet1.Cells[i, 4] = element.Value.Type_Pret.Description;
                sheet1.Cells[i, 5] = element.Value.Motif;
                sheet1.Cells[i, 6] = element.Value.Num_pv.ToString();
                sheet1.Cells[i, 7] = element.Value.Date_pv;
                sheet1.Cells[i, 8] = element.Value.Montant.ToString();
                sheet1.Cells[i, 9] = element.Value.Date_demande;
                sheet1.Cells[i, 10] = element.Value.Montant_lettre;
                sheet1.Cells[i, 11] = element.Value.Date_premier_paiment;
                if (element.Value.En_cours == 1)
                {
                    sheet1.Cells[i, 12] = "paiement régulier";
                }
                else
                {
                    sheet1.Cells[i, 12] = "paiment retardée";
                }
                sheet1.Cells[i, 13] = element.Value.Durée;
                sheet1.Cells[i, 24] = "-1";

                int k = 1;

                foreach (KeyValuePair<int, double> elemens in element.Value.Etat)
                {
                    sheet1.Cells[i, k + 13] = elemens.Value;
                    k++;
                }
                i++;
            }
        }
        public static void export_prêts_non_remboursable()
        {
            Excel.Application excel = new Excel.Application();

            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            sheet1.Cells[1, 1] = "numero social";
            sheet1.Cells[1, 2] = "Nom";
            sheet1.Cells[1, 3] = "Prenom";
            sheet1.Cells[1, 4] = "type";
            sheet1.Cells[1, 5] = "motif";
            sheet1.Cells[1, 6] = "num_pv";
            sheet1.Cells[1, 7] = "date_pv";
            sheet1.Cells[1, 8] = "montant";
            sheet1.Cells[1, 9] = "date_demande";
            sheet1.Cells[1, 10] = "montant_lettre";

            int i = 2;
            foreach (KeyValuePair<int, pret_non_remboursable> element in responsable.liste_pret_non_remboursables)
            {
                sheet1.Cells[element.Key + 1, 1] = element.Value.Employé.sec_soc;
                sheet1.Cells[element.Key + 1, 2] = element.Value.Employé.Nom;
                sheet1.Cells[element.Key + 1, 3] = element.Value.Employé.Prenom;
                sheet1.Cells[element.Key + 1, 4] = element.Value.Type_Pret.Description;
                sheet1.Cells[element.Key + 1, 5] = element.Value.Motif;
                sheet1.Cells[element.Key + 1, 6] = element.Value.Num_pv;
                sheet1.Cells[element.Key + 1, 7] = element.Value.Date_pv;
                sheet1.Cells[element.Key + 1, 8] = element.Value.Montant;
                sheet1.Cells[element.Key + 1, 9] = element.Value.Date_demande;
                sheet1.Cells[element.Key + 1, 10] = element.Value.Montant_lettre;
                i++;
            }
            excel.Visible = true;
        }
        public static void export_Archive()
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            sheet1.Cells[1, 1] = "numero social";
            sheet1.Cells[1, 2] = "Nom";
            sheet1.Cells[1, 3] = "Prenom";
            sheet1.Cells[1, 4] = "type";
            sheet1.Cells[1, 5] = "motif";
            sheet1.Cells[1, 6] = "num_pv";
            sheet1.Cells[1, 7] = "date_pv";
            sheet1.Cells[1, 8] = "montant";
            sheet1.Cells[1, 9] = "date_demande";
            sheet1.Cells[1, 10] = "montant_lettre";
            sheet1.Cells[1, 11] = "date_fin_remboursement";
            sheet1.Cells[1, 12] = "durée";
            sheet1.Cells[1, 13] = "Observation";

            int i = 2;

            foreach (KeyValuePair<int, Archive> element in responsable.liste_archives)
            {
                sheet1.Cells[i, 1] = element.Value.Pret.Employé.sec_soc;
                sheet1.Cells[i, 2] = element.Value.Pret.Employé.Nom;
                sheet1.Cells[i, 3] = element.Value.Pret.Employé.Prenom;
                sheet1.Cells[i, 4] = element.Value.Pret.Type_Pret.Description;
                sheet1.Cells[i, 5] = element.Value.Pret.Motif;
                sheet1.Cells[i, 6] = element.Value.Pret.Num_pv;
                sheet1.Cells[i, 7] = element.Value.Pret.Date_pv;
                sheet1.Cells[i, 8] = element.Value.Pret.Montant;
                sheet1.Cells[i, 9] = element.Value.Pret.Date_demande;
                sheet1.Cells[i, 10] = element.Value.Pret.Montant_lettre;
                sheet1.Cells[i, 11] = element.Value.Date_fin_remboursement;
                sheet1.Cells[i, 12] = element.Value.Durée;
                sheet1.Cells[i, 13] = element.Value.Observations;

                i++;
            }
        }
        public static void import_prêts_remboursable()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xlsx";
            ofd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            var sel = ofd.ShowDialog();
            if (sel == true)
            {

                String a = ofd.FileName;
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                Excel.Workbook workBook = excelApp.Workbooks.Open(a);
                Worksheet sheet = (Worksheet)workBook.Sheets[1];

                int i = 2;
                int lastRow = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                while (i < lastRow) // Parcours par lignes du fichier excel
                {
                    Employé c;
                    foreach (KeyValuePair<int, Employé> elemen in responsable.liste_employes)
                    {
                        if (((elemen.Value.sec_soc) == sheet.Cells[i, 1].Value.ToString()) && elemen.Value.Nom.Equals(sheet.Cells[i, 2].Value.ToString()) && elemen.Value.Prenom.Equals(sheet.Cells[i, 3].Value.ToString()))
                        {
                            c = elemen.Value; //trouver l employé existant

                            Type_pret t;
                            foreach (KeyValuePair<int, Type_pret> elem in responsable.liste_types)
                            {
                                if (elem.Value.Description.Equals(sheet.Cells[i, 4].Value.ToString()))
                                {
                                    t = elem.Value;
                                    Dictionary<int, double> dicot = new Dictionary<int, double>();
                                    int z = 1;
                                    while (z < 11)
                                    {
                                        if (sheet.Cells[i, z + 13].Value.ToString() != null)
                                        {
                                            dicot.Add(z, double.Parse(sheet.Cells[i, z + 13].Value.ToString())); //initialization de l attribut dico
                                        }
                                        z++;
                                    }

                                    string b = sheet.Cells[i, 12].Value.ToString(); //contenue de la case pour attribut en cours en string
                                    if (b.Equals("paiement régulier"))
                                    {
                                        int e = 1; // en cours en int
                                        //création du nouveau préts 
                                        pret_remboursable u = new pret_remboursable(cle_a_affecter_pret_remboursable(), c, t, sheet.Cells[i, 5].Value.ToString(), Int32.Parse(sheet.Cells[i, 6].Value.ToString()), DateTime.Parse(sheet.Cells[i, 7].Value.ToString()), double.Parse(sheet.Cells[i, 8].Value.ToString()), DateTime.Parse(sheet.Cells[i, 9].Value.ToString()), sheet.Cells[i, 10].Value.ToString(), DateTime.Parse(sheet.Cells[i, 11].Value.ToString()), Int32.Parse(sheet.Cells[i, 13].Value.ToString()), e, dicot, -1);
                                        responsable.liste_pret_remboursable.Add(u.Cle, u);
                                    }
                                    else
                                    {
                                        pret_remboursable q = new pret_remboursable(cle_a_affecter_pret_remboursable(), c, t, sheet.Cells[i, 5].Value.ToString(), Int32.Parse(sheet.Cells[i, 6].Value.ToString()), DateTime.Parse(sheet.Cells[i, 7].Value.ToString()), double.Parse(sheet.Cells[i, 8].Value.ToString()), DateTime.Parse(sheet.Cells[i, 9].Value.ToString()), sheet.Cells[i, 10].Value.ToString(), DateTime.Parse(sheet.Cells[i, 11].Value.ToString()), Int32.Parse(sheet.Cells[i, 13].Value.ToString()), 1, dicot, -1);
                                        responsable.liste_pret_remboursable.Add(q.Cle, q);
                                    }
                                }
                            }
                        }
                    }
                    i++;
                }
                excelApp.Quit();
            }
        }
        public static void import_prêts_non_remboursable()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xlsx";
            ofd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            var sel = ofd.ShowDialog();
            if (sel == true)
            {

                String a = ofd.FileName;
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                Excel.Workbook workBook = excelApp.Workbooks.Open(a);
                Worksheet sheet = (Worksheet)workBook.Sheets[1];

                int i = 2;

                int lastRow = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                while (i < lastRow + 1) // Parcours par lignes du fichier excel
                {
                    Employé c;
                    foreach (KeyValuePair<int, Employé> elemen in responsable.liste_employes)
                    {
                        if (((elemen.Value.sec_soc) == sheet.Cells[i, 1].Value.ToString()) && elemen.Value.Nom.Equals(sheet.Cells[i, 2].Value.ToString()) && elemen.Value.Prenom.Equals(sheet.Cells[i, 3].Value.ToString()))
                        {
                            c = elemen.Value; //trouver l employé existant

                            Type_pret t;
                            foreach (KeyValuePair<int, Type_pret> elem in responsable.liste_types)
                            {
                                if (elem.Value.Description.Equals(sheet.Cells[i, 4].Value.ToString()))
                                {
                                    t = elem.Value;
                                    pret_non_remboursable alphab = new pret_non_remboursable(cle_a_affecter_pret_non_remboursable(), c, t, sheet.Cells[i, 5].Value.ToString(), int.Parse(sheet.Cells[i, 6].Value.ToString()), DateTime.Parse(sheet.Cells[i, 7].Value.ToString()), double.Parse(sheet.Cells[i, 8].Value.ToString()), DateTime.Parse(sheet.Cells[i, 9].Value.ToString()), sheet.Cells[i, 10].Value.ToString());
                                    liste_pret_non_remboursables.Add(alphab.Cle, alphab); // ajout du nouveau prêts
                                }
                            }
                        }
                    }
                    i++;
                }
                excelApp.Quit();
            }
        }*/

        public static void Envoi_mail(pret_remboursable pret, double montant)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;

            client.Credentials = new System.Net.NetworkCredential(User_mail, User_pwd);

            client.Send(User_mail, pret.Employé.Email, "[Prelevement COS]", "Un montant de " + montant + " DA a été prelevé de votre compte le :" + DateTime.Now.ToShortDateString() + ",\n Cordialement.");


        }
    }
}