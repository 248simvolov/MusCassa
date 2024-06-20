using MusCassa.DB;
using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;
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
using System.Net.Mime;

namespace MusCassa.Page_
{
    /// <summary>
    /// Логика взаимодействия для Kyplinovoe.xaml
    /// </summary>
    public partial class Kyplinovoe : Page
    {
        Посетитель пос = new Посетитель();
        public Kyplinovoe(List<Билет> билетs,Посетитель посетитель)
        {
            InitializeComponent();
            пос = посетитель;
            using(var context = new MusCassaEntities()) 
            {
                var выс = context.Выставка.ToList();
                var пос = context.Посетитель.ToList();

                List<Билет> билетсов = new List<Билет>();
                for(int i = 0; i < билетs.Count; i++) 
                {
                    Билет билетс = context.Билет.Find(билетs[i].КодБилета);
                    билетсов.Add(билетс);
                    pochta(context.Выставка.Find(билетс.Выставка).Название.ToString(), context.Выставка.Find(билетс.Выставка).Описание.ToString(), context.Выставка.Find(билетс.Выставка).ВремяНачала.ToString() + " - " + context.Выставка.Find(билетс.Выставка).ВремяОкончания.ToString(), билетс) ;
                    
                }
                dgrid.ItemsSource = билетсов;
                
            }
        }

        public void pochta(string название, string описание, string времяпроведение, Билет билет)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.outlook.com";
                //указание данных почтового ящика
                //smtp.Credentials = new NetworkCredential("gai.museum@outlook.com", "80$$OfGym");
                smtp.Credentials = new NetworkCredential("typo.testovaiapochta@outlook.com", "ZbcP@r0l");
                smtp.EnableSsl = true;
                string msg = $@"Название выставки: {название}<br>Описание: {описание}<br>Время проведения: {времяпроведение}<br> <img src=""cid:uniqueId"" width=""200"" height=""200"" >";
                AlternateView html_view = AlternateView.CreateAlternateViewFromString(msg, null, "text/html");
                BarcodeSettings settings = new BarcodeSettings();
                settings.Type = BarCodeType.QRCode;
                settings.Data = $"Ticket={билет.КодБилета}Code={билет.УникальныйКод}";
                settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
                settings.X = 1.0f;
                settings.QRCodeECL = QRCodeECL.H;
                BarCodeGenerator generator = new BarCodeGenerator(settings);
                System.Drawing.Image image = generator.GenerateImage();
                image.Save("QRCode2.png");
                AlternateView jpeg_view = new AlternateView("QRCode2.png");
                jpeg_view.ContentId = "uniqueId";
                jpeg_view.TransferEncoding = TransferEncoding.Base64;
                //что куда отправляется
                //MailMessage m = new MailMessage("gai.museum@outlook.com", tbPochta.Text);
                MailMessage m = new MailMessage("typo.testovaiapochta@outlook.com", пос.Email);
                //создание тела письма
                m.SubjectEncoding = Encoding.GetEncoding(1251);
                m.BodyEncoding = Encoding.GetEncoding(1251);
                m.Body = msg;
                m.AlternateViews.Add(jpeg_view);
                m.AlternateViews.Add(html_view);
                //запись тела письма

                //отправка
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void bpodr_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MusCassaEntities())
            {
                var выс = context.Выставка.ToList();
                var пос = context.Посетитель.ToList();
                Билет item = (Билет)dgrid.SelectedItem;
                Билет itemthis = context.Билет.Find(item.КодБилета);
                tbname.Text = "Выставка: " + context.Выставка.Find(itemthis.Выставка).Название.ToString();
                tbopis.Text = "Описание: " + context.Выставка.Find(itemthis.Выставка).Описание.ToString();
                tbtime.Text = "Время проведения: " + context.Выставка.Find(itemthis.Выставка).ВремяНачала.ToString() + " - " + context.Выставка.Find(itemthis.Выставка).ВремяОкончания.ToString();
                tbcode.Text = "Уникальный код: " + itemthis.УникальныйКод;
                qrgenericheskoe($"Ticket={itemthis.КодБилета}Code={itemthis.УникальныйКод}");
            }
        }

        public void qrgenericheskoe(string addres)
        {
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;
            settings.Data = addres;
            settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
            settings.X = 1.0f;
            settings.QRCodeECL = QRCodeECL.H;
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            System.Drawing.Image image = generator.GenerateImage();
            image.Save("QRCode1.png");
            //qr.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(image);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri("QRCode1.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            qr.Source = bi;
        }

    }
}
