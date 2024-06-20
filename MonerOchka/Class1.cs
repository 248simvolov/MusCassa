using System;
using System.Threading.Tasks;
using Monero.Client;
using Monero.Client.Daemon;
using Monero.Client.Network;
using Monero.Client.Wallet;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NBitcoin.RPC;
//using NBitcoin.RPC;


namespace MonerOchka
{
    public class Monernoe
    {
        string api = "2DJMAY5-ECF4NQ2-K81AF9T-NV9VXBA";



    }

    
    public class MoneroPaymentProcessor
    {
        static void Main(string[] args)
        {
            // Настройки кошелька
            string walletPath = @"C:\Path\To\Your\Wallet\file";
            string walletPassword = "your_wallet_password";

            // Создайте новый кошелек Monero
            var wallet = new Wallet(walletPath, walletPassword);

            // Получите адрес для приема платежей
            var address = wallet.GetPrimaryAddress();

            Console.WriteLine($"Address for receiving payments: {address}");

            // Ожидайте платежа
            ReceivePayment(wallet, 10.0m); // Ожидайте платежа в размере 10 XMR
        }

        static void ReceivePayment(Wallet wallet, decimal amount)
        {
            while (true)
            {
                try
                {
                    // Получите список входящих транзакций
                    var transactions = wallet.GetIncomingTransactions();

                    // Проверьте, есть ли транзакции с суммой не менее ожидаемой
                    foreach (var transaction in transactions)
                    {
                        if (transaction.Amount >= amount)
                        {
                            Console.WriteLine($"Received payment of {transaction.Amount} XMR");
                            ProcessPayment(amount);
                            break;
                        }
                    }

                    // Ожидайте 1 секунду перед следующей проверкой
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Thread.Sleep(1000);
                }
            }
        }

        static void ProcessPayment(decimal amount)
        {
            // Здесь вы можете выполнить любые действия, связанные с обработкой платежа
            Console.WriteLine($"Payment of {amount} XMR processed successfully");
        }
    }


}
