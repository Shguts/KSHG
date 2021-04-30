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
    /// <summary>
    /// Логика взаимодействия для POISK.xaml
    /// </summary>
    /// 
    //public class ItemModel
    //{
    //    public ItemModel()
    //    {
    //        this.EditCommand = new SimpleCommand
    //        {
    //            ExecuteDelegate = _ => MessageBox.Show("Execute"),
    //            CanExecuteDelegate = _ => this.Id == 1
    //        };
    //    }
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public ICommand EditCommand { get; set; }
    //}
    public partial class POISK : Page
    {

        public static string MEGATEST;
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
            
        private void Poisk_click(object sender, RoutedEventArgs e)
        {
            string PROV1 = VODPOISK.Text.Trim();
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
                    var spisfilms = db.Films.Where(X => X.NameofFilm == PROV1).Select(x => x).ToList<Films>();
                    if (spisfilms.Count != 0)
                    {
                        SPISOKPOISK.ItemsSource = spisfilms;
                    }
                    else MessageBox.Show("Такого фильма в базе данных не существует");
                }
            }

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            string PROV1 = VODPOISK.Text.Trim();
            MEGATEST = PROV1;
            NavigationService.Navigate(new INFOFILM());
        }
        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            string PROV1 = VODPOISK.Text.Trim();
            MEGATEST = PROV1;
            using (kursRabEntities db = new kursRabEntities())
            {
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(x => x).FirstOrDefault() != null)
                {
                    NavigationService.Navigate(new CHANGEFILM());
                }
                else { MessageBox.Show("Сюда может войти только администратор"); }
            }
        }

        private void BackPoisk(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }

}
