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

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для AUTH.xaml
    /// </summary>
    public partial class AUTH : Page
    {
        public static string GenLog;
        public AUTH()
        {
            InitializeComponent();
        }
        //Обработка кнопки выхода из приложения
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Авторизация пользователя или даминистратора в приложении
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string checklogin = LOG.Text.Trim();
            GenLog = LOG.Text;
            string checkpassw = PASSW.Password.Trim();
            Administrators admin = null;
            DataUsers user = null;
            using (kursRabEntities context = new kursRabEntities())
            {
                try
                {
                    user = context.DataUsers.Where(l => l.LoginUs == checklogin && l.PasswordUs == checkpassw).FirstOrDefault();
                    admin = context.Administrators.Where(l => l.Alogin == checklogin && l.Apassword == checkpassw).FirstOrDefault();
                }
                catch { MessageBox.Show("Проверьте подключение к серверу"); }
            }
            if ((user != null) || (admin != null))
            {
                MessageBox.Show("Вы авторизованы");
                NavigationService.Navigate(new Menu());
            }
            else
            {
                MessageBox.Show("Вы ввели некорректные данные");
            }
  

        }
        //Переход к окну регистрации
        private void BtgRegistr_Click(object sender, RoutedEventArgs e)
        {
            Window1 taskWindow = new Window1();
            taskWindow.ShowDialog();
            

        }
    }
}
