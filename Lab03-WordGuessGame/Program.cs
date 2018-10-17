using System;
using System.IO;

namespace Lab03_WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string wordBankFilePath = "../../../wordBankFile.txt";
            Console.WriteLine("Welcome to my word Guess Game!");
            OpenWordBank(wordBankFilePath);
            Console.WriteLine("");
            MainMenuUI(wordBankFilePath);
        }

        //Open word bank to hold words for game.
        public static void OpenWordBank(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        try
                        {
                            sw.WriteLine("possum");
                            sw.WriteLine("halloween");
                            sw.WriteLine("vampire");
                            sw.WriteLine("ghost");
                            sw.WriteLine("witch");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {
                    File.Delete(path);
                    OpenWordBank(path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Outputs the main menu items.
        public static void MainMenuUI(string path)
        {
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Admin");
            Console.WriteLine("3 - Exit");
            Console.WriteLine("");
            Console.WriteLine("Please choose a menu item.");
            int userInput = Convert.ToInt32(Console.ReadLine());
            MainMenuLogic(userInput, path);

        }

        //Outputs the admin menu items
        public static void AdminMenuUI(string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Admin Menu");
            Console.WriteLine("");
            Console.WriteLine("1 - Add Word");
            Console.WriteLine("2 - View Words");
            Console.WriteLine("3 - Delete Word");
            Console.WriteLine("4 - Delete Word Bank");
            Console.WriteLine("5 - Back To Main Menu");
            int userInput = Convert.ToInt32(Console.ReadLine());
            AdminMenuLogic(userInput, path);
        }

        public static void AddWordToBank(string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                Console.WriteLine("What word would you like to add to the word bank?");
                string wordToAdd = Console.ReadLine();
                sw.WriteLine(wordToAdd);
                Console.WriteLine($"Success: {wordToAdd} was added to the word bank.");
            }
            Console.WriteLine("");
            AdminMenuUI(path);
        }

        public static void ViewWordBankWords(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] fileText = File.ReadAllLines(path);
                    int count = 0;
                    foreach(string text in fileText)
                    {
                        count++;
                        Console.WriteLine($"{count}: {text}");
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Would you like to return to the main menu again? enter yes or no");
                    string userInput = Console.ReadLine();
                    if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                    {
                        MainMenuUI(path);
                    }
                }
                else
                {
                    Console.WriteLine("Your word bank does not exist!");
                    Console.WriteLine("Would you like to create one? enter yes or no");
                    string userResponse = Console.ReadLine();
                    if (userResponse.ToLower() == "yes" || userResponse.ToLower() == "y")
                    {
                        AddWordToBank(path);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteWordBank(string path)
        {
            File.Delete(path);
            Console.WriteLine("Success: Word bank has been deleted");
            Console.WriteLine("");
            AdminMenuUI(path);
        }

        //Runs the main menu logic by invoking methods off menu
        public static void MainMenuLogic(int input, string path)
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("one");
                    break;
                case 2:
                    AdminMenuUI(path);
                    break;
                case 3:
                    Console.WriteLine("Thank you for playing! have a good one.");
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Error: Please enter a valid menu option.");
                    Console.WriteLine("");
                    MainMenuUI(path);
                    break;
            }
        }

        public static void AdminMenuLogic(int input, string path)
        {
            switch (input)
            {
                case 1:
                    AddWordToBank(path);
                    break;
                case 2:
                    ViewWordBankWords(path);
                    break;
                case 3:
                    Console.WriteLine("admin three");
                    break;
                case 4:
                    DeleteWordBank(path);
                    break;
                case 5:
                    MainMenuUI(path);
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid admin menu option");
                    break;
            }
        }
        
    }
}
