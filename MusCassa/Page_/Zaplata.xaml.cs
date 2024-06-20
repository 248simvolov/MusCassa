using MusCassa.DB;
using Spire.Barcode;
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
    /// Логика взаимодействия для Zaplata.xaml
    /// </summary>
    public partial class Zaplata : Page
    {
        Выставка выс = new Выставка();
        Посетитель пос = new Посетитель();
        int count = 1;
        decimal kyrs = 0.00007090522418571836m;

        public Zaplata(Посетитель посетитель, Выставка выставка)
        {
            try
            {
                InitializeComponent();
                выс = выставка;
                пос = посетитель;
                tcena.Text = выставка.ЦенаБилета.ToString();
                tcount.Text = count.ToString();
                stoim = выставка.ЦенаБилета * count;
                tstoim.Text = stoim.ToString();
                tploti.Text = stoim * kyrs + " XMRT";
                //tploti.Text = https://labs.perplexity.ai/


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void qrgenericheskoe(string addres)
        {
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;
            settings.Data = addres;
            settings.Data2D = addres;
            settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
            settings.X = 1.0f;
            settings.QRCodeECL = QRCodeECL.H;
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            System.Drawing.Image image = generator.GenerateImage();
            image.Save("QRCode.png");
            //qr.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(image);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri("QRCode.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            qr.Source = bi;
        }
        decimal stoim = 0;


        private void bmen_Click(object sender, RoutedEventArgs e)
        {
            if (count <= 1)
                ;
            else
            {
                count--;
                tcount.Text = count.ToString();
                stoim = выс.ЦенаБилета * count;
                tstoim.Text = stoim.ToString();
                tploti.Text = stoim * kyrs + " XMRT";
            }
        }

        private void bbol_Click(object sender, RoutedEventArgs e)
        {
            if (count >= выс.МаксимумПосетителей - выс.БилетовКуплено)
                ;
            else
            {
                count++;
                tcount.Text = count.ToString();
                stoim = выс.ЦенаБилета * count;
                tstoim.Text = stoim.ToString();
                tploti.Text = stoim * kyrs + " XMRT";
            }
        }

        private void bkyp_Click(object sender, RoutedEventArgs e)
        {
            qrgenericheskoe($"monero:48pkRmWVhMANBhYK9QLf7te9eAGXLCYEpWqyr1ujwtwzjMFBP7Cj6jsf7JuxWA6wigDhgbaZTmaH2bBoCANDC2uRNxtB6Vf?tx_amount={stoim * kyrs}");
        }

        static Random rnd = new Random();
        static string GetRandomString(int lenght)
        {
            string Alphabet = "1234567890QWERTYUIPASDFGHJKLZXCVBNMqwertyuiopasdfghjkzxcvbnmЙЦГШЩЪФЫПЛДЖЭЯЧИБЮйцнгшщзъфыплджэячитбю";
            StringBuilder sb = new StringBuilder(lenght - 1);
            int position = 0;
            for (int i = 0; i < lenght; i++)
            {
                position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[position]);
            }
            return sb.ToString();
        }

        private void qr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Билет> блеты = new List<Билет>();
                using (var context = new MusCassaEntities())
                {
                    for (int i = 0; i < count; i++)
                    {
                        Билет блет = new Билет();
                        блет.Выставка = выс.Код;
                        блет.Посетитель = пос.КодПосетителя;
                        блет.УникальныйКод = GetRandomString(16);
                        блеты.Add(блет);
                    }
                    context.Билет.AddRange(блеты);
                    int neslishkomobebano = context.SaveChanges();
                    List<Билет> иблеты = (List<Билет>)context.Билет.OrderByDescending(eм => eм.КодБилета).Take(neslishkomobebano).ToList();
                    this.NavigationService.Content = new Page_.Kyplinovoe(иблеты, пос);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
