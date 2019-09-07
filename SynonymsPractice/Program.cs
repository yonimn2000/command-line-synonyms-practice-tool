using System;
using System.Collections.Generic;
using YonatanMankovich.SimpleConsoleMenus;

namespace YonatanMankovich.SynonymsPractice
{
    class Program
    {
        static readonly Keywords keywords = new Keywords("WordsList.txt");
        static void Main(string[] args)
        {
            Console.Title = "Yonatan's Synonyms Practice Tool";
            Console.CursorVisible = false;

            int wrongAnswerCount = 0;
            int questionNumber = 1;

            foreach (KeywordTest keywordTest in KeywordTest.GetKeywordTests(keywords))
            {
                Console.WriteLine($"[Question {questionNumber++} of {keywords.Count}]\n");
                Console.Write("Which of the following is not a synonym of ");
                WriteInColor(keywordTest.Word, ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine("?");

                IList<string> keywordTestSynonyms = keywordTest.GetSynonyms();

                SimpleConsoleMenu synonymsMenu = new SimpleConsoleMenu("", keywordTestSynonyms);
                Console.WriteLine();
                synonymsMenu.Show();
                Console.WriteLine();

                if (synonymsMenu.SelectedIndex == keywordTest.IndexOfBadSynonym)
                    WriteInColor("Correct!\n", ConsoleColor.Green, ConsoleColor.Black);
                else
                {
                    WriteInColor("Wrong.", ConsoleColor.Red, ConsoleColor.White);
                    Console.WriteLine($" The correct answer was '{keywordTestSynonyms[keywordTest.IndexOfBadSynonym]}'.");
                    wrongAnswerCount++;
                }

                Keyword keywordOfWrongSynonym = keywords.GetKeywordOfSynonym(keywordTestSynonyms[keywordTest.IndexOfBadSynonym]);
                Console.WriteLine($"'{keywordOfWrongSynonym.GetSynonyms()[keywordTest.IndexOfBadSynonym]}' means '{keywordOfWrongSynonym.Word}'.\n");
                Console.WriteLine("Hit ENTER to continue:");
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine($"You answered correctly on {keywords.Count - wrongAnswerCount} out of {keywords.Count} questions. " +
                $"({(keywords.Count - wrongAnswerCount) * 100 / keywords.Count}%)");
            Console.ReadKey();
        }

        public static void WriteInColor(string text, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            ConsoleColor prevBgColor = Console.BackgroundColor;
            ConsoleColor prevFgColor = Console.ForegroundColor;
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.Write(text);
            Console.BackgroundColor = prevBgColor;
            Console.ForegroundColor = prevFgColor;
        }
    }
}