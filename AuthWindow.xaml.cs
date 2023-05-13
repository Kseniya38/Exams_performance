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
using System.Windows.Shapes;
using System.Data.Entity;


namespace ExamsPerformance
{
    public partial class AuthWindow : Window
    {
        public AppContext db = new AppContext();
        public AuthWindow()
        {
            InitializeComponent();
        }


        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string userLogin = LoginTextBox.Text.Trim();
            string userPassword = PasswordTextBox.Password.Trim();

            List<User> users = db.User.ToList();
            foreach (var item in users)
            {
                if (item.UserLogin == userLogin && item.UserPassword == userPassword)
                {
                    new MainWindow().Show();
                    Hide();
                    return;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    break;
                }
            }
        }
    }
}

