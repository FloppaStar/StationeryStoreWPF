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
	/// Логика взаимодействия для UserCartPage.xaml
	/// </summary>
	public partial class UserCartPage : Page
	{
		private Users currentUser;
		private Cart currentCart;
        public List<Cards> UserCardsList { get; set; }
        public UserCartPage(Users userData)
		{
			InitializeComponent();
            UserCardsList = App._context.Cards.ToList();
            DataContext = this;
            currentUser = userData;
			LoadCart();
		}
		private void LoadCart()
		{
			currentCart = App._context.Cart.FirstOrDefault(c => c.IDUser == currentUser.ID && c.PurchaseDate == null);
			UpdateCart();
		}
		private void UpdateCart()
		{
			var goodsInCart = App._context.GoodsInCart.Where(g => g.IDCart == currentCart.ID).
				GroupBy(g => g.Stationery).
				Select(g => new{Stationary = g.Key, Quantity = g.Count(),TotalPrice = g.Key.Price * g.Count()}).ToList();

			LPrice.Content = $"К оплате: {currentCart.Price} руб.";
			
			LVGoodsInCart.ItemsSource = goodsInCart;

			if (goodsInCart.Any())
			{
				CartPanel.Visibility = Visibility.Visible;
				BBuy.Visibility = Visibility.Visible;
				EmptyCartPanel.Visibility = Visibility.Collapsed;
			}
			else
			{
				CartPanel.Visibility = Visibility.Collapsed;
				BBuy.Visibility= Visibility.Collapsed;
				EmptyCartPanel.Visibility = Visibility.Visible;
			}
		}
		private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button != null)
			{
                int goodsId = (int)button.Tag;
				var goodsInCart = new GoodsInCart { IDCart = currentCart.ID, IDGood = goodsId };
				currentCart.Price += App._context.Stationery.FirstOrDefault(p => p.ID == goodsId).Price;
				App._context.GoodsInCart.Add(goodsInCart);
				App._context.SaveChanges();
				UpdateCart();
			}
		}
		private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button != null)
			{
				int goodsId = (int)button.Tag;
				var goodsInCart = App._context.GoodsInCart.FirstOrDefault(g => g.IDCart == currentCart.ID && g.IDGood == goodsId);
				if (goodsInCart != null)
				{
                    currentCart.Price -= App._context.Stationery.FirstOrDefault(p => p.ID == goodsId).Price;
                    App._context.GoodsInCart.Remove(goodsInCart);
					App._context.SaveChanges();
					UpdateCart();
				}
			}
		}

        private void BtPay_Click(object sender, RoutedEventArgs e)
        {
			if (!string.IsNullOrEmpty(CBoxCard.Text))
			{
				var goodsInCart = App._context.GoodsInCart.Where(g => g.IDCart == currentCart.ID);
				foreach (var item in goodsInCart)
				{
					var stationaryItem = App._context.Stationery.FirstOrDefault(s => s.ID == item.IDGood);
					if (stationaryItem != null)
					{
						stationaryItem.Count--;
					}
				}
				currentCart.PurchaseDate = DateTime.Now;
				App._context.SaveChanges();
				MessageBox.Show("Спасибо за покупку!");
				Cart newCart = new Cart
				{
					IDUser = currentUser.ID,
					Price = 0,
					PurchaseDate = null
				};
				App._context.Cart.Add(newCart);
				App._context.SaveChanges();
                App.MainFrame.GoBack();
                App.MainFrame.RemoveBackEntry();

            }
			else
				MessageBox.Show("Выберите карту для оплаты!");
        }
    }
}
