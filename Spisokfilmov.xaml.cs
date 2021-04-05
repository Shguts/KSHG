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
                var spisfilms = db.Films;
                foreach (Films f in spisfilms)
                {
                    SPISOKF.Items.Add(f);
                }
            }
        }

    }
}
