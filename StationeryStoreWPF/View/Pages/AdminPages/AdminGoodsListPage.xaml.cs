﻿using StationeryStoreWPF.DataBase;
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
	/// Логика взаимодействия для AdminGoodsListPage.xaml
	/// </summary>
	public partial class AdminGoodsListPage : Page
	{
		public AdminGoodsListPage()
		{
			InitializeComponent();
			UpdateGrid();
		}
		private void UpdateGrid()
		{
			LVGoods.ItemsSource = App._context.Stationery.ToList();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateGrid();
		}

		private void BtEdit_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button != null)
			{
				Stationery selectedStationery = button.DataContext as Stationery;
				if (selectedStationery != null)
				{
					App.MainFrame.Navigate(new AdminAddEditGoodsPage(selectedStationery));
				}
			}
		}

		private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TBoxSearch.Text.Length == 0)
				UpdateGrid();
			else
				LVGoods.ItemsSource = App._context.Stationery.Where(g => g.Name.Contains(TBoxSearch.Text)).ToList();
		}

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new AdminAddEditGoodsPage(null));
        }
    }
}
