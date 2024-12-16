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
	/// Логика взаимодействия для AdminAddEditGoodsTypePage.xaml
	/// </summary>
	public partial class AdminAddEditGoodsTypePage : Page
	{
		GoodsType goodType = new GoodsType();
		public AdminAddEditGoodsTypePage(GoodsType newGoodType)
		{
			InitializeComponent();
			if (newGoodType != null)
				goodType = newGoodType;
			DataContext = goodType;
			TBoxName.Focus();
			if (goodType.ID == 0)
			{
				LAddEdit.Content = "Добавление типа товара";
				BAddEdit.Content = "Добавить";
			}
			else
			{
				LAddEdit.Content = "Редактирование типа товара";
				BAddEdit.Content = "Редактировать";
			}
		}

		private void BAddEdit_Click(object sender, RoutedEventArgs e)
		{
			if (TBoxName.Text != null)
			{
				if (goodType.ID == 0)
				{
					goodType.Name = TBoxName.Text;
					App._context.GoodsType.Add(goodType);
				}
				App._context.SaveChanges();
				App.MainFrame.GoBack();
				App.MainFrame.RemoveBackEntry();
			}
			else
				MessageBox.Show("Введите данные");
		}

		private void BCancel_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.GoBack();
			App.MainFrame.RemoveBackEntry();
		}

		private void TBoxName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				BAddEdit_Click(sender, e);
			}
		}
	}
}
