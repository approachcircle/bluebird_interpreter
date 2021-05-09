using System;

public class mainClass
{
    public static void die(string what = "no reason given"){
        Console.ResetColor();
        throw new Exception(what);
    }

    public static void help(){
        string[] cmds = {
            "put: allows you to write text to the screen",
            "get: allows you to get input",
            "getandstore: allows you to get input, store it, then choose whether to display it afterwards or not",
            "help: displays these entries",
            "?: is an alias for help",
            "exit: exits bluebird",
            "retin: returns the command that you just input (this should ALWAYS return the value of \"retin&\")",
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
        string valueStore = "";
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
                    string putVal = Console.ReadLine();
                    Console.WriteLine(putVal);
                    break;
                case "get&":
                    Console.Write("get: ");
                    Console.ReadLine();
                    break;
                case "getandstore&":
                    Console.Write("get: ");
                    valueStore = Console.ReadLine();
                    Console.Write("would you like to read the value you just stored? (yes/no): ");
                    string readOption = Console.ReadLine();
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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (crashOption == "yes") {
                        Console.WriteLine("crashing...");
                        die("user defined crash inflicted");
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
                        Console.WriteLine("error: no value to recall");
                    } else {
                        Console.WriteLine("value is: \"" + valueStore + "\"");
                    }
                    break;
                case "clear&":
                    Console.Clear();
                    break;
                case "":
                    break;
                case "&":
                    break;
                case null:
                    die("null value entered");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    userIn = userIn.Trim(new Char[] {'&'});
                    /**
                    the above line deletes the semicolon
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