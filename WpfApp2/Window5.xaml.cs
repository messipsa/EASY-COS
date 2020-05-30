using System;
using System.Windows;

namespace WpfTutorialSamples.Dialogs
{
	public partial class InputDialogSample : Window
	{
		public bool sortie = false;
		public bool aff = false;
		public InputDialogSample(string question, string defaultAnswer = "")
		{
			InitializeComponent();			
			lblQuestion.Content = question;
			txtAnswer.Text = defaultAnswer;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			aff = true;
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
