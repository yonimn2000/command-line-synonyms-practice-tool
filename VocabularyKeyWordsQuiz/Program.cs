using System;
using System.Collections.Generic;
using System.IO;

namespace VocabularyKeyWordsQuiz
{
    class Program
    {
        static List<string[]> myWords = new List<string[]>();
        static Random randomWrongWord = new Random();
        static Random randomWrongKeyword = new Random();
        static void Main(string[] args)
        {
            ReadFile("WordsList.txt");
            Test();
        }

        static void Test()
        {
            ShuffleList(myWords);
            for (int i = 0; i < myWords.Count; i++) // Test for every keyword
            {
                int wrongKeywordIndex = randomWrongKeyword.Next(myWords.Count);
                while (wrongKeywordIndex == i) // If the picked keyword the same as the tested keyword,
                    wrongKeywordIndex = randomWrongKeyword.Next(myWords.Count); // choose a different one.
                Console.WriteLine($"Question {i + 1} of {myWords.Count}:");
                Console.WriteLine("Which word does not belong in the list:");
                Console.WriteLine();
                Console.WriteLine("Key word: " + myWords[i][0]);
                Console.WriteLine();
                int WrongWordIndex = randomWrongWord.Next(1, myWords[i].Length);
                for (int j = 1; j < myWords[i].Length; j++)
                {
                    Console.Write($"{j}: ");
                    if (j == WrongWordIndex)
                        Console.WriteLine(myWords[wrongKeywordIndex][WrongWordIndex]);
                    else
                        Console.WriteLine(myWords[i][j]);
                }
                Console.WriteLine();
                if (int.Parse(Console.ReadLine()) == WrongWordIndex)
                    Console.WriteLine("Correct");
                else
                    Console.WriteLine($"Wrong. It is #{WrongWordIndex}.");
                Console.WriteLine($"'{myWords[wrongKeywordIndex][WrongWordIndex]}' means '{myWords[wrongKeywordIndex][0]}'.");
                Console.WriteLine($"The missing word is '{myWords[i][WrongWordIndex]}'.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static Random randomShuffle = new Random();
        static void ShuffleList(List<string[]> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int k = randomShuffle.Next(i, list.Count);
                string[] value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
        }

        static void ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
                myWords.Add(line.Split(','));
        }
    }
}