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
	/// Логика взаимодействия для AdminGoodsTypeListPage.xaml
	/// </summary>
	public partial class AdminGoodsTypeListPage : Page
	{
		public AdminGoodsTypeListPage()
		{
			InitializeComponent();
			UpdateGrid();
		}

		private void UpdateGrid()
		{
			DGGoodsType.ItemsSource = App._context.GoodsType.ToList();
		}
		private void BtAdd_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.Navigate(new AdminAddEditGoodsTypePage(null));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateGrid();
		}

		private void BtEdit_Click(object sender, RoutedEventArgs e)
		{
			if (DGGoodsType.SelectedItem != null)
				App.MainFrame.Navigate(new AdminAddEditGoodsTypePage((GoodsType)DGGoodsType.SelectedItem));
			else
				MessageBox.Show("Выберите тип товара");
		}

		private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TBoxSearch.Text.Length == 0)
				UpdateGrid();
			else
				DGGoodsType.ItemsSource = App._context.GoodsType.Where(g => g.Name.Contains(TBoxSearch.Text)).ToList();
		}
	}
}
