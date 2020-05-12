using System;
using System.Windows;

namespace WpfTutorialSamples.Dialogs
{
	public partial class InputDialogSample : Window
	{
		public bool sortie = false;
		public bool aff = false;
		public double mo = 0;
		WpfApp2.pret_remboursable pr;
		public InputDialogSample(WpfApp2.pret_remboursable p, double m, string question, string defaultAnswer = "")
		{
			InitializeComponent();			
			lblQuestion.Content = question;
			txtAnswer.Text = defaultAnswer;
			mo = m;
			pr = p;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{

			pr.Employé.Email = txtAnswer.Text;

			WpfApp2.responsable.Envoi_mail(pr, mo);
			aff = true;
			this.Close();
			//this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtAnswer.SelectAll();
			txtAnswer.Focus();
		}

		public string Answer
		{
			get { return txtAnswer.Text; }
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.sortie = true;
			this.Close();
		}
	}
}
