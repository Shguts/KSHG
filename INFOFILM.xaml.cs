using Microsoft.Win32;
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
using System.Drawing;
using System.IO;



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
                int r = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.IDFilm).FirstOrDefault();
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                var helpforfilwithimg = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x).FirstOrDefault();
                TESTNAMEF.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.NameofFilm).FirstOrDefault();
                TESTDATEF.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                TESTRATE.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.rating).FirstOrDefault().ToString();
                ////
                int helpfortextblockG = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.IDgenre).FirstOrDefault();
                TESTgenre.Text = db.GENRES.Where(y => y.IDgenre == helpfortextblockG).Select(x => x.NameofGenre).FirstOrDefault();
                ////
                int helpfortextblockC = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.IDCountry).FirstOrDefault();
                TESTcountry.Text = db.Countries.Where(y => y.IDCountry == helpfortextblockC).Select(x => x.NameofCountry).FirstOrDefault();
                //
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(X => X).FirstOrDefault() == null)
                {
                    OCENKA.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Mark).FirstOrDefault().ToString();
                    COMOFF.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Comment).FirstOrDefault();
                }
                var spisact = db.roleofactor.Where(y => y.IDFilm == r).Select(x => x).ToList();
                DataofRole.ItemsSource = spisact;
                try
                {
                    Stream streamobg = new MemoryStream(helpforfilwithimg.Baner);
                    BitmapImage BitObj = new BitmapImage();
                    BitObj.BeginInit();
                    BitObj.StreamSource = streamobg;
                    BitObj.EndInit();
                    this.fotoImage.Source = BitObj;
                } catch { }
            }

        }
        private void Backf(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void PUTTHEBALL(object sender, RoutedEventArgs e)
        {
            string PROV1;
            string PROV2;
            PROV1 = OCENKA.Text.Trim();
            PROV2 = COMOFF.Text.Trim();
            bool necro = true;
            int getidofball = 0;
            using (kursRabEntities db = new kursRabEntities())
            {
                Administrators ad = new Administrators();
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(X => X).FirstOrDefault() == null)
                {
                    BALLS newsetball = new BALLS();
                    int kostyl = POISK.MEGATEST;
                    int r = db.Films.Where(x => x.IDFilm == kostyl).Select(X => X.IDFilm).FirstOrDefault();
                    int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                    BALLS PROVball = db.BALLS.Where(X => X.IDUser == r2 && X.IDFilm == r).Select(x => x).FirstOrDefault();
                    Films setrate = db.Films.Where(X => X.IDFilm == r).Select(x => x).FirstOrDefault();
                    try
                    {
                        getidofball = Convert.ToInt32(OCENKA.Text.Trim());
                    }
                    catch { MessageBox.Show("Вы ввели некорректно оценку "); necro = false; }
                    if (necro)
                    {
                        if (PROVball == null)
                        {
                            newsetball.IDFilm = r;
                            newsetball.IDUser = r2;
                            newsetball.Mark = getidofball;
                            newsetball.Comment = COMOFF.Text.Trim();
                            db.BALLS.Add(newsetball);
                        }
                        else
                        {
                            PROVball.Mark = getidofball;
                            PROVball.Comment = COMOFF.Text.Trim();
                        }
                        double checkcountofBALLS = db.BALLS.Where(x => x.IDFilm == r).Average(p => p.Mark);
                        setrate.rating = (float)checkcountofBALLS;
                        TESTRATE.Text = checkcountofBALLS.ToString();
                        try
                        {
                            db.SaveChanges();
                        }
                        catch { MessageBox.Show("Вы некорректно ввели комментарий или оценку"); }
                    }
                }
                else MessageBox.Show("Может оценивать только Юзер");

            }
        }
    }
}
