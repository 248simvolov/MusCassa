using MusCassa.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для PolzReg.xaml
    /// </summary>
    public partial class PolzReg : Page
    {
        public PolzReg()
        {
            InitializeComponent();
        }
        string code;
        string Pochta;
        string Login;
        string passwrd;
        public void pochta()
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.outlook.com";
                //указание данных почтового ящика
                //smtp.Credentials = new NetworkCredential("gai.museum@outlook.com", "80$$OfGym");
                smtp.Credentials = new NetworkCredential("typo.testovaiapochta@outlook.com", "ZbcP@r0l");
                smtp.EnableSsl = true;
                //что куда отправляется
                //MailMessage m = new MailMessage("gai.museum@outlook.com", tbPochta.Text);
                MailMessage m = new MailMessage("typo.testovaiapochta@outlook.com", tbPochta.Text);
                //создание тела письма
                Random rnd = new Random();
                code = rnd.Next(10000, 99999).ToString();
                string data = $"Код регистрации: {code}";
                //запись тела письма
                m.Body = data;
                //отправка
                smtp.Send(m);
                Pochta = tbPochta.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bReg_Click(object sender, RoutedEventArgs e)
        {
            registration(tbLogin.Text, tbPochta.Text, pbPasswrd.Password);
            //StringBuilder sb = new StringBuilder();
            //if (tbLogin.Text.Length == 0)
            //    sb.Append($"Введите логин\n");
            //if (tbPochta.Text.Length == 0)
            //    sb.Append($"Введите почту\n");
            //if (pbPasswrd.Password.Length == 0)
            //    sb.Append($"Введите пароль\n");
            //if (sb.Length == 0)
            //    try
            //    {
            //        using (MusCassaEntities context = new MusCassaEntities())
            //        {
            //            if (context.Посетитель.Where(a => a.Email.Equals(tbPochta.Text)).Count() == 0)
            //            {
            //                if (context.Посетитель.Where(a => a.Логин.Equals(tbLogin.Text)).Count() == 0)
            //                {
            //                    Login = tbLogin.Text;
            //                    passwrd = pbPasswrd.Password;
            //                    pochta();
            //                    tCode.Visibility = Visibility.Visible;
            //                    tbCode.Visibility = Visibility.Visible;
            //                    bAccept.Visibility = Visibility.Visible;
            //                }
            //                else
            //                    MessageBox.Show("Пользователь с таким логином уже существует");
            //            }
            //            else
            //                MessageBox.Show("Эта почта уже используется");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //else
            //    MessageBox.Show(sb.ToString());
        }

        public bool registration(string login, string email, string password)
        {

            StringBuilder sb = new StringBuilder();
            if (login.Length == 0)
                sb.Append($"Введите логин\n");
            if (email.Length == 0)
                sb.Append($"Введите почту\n");
            if (password.Length == 0)
                sb.Append($"Введите пароль\n");
            if (sb.Length == 0)
                try
                {
                    using (MusCassaEntities context = new MusCassaEntities())
                    {
                        if (context.Посетитель.Where(a => a.Email.Equals(email)).Count() == 0)
                        {
                            if (context.Посетитель.Where(a => a.Логин.Equals(login)).Count() == 0)
                            {

                                Login = login;
                                passwrd = password;
                                Pochta = email;
                                pochta();
                                tCode.Visibility = Visibility.Visible;
                                tbCode.Visibility = Visibility.Visible;
                                bAccept.Visibility = Visibility.Visible;
                            }
                            else
                                MessageBox.Show("Пользователь с таким логином уже существует");
                        }
                        else
                            MessageBox.Show("Эта почта уже используется");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            else
                MessageBox.Show(sb.ToString());
            return true;
        }



        private void bAccept_Click(object sender, RoutedEventArgs e)
        {
            if(code == tbCode.Text)
            {
                try
                {
                    using (MusCassaEntities context = new MusCassaEntities())
                    {
                        Посетитель посетитель = new Посетитель() { Email=Pochta, Логин=Login, Пароль=passwrd};
                        context.Посетитель.Add(посетитель);
                        context.SaveChanges();
                        MessageBox.Show($"Пользователь с\nлогином: {посетитель.Логин}\nпочтой: {посетитель.Email}\nпаролем: {посетитель.Пароль}\nУспешно создан");
                        this.NavigationService.Content = new login();
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Коды не совпадают");
            }
        }
    }

}
