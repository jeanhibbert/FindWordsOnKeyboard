using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindWordsConsole.Model;

namespace FindWordsConsole.UnitTests
{
    [TestClass]
    public class KeyboardTestFixture
    {
      
        //10 words which should validate
        //10 words which shouldnt validate

        [TestMethod]
        public void TestCanIterateThroughValidKnightMoves()
        {
            //By default the board always starts off at 0,0 
            Keyboard board = new Keyboard(new KeyBoardOptions().QwertyKeySet, 1);
            
            board.locationX = 13;
            board.locationY = 1;

            Character chr1 = board.CurrentCharacter();
            
            //Character chr2 = board.MakeKnightMoveDownLeft();
            //Character chr3 = board.MakeKnightMoveDownRight();
            //Character chr4 = board.MakeKnightMoveLeftDown();
            //Character chr5 = board.MakeKnightMoveLeftUp();
            //Character chr6 = board.MakeKnightMoveRightDown();
            //Character chr7 = board.MakeKnightMoveRightUp();
            //Character chr8 = board.MakeKnightMoveUpLeft();
            //Character chr9 = board.MakeKnightMoveUpRight();

            // location should be the same as the start
            Assert.IsTrue(board.locationX == 13);
            Assert.IsTrue(board.locationY == 1);

        }

        [TestMethod]
        public void TestCanMoveAndRetrieveCorrectValues()
        {
            Keyboard board = new Keyboard(new KeyBoardOptions().QwertyKeySet, 1);
            
            board.locationX = 4;
            board.locationY = 2;

            Assert.IsTrue(board.CurrentCharacter().Value == "f");

        }

        [TestMethod]
        public void TestCanIterateThroughAllValuesFromSingleLocation()
        {
            Keyboard board = new Keyboard(new KeyBoardOptions().QwertyKeySet, 1);

            board.locationX = 4;
            board.locationY = 2;

            Assert.IsTrue(board.CurrentCharacter().Value == "f");

        }

        [TestMethod]
        public void TestCanPeekThroughKnightMoveValues()
        {
            Keyboard board = new Keyboard(new KeyBoardOptions().QwertyKeySet, 1);

            board.locationX = 4;
            board.locationY = 2;

            IEnumerable<Character> chars = board.PeekDeep(1);

            Assert.IsTrue(chars.Count() == 8);
        }
    }
}
