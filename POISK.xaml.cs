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
using Microsoft.TeamFoundation.Build.Controls;

namespace KSHG
{
    public partial class POISK : Page
    {

        public static int GenID;
        public int PEREDPOISK;
        public POISK()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {

                var spisfilms = db.Films.Select(x => x).ToList<Films>();
                SPISOKPOISK.ItemsSource = spisfilms;
            }
        }
        //Поиск по фильмам    
        private void Poisk_click(object sender, RoutedEventArgs e)
        {
            string PROV1 = VODPOISK.Text.Trim();
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", $"%{PROV1}%");
            using (kursRabEntities db = new kursRabEntities())
            {               
                if (PROV1 == "")
                {
                    var spisfilms = db.Films.Select(x => x).ToList<Films>();
                    SPISOKPOISK.ItemsSource = spisfilms;
                    MessageBox.Show("Вы не ввели название фильма повторите поиск");
                }
                else
                {
                    var likenamefilms = db.Database.SqlQuery<Films>("SELECT * FROM Films WHERE NameofFilm LIKE @name", param);
                    if (likenamefilms != null)
                    {
                        SPISOKPOISK.ItemsSource = likenamefilms.ToList();
                    }
                    else MessageBox.Show("Такого фильма в базе данных не существует");               
                }
            }

        }
        //Переход к ниформации о фильме
        private void Come_Click(object sender, RoutedEventArgs e)
        {
                using (kursRabEntities db = new kursRabEntities())
                {
                try
                {
                    int pid = ((Films)SPISOKPOISK.SelectedItem).IDFilm;
                    if (pid != 0)
                    {
                        MessageBox.Show(Convert.ToString(pid));
                        MessageBox.Show(((Films)SPISOKPOISK.SelectedItem).NameofFilm);
                        string PROV1 = VODPOISK.Text.Trim();
                        GenID = pid;
                        NavigationService.Navigate(new INFOFILM());
                    }
                }
                catch { }
                }
        }
        //Удаление Фильма
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (kursRabEntities db = new kursRabEntities())
                {
                    if (db.Administrators.Where(x => x.Alogin == AUTH.GenLog).Select(x => x).FirstOrDefault() != null)
                    {
                        int pid = ((Films)SPISOKPOISK.SelectedItem).IDFilm;
                        if (pid != 0)
                        {
                            MessageBox.Show(Convert.ToString(pid));
                            MessageBox.Show(((Films)SPISOKPOISK.SelectedItem).NameofFilm);
                            string PROV1 = VODPOISK.Text.Trim();
                            GenID = pid;
                            var getIDFILM = db.Films.Where(x => x.IDFilm == POISK.GenID).Select(y => y).FirstOrDefault();
                            db.Films.Remove(getIDFILM);
                            db.SaveChanges();
                            var spisfilms = db.Films.Select(x => x).ToList<Films>();
                            SPISOKPOISK.ItemsSource = spisfilms;
                        } 
                    }
                    else { MessageBox.Show("Сюда может войти только администратор"); }
                }
            }
            catch { }
        }
        //Изменение фильма 
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int pid = ((Films)SPISOKPOISK.SelectedItem).IDFilm;
                if (pid != 0)
                {
                    MessageBox.Show(Convert.ToString(pid));
                    MessageBox.Show(((Films)SPISOKPOISK.SelectedItem).NameofFilm);
                    string PROV1 = VODPOISK.Text.Trim();
                    GenID = pid;
                    using (kursRabEntities db = new kursRabEntities())
                    {
                        if (db.Administrators.Where(x => x.Alogin == AUTH.GenLog).Select(x => x).FirstOrDefault() != null)
                        {
                            NavigationService.Navigate(new CHANGEFILM());
                        }
                        else { MessageBox.Show("Сюда может войти только администратор"); }
                    }
                }
            }
            catch { }
        }
        //
        //Возвращение к меню
        private void BackPoisk(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }

}
