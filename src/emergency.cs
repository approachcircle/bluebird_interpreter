using System;
using System.IO;

namespace bluebird {
    class emergency {
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
        public void die(string why = "no reason for crash provided") {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("crashing...");
            throw new Exception(why);
        }
    }
}