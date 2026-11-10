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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginHost_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage(null, true));
        }

        private async void LoginALT_Click(object sender, RoutedEventArgs e)
        {
            string login = TBlogin.Text;
            string password = TBpassword.Text;
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("ошибка ввода данных");
                return;
            }
            User user = Biglov41SizeEntities.GetContext().User.ToList().Find(x => x.UserLogin == login && x.UserPassword == password);
            if(user != null)
            {
                TBlogin.Text = "";
                TBpassword.Text = "";
                Manager.MainFrame.Navigate(new ProductPage(user, false));
            }
            else
            {
                MessageBox.Show("Неверные данные, пройдите капчу!");
                Manager.MainFrame.Navigate(new CaptchaPage());
            }
        }
    }
}
