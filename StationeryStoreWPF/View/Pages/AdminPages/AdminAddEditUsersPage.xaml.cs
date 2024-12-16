using StationeryStoreWPF.DataBase;
using StationeryStoreWPF.View.Windows;
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

namespace StationeryStoreWPF.View.Pages.AdminPages
{
	/// <summary>
	/// Логика взаимодействия для AdminAddEditUsersPage.xaml
	/// </summary>
	public partial class AdminAddEditUsersPage : Page
	{
		Users user = new Users();
		string initialLogin;
		public AdminAddEditUsersPage(Users userData)
		{
			InitializeComponent();
			if (userData != null )
				user = userData;
			DataContext = user; 
			CBoxRole.ItemsSource = App._context.Roles.ToList();
			TBoxSecondName.Focus();
			if (user.ID == 0)
			{
				LAddEdit.Content = "Добавление пользователя";
				BAddEdit.Content = "Добавить";
			}
			else
			{
				LAddEdit.Content = "Редактирование пользователя";
				BAddEdit.Content = "Редактировать";
				initialLogin = user.Login;
			}
		}

		private void TBoxSecondName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TBoxFirstName.Focus();
		}

		private void TBoxFirstName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TBoxSurName.Focus();
		}

		private void TBoxSurName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TBoxLogin.Focus();	
		}

		private void TBoxLogin_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TBoxPassword.Focus();
		}

		private void TBoxPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				CBoxRole.Focus();
		}

		private void CBoxRole_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				BAddEdit_Click(sender, e);
		}
		private void BAddEdit_Click(object sender, RoutedEventArgs e)
		{
			if (TBoxSecondName.Text != null &
				TBoxFirstName.Text != null &
				TBoxLogin.Text != null &
				TBoxPassword.Text != null &
				CBoxRole != null)
			{
				if (IsLoginUnique(TBoxLogin.Text))
				{
					if (user.ID == 0)
					{
						user.SecondName = TBoxSecondName.Text;
						user.FirstName = TBoxFirstName.Text;
						user.SurName = TBoxSurName.Text;
						user.Login = TBoxLogin.Text;
						user.Password = TBoxPassword.Text;
						if (CBoxRole.Text == "Admin")
							user.IDRole = 1;
						else
							user.IDRole = 2;
						App._context.Users.Add(user);
					}
					App._context.SaveChanges();
					App.MainFrame.GoBack();
					App.MainFrame.RemoveBackEntry();
				}
				else
					MessageBox.Show("Данный логин уже занят");
			}
			else
				MessageBox.Show("Введите данные");
		}

		private void BCancel_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.GoBack();
			App.MainFrame.RemoveBackEntry();
		}
		private bool IsLoginUnique(string login)
		{		
			if (initialLogin != login)
				return !App._context.Users.Any(u => u.Login == login);
			else
				return true;
		}
	}
}
