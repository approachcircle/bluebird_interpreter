using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using bluebird;

public class mainClass
{
    public static void Main(string[] args)
    {
        Console.Title = "bluebird interpreter";
        pingStuff ping = new pingStuff();
        helpCommand helpcmd = new helpCommand();
        emergency emg = new emergency();
        bool dumpExists = File.Exists("dump.dat");
        string valueStore = "";
        if (dumpExists == true) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("info: last exit was unclean, recovering data...");
            string[] filelines = File.ReadAllLines("dump.dat");
            string newval = String.Concat(filelines);
            valueStore = newval;
            Console.WriteLine("info: data has been written to memory slot, deleting dump...");
            File.Delete("dump.dat");
            Console.WriteLine("info: dump deleted");
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("strike ctrl+c to terminate in case of an emergency");
        Console.WriteLine("type \"help\" to see a list of available commands");
        Console.ResetColor();
        while (true) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("bluebird>");
            Console.ResetColor();
            string userIn = Console.ReadLine(); // main command input method
            userIn = userIn.ToLower();
            userIn = userIn + "&";
            /**
            the above line adds the verification character
            to the end of the input string
            **/
            switch (userIn) {
                case "put&":
                    Console.Write("put (what):");
                    Console.ResetColor();
                    string putVal = Console.ReadLine();
                    Console.WriteLine(putVal);
                    break;
                case "get&":
                    Console.Write("get (what):");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
                case "savetomem&":
                    if (valueStore != "") {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("info: data is already in memory slot, anything will be overwritten");
                    }
                    Console.Write("savetomem (what):");
                    Console.ResetColor();
                    valueStore = Console.ReadLine();
                    Console.WriteLine("value saved to memory slot");
                    break;
                case "exit&":
                    Console.ResetColor();
                    System.Environment.Exit(0);
                    break;
                case "help&":
                    helpcmd.help();
                    break;
                case "?&":
                    helpcmd.help();
                    break;
                case "retin&":
                    Console.WriteLine(userIn);
                    break;
                case "crash&":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("are you sure you want to do this? (yes/no):");
                    Console.ResetColor();
                    string crashOption = Console.ReadLine();
                    if (crashOption == "yes") {
                        emg.dump(valueStore);
                        emg.die("user inflicted crash");
                        break;
                    } else {
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
                        Console.WriteLine("value is: \"{0}\"", valueStore);
                    }
                    break;
                case "clearmem&":
                    if (valueStore == "") {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: nothing to clear anyway");
                        break;
                    } else {
                        valueStore = "";
                        Console.WriteLine("memory slot cleared");
                        break;
                    }
                case "delete&":
                    Console.Write("delete (filename):");
                    Console.ResetColor();
                    string filename = Console.ReadLine();
                    filename.ToLower();
                    if (filename == "license") {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: cannot delete license files, doing this may violate the license in itself, so this action is disallowed");
                        break;
                    } else if (filename == "main.cs") {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: cannot delete source files, if you would like to delete this file, rename it to something other than \"main.cs\" and try again");
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("are you sure you want to do this? (yes/no):");
                    Console.ResetColor();
                    string delOption = Console.ReadLine();
                    if (delOption == "yes") {
                        File.Delete(filename);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("info: file deleted");
                        break;
                    } else {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("okay");
                        break;
                    }
                case "dump&":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("would you like to dump data from memory, or dump your own custom data? (mem/custom):");
                    Console.ResetColor();
                    string dumpWhat = Console.ReadLine();
                    if (dumpWhat == "mem") {
                        emg.dump(valueStore);
                        break;
                    } else if (dumpWhat == "custom") {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("please provide the custom data to be dumped:");
                        Console.ResetColor();
                        string customData = Console.ReadLine();
                        emg.dump(customData);
                        break;
                    }
                    break;
                case "hangthread&":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("are you sure you want to do this? (yes/no):");
                    Console.ResetColor();
                    string hangOption = Console.ReadLine();
                    if (hangOption == "yes") {
                        emg.dump(valueStore);
                        Console.WriteLine("putting thread to sleep, goodnight...");
                        while (true) {
                            Thread.Sleep(0);
                        }
                    } else {
                        Console.WriteLine("okay");
                        break;
                    }
                case "ping&":
                    ping.invokeping();
                    break;
                case "clear&":
                    Console.Clear();
                    break;
                case "":
                    emg.dump(valueStore);
                    emg.die("data without the verification character at the end should never be processed");
                    break;
                case "&":
                    break;
                case null:
                    emg.dump(valueStore);
                    emg.die("data without the verification character at the end should never be processed");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    userIn = userIn.Trim(new Char[] {'&'});
                    /**
                    the above line deletes the verification character
                    from the end of the user input variable,
                    since that's internal and shouldn't be
                    displayed to the user in the syntax
                    error message
                    **/
                    Console.WriteLine("error: \"{0}\" was an unexpected token at this time.", userIn);
                    break;
            }
        }
    }
}
