using System;
using HtmlAgilityPack;

namespace ArmaComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\schwe\Desktop\Arma 3 Mod Preset ww2.html";
            var doc = new HtmlDocument();
            doc.Load(path);

            var nodes = doc.DocumentNode.SelectNodes("//body//table//a");

            foreach (HtmlNode node in nodes)
            {

                Console.WriteLine(node.InnerText);
                
            }

            Console.ReadKey(true);
        }
    }
}
