using System;
using System.IO;
using Bluebird.Debug;
using Bluebird.Exceptions;

namespace Bluebird {
    namespace Severe {
        class SevereClass {
            public void Dump(string data) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("info: bluebird just called the function to do an emergency dump of data...");

            if (data == "") {
                Console.WriteLine("info: no data found in memory/provided to be dumped");
                Console.ResetColor();
            } else {
                Console.WriteLine("info: {0}='{1}'", nameof(data), data);
                Console.WriteLine("info: dumping data...");
                File.WriteAllText("dump.dat", data);
                Console.WriteLine("info: data dumped");
                Console.ResetColor();
            }
        }

            public void Die(string dataToDump, params Exception[] exceptionCaused) {
                Dump(dataToDump);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("crashing...");
                Console.ResetColor();
                throw new AggregateException(exceptionCaused);
            }
        }
    }
}