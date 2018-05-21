using System;

namespace ArmaComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lineStrings = System.IO.File.ReadAllLines(@"C:\Users\schwe\Desktop\testReadFile.txt");
            foreach (string lineString in lineStrings)
            {
                Console.WriteLine(lineString);
            }
            Console.ReadKey(true);
        }
    }
}
