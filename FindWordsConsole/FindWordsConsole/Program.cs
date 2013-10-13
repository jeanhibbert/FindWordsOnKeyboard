using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using FindWordsConsole.Model;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FindWordsConsole
{
    class Program
    {
        // Assumptions made : 
        //1) implicit columns exist on the keyboard
        //2) Side effecting keys do not generate a character but do count as a key stroke 
        //  2.1) Added feature where side effecting keys do not count as a key stroke. (Set SEARCH_DEPTH to 6 for this behaviour)
        //  Need to provide the depth of search on the keyboard for this scenario to prevent infinite recursion.
        //3) All the words in the dictionary are valid words including the character sequences like 'ae'??.
        //  3.1) Have filtered out all words that only consist of 2 vowels since they are pretty rare... (http://en.wikipedia.org/wiki/English_words_with_uncommon_properties)
        //4) Functionality to return special key (shift/spacebar/enter) meta data provided (not working(never returns any additional characters.))
        //Note:
        //4) Rx v v1.0.2856.0 referenced but not required.
        private const int SEARCH_DEPTH = 1;

        static void Main(string[] args)
        {
            // Set the minimum no. of threads in the threadpool
            ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
            string[] finalWordList = new string[] {};

            using (new MeasureUtil("QWERTY"))
            {
                finalWordList = FindValidWords(new KeyBoardOptions().QwertyKeySet);
            }
            finalWordList.Select(s => s).Take(20).ToList().ForEach(Console.WriteLine);
            Console.WriteLine("Total words discovered : {0}", finalWordList.Length.ToString());
        
            using (new MeasureUtil("MOBILE"))
            {
                finalWordList = FindValidWords(new KeyBoardOptions().MobileKeySet);
            }
            finalWordList.Select(s => s).Take(20).ToList().ForEach(Console.WriteLine);
            Console.WriteLine("Total words discovered : {0}", finalWordList.Length.ToString());
        
            Console.ReadKey();
        }

        // Decided it would be more efficient to use
        // the list of possible valid words from the dictionary and evaluate them then 
        // against the keyboard Knight Move permutations using PLinq.
        private static string[] FindValidWords(Character[,] characterSet)
        {
            string fileName = @"..\..\Data\SINGLE.TXT";
            string[] filteredDictionary; // PLinq is more efficient with arrays.
            Keyboard board = new Keyboard(characterSet, SEARCH_DEPTH);

            using (StreamReader sr = new StreamReader(fileName))
            {
                filteredDictionary = sr.Lines().ToArray();
            }

            string[] finalWordList = (from word in filteredDictionary.AsParallel()
                                            .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                                            .WithDegreeOfParallelism(Environment.ProcessorCount)
                                      where board.ValidateWord(new Word(word))
                                      select word).ToArray();

            return finalWordList;
        }

    }
}
