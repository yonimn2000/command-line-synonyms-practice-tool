using System.Collections.Generic;

namespace YonatanMankovich.SynonymsPractice
{
    public class Keyword
    {
        public string Word { get; }
        public string PartOfSpeech { get; }

        internal readonly IList<string> synonyms;

        public Keyword(string keyword, string partOfSpeech, IList<string> synonyms)
        {
            Word = keyword;
            PartOfSpeech = partOfSpeech;
            this.synonyms = new List<string>(synonyms);
        }

        public IList<string> GetSynonyms()
        {
            return new List<string>(synonyms);
        }

        public bool Equals(Keyword keyword)
        {
            return Word.Equals(keyword.Word);
        }
    }
}