using System;

namespace ArmaComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\schwe\Desktop\testReadFile.txt";
            string[] lineStrings = System.IO.File.ReadAllLines(path);
            foreach (string lineString in lineStrings)
            {
                Console.WriteLine(lineString);
            }
            Console.ReadKey(true);
        }
    }
}
