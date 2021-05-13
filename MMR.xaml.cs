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
    /// Логика взаимодействия для MMR.xaml
    /// </summary>
    public partial class MMR : Page
    {
        public MMR()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {

                var spisfilms = db.Films.OrderByDescending(y=>y.rating).Select(x => x).Take(10).ToList<Films>();
                SPISOKF.ItemsSource = spisfilms;
            }
        }
        //Возвращение к меню
        private void Backmenuforcemmr(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }
}
