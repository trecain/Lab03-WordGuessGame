using System;
using System.IO;

namespace Lab03_WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            OpenWordBank();
        }

        static void OpenWordBank()
        {
            string path = @"C:\Users\ercai\Desktop\codefellows\401\Lab03-WordGuessGame\BankOfWords.txt";

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    Byte[] fileText = new System.Text.UTF8Encoding(true).GetBytes("Hello guest, welcome to word guessing game.");
                    fs.Write(fileText, 0, fileText.Length);
                }
            }
            else
            {
                string[] fileText = File.ReadAllLines(path);
                int fileTextLength = fileText.Length;
            }
        }

    }
}
