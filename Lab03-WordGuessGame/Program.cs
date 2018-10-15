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
            UserInterfaceMenu();
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

        static public int UserInterfaceMenu()
        {
            try
            {
                Console.WriteLine("Hello contestant!, Welcome to Word Guess Game.");
                Console.WriteLine("1: New Game");
                Console.WriteLine("2: Add A Word");
                Console.WriteLine("3: View Words");
                Console.WriteLine("4: Delete Words");
                Console.WriteLine("5: Exit Game");
                int userMenuInput = Convert.ToInt32(Console.ReadLine());
                return userMenuInput;

            }
            catch (FormatException)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Good Luck Contestant!");
            }
        }
    }
}
