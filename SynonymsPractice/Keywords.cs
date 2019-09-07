using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace YonatanMankovich.SynonymsPractice
{
    public class Keywords : IEnumerable<Keyword>
    {
        private readonly IList<Keyword> keywords = new List<Keyword>();

        public int Count { get => keywords.Count; }

        static readonly Random random = new Random();

        public Keywords(string pathToWordsFile)
        {
            string[] lines = File.ReadAllLines(pathToWordsFile);
            if (lines.Length < 2)
                throw new FormatException("The file has to have at least 2 lines.");
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                string[] columns = lines[lineNumber].Split(',');
                if (columns.Length < 3)
                    throw new FormatException($"The number of columns on line {lineNumber + 1} in the file {pathToWordsFile} was too small. " +
                        $"Each line has to have a keyword, a part of speech, and at least one synonym separated by commas.");
                IList<string> synonyms = new List<string>(columns.Length - 2);
                for (int columnIndex = 2; columnIndex < columns.Length; columnIndex++)
                    synonyms.Add(columns[columnIndex]);
                // Mix the keywords while adding them.
                keywords.Insert(random.Next(keywords.Count + 1), new Keyword(columns[0], columns[1], synonyms));
            }
        }

        public IList<Keyword> GetKeywordsWithPartOfSpeech(string partOfSpeech)
        {
            IList<Keyword> keywordsWithSamePOS = new List<Keyword>();
            foreach (Keyword keyword in keywords)
                if (keyword.PartOfSpeech.Equals(partOfSpeech))
                    keywordsWithSamePOS.Add(keyword);
            return keywordsWithSamePOS;
        }

        public Keyword GetKeywordOfSynonym(string synonymSearch)
        {
            foreach (Keyword keyword in keywords)
                foreach (string synonym in keyword.GetSynonyms())
                    if (synonymSearch.Equals(synonym))
                        return keyword;
            return null;
        }

        public IEnumerator<Keyword> GetEnumerator()
        {
            foreach (Keyword keyword in keywords)
                yield return keyword;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}