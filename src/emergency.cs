using System;
using System.IO;
using System.Linq;
using System.Reflection;
using bluebird.exception;
using bluebird.main;
using bluebird.debug;

namespace bluebird {
    namespace emergency {
        class emergencyClass {
            public void dump(string data) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("info: bluebird just called the function to do an emergency dump of data...");

            if (data == "") {
                Console.WriteLine("info: no data found in memory/provided to be dumped");
                Console.ResetColor();
            } else {
                Console.WriteLine("info: data has been found, so it will be dumped");
                Console.WriteLine("info: dumping data...");
                File.WriteAllText("dump.dat", data);
                Console.WriteLine("info: data dumped");
                Console.ResetColor();
            }
        }

            public void die(string dataToDump, params Exception[] exceptionCaused) {
                dump(dataToDump);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("crashing...");
                Console.ResetColor();
                throw new AggregateException(exceptionCaused);
            }
        }
    }
}