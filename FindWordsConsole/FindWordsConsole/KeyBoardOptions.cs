using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FindWordsConsole.Model;

namespace FindWordsConsole
{
    // Implicit columns on they keyboard have been assumed

    public class KeyBoardOptions
    {
        public Character[,] QwertyKeySet
        {
            get
            {
                Character[,] qwertyKeySet = new Character[,]
                {
                    {new Character("¬"), new Character("1"), new Character("2"),new Character("3"), new Character("4"), new Character("4"),new Character("5"), new Character("6"), new Character("7"),new Character("8"), new Character("9"), new Character("0"),new Character("-"), new Character("=")},
                    {new Character(), new Character("q"), new Character("w"),new Character("e", true), new Character("r"), new Character("t"),new Character("y"), new Character("u", true), new Character("i", true),new Character("o", true), new Character("p"), new Character("["),new Character("]"), new Character(3)},
                    {new Character(), new Character("a", true), new Character("s"),new Character("d"), new Character("f"), new Character("g"),new Character("h"), new Character("j"), new Character("k"),new Character("l"), new Character(";"), new Character("'"),new Character("#"), new Character(3)},
                    {new Character(), new Character("\\"), new Character("z"),new Character("x"), new Character("c"), new Character("v"),new Character("b"), new Character("n"), new Character("m"),new Character(","), new Character("."), new Character("/"),new Character(2), new Character(2)},
                    {new Character(), new Character(), new Character(),new Character(1), new Character(1), new Character(1),new Character(1), new Character(1), new Character(1),new Character(1), new Character(), new Character(),new Character(), new Character()},
                };
                return qwertyKeySet;
            }
        }

        public Character[,] MobileKeySet
        {
            get
            {
                Character[,] mobileKeySet = new Character[,]
                {
                    {new Character(), new Character(new string[] {"a", "b", "c"}), new Character(new string[] {"d", "e", "f"})},
                    {new Character(new string[] {"g", "h", "i"}), new Character(new string[] {"j", "k", "l"}), new Character(new string[] {"m", "n", "o"})},
                    {new Character(new string[] {"p", "q", "r", "s"}), new Character(new string[] {"t", "u", "v"}), new Character(new string[] {"w", "x", "y", "z"})},
                    {new Character(), new Character(), new Character()},
                };
                return mobileKeySet;
            }
        }
    }
}
