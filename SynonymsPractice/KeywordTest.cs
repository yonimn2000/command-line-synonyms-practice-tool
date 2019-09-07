using System;
using System.Collections.Generic;

namespace YonatanMankovich.SynonymsPractice
{
    public class KeywordTest : Keyword
    {
        public int IndexOfBadSynonym { get; }

        public KeywordTest(string keyword, string partOfSpeech, IList<string> synonyms, int indexOfBadSynonym) : base(keyword, partOfSpeech, synonyms)
        {
            IndexOfBadSynonym = indexOfBadSynonym;
        }

        public static IEnumerable<KeywordTest> GetKeywordTests(Keywords keywords)
        {
            foreach (Keyword keyword in keywords)
            {
                IList<string> newSynonyms = new List<string>(keyword.GetSynonyms());
                IList<string> synonymsOfWrongKeyword = GetWrongKeywordForKeyword(keyword, keywords).GetSynonyms();
                int indexOfWrongSynonym = new Random().Next(Math.Min(keyword.GetSynonyms().Count, synonymsOfWrongKeyword.Count));
                newSynonyms.Insert(indexOfWrongSynonym, synonymsOfWrongKeyword[indexOfWrongSynonym]);
                yield return new KeywordTest(keyword.Word, keyword.PartOfSpeech, newSynonyms, indexOfWrongSynonym);
            }
        }

        private static Keyword GetWrongKeywordForKeyword(Keyword keyword, Keywords keywords)
        {
            IList<Keyword> keywordsWithSamePOS = keywords.GetKeywordsWithPartOfSpeech(keyword.PartOfSpeech);
            if (keywordsWithSamePOS.Count == 1)
                throw new Exception($"The program could not find more than one word with a '{keyword.PartOfSpeech}' part of speech.");
            Keyword wrongKeyword;
            do wrongKeyword = keywordsWithSamePOS[new Random().Next(keywordsWithSamePOS.Count)];
            while (keyword.Equals(wrongKeyword)); // Avoid picking the current keyword.
            return wrongKeyword;
        }
    }
}