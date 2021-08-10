using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using Bluebird.Help;
using Bluebird.Debug;
using Bluebird.Severe;
using Bluebird.Networking;
using Bluebird.Exceptions;

namespace Bluebird {

    public class MainClass {

        public static void Main(string[] args) {
            string Memory = String.Empty;

            Help.HelpClass Help = new Help.HelpClass();
            Debug.DebugClass Debug = new Debug.DebugClass();
            Severe.SevereClass Severe = new Severe.SevereClass();
            Networking.PingClass Ping = new Networking.PingClass();
            Networking.DLSpeedClass DLSpeed = new Networking.DLSpeedClass(); 

            Console.Title = "bluebird interpreter";

            if (File.Exists("dump.dat")) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("info: last exit was unclean, recovering data...");
                string[] fileLines = File.ReadAllLines("dump.dat");
                string newVal = String.Concat(fileLines);
                Memory = newVal;
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
                        if (Memory != String.Empty) {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("info: data is already in memory slot, anything will be overwritten");
                        }

                        Console.Write("savetomem (what):");
                        Console.ResetColor();
                        Memory = Console.ReadLine();
                        Console.WriteLine("value saved to memory slot");
                        break;
                    case "exit&":
                        Console.ResetColor();
                        System.Environment.Exit(0);
                        break;
                    case "help&":
                        Help.Help();
                        break;
                    case "?&":
                        Help.Help();
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
                            Severe.Die(Memory, new CrashCommandException("user executed crash command"));
                            break;
                        } else {
                            Console.WriteLine("okay");
                            break;
                        }
                    case "thank you&":
                        Console.WriteLine("you're welcome");
                        break;
                    case "recallmem&":
                        if (Memory == String.Empty) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: no value to recall from memory slot");
                        } else {
                            Console.WriteLine("value is: \"{0}\"", Memory);
                        }
                        break;
                    case "clearmem&":
                        if (Memory == String.Empty) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: nothing to clear anyway");
                            break;
                        } else {
                            Memory = String.Empty;
                            Console.WriteLine("memory slot cleared");
                            break;
                        }
                    case "delete&":
                        Console.Write("delete (fileName):");
                        Console.ResetColor();
                        string fileName = Console.ReadLine();
                        fileName = fileName.ToLower();

                        if (fileName == "license") {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: cannot delete license files, doing this may violate the license in itself, so this action is disallowed");
                            Console.ResetColor();
                            break;
                        }

                        bool isSource = fileName.Contains(".cs");

                        if (isSource) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: cannot delete source files, this contradicts Asimov\'s third law of robotics, and these files are needed by the program to compile itself");
                            Console.ResetColor();
                            break;
                        }

                        if (!File.Exists(fileName)) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: this file doesn't exist anyway");
                            Console.ResetColor();
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("are you sure you want to do this? (yes/no):");
                        Console.ResetColor();
                        string delOption = Console.ReadLine();

                        if (delOption == "yes") {
                            File.Delete(fileName);
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
                            Severe.Dump(Memory);
                            break;
                        } else if (dumpWhat == "custom") {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("please provide the custom data to be dumped:");
                            Console.ResetColor();
                            string customData = Console.ReadLine();
                            Severe.Dump(customData);
                            break;
                        }
                        break;
                    case "hangthread&":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("are you sure you want to do this? (yes/no):");
                        Console.ResetColor();
                        string hangOption = Console.ReadLine();

                        if (hangOption == "yes") {
                            Severe.Dump(Memory);
                            Console.WriteLine("putting thread to sleep, goodnight...");
                            while (true) {
                                Thread.Sleep(0);
                            }
                        } else {
                            Console.WriteLine("okay");
                            break;
                        }
                    case "ping&":
                        Ping.InvokePing();
                        Ping.InvokePing();
                        Ping.InvokePing();
                        Ping.InvokePing();
                        break;
                    case "cleardump&":
                        if (!File.Exists("dump.dat")) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: a data dump created by bluebird does not exist anyway");
                            Console.ResetColor();
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("are you sure you want to do this? (yes/no):");
                        Console.ResetColor();
                        string deldOption = Console.ReadLine();

                        if (deldOption == "yes") {
                            File.Delete("dump.dat");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("info: dump deleted");
                            break;
                        } else {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("okay");
                            break;
                        }
                    case "recalldump&":
                        if (File.Exists("dump.dat")) {
                            string[] fileLines2 = File.ReadAllLines("dump.dat");
                            string dumpedData = String.Concat(fileLines2);
                            Memory = dumpedData;
                            Console.WriteLine("info: data has been written to memory slot, deleting dump...");
                            File.Delete("dump.dat");
                            Console.WriteLine("info: dump deleted");
                        } else if (!File.Exists("dump.dat")) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("error: a data dump created by bluebird does not exist anyway");
                            Console.ResetColor();
                            break;
                        }
                        break;
                    case "list&":
                        string[] files = Directory.GetFiles(".");
                        string[] dirs = Directory.GetDirectories(".");
                        
                        Console.WriteLine("files:");
                        for (int i = 0; i < files.Length; i++) {
                            Console.WriteLine(files[i]);
                        }
                        
                        Console.WriteLine();

                        Console.WriteLine("directories:");
                        for (int i = 0; i < dirs.Length; i++) {
                            Console.WriteLine(dirs[i]);
                        }
                        break;
                    case "downspeed&":
                        DLSpeed.InvokeDLSpeedtest();
                        break;
                    case "clear&":
                        Console.Clear();
                        break;
                    case "":
                        Severe.Die(Memory, new InvalidDataEnteredException("data without the verification character at the end should never be processed"));
                        break;
                    case "&":
                        break;
                    case null:
                        Severe.Die(Memory, new InvalidDataEnteredException("null data entered, and no verification character was found in the string"));
                        break;
                    default:
                        try {
                            throw new CommandNotFoundException();
                        } catch (CommandNotFoundException) {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            userIn = userIn.Trim(new Char[] {'&'});
                            /**
                            the above line deletes the verification character
                            from the end of the user input variable,
                            since that's internal and shouldn't be
                            displayed to the user in the syntax
                            error message
                            */
                            Console.WriteLine("error: '{0}' is not recognised as a valid command. check the spelling and try again", userIn);
                            break;
                        }
                }
            }
        }
    }
}
