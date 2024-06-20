using MusCassa.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Spire.Barcode;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net;

namespace MusCassa.Window_
{
    /// <summary>
    /// Логика взаимодействия для PoraPlatit.xaml
    /// </summary>
    public partial class PoraPlatit : Window
    {
        public PoraPlatit(Посетитель посетитель, Выставка выставка)
        {
            try
            {
                InitializeComponent();
                frfr.Content = new Page_.Zaplata(посетитель, выставка);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
