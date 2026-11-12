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
        bool openCatcha = false;
        string tempTextCaptcha;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginHost_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage(null, true));
            TblockCaptcha.Visibility = Visibility.Collapsed;
            TBcapthca.Visibility = Visibility.Collapsed;
            TblockTextCaptcha.Visibility = Visibility.Collapsed;
            TBlogin.Text = "";
            TBpassword.Text = "";
            TBcapthca.Text = "";
            openCatcha = false;
        }

        private async void LoginALT_Click(object sender, RoutedEventArgs e)
        {
            if (openCatcha)
            {
                if (string.IsNullOrEmpty(TBlogin.Text) || string.IsNullOrEmpty(TBpassword.Text))
                {
                    MessageBox.Show("ошибка ввода данных");
                    return;
                }
                if(tempTextCaptcha != TBcapthca.Text)
                {
                    MessageBox.Show("Неверные данные, пройдите капчу!");
                    LoginALT.IsEnabled = false;
                    for(int i = 10; i > 0; i--)
                    {
                        LoginALT.Content = $"Подождите {i} сек";
                        await Task.Delay(1000);
                    }
                    LoginALT.IsEnabled = true;
                    LoginALT.Content = "Войти";
                    tempTextCaptcha = GenerateRandomWord();
                    TblockTextCaptcha.Text = tempTextCaptcha;
                    return;
                }
            }
            string login = TBlogin.Text;
            string password = TBpassword.Text;
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("ошибка ввода данных");
                return;
            }
            User user = Biglov41SizeEntities.GetContext().User.ToList().Find(x => x.UserLogin == login && x.UserPassword == password);
            if (user != null)
            {
                TBlogin.Text = "";
                TBpassword.Text = "";
                openCatcha = false;
                TBcapthca.Visibility = Visibility.Collapsed;
                TblockCaptcha.Visibility = Visibility.Collapsed;
                TblockTextCaptcha.Visibility = Visibility.Collapsed;
                TBlogin.Text = "";
                TBpassword.Text = "";
                TBcapthca.Text = "";
                Manager.MainFrame.Navigate(new ProductPage(user, false));
            }
            else
            {
                MessageBox.Show("Неверные данные, пройдите капчу!");
                tempTextCaptcha = GenerateRandomWord();
                TblockTextCaptcha.Text = tempTextCaptcha;
                TblockCaptcha.Visibility = Visibility.Visible;
                TBcapthca.Visibility = Visibility.Visible;
                TblockTextCaptcha.Visibility = Visibility.Visible;
                openCatcha = true;
            }
        }
        private string GenerateRandomWord(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
