using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FindWordsConsole.Model
{
    public class Keyboard
    {
        public Keyboard(Character [,] keySet, int maxDepth)
        {
            keys = keySet;
            _maxDepth = maxDepth;

            DimensionX = keys.GetUpperBound(1);
            DimensionY = keys.GetUpperBound(0);

            // Set up the keyboard character meta data
            SetKeyBoardCharacterList();
            SetKnightMoveFunctionList();
            SetKeyLocations();
            SetKeyKnightMoveOptions();
        }

        private int _maxDepth = 1;

        private List<Func<Character>> _knightMoveFunctions = new List<Func<Character>>();

        private void SetKnightMoveFunctionList()
        {
            _knightMoveFunctions.Add(MakeKnightMoveDownLeft);
            _knightMoveFunctions.Add(MakeKnightMoveDownRight);
            _knightMoveFunctions.Add(MakeKnightMoveLeftDown);
            _knightMoveFunctions.Add(MakeKnightMoveLeftUp);
            _knightMoveFunctions.Add(MakeKnightMoveRightDown);
            _knightMoveFunctions.Add(MakeKnightMoveRightUp);
            _knightMoveFunctions.Add(MakeKnightMoveUpLeft);
            _knightMoveFunctions.Add(MakeKnightMoveUpRight);
        }

        public int DimensionX = 0;
        public int DimensionY = 0;


        /// <summary>
        /// Add all the characters on the keyboard to a list for simple querying
        /// </summary>
        private void SetKeyBoardCharacterList()
        {
            CharacterList.Clear();
            for (int i = 0; i <= DimensionY; i++)
            {
                for (int x = 0; x <= DimensionX; x++)
                {
                    CharacterList.Add(keys[i, x]);
                }
            }
        }

        /// <summary>
        /// Set all the character location meta data
        /// </summary>
        private void SetKeyLocations()
        {
            for (int i = 0; i <= DimensionY; i++)
            {
                for (int x = 0; x <= DimensionX; x++)
                {
                    keys[i, x].LocationX = x;
                    keys[i, x].LocationY = i;
                }
            }
        }

        /// <summary>
        /// Set each characters list of possible knight moves
        /// </summary>
        private void SetKeyKnightMoveOptions()
        {
            for (int i = 0; i <= DimensionY; i++)
            {
                for (int x = 0; x <= DimensionX; x++)
                {
                    locationX = x;
                    locationY = i;
                    keys[i, x].KnightMoveOptionList = this.PeekDeep(_maxDepth).ToList();
                }
            }
        }

        public List<Character> CharacterList = new List<Character>();

        public Character[,] keys = new Character [,] {}; 

        // Find ways to ensure access and manipulation of
        // these properties is atomic. They should not actually be modified
        // to start off with. (Try find a way that they would not be required)
        public int locationX { get; set; }
        public int locationY { get; set; }

        public Character CurrentCharacter()
        {
            return keys[locationY, locationX];
        }


        /// <summary>
        /// Special keys return more than just a single character
        /// </summary>
        /// <returns>All keys relating to the special keys</returns>
        public List<Character> SpecialKeys(Character currentChar)
        {
            List<Character> charList = new List<Character>();

            // Check if we are on a special key like the spacebar/shift/enter
            // If we are return all the other keys that do not have the same location
            if (this.CurrentCharacter().Id.HasValue)
                charList = (from chr in CharacterList
                            where chr.Id.Value == currentChar.Id.Value
                                && chr.LocationX != currentChar.LocationX
                                && chr.LocationY != currentChar.LocationY
                            select chr).ToList();

            return charList;

        }

        /// <summary>
        /// Find character list containing the eight possible Knight move permutions
        /// & when a side affecting key is pressed dont count it as a key press, continue to search
        /// for knight move permutations until the maximum depth is reached
        /// </summary>
        /// <returns>Character list of knight move permutations following on from current location</returns>
        /// 
        // Add X, Y parameters to PeekDeep();
        public IEnumerable<Character> PeekDeep(int maxDepth, int currentDepth = 1)
        {
            //Should not need to do this.
            int x = locationX;
            int y = locationY;

            foreach (Func<Character> mv in _knightMoveFunctions)
            {
                //Knight move methods need to take an x & y value mv.Invoke(x,y);
                Character dlc = mv.Invoke();
                
                if (dlc.Values.Count > 0 || currentDepth == maxDepth)
                {
                    yield return dlc;
                }
                
                // Not required... data is duplicated
                //else
                //{
                //    currentDepth++;
                //    foreach (Character chr in PeekDeep(maxDepth, currentDepth))
                //    {
                //        yield return chr;
                //    }
                //}

                // Reset the location
                locationX = x;
                locationY = y;
            }
            
            // Return additional special characters - Get working
            foreach (Character chr in SpecialKeys(this.CurrentCharacter()))
            {
                yield return chr;
            }

        }

        #region Various Knight Move permutations

        public Character MakeKnightMoveUpRight()
        {
            locationX = (locationX + 1) % (DimensionX + 1);
            locationY = (locationY + 2) % (DimensionY + 1);

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveUpLeft()
        {
            locationX = (locationX + 2) % (DimensionX + 1);
            locationY = (locationY + 1) % (DimensionY + 1);

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveDownLeft()
        {
            locationX = (locationX - 1) % (DimensionX + 1);
            if (locationX < 0)
                locationX = (DimensionX + 1) +locationX;

            locationY = (locationY - 2) % (DimensionY + 1);
            if (locationY < 0)
                locationY = DimensionY + 1 +locationY;

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveDownRight()
        {
            locationX = (locationX - 2) % (DimensionX + 1);
            if (locationX < 0)
                locationX = DimensionX + 1 + locationX;

            locationY = (locationY - 1) % (DimensionY + 1);
            if (locationY < 0)
                locationY = DimensionY + 1 + locationY;

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveRightUp()
        {
            locationX = (locationX + 1) % (DimensionX + 1);

            locationY = (locationY - 2) % (DimensionY + 1);
            if (locationY < 0)
                locationY = DimensionY + 1 + locationY;

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveLeftUp()
        {
            locationX = (locationX - 2) % (DimensionX + 1);
            if (locationX < 0)
                locationX = DimensionX + 1 + locationX;

            locationY = (locationY + 1) % (DimensionY + 1);

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveLeftDown()
        {
            locationX = (locationX - 1) % (DimensionX + 1);
            if (locationX < 0)
                locationX = DimensionX + 1 + locationX;

            locationY = (locationY + 2) % (DimensionY + 1);

            return this.CurrentCharacter();
        }

        public Character MakeKnightMoveRightDown()
        {
            locationX = (locationX + 2) % (DimensionX + 1);

            locationY = (locationY - 1) % (DimensionY + 1);
            if (locationY < 0)
                locationY = DimensionY + 1 + locationY;

            return this.CurrentCharacter();
        }

        #endregion

        /// <summary>
        /// Validate the word or sequence of characters from the dictionary conform to a knight moves sequence
        /// </summary>
        /// <param name="word">word discovered in the dictionary</param>
        /// <returns>boolean to indicate whether the full sequence was validated</returns>
        internal bool ValidateWord(Word word)
        {
            bool validWord = false;
            for (int i = 0; i < word.Characters.Count - 1; i++)
            {

                Character nextCharacter = (from c in CharacterList 
                                                    // check hash lookup speed 
                                                    where c.Values.Contains(word.Characters[i].Value)
                                                    // Fix bug with this only 1 level down required. potentially replace knightmoveoptionlist with Set.
                                                        && c.KnightMoveOptionList.Contains(word.Characters[i + 1], new CharacterEqualityComparer())
                                                        select c).SingleOrDefault();

                if (nextCharacter == null)
                    break;
     
                if (i == word.Characters.Count - 2)
                {
                    validWord = true;
                    break;
                }

            }
            return validWord;
        }


        
    }
}
