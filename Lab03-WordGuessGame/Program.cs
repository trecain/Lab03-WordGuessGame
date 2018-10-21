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
                //Grabs the location of the word bank file
                //Invokes all the files for the game
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
            try
            {
                Console.WriteLine("1 - New Game");
                Console.WriteLine("2 - Admin");
                Console.WriteLine("3 - Exit");
                Console.WriteLine("");
                Console.WriteLine("Please choose a menu item.");
                int userInput = Convert.ToInt32(Console.ReadLine());
                MainMenuLogic(userInput, path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Outputs the admin menu items
        public static void AdminMenuUI(string path)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }


        // uses stream writer to append new text to file path
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


        //delegate to use a function as a method
        public delegate void MenuFunctions(string path);


        //renders the menus with different messages.
        public static void RenderMenus(string path, string message, MenuFunctions cb)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    cb(path);
                }
                else if (userInput.ToLower() == "no" || userInput.ToLower() == "n")
                {
                    Console.WriteLine("Good bye!!");
                } else
                {
                    RenderMenus(path, message, cb);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        //uses the type random to randomly grab the game word from file path
        public static string GrabRandomWordFromFile(string path)
        {
            try
            {
                Random random = new Random();
                string[] wordsOnFile = File.ReadAllLines(path);
                string randomlyChosenWord = wordsOnFile[random.Next(0, wordsOnFile.Length)];
                return randomlyChosenWord;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Fills in the underscores for the game ui on the console
        public static string CreatesGameUI(string word)
        {
            try
            {
                char[] arrayOfChars = word.ToCharArray();
                string[] arrayOfStrings = new string[arrayOfChars.Length];
                for (int i = 0; i < arrayOfChars.Length; i++)
                {
                    arrayOfStrings[i] = "_";
                }
                return string.Join("", arrayOfStrings);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //takes in userguess, actual word, and the game progress to check if the guess is in the word.
        public static string CheckIfCharInTheWord(char userGuess, string progress, string actualWord)
        {
            try
            {
                char[] wordInChars = actualWord.ToCharArray();
                char[] progressInChars = progress.ToCharArray();
        
                for (int i = 0; i < wordInChars.Length; i++)
                {
                    if (userGuess == wordInChars[i])
                    {
                        progressInChars[i] = userGuess;
                    }
                }
                return string.Join("", progressInChars);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Checks to see if the string contains any underscores and returns true or false to stop the loop
        public static bool CheckStringForUnderscores(string word)
        {
            try
            {
                return word.Contains("_");
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Starts a new game, runs the functionality for populating the array and replacing underscores with the chars
        public static void NewGame(string path)
        {
            try
            {
                string randomWord = GrabRandomWordFromFile(path);
                string checkIfChar = CreatesGameUI(randomWord);
                bool stopLoop = true;
                while (stopLoop)
                {
                    string updatedGameWord;
                    Console.WriteLine(Environment.NewLine);
                    Console.Write("Could you please select a letter? ");
                    Console.WriteLine(checkIfChar);
                    char guess = Console.ReadKey().KeyChar;
                    if (guess != ' ')
                    {
                        updatedGameWord = CheckIfCharInTheWord(guess, checkIfChar, randomWord);
                        checkIfChar = updatedGameWord;
                        stopLoop = CheckStringForUnderscores(checkIfChar);
                    }
                    else
                    {
                        NewGame(path);
                    }
                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Success: you guessed {randomWord} correctly.");
                MainMenuUI(path);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Reads all the words from the file and displays them to the console
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


        //Deletes word bank file 
        public static void DeleteWordBank(string path)
        {
            try
            {
                File.Delete(path);
                Console.WriteLine("Success: Word bank has been deleted");
                Console.WriteLine("");
                AdminMenuUI(path);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //helper function used to read all the lines in a file.
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


        //Deletes word from a file then recreates it.
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


        //Switch to run all the functions from the admin menu
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
