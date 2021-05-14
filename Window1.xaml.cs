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
using System.Text.RegularExpressions;

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    ///
    public partial class Window1 : Window
    {
        public static bool GENeralBool;
        //Инициализация компонентов
        public Window1()
        {
            InitializeComponent();
            GENeralBool = true;
        }

        //Функция которая проверяет на корректность при регистрации пользователей
        public static void generalrule (ref string PROV1, ref string PROV2, ref string PROV3, ref string PROV4, ref string PROV5, ref string PROV6,ref bool necro)
        {
            try
            {
                char[] helpmassive = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', '*', '"', '/', '\\', '[', ']', ':', ';', '|', '=' };
                DateTime chekdatetime;
                using (kursRabEntities db = new kursRabEntities())
                {
                    if ((PROV1 != "") && (PROV1.Length < 50))
                    {
                        foreach (char i in helpmassive)
                        {
                            if ((PROV1.Contains(i) || PROV2.Contains(i) || PROV3.Contains(i) || PROV5.Contains(i)) && (necro))
                            {
                                necro = false;
                                MessageBox.Show("Вы ввели цифры в имени,фамилии или отчестве");
                            }
                        }
                        if (!Regex.IsMatch(PROV5, @"^[\da-z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
                        {
                            necro = false;
                            MessageBox.Show("Можно вводить только символы латинницы");
                        }
                        if ((PROV2 != "") && (PROV2.Length < 50))
                        {

                            if ((PROV3 != "") && (PROV3.Length < 50))
                            {
                                try
                                {
                                    chekdatetime = Convert.ToDateTime(PROV4);
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
                        MessageBox.Show("Вы не ввели имя ");
                        necro = false;
                    }
                    if (necro)
                    {
                        if (PROV5 == null)
                        {
                            MessageBox.Show("Вы не ввели логин");
                            necro = false;
                        }
                        else
                        {

                            if ((PROV5.Length < 5) && necro)
                            {
                                MessageBox.Show("Вы ввели логин меньше 5 символов");
                                necro = false;
                            }
                            if ((PROV5.Length > 50) && necro)
                            {
                                MessageBox.Show("Вы ввели слишком длинный логин");
                                necro = false;
                            }
                            if (necro && Window1.GENeralBool)
                            {
                                var proverkaloginov = db.DataUsers;
                                var proverkaloginov1 = db.Administrators;
                                foreach (DataUsers d in proverkaloginov)
                                {
                                    if (PROV5 == d.LoginUs)
                                    {
                                        necro = false;
                                        MessageBox.Show("Такой логин уже существует");
                                    }
                                }
                                foreach (Administrators d in proverkaloginov1)
                                {
                                    if (PROV5 == d.Alogin)
                                    {
                                        necro = false;
                                        MessageBox.Show("Такой логин уже существует");
                                    }
                                }
                            }
                            if ((PROV6 != null) && necro)
                            {
                                if (PROV6.Length < 5)
                                {
                                    MessageBox.Show("Вы ввели пароль меньше 5 символов");
                                    necro = false;
                                }
                                if (PROV6.Length > 50)
                                {
                                    MessageBox.Show("Вы ввели слишком длинный пароль");
                                    necro = false;
                                }
                                if (PROV6.Contains(" ") || PROV5.Contains(" "))
                                {
                                    MessageBox.Show("Нельзя вводить пробелы в логине или паролле");
                                    necro = false;
                                }
                            }
                            else { MessageBox.Show("Вы не ввели пароль"); necro = false; }
                        }
                    }
                }
            }
            catch { MessageBox.Show("Проверьте подключение к серверу"); necro = false; }
        }

        //Функция регистрации пользователя 
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            char[] helpmassive = new char[10] { '1','2','3','4','5','6','7','8','9','0'}; 
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
                DataUsers sourse1 = new DataUsers();
                generalrule(ref PROV1, ref PROV2, ref PROV3, ref PROV4, ref PROV5, ref PROV6,ref necro);
                if (necro)
                {
                    try
                    {
                        sourse2.UName = PROV1;
                        sourse2.USecondName = PROV3;
                        sourse2.ULastName = PROV2;
                        sourse2.DateofBirth = Convert.ToDateTime(PROV4);
                        db.Users.Add(sourse2);
                        db.SaveChanges();
                        sourse1.LoginUs = PROV5;
                        sourse1.PasswordUs = PROV6;
                        int TEST1 = db.Users.OrderByDescending(x => x.IDUser).Select(X => X.IDUser).FirstOrDefault();
                        sourse1.IDUser = TEST1;
                        db.DataUsers.Add(sourse1);
                        db.SaveChanges();
                    }
                    catch { MessageBox.Show("Вы ввели некорректные данные(Логин)"); necro = false; }
                }
                else MessageBox.Show("Вы ввели некорректные данные");
            }
            if (necro)
            {
                MessageBox.Show("NICE");
                Close();
            } else { MessageBox.Show("so bad"); }                      
        }
    }
}