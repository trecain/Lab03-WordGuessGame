using System;
using System.IO;

namespace Lab03_WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string wordBankFilePath = "../../../wordBankFile.txt";
                Console.WriteLine("Welcome to my word Guess Game!");
                OpenWordBank(wordBankFilePath);
                Console.WriteLine("");
                MainMenuUI(wordBankFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
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
            try
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
            catch(Exception)
            {
                throw;
            }
        }


        public delegate void MenuFunctions(string path);
        public static void RenderMenus(string path, string message, MenuFunctions cb)
        {
            Console.WriteLine("");
            Console.WriteLine(message);
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
            {
                cb(path);
            }
        }


        public static void NewGame(string path)
        {
            string[] wordArr = ReadFile(path);
            Random random = new Random();
            int randomInteger = random.Next(0, wordArr.Length);
            string[] underscores = new string[wordArr.Length];
            for(int i = 0; i < wordArr.Length; i++)
            {
                underscores[i] = "_ ";
            }
            Console.WriteLine(String.Join("", underscores));
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
                    RenderMenus(path, "Would you like to return to the admin menu again? enter yes or no", AdminMenuUI);
                }
                else
                {
                    RenderMenus(path, "Your word bank does not exist! Would you like to create one? enter yes or no", AddWordToBank);
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

        public static string[] ReadFile(string path)
        {
            try
            {
                string[] myWords = File.ReadAllLines(path);
                return myWords;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteWordFromWordBank(string path)
        {
           try
            {
                Console.WriteLine("Word to delete from word bank.");
                string wordToRemove = Console.ReadLine();
                string[] currentWords = ReadFile(path);
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (string line in currentWords)
                    {
                        if (line != wordToRemove)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                AdminMenuUI(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Runs the main menu logic by invoking methods off menu
        public static void MainMenuLogic(int input, string path)
        {
            try
            {
                switch (input)
                {
                    case 1:
                        NewGame(path);
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
            catch (Exception)
            {
                throw;
            }
        }

        public static void AdminMenuLogic(int input, string path)
        {
            try
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
                        DeleteWordFromWordBank(path);
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
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
