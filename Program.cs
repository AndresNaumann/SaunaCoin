using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using EllipticCurve;
using Newtonsoft.Json;

namespace MyFirstBlockchain
{
    internal class Program
    {
        static void Main(string[] args)
        {

            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            PrivateKey key3 = new PrivateKey();
            PublicKey wallet3 = key3.publicKey();

            Blockchain saunaCoin = new Blockchain(2, 100);

            saunaCoin.MinePendingTransactions(wallet1);
            saunaCoin.MinePendingTransactions(wallet2);
            saunaCoin.MinePendingTransactions(wallet3);

            Console.Write("\nBalance of Wallet 1: $" + saunaCoin.GetBalanceOfWallet(wallet1).ToString());
            Console.Write("\nBalance of Wallet 2: $" + saunaCoin.GetBalanceOfWallet(wallet2).ToString());
            Console.Write("\nBalance of Wallet 3: $" + saunaCoin.GetBalanceOfWallet(wallet3).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 55.00m);
            tx1.SignTransaction(key1);
            saunaCoin.addPendingTransaction(tx1);

            Transaction tx2 = new Transaction(wallet3, wallet2, 20.00m);
            tx2.SignTransaction(key3);
            saunaCoin.addPendingTransaction(tx2);

            saunaCoin.MinePendingTransactions(wallet3);

            Console.Write("\nBalance of Wallet 1: $" + saunaCoin.GetBalanceOfWallet(wallet1).ToString());
            Console.Write("\nBalance of Wallet 2: $" + saunaCoin.GetBalanceOfWallet(wallet2).ToString());
            Console.Write("\nBalance of Wallet 3: $" + saunaCoin.GetBalanceOfWallet(wallet3).ToString());

            string blockJSON = JsonConvert.SerializeObject(saunaCoin, Formatting.Indented);

            Console.WriteLine(blockJSON);

            if (saunaCoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is Valid");
            } else
            {
                Console.WriteLine("Blockchain is Not Valid");
            }
        }
    }
}