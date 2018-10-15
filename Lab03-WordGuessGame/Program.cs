using System;
using System.IO;

namespace Lab03_WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
            OpenWordBank();
        }

        static void OpenWordBank()
        {
            string path = @"C:\Users\ercai\Desktop\codefellows\401\Lab03-WordGuessGame\BankOfWords.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("possum");
                sw.WriteLine("turtle");
                sw.WriteLine("pig");
            }
            HandlesTheUsersInput(path);
        }

        static public int UserInterfaceMenu()
        {
            try
            {
                Console.WriteLine("Hello contestant!, Welcome to Word Guess Game.");
                Console.WriteLine();
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
                Console.WriteLine("Error: That was not a number.");
            }
            finally
            {
                Console.WriteLine("Good Luck Contestant!");
            }
            return 0;
        }

        static public void HandlesTheUsersInput(string path)
        {
            int userInput = UserInterfaceMenu();

            switch (userInput)
            {
                case 1:
                    InitializeGame(path);
                    break;
                case 2:
                    AddToWordBank(path);
                    break;
                case 3:
                    ViewWords(path);
                    break;
                case 4:
                    DeleteWord(path);
                    break;
                case 5:
                    Console.WriteLine("Thank you for playing, have a good one.");
                    break;
                default:
                    Console.WriteLine("Error: option selected not on the menu.");
                    HandlesTheUsersInput(path);
                    break;
            }
        }


        public static void InitializeGame(string path)
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                Random random = new Random();
                int line = random.Next(0, lines.Length);
                char[] underscores = new char[lines[line].Length];

                for (int i = 0; i < underscores.Length; i++)
                {
                    underscores[i] = '_';
                }

                Console.Write("Your word to guess is: ");
                Console.WriteLine(string.Join(" ", underscores));
            }
        }

        public static void AddToWordBank(string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                Console.WriteLine("What word would you like to add?");

                sw.WriteLine(Console.ReadLine());
            }
            Console.WriteLine();
            HandlesTheUsersInput(path);
        }

        public static void DeleteWord(string path)
        {
            File.Delete(path);
            Console.WriteLine("Success: File has been deleted");
            Console.WriteLine();
            UserInterfaceMenu();
            HandlesTheUsersInput(path);
        }

        public static void ViewWords(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    int count = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        count++;
                        Console.WriteLine($"{count}: {s}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Your word bank does not exist!");
                Console.WriteLine("Would you like to create one? enter yes or no");
                string userName = Console.ReadLine();
                if (userName.ToLower() == "yes")
                {
                    AddToWordBank(path);
                }
            }
            Console.WriteLine();
            HandlesTheUsersInput(path);
        }

  
    }
}
