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
using System.Windows.Shapes;

namespace StationeryStoreWPF.View.Windows
{
	/// <summary>
	/// Логика взаимодействия для AuthorizationWindow.xaml
	/// </summary>
	public partial class AuthorizationWindow : Window
	{
		public AuthorizationWindow()
		{
			InitializeComponent();
			TBoxLogin.Focus();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var user = App._context.Users.FirstOrDefault(u =>
			u.Login.Trim() == TBoxLogin.Text.Trim()
			& u.Password.Trim() == PBoxPassword.Password.Trim());
			if (user != null)
			{
				MainWindow mainWindow = new MainWindow(user);
				mainWindow.Show();
				this.Close();
			}
			else
			{
				MessageBox.Show("Вы ввели неправильный логин или пароль!");
			}
		}

		private void TBoxLogin_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				PBoxPassword.Focus();
			}
		}

		private void PBoxPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Button_Click(sender, e);
			}
		}
	}
}
