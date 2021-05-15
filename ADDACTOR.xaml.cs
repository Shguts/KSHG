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
        //Переход к мню
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        //Удаление актера через контекстное меню
        private void deleteactor_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            { 
                    try
                    {
                        int getidActor = ((CREATORSOFFILMS)DataofRole.SelectedItem).IDCreator;
                        var addroleofac = db.CREATORSOFFILMS.Where(x=>x.IDCreator==getidActor).Select(x=>x).FirstOrDefault();
                        db.CREATORSOFFILMS.Remove(addroleofac);
                        db.SaveChanges();
                    }
                    catch { MessageBox.Show("?"); }
                var result = db.CREATORSOFFILMS.Select(x => x).ToList();
                DataofRole.ItemsSource = result;
            }

        }
        //Добавление актера который уже существует через контекстное меню
        private void addactor_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            {
                int getidActor=0;
                try
                {
                    getidActor = ((CREATORSOFFILMS)DataofRole.SelectedItem).IDCreator;
                }
                catch { }
                string getroleActor = rolefornow.Text.Trim();
                if ((getroleActor.Length > 50) || (getroleActor == ""))
                {
                    MessageBox.Show("Вы ввели некорректную роль для создателя");
                }
                else
                {
                    if (db.CREATORSOFFILMS.Where(x => x.IDCreator == getidActor).Select(y => y.DateofBirth).FirstOrDefault() > db.Films.Where(x => x.IDFilm == POISK.GenID).Select(y => y.DateofCreate).FirstOrDefault())
                    {
                        MessageBox.Show("Вы некорректно добавили существубщего актера он не может учавствовать в фильме неродившись");
                    }
                    else
                    {
                        try
                        {
                            roleofactor addroleofac = new roleofactor();
                            addroleofac.IDFilm = POISK.GenID;
                            addroleofac.IDCreator = getidActor;
                            addroleofac.RoleofActor1 = getroleActor;
                            db.roleofactor.Add(addroleofac);
                            db.SaveChanges();
                        }
                        catch { MessageBox.Show("Этот актер на этот фильм уже добавлен"); }
                    }
                }
            }
        }
        //Добавление нового актера в фильм
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            char[] helpmassive = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', '*', '"', '/', '\\', '[', ']', ':', ';', '|', '=' };
            string checkName = Name.Text.Trim();
            string checkLName = Last_name.Text.Trim();
            string checkSName = Second_name.Text.Trim();
            string checkDbirth = Date_of_birth.Text.Trim();
            string checkrole = role.Text.Trim();
            string checkDcareer = Date_of_career.Text.Trim();
            bool necro = true;
            if ((checkName != "") && (checkName.Length < 50)&&(checkLName != "") && (checkLName.Length < 50)&&(checkSName != "") && (checkSName.Length < 50))
            {
                foreach (char i in helpmassive)
                {
                    if ((checkName.Contains(i) || checkLName.Contains(i) || checkSName.Contains(i)) && (necro))
                    {
                        necro = false;
                        MessageBox.Show("Вы ввели цифры в имени,фамилии или отчестве");
                    }
                }
                try
                {
                    DateTime chekdatetime = Convert.ToDateTime(checkDbirth);
                    DateTime chekdatetimecareer = Convert.ToDateTime(checkDcareer);
                    if (chekdatetime > chekdatetimecareer) { necro = false; MessageBox.Show("Актер появился в животе(нет) Ошибка!"); }
                    using (kursRabEntities db = new kursRabEntities())
                    {
                        if (db.Films.Where(x => x.IDFilm == POISK.GenID).Select(y => y.DateofCreate).FirstOrDefault()< chekdatetime) { necro = false; MessageBox.Show("В фильме учавствует неродившийся актер так нельзя!"); }
                    }
                }
                catch
                {
                    necro = false;
                    MessageBox.Show("Вы ввели некорректно дату");

                }
                if (checkrole == "" && necro && checkLName.Length < 50)
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
                        roleofactor.IDFilm = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(X => X.IDFilm).FirstOrDefault();
                        roleofactor.IDCreator = db.CREATORSOFFILMS.OrderByDescending(X => X.IDCreator).Select(x => x.IDCreator).FirstOrDefault() + 1;
                        db.CREATORSOFFILMS.Add(addactor);
                        db.roleofactor.Add(roleofactor);
                        db.SaveChanges();
                        MessageBox.Show("Актер бы успешно добавлен");
                    }
                    catch { MessageBox.Show("Вы некорректно добавили актера"); }
                    var result = db.CREATORSOFFILMS.Select(x => x).ToList();
                    DataofRole.ItemsSource = result;
                }
            }

        }
    }
}
