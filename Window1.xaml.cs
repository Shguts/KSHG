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
using System.Data.SqlClient;
using System.Configuration;

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string PROV1 = Name.Text.Trim();
            string PROV2 = Last_name.Text.Trim();
            string PROV3 = Second_name.Text.Trim();
            string PROV4 = Date_of_birth.Text.Trim();
            string PROV5 = Login.Text.Trim();
            string PROV6 = Password.Password.Trim();
            using (kursRabEntities db = new kursRabEntities())
            {
                DataUsers sourse1 = new DataUsers();
                sourse1.LoginUs = PROV5;
                sourse1.PasswordUs = PROV6;
                db.DataUsers.Add(sourse1);
                db.SaveChanges();

            }
            MessageBox.Show("NICE");
            Close();
        }
    }
}
