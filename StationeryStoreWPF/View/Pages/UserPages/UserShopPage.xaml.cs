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

namespace StationeryStoreWPF.View.Pages.UserPages
{
	/// <summary>
	/// Логика взаимодействия для UserShopPage.xaml
	/// </summary>
	public partial class UserShopPage : Page
	{
		private Users currentUser;
		private Cart currentCart;
		public UserShopPage(Users userData)
		{
			InitializeComponent();
			currentUser = userData;
			InitializeCart();
			UpdateGrid();
		}

		private void InitializeCart()
		{
			currentCart = App._context.Cart.FirstOrDefault(c => c.IDUser == currentUser.ID && c.PurchaseDate == null);
			if (currentCart == null)
			{
				currentCart = new Cart { IDUser = currentUser.ID, PurchaseDate = null, Price = 0 };
				App._context.Cart.Add(currentCart);
				App._context.SaveChanges();
			}
		}
		private void UpdateGrid()
		{
			LVGoods.ItemsSource = App._context.Stationery.ToList();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateGrid();
		}

		private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TBoxSearch.Text.Length == 0)
				UpdateGrid();
			else
				LVGoods.ItemsSource = App._context.Stationery.Where(g => g.Name.Contains(TBoxSearch.Text)).ToList();
		}

		private void BtAddCart_Click(object sender, RoutedEventArgs e)
		{
			Button addButton = sender as Button;
			if (addButton != null)
			{
				Stationery selectedGood = addButton.DataContext as Stationery;
				if (selectedGood != null)
				{
							GoodsInCart newGoodsInCart = new GoodsInCart
							{
								IDCart = currentCart.ID,
								IDGood = selectedGood.ID
							};
							App._context.GoodsInCart.Add(newGoodsInCart);
						App._context.SaveChanges();
						currentCart.Price += selectedGood.Price;
						App._context.SaveChanges();
					addButton.Content = "Добавлено в корзину";
					addButton.IsEnabled = false;
				}
			}
		}
	}
}
