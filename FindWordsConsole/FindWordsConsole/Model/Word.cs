using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FindWordsConsole.Model
{
    public class Word
    {
        private List<Character> _characters = new List<Character>();

        public List<Character> Characters
        {
          get { return _characters; }
          set { _characters = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            _characters.ForEach(chr => sb.Append(chr.Value));
            return sb.ToString();
        }

        public Word(string word)
        {
            foreach (char c in word)
            {
                _characters.Add(new Character(c.ToString()));
            }
        }

        public Word()
        {}

        public bool HasTwoVowels()
        {

            int count = 0;
            _characters.ForEach(chr =>
                {
                    if (Regex.IsMatch(chr.Value, "[aoeui]")) count++;
                });

            return count > 1;
        }
    }
}
