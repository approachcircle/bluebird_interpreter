using System;

public class mainClass
{
    public static void die(){
        Console.ResetColor();
        throw new Exception();
    }

    public static void help(){
        string[] cmds = {
            "put: allows you to write text to the screen",
            "get: allows you to get input",
            "getandstore: allows you to get input, store it, then choose whether to display it afterwards or not",
            "help: displays these entries",
            "?: is an alias for help",
            "exit: exits bluebird",
            "retin: returns the command that you just input (this should ALWAYS return the value of \"retin\")",
            "crash: throws an exception that the program does not handle",
            "clear: clears the console output",
            "thank you: you're welcome"
        };
        for (int i = 0; i < cmds.Length; i++) {
            Console.WriteLine(cmds[i]);
        }
    }

    public static void Main(string[] args)
    {
        while (true) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("bluebird>");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string userIn = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            switch (userIn) {
                case "put":
                    Console.Write("put: ");
                    string putVal = Console.ReadLine();
                    Console.WriteLine(putVal);
                    break;
                case "get":
                    Console.Write("get: ");
                    Console.ReadLine();
                    break;
                case "getandstore":
                    Console.Write("get: ");
                    string valueStore = Console.ReadLine();
                    Console.Write("would you like to read the value you just stored? (yes/no): ");
                    string readOption = Console.ReadLine();
                    if (readOption == "yes") {
                        Console.WriteLine(valueStore);
                        break;
                    } else {
                        Console.WriteLine("okay");
                        break;
                    }
                case "exit":
                    Console.ResetColor();
                    System.Environment.Exit(1);
                    break;
                case "help":
                    help();
                    break;
                case "?":
                    help();
                    break;
                case "retin":
                    Console.WriteLine(userIn);
                    break;
                case "crash":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("are you sure you want to do this? (yes/no): ");
                    string crashOption = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (crashOption == "yes") {
                        Console.WriteLine("crashing...");
                        die();
                        break;
                    } else {
                        Console.WriteLine("okay");
                        break;
                    }
                case "thank you":
                    Console.WriteLine("you're welcome");
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "":
                    break;
                case null:
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: \"" + userIn + "\" is not a valid command.");
                    break;
            }
        }
    }
}