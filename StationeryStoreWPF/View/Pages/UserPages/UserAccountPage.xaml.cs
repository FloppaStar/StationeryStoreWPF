using StationeryStoreWPF.DataBase;
using StationeryStoreWPF.View.Pages.AdminPages;
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

namespace StationeryStoreWPF.View.Pages.UserPages
{
	/// <summary>
	/// Логика взаимодействия для UserAccountPage.xaml
	/// </summary>
	public partial class UserAccountPage : Page
	{
		Users user = new Users();
        int mode;
		public UserAccountPage(Users userData)
		{
			InitializeComponent();
            user = userData;
			DataContext = user;
            LAccount.Content = $"Аккаунт пользователя {user.Login}";
			mode = 0;
			UpdateGrid();
		}

		private void BtSave_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(PBoxOldPassword.Password))
			{
				if (PBoxOldPassword.Password == user.Password)
				{
					if (!string.IsNullOrEmpty(TBoxSecondName.Text) && !string.IsNullOrEmpty(TBoxFirstName.Text))
					{
						user.SecondName = TBoxSecondName.Text;
						user.FirstName = TBoxFirstName.Text;
						user.SurName = TBoxSurName.Text;
						if (!string.IsNullOrEmpty(PBoxNewPassword.Password))
						{
							user.Password = PBoxNewPassword.Password;
						}
						App._context.SaveChanges();
						BtEdit.Visibility = Visibility.Visible;
						BtSave.Visibility = Visibility.Collapsed;
						BtCancel.Visibility = Visibility.Collapsed;
						TBoxSecondName.IsEnabled = false;
						TBoxFirstName.IsEnabled = false;
						TBoxSurName.IsEnabled = false;
						PBoxNewPassword.Visibility = Visibility.Collapsed;
						PBoxOldPassword.Visibility = Visibility.Collapsed;
						BtSwitcher.Visibility = Visibility.Visible;
					}
					else
					{
						MessageBox.Show("Введите данные!");
					}
				}
				else
				{
					MessageBox.Show("Вы ввели неправильный пароль!");
				}
			}
			else
			{
				MessageBox.Show("Для внесения изменений необходимо ввести основной пароль!");
			}
        }

		private void BtCancel_Click(object sender, RoutedEventArgs e)
		{
			BtEdit.Visibility = Visibility.Visible;
			BtSave.Visibility = Visibility.Collapsed;
			BtCancel.Visibility = Visibility.Collapsed;
			TBoxSecondName.IsEnabled = false;
			TBoxFirstName.IsEnabled = false;
			TBoxSurName.IsEnabled = false;
			PBoxNewPassword.Visibility = Visibility.Collapsed;
			PBoxOldPassword.Visibility = Visibility.Collapsed;
			BtSwitcher.Visibility = Visibility.Visible;
		}

		private void BtEdit_Click(object sender, RoutedEventArgs e)
		{
			BtEdit.Visibility = Visibility.Collapsed;
			BtSave.Visibility = Visibility.Visible;
			BtCancel.Visibility = Visibility.Visible;
			TBoxSecondName.IsEnabled = true;
			TBoxFirstName.IsEnabled = true;
			TBoxSurName.IsEnabled = true;
			PBoxNewPassword.Visibility = Visibility.Visible;
			PBoxOldPassword.Visibility = Visibility.Visible;
			BtSwitcher.Visibility = Visibility.Collapsed;
		}

		private void BtSwitcher_Click(object sender, RoutedEventArgs e)
		{
			if (mode == 0)
			{
				SPUserData.Visibility = Visibility.Collapsed;
				SPUserCards.Visibility = Visibility.Visible;
                BtAddCard.Visibility = Visibility.Visible;
                BtSwitcher.Content = "Посмотреть данные";
				mode = 1;
			}
			else
			{
				SPUserData.Visibility = Visibility.Visible;
				SPUserCards.Visibility = Visibility.Collapsed;
                BtAddCard.Visibility = Visibility.Collapsed;
                BtSwitcher.Content = "Посмотреть банковские карты";
				mode = 0;
			}
		}
		private void UpdateGrid()
		{
			LVCards.ItemsSource = App._context.Cards.Where(c => c.IDUser == user.ID).ToList();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateGrid();
			mode = 0;
		}

        private void BtAddCard_Click(object sender, RoutedEventArgs e)
        {
			App.MainFrame.Navigate(new UserAddEditCardPage(null, user));
        }

        private void BtEditCard_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Cards selectedCard = button.DataContext as Cards;
                if (selectedCard != null)
                {
                    App.MainFrame.Navigate(new UserAddEditCardPage(selectedCard, user));
                }
            }
        }
    }
}
