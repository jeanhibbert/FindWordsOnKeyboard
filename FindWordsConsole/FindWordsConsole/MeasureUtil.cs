using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FindWordsConsole
{
    public class MeasureUtil : IDisposable
    {
        private Stopwatch _stopWatch;

        public MeasureUtil(string name)
        {
            Console.WriteLine("--- {0} ---", name);
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        #region IDisposable Members

        public void Dispose()
        {
            _stopWatch.Stop();
            TimeSpan ts = _stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime, "RunTime");
            Console.WriteLine("------");
        }

        #endregion
    }
}
