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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }
        //Переход к странице по добавлению и получению списка фильмов но в ообщем по добавлению
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            {
                if (db.Administrators.Where(x => x.Alogin == AUTH.GenLog).Select(x => x).FirstOrDefault() != null)
                {
                    NavigationService.Navigate(new Spisokfilmov());
                }
                else { MessageBox.Show("Сюда может войти только администратор"); }
            }
        }
        //Переход к странице по поиску к фильмам и также там содержатся функции удаления добавления и изменения фильма
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new POISK());
        }
        //Переход к странице профилю пользователя или администратора
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Profil());
        }
        //Переход к странице рейтинга фильмов
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MMR());
            
        }
    }
}
