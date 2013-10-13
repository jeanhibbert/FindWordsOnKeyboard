using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindWordsConsole.Model
{
    public class Character
    {
        public Character(string value)
        {
            Value = value;
            Values.Add(value);
        }

        public Character(string [] values)
        {
            Values = values.ToList();
        }

        public Character(string value, bool isVowel)
        {
            Value = value;
            Values.Add(value);
            IsVowel = isVowel;
        }

        public Character()
        { }

        public Character(int id)
        { Id = id; }

        public int? Id = null;
        public List<string> Values = new List<string>();
        public string Value {get; set;}
        public bool IsVowel { get; set; }
        
        public bool NonCharacter 
        {
            get
            {
                return Value == null;
            }
        }

        public override string ToString()
        {
            if (NonCharacter) return "' '";

            return String.Concat("'", Value, "'");
        }

        public int LocationY { get; set; }
        public int LocationX { get; set; }

        public List<Character> KnightMoveOptionList = new List<Character>();

    }
}
