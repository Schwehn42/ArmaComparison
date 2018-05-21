using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ArmaComparison
{
    class ArmaModFile
    {

        private string path { get; }
        private List<string> idList { get; }

        public ArmaModFile(string path)
        {
            this.path = path;
        }


        static void Main(string[] args)
        {
            string pathFile1 = @"C:\Users\schwe\Desktop\Arma 3 Mod Preset ww2.html";
            var doc = new HtmlDocument();
            doc.Load(pathFile1);

            var nodes = doc.DocumentNode.SelectNodes("//body//table//a");

            foreach (HtmlNode node in nodes)
            {

                Console.WriteLine(node.InnerText.Split("id=")[1]);
                
            }

            Console.ReadKey(true);
        }
    }
}
