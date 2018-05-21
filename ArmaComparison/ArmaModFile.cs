using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using HtmlAgilityPack;

namespace ArmaComparison
{
    class ArmaModFile
    {

        private const string NODE_SELECTOR = "//body//table//a";

        private string path { get; }
        private readonly List<string> idList = new List<string>();

        public ArmaModFile(string path)
        {
            this.path = path;
            this.ParseIdList();
        }

        private void ParseIdList()
        {
            var doc = new HtmlDocument();
            doc.Load(this.path);

            var nodes = doc.DocumentNode.SelectNodes(NODE_SELECTOR);

            foreach (HtmlNode node in nodes)
            {
                /*
                 * example: node looks like this:  <a href="http://steamcommunity.com/sharedfiles/filedetails/?id=450814997" data-type="Link">
                   http://steamcommunity.com/sharedfiles/filedetails/?id=450814997</a>
                 * then node.InnerText == http://steamcommunity.com/sharedfiles/filedetails/?id=450814997
                 * and the split and array select results in 450814997
                 */

                string id = node.InnerText.Split("id=")[1];
                this.idList.Add(id);
            }
        }

        public void PrintIdList()
        {
            foreach (string s in this.idList)
            {
                Console.WriteLine(s);
            }
        }

        private bool hasId(string search)
        {
            return this.idList.Contains(search);
        }


        static void Main(string[] args)
        {
            ArmaModFile file1 = new ArmaModFile(@"C:\Users\schwe\Desktop\Arma 3 Mod Preset ww2.html");
            file1.PrintIdList();

            Console.WriteLine(file1.hasId("450814997"));

            Console.ReadKey(true);
        }
    }
}
