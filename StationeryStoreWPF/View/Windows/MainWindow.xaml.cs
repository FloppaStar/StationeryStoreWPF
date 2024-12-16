using StationeryStoreWPF.DataBase;
using StationeryStoreWPF.View.Pages.AdminPages;
using StationeryStoreWPF.View.Pages.UserPages;
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

namespace StationeryStoreWPF.View.Windows
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Users NewUser = new Users();
		Cart currentCart;
		public MainWindow(Users user)
		{
			InitializeComponent();
			App.MainFrame = MainFrame;
			NewUser = user;
			if (NewUser.IDRole == 1)
			{
				MainFrame.Navigate(new AdminUsersListPage());
				AdminControlPanel.Visibility = Visibility.Visible;
				ImgShop.Visibility = Visibility.Collapsed;
				BtCart.Visibility = Visibility.Collapsed;
				BtUserAccount.IsEnabled = false;
			}
			else
			{
				MainFrame.Navigate(new UserShopPage(NewUser));
			}
			BtUserAccount.Content = $"{NewUser.FirstName} {NewUser.SecondName}";
			currentCart = App._context.Cart.FirstOrDefault(c => c.IDUser == NewUser.ID && c.PurchaseDate == null);
			if (currentCart == null)
			{
				currentCart = new Cart { IDUser = NewUser.ID, PurchaseDate = null, Price = 0 };
				App._context.Cart.Add(currentCart);
				App._context.SaveChanges();
			}
		}

		private void BtBack_Click(object sender, RoutedEventArgs e)
		{
			if (App.MainFrame.CanGoBack)
			{
				MainFrame.GoBack();
			}
        }

		private void BtNext_Click(object sender, RoutedEventArgs e)
		{
			if (App.MainFrame.CanGoForward)
			{
				MainFrame.GoForward();
			}
		}

		private void BtGoods_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new AdminGoodsListPage());
        }

		private void BtUsers_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new AdminUsersListPage());
		}

		private void BtGoodsTypes_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new AdminGoodsTypeListPage());
		}

		private void BtExit_Click(object sender, RoutedEventArgs e)
		{
			AdminControlPanel.Visibility = Visibility.Hidden;
			ImgShop.Visibility = Visibility.Visible;
			AuthorizationWindow authorizationWindow = new AuthorizationWindow();
			authorizationWindow.Show();
			this.Close();
        }

		private void BtUserAccount_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new UserAccountPage(NewUser));
        }

		private void ImgShop_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				MainFrame.Navigate(new UserShopPage(NewUser));
		}

		private void BtCart_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new UserCartPage(NewUser));
		}
	}
}
