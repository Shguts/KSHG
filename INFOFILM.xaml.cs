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
    /// Логика взаимодействия для INFOFILM.xaml
    /// </summary>
    public partial class INFOFILM : Page
    {
        public INFOFILM()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                TESTNAMEF.Text = db.Films.Select(x => x.NameofFilm).FirstOrDefault();
                TESTDATEF.Text = db.Films.Select(x => x.DateofCreate).FirstOrDefault().ToString();
                TESTRATE.Text = db.Films.Select(x => x.rating).FirstOrDefault().ToString();
                var spisact = db.roleofactor.Select(x => x).ToList();
                DataofRole.ItemsSource = spisact;
                var spisdataact= db.CREATORSOFFILMS.Select(x => x).ToList();
                DataofRole.ItemsSource = spisdataact;
            }

        }

        private void Backf(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void PUTTHEBALL(object sender, RoutedEventArgs e)
        {

            using (kursRabEntities db = new kursRabEntities())
            {
                //int r = db.Films.Where(x=>x.NameofFilm==);
                int r = 0;
                int r2 = 0;
                BALLS setball = db.BALLS.Where(X => X.IDUser == r && X.IDFilm == r2).Select(x => x).FirstOrDefault();
                Films setrate = db.Films.Where(X => X.IDFilm == r2).Select(x => x).FirstOrDefault();
                double checkcountofBALLS = db.BALLS.Where(x => x.IDFilm == r2).Average(p=>p.Mark);
                int getidofball = Convert.ToInt32(OCENKA.Text.Trim());
                setball.IDFilm = r2;
                setball.IDUser = r;
                setball.Mark = getidofball;
                setball.Comment = COMOFF.Text.Trim();

                db.BALLS.Add(setball);
                db.SaveChanges();
            }
        }
    }
}
