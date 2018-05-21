using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using HtmlAgilityPack;

namespace ArmaComparison
{
    class ArmaModFile
    {

        /**
         * <summary>This is a selector to help find all the IDs.</summary>
         */
        private const string NodeSelectorId = "//body//table//a";
        private const string NodeSelectorName = "//body//table//td";

        private string path { get; }
        private readonly List<ModElement> modList = new List<ModElement>();

        public ArmaModFile(string path)
        {
            this.path = path;
            this.ParseModList();
        }

        /**
         * <summary>This method parses the html document provided.
         * It will retrieve all IDs and add them to a list
         * This method is called by the constructor.</summary>
         */
        private void ParseModList()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(this.path);

            HtmlNodeCollection nodesIds = doc.DocumentNode.SelectNodes(NodeSelectorId);
            HtmlNodeCollection nodesNames = doc.DocumentNode.SelectNodes(NodeSelectorName);

            List<string> idElements = new List<string>();
            List<string> textElements = new List<string>();


            /*
             * idElements and textElements are ALWAYS the same size !!!
             * Because of this attribute, we can iterate the lists and add the i-th element to a ModElement
             */

            foreach (HtmlNode node in nodesIds)
            {
                /*
                 * example: node looks like this:  <a href="http://steamcommunity.com/sharedfiles/filedetails/?id=450814997" data-type="Link">
                   http://steamcommunity.com/sharedfiles/filedetails/?id=450814997</a>
                 * then node.InnerText == http://steamcommunity.com/sharedfiles/filedetails/?id=450814997
                 * and the split and array select results in 450814997
                 */

                string id = node.InnerText.Split("id=")[1];
                idElements.Add(id);
            }

            foreach (HtmlNode node in nodesNames)
            {
                if (node.Attributes.Contains("data-type") && node.Attributes["data-type"].Value == "DisplayName")
                {
                    string text = node.InnerText;
                    textElements.Add(text);
                }
            }

            for (var i = 0; i < idElements.Count; i++) //since idElements and textElements are the same size, no array out of bounds exception can happen.
            {
                ModElement addModElement = new ModElement(textElements[i], idElements[i]);
                //now add this to out object refererence var
                this.modList.Add(addModElement);
            }

           

            
        }

        public void PrintModList()
        {
            foreach (ModElement el in this.modList)
            {
                Console.WriteLine(el);
            }
        }

        private bool HasId(string search)
        {
            foreach (ModElement mod in this.modList)
            {
                if (mod.id.Equals(search))
                {
                    return true;
                }
            }

            return false;
        }

        /**
         * <summary>This method will compare two files and will point out which files are in common,
         * and more importantly the differences</summary>
         */
        private void Compare(ArmaModFile compareModFile, bool showInCommon)
        {
            //first round: check if file 2 has every element file 1 has (missing elements)
            foreach (ModElement mod in this.modList)
            {
                string currId = mod.id;
                if (compareModFile.HasId(currId))
                {
                    if (showInCommon)
                        Console.WriteLine($"Both files have {mod}.");
                }
                else
                {
                    Console.WriteLine($"File 2 is missing {mod}!");
                }
            }
            //second round: check if file 1 has every element file 2 has (exceeding elements)
            foreach (ModElement mod in compareModFile.modList)
            {
                string currId = mod.id;
                if (this.HasId(currId))
                {
                    //reduntant to print they both have it, since that is already done above
                }
                else
                {
                    Console.WriteLine($"File 1 is missing {mod}!");
                }
            }
        }


        static void Main(string[] args)
        {
            ArmaModFile file1 = new ArmaModFile(@"C:/Users/schwe/Desktop/jonas_ace.html");
            ArmaModFile file2 = new ArmaModFile(@"C:/Users/schwe/Desktop/Arma 3 Mod Preset broken_ace.html");
            /*Console.WriteLine("File 1:");
            file1.PrintModList();
            Console.WriteLine("File 2:");
            file2.PrintModList();*/

            file1.Compare(file2, false);

            //Console.WriteLine(file2.HasId("753946944"));

            

            Console.ReadKey(true);
        }
    }
}
