using System;
using System.Text;
using System.IO;

namespace approachcircle {
    namespace Debug {
        class Evaluations {
            public void Evaluate(string data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }
            
            public void Evaluate(Int32 data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }

            public void Evaluate(Int64 data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }

            public void Evaluate(double data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }

            public void Evaluate(FileInfo data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }

            public void Evaluate(Exception data) {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Type T = data.GetType();
                Console.WriteLine("{0} has value: '{1}' and is typeof: {2}", nameof(data), data, T.ToString());

                Console.ResetColor();
            }
        }
    }
}