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
    /// Логика взаимодействия для CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : Page
    {
        string textCaptcha;
        public CaptchaPage()
        {
            InitializeComponent();
            textCaptcha = GenerateRandomWord();
            TBcaptcha.Text = textCaptcha;
        }

        private void BTcheck_Click(object sender, RoutedEventArgs e)
        {
            if(TBcheckCaptcha.Text == textCaptcha)
            {
                Manager.MainFrame.GoBack();
            } else
            {
                textCaptcha = GenerateRandomWord();
                TBcaptcha.Text = textCaptcha;
                TBcheckCaptcha.Text = "";
                MessageBox.Show("неправильно");
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