using MusCassa.DB;
using MusCassa.Window_;
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

namespace MusCassa.Page_
{
    /// <summary>
    /// Логика взаимодействия для smotrvistavki.xaml
    /// </summary>
    public partial class smotrvistavki : Page
    {
        Выставка выс = new Выставка();
        Посетитель пос = new Посетитель();
        public smotrvistavki(Выставка выставка, Посетитель посетитель)
        {
            InitializeComponent();
            выс = выставка;
            пос = посетитель;
            tbNazv.Text = выставка.Название;
            tbOpis.Text = выставка.Описание;
            tbCena.Text = выставка.ЦенаБилета.ToString() +" рублей";
            tbVremia.Text = выставка.ВремяНачала.ToString() + "-" + выставка.ВремяОкончания.ToString();
            try
            {
                using (var context = new MusCassaEntities())
                {
                    //ЭкспонатВыставка exvis = new ЭкспонатВыставка();
                    ЭкспонатВыставка exvis = (ЭкспонатВыставка)context.ЭкспонатВыставка.Where(a => a.Выставка.Equals(выставка.Код)).ToList();
                    var expon = context.Экспонат.Where(a => a.Код.Equals(exvis.Эксопнат)).ToList();
                    lvs.ItemsSource = expon;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void bBuy_Click(object sender, RoutedEventArgs e)
        {
            PoraPlatit poraPlatit = new PoraPlatit(пос, выс);
            poraPlatit.Show();
        }
    }
}
