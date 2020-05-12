using System;
using System.Windows;

namespace WpfTutorialSamples.Dialogs
{
	public partial class InputDialogSample2 : Window
	{
		public InputDialogSample2()
		{
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			int choice=0;
			choice = int.Parse((choix_sup.Text.Split(')'))[0]);
			WpfApp2.responsable.remise_a_zero(choice);
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
