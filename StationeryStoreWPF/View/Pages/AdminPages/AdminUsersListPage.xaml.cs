using StationeryStoreWPF.DataBase;
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
	/// Логика взаимодействия для AdminUsersListPage.xaml
	/// </summary>
	public partial class AdminUsersListPage : Page
	{
		public AdminUsersListPage()
		{
			InitializeComponent();
			UpdateGrid();
		}

		private void UpdateGrid()
		{
			DGUsers.ItemsSource = App._context.Users.ToList();
		}
		private void BtAdd_Click (object sender, RoutedEventArgs e)
		{
			App.MainFrame.Navigate(new AdminAddEditUsersPage(null));
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateGrid();
		}

		private void BtEdit_Click(object sender, RoutedEventArgs e)
		{
			if (DGUsers.SelectedItem != null)
				App.MainFrame.Navigate(new AdminAddEditUsersPage((Users)DGUsers.SelectedItem));
			else
				MessageBox.Show("Выберите пользователя");
		}

		private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TBoxSearch.Text.Length == 0)
				UpdateGrid();
			else
				DGUsers.ItemsSource = App._context.Users.Where(u => u.Login == TBoxSearch.Text 
				|| u.Password == TBoxSearch.Text
				|| u.SecondName.Contains(TBoxSearch.Text) || u.SurName.Contains(TBoxSearch.Text) 
				|| u.FirstName.Contains(TBoxSearch.Text)).ToList();
		}
	}
}
