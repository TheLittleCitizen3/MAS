using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MAS.IO
{
    static class Output
    {
        public static void print(string output)
        {
            Console.WriteLine(output + $" by Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
