using Newtonsoft.Json;
using System;

namespace BlockchainCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain ourblockchain = new Blockchain();
            DateTime startTime = DateTime.Now;

            //ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Ferit,receiver:Deneme,amount:5}"));
            //ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Retail,receiver:Ferit,amount:10}"));
            //ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Ferit,receiver:Klio,amount:12}"));

            ourblockchain.CreateTransaction(new Transaction("Ferit", "Deneme", 15));
            ourblockchain.ProcessPendingTransactions("Miner1");

            ourblockchain.CreateTransaction(new Transaction("Deneme", "Ferit", 10));
            ourblockchain.CreateTransaction(new Transaction("Deneme", "Ferit", 8));

            ourblockchain.ProcessPendingTransactions("Miner1");

                       
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("Süre:"+(finishTime - startTime).ToString());

            Console.WriteLine("Ferit Balance:" + ourblockchain.GetBalance("Ferit").ToString());
            Console.WriteLine("Deneme Balance:" + ourblockchain.GetBalance("Deneme").ToString());
            Console.WriteLine("Miner1 Balance:" + ourblockchain.GetBalance("Miner1").ToString());


            Console.WriteLine(JsonConvert.SerializeObject(ourblockchain,Formatting.Indented));
            Console.WriteLine("gecerli mi?" + ourblockchain.IsValid().ToString());
            Console.WriteLine("Veri değiştiriliyor...");
            //ourblockchain.Chain[1].Data = "{sender:Ferit,receiver:Klio,amount:100}";
            Console.WriteLine("gecerli mi?" + ourblockchain.IsValid().ToString());
            Console.WriteLine("Hash güncelleniyor...");
            ourblockchain.Chain[1].Hash = ourblockchain.Chain[1].CalculateHash();
            Console.WriteLine("hash değiştirildi gecerli mi?" + ourblockchain.IsValid().ToString());

            ourblockchain.Chain[2].PreviousHash = ourblockchain.Chain[1].Hash;
            ourblockchain.Chain[2].Hash = ourblockchain.Chain[2].CalculateHash();

            ourblockchain.Chain[3].PreviousHash = ourblockchain.Chain[2].Hash;
            ourblockchain.Chain[3].Hash = ourblockchain.Chain[3].CalculateHash();

            Console.WriteLine("tüm zincir değiştirildi gecerli mi?" + ourblockchain.IsValid().ToString());

            Console.ReadKey();
        }
    }
}
