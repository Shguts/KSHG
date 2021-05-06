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
    /// Логика взаимодействия для ADDACTOR.xaml
    /// </summary>
    public partial class ADDACTOR : Page
    {
        public ADDACTOR()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                var result = db.CREATORSOFFILMS.Select(x => x).ToList();
                DataofRole.ItemsSource = result;
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        private void deleteactor_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            {
                int getidActor = ((CREATORSOFFILMS)DataofRole.SelectedItem).IDCreator;
                    try
                    {
                        var addroleofac = db.CREATORSOFFILMS.Where(x=>x.IDCreator==getidActor).Select(x=>x).FirstOrDefault();
                        db.CREATORSOFFILMS.Remove(addroleofac);
                        db.SaveChanges();
                    }
                    catch { MessageBox.Show("?"); }
            }
        }

        private void addactor_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            {
                    int getidActor = ((CREATORSOFFILMS)DataofRole.SelectedItem).IDCreator;
                    string getroleActor = rolefornow.Text.Trim();
                if ((getroleActor.Length > 50) || (getroleActor == ""))
                {
                    MessageBox.Show("Вы ввели некорректную роль для создателя");
                }
                else
                {
                    try
                    {
                        roleofactor addroleofac = new roleofactor();
                        addroleofac.IDFilm = POISK.MEGATEST;
                        addroleofac.IDCreator = getidActor;
                        addroleofac.RoleofActor1 = getroleActor;
                        db.roleofactor.Add(addroleofac);
                        db.SaveChanges();
                    }
                    catch { MessageBox.Show("Этот актер на этот фильм уже добавлен"); }
                }
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            char[] helpmassive = new char[11] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',' '};
            string PROV1 = Name.Text.Trim();
            string PROV2 = Last_name.Text.Trim();
            string PROV3 = Second_name.Text.Trim();
            string PROV4 = Date_of_birth.Text.Trim();
            string PROV5 = role.Text.Trim();
            string PROV6 = Date_of_career.Text.Trim();
            DateTime chekdatetime = DateTime.Now;
            DateTime chekdatetimecareer = DateTime.Now;
            bool necro = true;
            if ((PROV1 != "") && (PROV1.Length < 50)&&(PROV2 != "") && (PROV2.Length < 50)&&(PROV3 != "") && (PROV3.Length < 50))
            {
                foreach (char i in helpmassive)
                {
                    if ((PROV1.Contains(i) || PROV2.Contains(i) || PROV3.Contains(i)) && (necro))
                    {
                        necro = false;
                        MessageBox.Show("Вы ввели цифры в имени,фамилии или отчестве");
                    }
                }
                try
                {
                    chekdatetime = Convert.ToDateTime(PROV4);
                    chekdatetimecareer = Convert.ToDateTime(PROV6);
                }
                catch
                {
                    necro = false;
                    MessageBox.Show("Вы ввели некорректно дату");

                }
                if (PROV5 == "" && necro && PROV2.Length < 50)
                {
                    necro = false;
                    MessageBox.Show("Вы не выбрали жанр");
                }

            }
            else { MessageBox.Show("Вы ввели некорректное имя,фамилию или отчество"); necro = false; }
            if (necro)
            {
                using (kursRabEntities db = new kursRabEntities())
                {
                    try
                    {
                        CREATORSOFFILMS addactor = new CREATORSOFFILMS();
                        addactor.AcName = Name.Text;
                        addactor.AcLastName = Last_name.Text;
                        addactor.AcSecondName = Second_name.Text;
                        addactor.DateofBirth = Convert.ToDateTime(Date_of_birth.Text);
                        addactor.Dateofcareer = Convert.ToDateTime(Date_of_career.Text);
                        addactor.IDCreator = db.CREATORSOFFILMS.OrderByDescending(X => X.IDCreator).Select(x => x.IDCreator).FirstOrDefault() + 1;
                        roleofactor roleofactor = new roleofactor();
                        roleofactor.RoleofActor1 = role.Text;
                        roleofactor.IDFilm = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(X => X.IDFilm).FirstOrDefault();
                        roleofactor.IDCreator = db.CREATORSOFFILMS.OrderByDescending(X => X.IDCreator).Select(x => x.IDCreator).FirstOrDefault() + 1;
                        db.CREATORSOFFILMS.Add(addactor);
                        db.roleofactor.Add(roleofactor);
                        db.SaveChanges();
                        MessageBox.Show("Актер бы успешно добавлен");
                    }
                    catch { MessageBox.Show("Вы некорректно добавили актера"); }
                }
            }
        }
    }
}
