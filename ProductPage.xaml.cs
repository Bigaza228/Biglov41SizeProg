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
        public ProductPage()
        {
            InitializeComponent();
            ProductListView.ItemsSource = Biglov41SizeEntities.GetContext().Product.ToList();
        }
        public void UpdateProductListView()
        {
            ProductListView.ItemsSource = Biglov41SizeEntities.GetContext().Product.ToList();
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
    }
}
