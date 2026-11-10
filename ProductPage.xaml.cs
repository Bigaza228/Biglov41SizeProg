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

namespace Biglov41SizeProg
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public int countProductInStock;
        public User user {  get; set; }
        public ProductPage(User user, bool isHost)
        {
            InitializeComponent();
            UpdateProductListView();
            CBoxSort.SelectedIndex = 0;
            if(isHost == false) 
            {
                this.user = user;
                TBlogin.Text = $"Вы авторизованы как {user.UserSurname} {user.UserName} {user.UserPatronymic}";
                TBrole.Text = $"Роль: {user.UserRoleName}";
            }
            else
            {
                TBlogin.Text = $"Вы авторизованы как Гость";
                TBrole.Text = $"Роль: клиент гость";
            }
        }
        public void UpdateProductListView()
        {
            var ListProduct = Biglov41SizeEntities.GetContext().Product.ToList();
            if (RButtonDown.IsChecked == true)
            {
                ListProduct = ListProduct.OrderByDescending(i => i.ProductCost).ToList();
            }
            if(RButtonUp.IsChecked == true)
            {
                ListProduct = ListProduct.OrderBy(i => i.ProductCost).ToList();
            }
            if (!string.IsNullOrWhiteSpace(TBsearchName.Text))
            {
                ListProduct = ListProduct.Where(x => x.ProductName.ToLower().Contains(TBsearchName.Text.ToLower())).ToList();
            }
            if(CBoxSort.SelectedIndex == 1)
            {
                ListProduct = ListProduct.Where(x => x.ProductDiscountAmountInt >= 0 && x.ProductDiscountAmountInt < 10).ToList();
            }
            if (CBoxSort.SelectedIndex == 2)
            {
                ListProduct = ListProduct.Where(x => x.ProductDiscountAmountInt >= 10 && x.ProductDiscountAmountInt < 15).ToList();
            }
            if (CBoxSort.SelectedIndex == 3)
            {
                ListProduct = ListProduct.Where(x => x.ProductDiscountAmountInt >= 15).ToList();
            }
            countProductInStock = ListProduct.Count();
            TBcount.Text = $"кол-во {countProductInStock} из {Biglov41SizeEntities.GetContext().Product.Count().ToString()}";

            ProductListView.ItemsSource = ListProduct;
        }

        private void RemBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Точно ли хотите удалить?", "подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;
            Product _currentProduct = (sender as Button)?.DataContext as Product;
            Biglov41SizeEntities.GetContext().Product.Remove(_currentProduct);
            try
            {
                Biglov41SizeEntities.GetContext().SaveChanges();
                MessageBox.Show("Удаление прошло успешно");
                UpdateProductListView();
            }
            catch
            {
                MessageBox.Show("Удаление не получилось", "ошибка");
            }
        }

        private void TBsearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductListView();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProductListView();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProductListView();
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProductListView();
        }
    }
}
