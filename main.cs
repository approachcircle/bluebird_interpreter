using System;
using System.IO;

public class mainClass
{
    public static void die(string data, string what = "no reason given") {
        Console.ResetColor();
        if (data == "") {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("crashing...");
            throw new Exception(what);
        } else {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("info: crash inflicted, dumping data...");
            File.WriteAllText(@"dump.dat", data);
            Console.WriteLine("info: data dumped");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("crashing...");
            throw new Exception(what);
        }
        
    }

    public static void help(){
        string[] cmds = {
            "put: allows you to write text to the screen",
            "get: allows you to get input",
            "getandstore: allows you to get input, store it to the memory slot, then choose whether to display it afterwards or not",
            "help: displays these entries",
            "?: is an alias for help",
            "exit: exits bluebird",
            "retin: returns the command that you just input (this should ALWAYS return the value of \"retin&\")",
            "crash: throws an exception that the program does not handle",
            "clear: clears the console output",
            "thank you: you're welcome",
            "recallmem: recalls the value stored in the memory slot",
            "clearmem: clears the value stored in the memory slot"
        };
        for (int i = 0; i < cmds.Length; i++) {
            Console.WriteLine(cmds[i]);
        }
    }

    public static void Main(string[] args)
    {
        bool dumpExists = File.Exists(@"dump.dat");
        string valueStore = "";
        if (dumpExists == true) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("info: last exit was unclean, recovering data...");
            string[] filelines = File.ReadAllLines("dump.dat");
            string newval = String.Concat(filelines);
            valueStore = newval;
            Console.WriteLine("info: data has been written to memory slot, deleting dump...");
            File.Delete(@"dump.dat");
            Console.WriteLine("info: dump deleted");
        }
        while (true) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("bluebird>");
            Console.ResetColor();
            string userIn = Console.ReadLine();
            userIn = userIn + "&";
            /**
            the above line verifies that commands have been
            executed through the readline method above
            **/
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            switch (userIn) {
                case "put&":
                    Console.Write("put: ");
                    Console.ResetColor();
                    string putVal = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(putVal);
                    break;
                case "get&":
                    Console.Write("get: ");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
                case "getandstore&":
                    Console.Write("get: ");
                    Console.ResetColor();
                    valueStore = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("value saved to memory slot");
                    Console.Write("would you like to read the value you just stored? (yes/no): ");
                    Console.ResetColor();
                    string readOption = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    if (readOption == "yes") {
                        Console.WriteLine(valueStore);
                        break;
                    } else {
                        Console.WriteLine("okay");
                        break;
                    }
                case "exit&":
                    Console.ResetColor();
                    System.Environment.Exit(0);
                    break;
                case "help&":
                    help();
                    break;
                case "?&":
                    help();
                    break;
                case "retin&":
                    Console.WriteLine(userIn);
                    break;
                case "crash&":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("are you sure you want to do this? (yes/no): ");
                    Console.ResetColor();
                    string crashOption = Console.ReadLine();
                    if (crashOption == "yes") {
                        if (valueStore == "") {
                            die("", "user inflicted crash");
                        } else {
                            die(valueStore, "user inflicted crash");
                        }
                        break;
                    } else {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("okay");
                        break;
                    }
                case "thank you&":
                    Console.WriteLine("you're welcome");
                    break;
                case "recallmem&":
                    if (valueStore == "") {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: no value to recall from memory slot");
                    } else {
                        Console.WriteLine("value is: \"" + valueStore + "\"");
                    }
                    break;
                case "clearmem&":
                    if (valueStore == "") {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: no value in memory slot anyway");
                        break;
                    } else {
                        valueStore = "";
                        Console.WriteLine("memory slot cleared");
                        break;
                    }
                case "clear&":
                    Console.Clear();
                    break;
                case "":
                    break;
                case "&":
                    break;
                case null:
                    if (valueStore == "") {
                        die("", "null value entered");
                    } else {
                        die(valueStore, "null value entered");
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    userIn = userIn.Trim(new Char[] {'&'});
                    /**
                    the above line deletes the end character
                    from the end of the user input variable,
                    since thats internal and shouldn't be
                    displayed to the user in the syntax
                    error message
                    **/
                    Console.WriteLine("error: \"" + userIn + "\" was an unexpected token at this time.");
                    break;
            }
        }
    }
}