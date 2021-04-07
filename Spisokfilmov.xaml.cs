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
    /// Логика взаимодействия для Spisokfilmov.xaml
    /// </summary>
    public partial class Spisokfilmov : Page
    {
        public Spisokfilmov()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {

                var spisfilms = db.Films.Select(x => x).ToList<Films>();
                SPISOKF.ItemsSource = spisfilms;
            }
            using (kursRabEntities db = new kursRabEntities())
            {
                var COMBOSPIS = db.GENRES.Select(x => x).ToList<GENRES>();
                foreach (GENRES gen in COMBOSPIS)
                {
                    COMBOBOXGENRE.Items.Add(gen);
                }
            }
            using (kursRabEntities db = new kursRabEntities())
            {
                var COMBOSPIS1 = db.Countries.Select(x => x).ToList();
                foreach (Countries sen in COMBOSPIS1)
                {
                    COMBOBOXCOUNTRY.Items.Add(sen);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void Dobavlfilma(object sender, RoutedEventArgs e)
        {
            string PROV1 = Name_of_Film.Text.Trim();
            string PROV2 = DATEOC.Text.Trim();
            string PROV3 = COMBOBOXGENRE.Text.Trim();
            string PROV4 = COMBOBOXCOUNTRY.Text.Trim();
            string PROV5 = AGE.Text.Trim();
            using (kursRabEntities DB = new kursRabEntities())
            {
                try
                {
                    Films MDFilm = new Films();
                    int getСid = DB.Countries.Where(y => y.NameofCountry == PROV4).Select(x => x.IDCountry).FirstOrDefault();
                    MDFilm.IDCountry = getСid;
                    int getGid = DB.GENRES.Where(y => y.NameofGenre == PROV3).Select(x => x.IDgenre).FirstOrDefault();
                    MDFilm.IDgenre = getGid;
                    MDFilm.IDFilm = DB.Films.OrderByDescending(X => X.IDFilm).Select(x => x.IDFilm).FirstOrDefault() + 1;
                    MDFilm.AgeRestriction = Convert.ToInt32(PROV5);
                    MDFilm.NameofFilm = PROV1;
                    MDFilm.rating = 0;
                    MDFilm.DateofCreate = Convert.ToDateTime(PROV2);
                    DB.Films.Add(MDFilm);
                    DB.SaveChanges();
                    MessageBox.Show("ФИЛЬМ БЫЛ УСПЕШНО ДОБАВЛЕН");
                    var spisfilms = DB.Films.Select(x => x).ToList();
                    SPISOKF.ItemsSource = spisfilms;

                }
                catch
                {
                    MessageBox.Show("Вы ввели некорректные данные");
                }
                //MDFilm.IDCountry = DB.Films.Select(X => X.IDCountry).Where(x=>x.).FirstOrDefault(); 
            }
            //string PROV5 = Login.Text.Trim();
            //string PROV6 = Password.Password.Trim();
        }
    }
}
