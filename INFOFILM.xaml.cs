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
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                var helpforfilwithimg = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x).FirstOrDefault();
                TESTNAMEF.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.NameofFilm).FirstOrDefault();
                TESTDATEF.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                TESTRATE.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.rating).FirstOrDefault().ToString();
                ////
                int helpfortextblockG = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.IDgenre).FirstOrDefault();
                TESTgenre.Text = db.GENRES.Where(y => y.IDgenre == helpfortextblockG).Select(x => x.NameofGenre).FirstOrDefault();
                ////
                int helpfortextblockC = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.IDCountry).FirstOrDefault();
                TESTcountry.Text = db.Countries.Where(y => y.IDCountry == helpfortextblockC).Select(x => x.NameofCountry).FirstOrDefault();
                //
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(X => X).FirstOrDefault() == null)
                {
                    OCENKA.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Mark).FirstOrDefault().ToString();
                    COMOFF.Text = db.BALLS.Where(y => y.IDFilm == r && y.IDUser == r2).Select(x => x.Comment).FirstOrDefault();
                }
                var spisact = db.roleofactor.Where(y => y.IDFilm == r).Select(x => x).ToList();
                DataofRole.ItemsSource = spisact;
                //фото
                //OpenFileDialog dlg = new OpenFileDialog();
                //dlg.InitialDirectory = "";
                //dlg.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
                //if (dlg.ShowDialog() == true)
                //{
                //    string selectedFileName = dlg.FileName;
                //    helpforfilwithimg.Baner = ConvertImageToByteArray(selectedFileName);
                //}

            }

        }
        //private byte[] ConvertImageToByteArray(string fileName)
        //{
        //    MemoryStream
        //    Bitmapimage bitMap = new Bitmap(fileName);
        //    ImageFormat bmpFormat = bitMap.RawFormat;
        //    var imageToConvert = Image.FromFile(fileName);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        imageToConvert.Save(ms, bmpFormat);
        //        return ms.ToArray();
        //    }
        //}
        private void Backf(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void PUTTHEBALL(object sender, RoutedEventArgs e)
        {                   
            using (kursRabEntities db = new kursRabEntities())
            {
                Administrators ad = new Administrators();
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(X => X).FirstOrDefault() == null)
                {
                    BALLS newsetball = new BALLS();
                    string kostyl = POISK.MEGATEST;
                    int r = db.Films.Where(x => x.NameofFilm == kostyl).Select(X => X.IDFilm).FirstOrDefault();
                    int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                    BALLS PROVball = db.BALLS.Where(X => X.IDUser == r2 && X.IDFilm == r).Select(x => x).FirstOrDefault();
                    Films setrate = db.Films.Where(X => X.IDFilm == r).Select(x => x).FirstOrDefault();
                    int getidofball = Convert.ToInt32(OCENKA.Text.Trim());
                    if (PROVball == null)
                    {
                        newsetball.IDFilm = r;
                        newsetball.IDUser = r2;
                        newsetball.Mark = getidofball;
                        newsetball.Comment = COMOFF.Text.Trim();
                        db.BALLS.Add(newsetball);
                        db.SaveChanges();
                    }
                    else
                    {
                        PROVball.Mark = getidofball;
                        PROVball.Comment = COMOFF.Text.Trim();
                        db.SaveChanges();
                    }
                    double checkcountofBALLS = db.BALLS.Where(x => x.IDFilm == r).Average(p => p.Mark);
                    setrate.rating = (float)checkcountofBALLS;
                    TESTRATE.Text = checkcountofBALLS.ToString();

                    db.SaveChanges();
                }
                else MessageBox.Show("Может оценивать только Юзер");

            }
        }
    }
}
