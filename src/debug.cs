using System;
using System.Text;

namespace Bluebird {
    namespace Debug {
        class DebugClass {
            public void Evaluate(string data, string action = "write") {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("--debugging session started--");
                
                if (action == "write") {
                    Console.WriteLine("data is: \"{0}\"", data);
                }

                Console.WriteLine("--debugging session ended--");
                Console.ResetColor();
            }
            // probably will add more functions in the future
        }
    }
}