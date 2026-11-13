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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private readonly User _currentUser;
        private readonly List<Product> products;
        private readonly Order _currentOrder = new Order();
        public OrderPage(User user, List<Product> products)
        {
            InitializeComponent();
            _currentUser = user;
            this.products = products;
            _currentOrder.OrderClientID = _currentUser?.UserID;
            ProductListView.ItemsSource = products;
            CBPickUpPoint.ItemsSource = Biglov41SizeEntities.GetContext().PickUpPoint.ToList();
        }

        private void BTturnDown_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Items.Refresh();
        }
        private void BTadd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
