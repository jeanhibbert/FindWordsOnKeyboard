using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindWordsConsole.Model
{
    class CharacterEqualityComparer : IEqualityComparer<Character>
    {

        public bool Equals(Character c1, Character c2)
        {
            if (c1.Values.Contains(c2.Value) || c2.Values.Contains(c1.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Character c)
        {
            return c.Value.GetHashCode();
        }

    }

}
