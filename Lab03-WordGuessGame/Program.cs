using System;
using System.IO;

namespace Lab03_WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
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
                    Byte[] fileText = new System.Text.UTF8Encoding(true).GetBytes("possum");
                    fs.Write(fileText, 0, fileText.Length);
                }
            }
            else
            {
                string[] fileText = File.ReadAllLines(path);
                int fileTextLength = fileText.Length;
            }
            HandlesTheUsersInput(path);
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
            string[] fileText = File.ReadAllLines(path);
            var random = new Random();
            var lineNum = random.Next(0, fileText.Length - 1);
            string lines = fileText[lineNum];

            int userGuesses = 0;

            Console.WriteLine("Please guess a word.");
            string userGuess = Console.ReadLine().ToLower();

            userGuesses += userGuesses;

            if (lines == userGuess)
            {
                Console.WriteLine("You're correct.");
            }
            else if (lines.ToLower().Contains(userGuess))
            {
                Console.WriteLine("That letter is correct.");
            }
            else
            {
                Console.WriteLine($"That's incorrect, you're wrong.  The number you meant to guess was {lines}");
            }
            Console.WriteLine();
            HandlesTheUsersInput(path);
        }

        public static void AddToWordBank(string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                Console.WriteLine("What word would you like to add?");
                sw.Write(Environment.NewLine);
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
        }

        public static void ViewWords(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string[] fileText = File.ReadAllLines(path);

                int wordLength = fileText.Length;
                foreach(string line in fileText)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine();
            UserInterfaceMenu();
        }

        
    }
}
