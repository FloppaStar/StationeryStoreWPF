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
	/// Логика взаимодействия для AdminAddEditGoodsPage.xaml
	/// </summary>
	public partial class AdminAddEditGoodsPage : Page
	{
		Stationery stationery = new Stationery();
		public List<GoodsType> GoodsTypeList { get; set; }
		public AdminAddEditGoodsPage(Stationery stationeryData)
		{
			InitializeComponent();
			GoodsTypeList = App._context.GoodsType.ToList();
			DataContext = this;
			if (stationeryData != null)
				stationery = stationeryData;
			DataContext = stationery;
			CBoxCategory.ItemsSource = App._context.GoodsType.ToList();
			TBoxName.Focus();
			if (stationery.ID == 0)
			{
				LAddEdit.Content = "Добавление товара";
				BAddEdit.Content = "Добавить";
			}
			else
			{
				LAddEdit.Content = "Редактирование товара";
				BAddEdit.Content = "Редактировать";
			}
		}

		private void TBoxName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				TBoxPrice.Focus();
			}
		}

		private void TBoxPrice_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				TBoxCount.Focus();
			}
		}

		private void TBoxCount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				CBoxCategory.Focus();
			}
		}

		private void CBoxCategory_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				TBoxPhotoLink.Focus();
			}
		}

		private void TBoxPhotoLink_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				BAddEdit_Click(sender, e);
			}
		}

		private void BAddEdit_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(TBoxName.Text) &&
				!string.IsNullOrEmpty(TBoxPrice.Text) &&
				!string.IsNullOrEmpty(TBoxCount.Text) &&
				!string.IsNullOrEmpty(CBoxCategory.Text) &&
				!string.IsNullOrEmpty(TBoxPhotoLink.Text))
			{
				try
				{
					if (stationery.ID == 0)
					{
						stationery.Name = TBoxName.Text;
						stationery.Price = int.Parse(TBoxPrice.Text);
						stationery.Count = int.Parse(TBoxCount.Text);
						stationery.IDGoodsType = (int)CBoxCategory.SelectedValue;
						stationery.Photo = TBoxPhotoLink.Text;
						App._context.Stationery.Add(stationery);
					}
					App._context.SaveChanges();
					App.MainFrame.GoBack();
					App.MainFrame.RemoveBackEntry();
				}
				catch (System.Data.Entity.Validation.DbEntityValidationException ex)
				{
					foreach (var validationErrors in ex.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
						}
					}
				}
			}
			else
				MessageBox.Show("Введите данные");
		}

		private void BCancel_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.GoBack();
			App.MainFrame.RemoveBackEntry();
		}
	}
}
