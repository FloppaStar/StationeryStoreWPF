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
    /// Логика взаимодействия для UserAddEditCardPage.xaml
    /// </summary>
    public partial class UserAddEditCardPage : Page
    {
        Cards card = new Cards();
        private Users currentUser;
        public List<Banks> BanksList { get; set; }
        public UserAddEditCardPage(Cards cardData, Users userData)
        {
            InitializeComponent();
            BanksList = App._context.Banks.ToList();
            DataContext = this;
            if (cardData != null)
                card = cardData;
            DataContext = card;
            currentUser = userData;
            CBoxBank.ItemsSource = App._context.Banks.ToList();
            TBoxCardNumber.Focus();
            if (card.ID == 0)
            {
                LAddEdit.Content = "Добавление карты";
                BAddEdit.Content = "Добавить";
            }
            else
            {
                LAddEdit.Content = "Редактирование карты";
                BAddEdit.Content = "Редактировать";
            }
        }

        private void TBoxCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TBoxIssueDate.Focus();
            }
        }

        private void TBoxIssueDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TBoxCVV.Focus();
            }
        }

        private void TBoxCVV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CBoxBank.Focus();
            }
        }

        private void CBoxBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BAddEdit_Click(sender, e);
            }
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.GoBack();
            App.MainFrame.RemoveBackEntry();
        }

        private void BAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBoxCardNumber.Text) &&
                !string.IsNullOrEmpty(TBoxIssueDate.Text) && 
                !string.IsNullOrEmpty(TBoxCVV.Text) && 
                !string.IsNullOrEmpty(CBoxBank.Text))
            {
                 if (card.ID == 0)
                 {
                        card.CardNumber = Convert.ToInt64(TBoxCardNumber.Text);
                        card.IssueDate = TBoxIssueDate.Text;
                        card.CVV = int.Parse(TBoxCVV.Text);
                        card.IDBank = (int)CBoxBank.SelectedValue;
                        card.IDUser = currentUser.ID;
                        App._context.Cards.Add(card);
                 }
                 App._context.SaveChanges();
                 App.MainFrame.GoBack();
                 App.MainFrame.RemoveBackEntry();
            }
            else
                MessageBox.Show("Введите данные");
        }
    }
}
