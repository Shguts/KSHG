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
using System.IO;

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для Spisokfilmov.xaml
    /// </summary>
    /// 
    public partial class Spisokfilmov : Page
    {
        public static bool FILMBoolhelp;
        public static byte[] ImagetoByte;
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
        //Переход к меню
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        //Проверка на корректность добавления фильма
        public static void generalfilms(ref string PROV1, ref string PROV2, ref string PROV3, ref string PROV4, ref string PROV5, ref DateTime chekdatetime, ref bool necro)
        {
            if ((PROV1 != "") && (PROV1.Length < 50))
            {
                try
                {
                    chekdatetime = Convert.ToDateTime(PROV2);
                }
                catch
                {
                    necro = false;
                    MessageBox.Show("Вы ввели некорректно дату");

                }
                if (PROV3 == "" && necro && Spisokfilmov.FILMBoolhelp)
                {
                    necro = false;
                    MessageBox.Show("Вы не выбрали жанр");
                }
                else
                {
                    if (PROV4 == "" && Spisokfilmov.FILMBoolhelp)
                    {
                        necro = false;
                        MessageBox.Show("Вы не выбрали страну");
                    }
                    else
                    {
                        if (PROV5 == "")
                        {
                            PROV5 = "0";
                        }
                    }
                }

            }
            else { MessageBox.Show("Вы ввели некорректное название для фильма"); necro = false; }
        }
        //Добавление фильма
        private void Dobavlfilma(object sender, RoutedEventArgs e)
        {
            Spisokfilmov.FILMBoolhelp = true;
            string PROV1 = Name_of_Film.Text.Trim();
            string PROV2 = DATEOC.Text.Trim();
            string PROV3 = COMBOBOXGENRE.Text.Trim();
            string PROV4 = COMBOBOXCOUNTRY.Text.Trim();
            string PROV5 = AGE.Text.Trim();
            DateTime chekdatetime= DateTime.Now;
            bool necro = true;
            generalfilms(ref PROV1, ref PROV2, ref PROV3, ref PROV4, ref PROV5,ref chekdatetime,ref necro);
            if (necro)
            {
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
                        MDFilm.Baner = ImagetoByte;
                        DB.Films.Add(MDFilm);
                        DB.SaveChanges();
                        MessageBox.Show("ФИЛЬМ БЫЛ УСПЕШНО ДОБАВЛЕН");
                        var spisfilms = DB.Films.Select(x => x).ToList();
                        SPISOKF.ItemsSource = spisfilms;
                        try
                        {
                            Stream streamobg = new MemoryStream(MDFilm.Baner);
                            BitmapImage BitObj = new BitmapImage();
                            BitObj.BeginInit();
                            BitObj.StreamSource = streamobg;
                            BitObj.EndInit();
                            this.test123123123212.Source = BitObj;
                        } catch { MessageBox.Show("Вы не добавили картинку для фильма.Ну ладно тогда будет Леонардо Ди Каприо =)) "); }

                    }
                    catch
                    {
                        MessageBox.Show("Вы ввели некорректные данные");
                    }
                }
            }
        }
        //Загрузка фотографии
        private void Loadphoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a photo";
            ofd.Filter = "Image Files | *.BMP; *.JPG; *.PNG";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show("Выбран файл " + ofd.FileName);
            }
            try
            {
                ImageSource III1 = new BitmapImage(new Uri(ofd.FileName));
                ImagetoByte = File.ReadAllBytes(ofd.FileName);
            }
            catch
            {
                MessageBox.Show("Вы не выбрали фотографию");
            }
        }
    }
}
