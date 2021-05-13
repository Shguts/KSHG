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
                int r = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.IDFilm).FirstOrDefault();
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.GenLog).Select(X => X.IDUser).FirstOrDefault();
                var helpforfilwithimg = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x).FirstOrDefault();
                TESTNAMEF.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.NameofFilm).FirstOrDefault();
                TESTDATEF.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                TESTRATE.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.rating).FirstOrDefault().ToString();
                ////
                int helpfortextblockG = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.IDgenre).FirstOrDefault();
                TESTgenre.Text = db.GENRES.Where(y => y.IDgenre == helpfortextblockG).Select(x => x.NameofGenre).FirstOrDefault();
                ////
                int helpfortextblockC = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.IDCountry).FirstOrDefault();
                TESTcountry.Text = db.Countries.Where(y => y.IDCountry == helpfortextblockC).Select(x => x.NameofCountry).FirstOrDefault();
                //
                if (db.Administrators.Where(x => x.Alogin == AUTH.GenLog).Select(X => X).FirstOrDefault() == null)
                {
                    OCENKA.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Mark).FirstOrDefault().ToString();
                    COMOFF.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Comment).FirstOrDefault();
                }
                var spisact = (from actor in db.CREATORSOFFILMS
                                            join role in db.roleofactor on actor.IDCreator equals role.IDCreator
                                            where role.IDFilm == POISK.GenID
                                            select new
                                            {
                                                actor.IDCreator,
                                                actor.AcName,
                                                actor.AcLastName,
                                                actor.AcSecondName,
                                                role.RoleofActor1
                });
                DataofRole.ItemsSource = spisact.ToList();
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
        //Возвращение к меню
        private void Backf(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        //Оценить фильм пользователю
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
                if (db.Administrators.Where(x => x.Alogin == AUTH.GenLog).Select(X => X).FirstOrDefault() == null)
                {
                    BALLS newsetball = new BALLS();
                    int kostyl = POISK.GenID;
                    int r = db.Films.Where(x => x.IDFilm == kostyl).Select(X => X.IDFilm).FirstOrDefault();
                    int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.GenLog).Select(X => X.IDUser).FirstOrDefault();
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
                        try
                        {
                            db.SaveChanges();
                            TESTRATE.Text = checkcountofBALLS.ToString();
                        }
                        catch { MessageBox.Show("Вы некорректно ввели комментарий или оценку"); }
                    }
                }
                else MessageBox.Show("Может оценивать только Юзер");

            }
        }
    }
}
