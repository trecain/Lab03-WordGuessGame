using System;
using Xunit;
using System.IO;
using Lab03_WordGuessGame;

namespace WordGuessGameTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Test that at file can be created
        /// </summary>
        [Fact]
        public void TestThatAFileCanBeCreated()
        {
            string wordBankFilePath = "../../../wordBankFile.txt";
            File.Delete(wordBankFilePath);
            Program.OpenWordBank(wordBankFilePath);
            string[] words = File.ReadAllLines(wordBankFilePath);
            Assert.Equal(5, words.Length);
        }


        /// <summary>
        /// Test that a word can be added
        /// </summary>
        [Fact]
        public void TestThatAWordCanBeAdded()
        {
            string wordBankFilePath = "../../../wordBankFile.txt";
            string word = "tre";
            Program.AddWordToBank(wordBankFilePath, word);
            string[] words = File.ReadAllLines(wordBankFilePath);
            Assert.Equal("tre", Program.AddWordToBank(wordBankFilePath, word));
        }


        /// <summary>
        /// Can update one word
        /// </summary>
        [Fact]
        public void CanUpdateOneWord()
        {
            string wordBankFilePath = "../../../wordBankFile.txt";
            Program.UpdateOneWord(wordBankFilePath, "possum", "eagle");
            string[] words = File.ReadAllLines(wordBankFilePath);
            Assert.Equal("eagle", words[0]);
        }


        /// <summary>
        /// Test to see if the word can be deleted
        /// </summary>
        [Fact]
        public void WordCanBeDeleted()
        {
            string wordBankFilePath = "../../../wordBankFile.txt";
            Program.DeleteWordFromWordBank(wordBankFilePath, "possum");
            string[] words = File.ReadAllLines(wordBankFilePath);
            Assert.Equal(4, words.Length);
        }


        /// <summary>
        /// Test that a game can be created
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("cat", "___")]
        [InlineData("purple", "______")]
        [InlineData("ducks", "_____")]
        public void ShouldCreateGameUI(string input, string expected)
        {
            Assert.Equal(expected, Program.CreatesGameUI(input));
        }


        /// <summary>
        /// Test that the word chosen can accurately detect if the letter exist
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="progress"></param>
        /// <param name="actualWord"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData('a', "_____", "apple", "a____")]
        [InlineData('b', "____", "baby", "b_b_")]
        [InlineData('s', "______", "possum", "__ss__")]
        public void CanCheckIfCharIsInWord(char guess, string progress, string actualWord, string expected)
        {
            Assert.Equal(expected, Program.CheckIfCharInTheWord(guess, progress, actualWord));
        }


        /// <summary>
        /// Check to see if underscores are in the word, returns true
        /// </summary>
        /// <param name="word"></param>
        [Theory]
        [InlineData("_adds")]
        [InlineData("_____")]
        [InlineData("__ad__t")]
        public void CheckForUnderscoresTrue(string word)
        {
            Assert.True(Program.CheckStringForUnderscores(word));
        }


        /// <summary>
        /// Check to see if no underscores are in the word, returns false
        /// </summary>
        /// <param name="word"></param>
        [Theory]
        [InlineData("akdkdk")]
        [InlineData("applejuice")]
        [InlineData("handyman")]
        public void CheckForUnderscoresFalse(string word)
        {
            Assert.False(Program.CheckStringForUnderscores(word));
        }


        /// <summary>
        /// Test that a file can be read
        /// </summary>
        [Fact]
        public void ReadFile()
        {
            string path = "C:/Users/ercai/Desktop/codefellows/401/Lab03-WordGuessGame/WordGuessGameTest/Test.txt";
            string[] testArrString = { "dave", "tre", "mike" };
            Assert.Equal(testArrString, Program.ReadFile(path));
        }


        /// <summary>
        /// Test that a random word can be grabbed
        /// </summary>
        [Fact]
        public void GrabRandomWordFromFile()
        {
            string path = "C:/Users/ercai/Desktop/codefellows/401/Lab03-WordGuessGame/WordGuessGameTest/Test2.txt";
            Assert.Equal("mike", Program.GrabRandomWordFromFile(path));
        }

    }
}
