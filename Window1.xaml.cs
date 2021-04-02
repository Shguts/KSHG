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
                int n = 0;
                Users sourse2 = new Users();
                if (PROV1 != "")
                {
                    sourse2.UName = PROV1;
                }
                else MessageBox.Show("Вы не ввели имя");
                if (PROV5 != "")
                {
                    sourse2.ULastName = PROV2;
                }
                else MessageBox.Show("Вы не ввели фамилию");
                if (PROV3 != "")
                {
                    sourse2.USecondName = PROV3;
                }
                switch (n)
                {
                    case 1: { break; }
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: { sourse2.DateofBirth = Convert.ToDateTime(PROV4);break;};
                }
                db.Users.Add(sourse2);
                db.SaveChanges();

            }           
            using (kursRabEntities db = new kursRabEntities())
            {
                DataUsers sourse1 = new DataUsers();
                Users sourse3 = new Users();
                sourse1.LoginUs = PROV5;
                sourse1.PasswordUs = PROV6;
                var TEST1= db.DataUsers.Where(x => x.LoginUs == PROV5).Select(X => X.IDUser).FirstOrDefault(); 
                sourse1.IDUser = TEST1;
                //var TEST1 = db.DataUsers.Where(x => x.LoginUs == PROV5).Select(X => X.IDUser);
                //sourse1.IDUser = Convert.ToInt32(TEST1);
                db.Users.Add(sourse2);
                db.DataUsers.Add(sourse1);
                db.SaveChanges();

            }
            MessageBox.Show("NICE");
            Close();
        }
    }
}
