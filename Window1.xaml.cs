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
            bool necro = true;
            using (kursRabEntities db = new kursRabEntities())
            {
                Users sourse2 = new Users();
                if (PROV1 != "")
                {
                    if (PROV5 != "")
                    {
                        if (PROV3 != "")
                        {
                            try
                            {
                                sourse2.DateofBirth = Convert.ToDateTime(PROV4);
                            }
                            catch
                            {
                                MessageBox.Show("Вы не верно ввели дату");
                                necro = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы не ввели Отчество");
                            necro = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не ввели фамилию");
                        necro = false;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не ввели имя");
                    necro = false;
                }
                if (necro)
                {
                    DataUsers sourse1 = new DataUsers();
                    if (PROV5==null)
                    {
                        MessageBox.Show("Вы не ввели логин");
                        necro = false;
                    }
                    else
                    {
                        var proverkaloginov = db.DataUsers;
                        foreach (DataUsers d in proverkaloginov)
                        {
                            if (PROV5==d.LoginUs)
                            {
                                necro = false;
                                MessageBox.Show("Такой логин уже существует");
                            }
                        }
                        if ((PROV6 == null)&&necro)
                        {
                            MessageBox.Show("Вы не ввели пароль");
                            necro = false;
                        }
                        else
                        {
                            if (necro)
                            {
                                sourse1.LoginUs = PROV5;
                                sourse1.PasswordUs = PROV6;
                                int TEST1 = db.Users.OrderByDescending(x => x.IDUser).Select(X => X.IDUser).FirstOrDefault();
                                sourse1.IDUser = TEST1;
                                sourse2.UName = PROV1;
                                sourse2.USecondName = PROV3;
                                sourse2.ULastName = PROV2;
                                db.Users.Add(sourse2);
                                db.DataUsers.Add(sourse1);
                                db.SaveChanges();
                            }
                            else MessageBox.Show("Вы ввели некорректные данные");
                        }
                    }
                }          
            }
            if (necro)
            {
                MessageBox.Show("NICE");
            } else { MessageBox.Show("so bad"); }
            Close();
                         
    }
    }
}