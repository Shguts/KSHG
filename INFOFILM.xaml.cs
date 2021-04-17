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
                int r = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.IDFilm).FirstOrDefault();
                TESTNAMEF.Text = db.Films.Where(y=>y.NameofFilm==POISK.MEGATEST).Select(x => x.NameofFilm).FirstOrDefault();
                TESTDATEF.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                TESTRATE.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.rating).FirstOrDefault().ToString();
                var spisact = db.roleofactor.Where(y=>y.IDFilm==r).Select(x => x).ToList();
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
                string kostyl = POISK.MEGATEST;
                int r = db.Films.Where(x => x.NameofFilm == kostyl).Select(X => X.IDFilm).FirstOrDefault();
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                BALLS setball = db.BALLS.Where(X => X.IDUser == r2 && X.IDFilm == r).Select(x => x).FirstOrDefault();
                Films setrate = db.Films.Where(X => X.IDFilm == r).Select(x => x).FirstOrDefault();
                int getidofball = Convert.ToInt32(OCENKA.Text.Trim());
                setball.IDFilm = r;
                setball.IDUser = r2;
                setball.Mark = getidofball;
                setball.Comment = COMOFF.Text.Trim();
                db.BALLS.Add(setball);
                db.SaveChanges();
                double checkcountofBALLS = db.BALLS.Where(x => x.IDFilm == r).Average(p => p.Mark);
                TESTRATE.Text = getidofball.ToString();


            }
        }
    }
}
