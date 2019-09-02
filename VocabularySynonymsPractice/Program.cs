using System;
using System.Collections.Generic;
using System.IO;

namespace VocabularyKeyWordsQuiz
{
    class Program
    {
        static List<Keyword> myKeyWords = new List<Keyword>();
        static Random randomWrongWord = new Random();
        static Random randomWrongKeyword = new Random();
        static void Main(string[] args)
        {
            ReadFile("WordsList.txt");
            Test();
        }

        static void Test()
        {
            int wrongAnswerCount = 0;
            for (int keywordIndex = 0; keywordIndex < myKeyWords.Count; keywordIndex++) // Test for every keyword
            {
                int wrongKeywordIndex = 0;
                do wrongKeywordIndex = randomWrongKeyword.Next(myKeyWords.Count); // If the picked keyword the same as the tested keyword, choose a different one.
                while (wrongKeywordIndex == keywordIndex || myKeyWords[keywordIndex].partOfSpeech != myKeyWords[wrongKeywordIndex].partOfSpeech);
                Console.WriteLine($"Question {keywordIndex + 1} of {myKeyWords.Count}:");
                Console.WriteLine("Which word does not belong in the list:");
                Console.WriteLine();
                Console.WriteLine("Keyword: " + myKeyWords[keywordIndex].keyword);
                Console.WriteLine();
                int wrongWordIndex = randomWrongWord.Next(myKeyWords[keywordIndex].words.Count);
                List<string> wordListToShow = new List<string>();
                wordListToShow.AddRange(myKeyWords[keywordIndex].words);
                wordListToShow.Insert(wrongWordIndex, myKeyWords[wrongKeywordIndex].words[wrongWordIndex]);
                for (int wordIndex = 0; wordIndex < wordListToShow.Count; wordIndex++)
                    Console.WriteLine($"{wordIndex + 1}: {wordListToShow[wordIndex]}");
                Console.WriteLine();
                int input = -1;
                while (!int.TryParse(Console.ReadLine(), out input) || input <= 0 || input > wordListToShow.Count)
                    Console.WriteLine("Bad input... Try again:");
                if (input == wrongWordIndex + 1)
                    Console.WriteLine("Correct");
                else
                {
                    Console.WriteLine($"Wrong. It is #{wrongWordIndex + 1}.");
                    wrongAnswerCount++;
                }
                Console.WriteLine($"'{myKeyWords[wrongKeywordIndex].words[wrongWordIndex]}' means '{myKeyWords[wrongKeywordIndex].keyword}'.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine($"You answered correctly on {myKeyWords.Count-wrongAnswerCount} out of {myKeyWords.Count} questions. ({(myKeyWords.Count - wrongAnswerCount)*100/ myKeyWords.Count}%)");
            Console.ReadKey();
        }

        static Random randomShuffle = new Random();
        static void ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                List<string> wordList = new List<string>();
                for (int i = 2; i < words.Length; i++)
                    wordList.Add(words[i]);
                myKeyWords.Insert(randomShuffle.Next(myKeyWords.Count), new Keyword(words[0], words[1], wordList));
            }
        }
    }
    public struct Keyword
    {
        public string keyword;
        public string partOfSpeech;
        public List<string> words;
        public Keyword(string newkeyword, string newpartOfSpeech, List<string> newwords)
        {
            keyword = newkeyword;
            partOfSpeech = newpartOfSpeech;
            words = newwords;
        }
    }
}