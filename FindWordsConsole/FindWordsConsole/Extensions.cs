using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FindWordsConsole.Model;
using System.IO;
using System.Text.RegularExpressions;

namespace FindWordsConsole
{

    public static class Extensions
    {
        public static bool HasTwoVowels(this string line)
        {
            int count = 0;
            for (int i = 0; i <= line.Length - 1; i ++)
            {
                if (Regex.IsMatch(line.Substring(i, 1), "[aoeui]"))
                {
                    count++; if (count == 2) break;
                }
            };

            return count > 1;
        }

        public static IEnumerable<string> Lines(this StreamReader source)
        {
            String line;

            if (source == null)
                throw new ArgumentNullException("source");
            while ((line = source.ReadLine()) != null)
            {
                if (line.Length > 1 && line.Length < 6 && line.HasTwoVowels())
                {
                    // Additional functionality to determine if the "word" from the dictionary is really a word??
                    // A word with just 2 vowels is not likely to be a word thus will remove it.
                    if (line.Length == 2) continue;

                    yield return line;
                }
            }
        }

        // Decided not to use Rx to read the file because it's faster to read it synchronously using Streamreader.

        //List<string> dictionary = new List<string>();
        //    var bufferSize = 4200000;
        //    var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true);
        //    stream.AsyncReadLines(bufferSize, board).Run(line => dictionary.Add(line));
        //    string[] finalWordList = dictionary.ToArray();

        public static IObservable<string> AsyncReadLines(
    this Stream stream, int bufferSize, Keyboard board)
        {
            return Observable.CreateWithDisposable<string>(observer =>
            {
                var sb = new StringBuilder();
                var blocks = AsyncRead(stream, bufferSize).Select(
                    block => Encoding.ASCII.GetString(block));
                Action produceCurrentLine = () =>
                {
                    var text = sb.ToString();
                    sb.Length = 0;
                    
                    if (text.Length > 1 && text.Length < 6 && text.HasTwoVowels() && board.ValidateWord(new Word(text)))
                        observer.OnNext(text);
                };
                return blocks.Subscribe(data =>
                {
                    for (var i = 0; i < data.Length; i++)
                    {
                        var atEndofLine = false;
                        var c = data[i];
                        if (c == '\r')
                        {
                            atEndofLine = true;
                            var j = i + 1;
                            if (j < data.Length && data[j] == '\n')
                                i++;
                        }
                        else if (c == '\n')
                        {
                            atEndofLine = true;
                        }
                        if (atEndofLine)
                        {

                            produceCurrentLine();
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                },
                observer.OnError,
                () =>
                {
                    produceCurrentLine();
                    observer.OnCompleted();
                });
            });
        }

        public static IObservable<byte[]> AsyncRead(this Stream stream, int bufferSize)
        {
            var asyncRead = Observable.FromAsyncPattern<byte[], int, int, int>(
                stream.BeginRead, stream.EndRead);

            var buffer = new byte[bufferSize];

            return asyncRead(buffer, 0, bufferSize).Select(readBytes =>
            {
                var newBuffer = new byte[readBytes];
                Array.Copy(buffer, newBuffer, readBytes);

                return newBuffer;
            });
        }


        private static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 1 && line.Length < 6 && line.HasTwoVowels())
                        yield return line;
                }
            }
        }



    }
}
