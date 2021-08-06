using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using bluebird.help;
using bluebird.ping;
using bluebird.debug;
using bluebird.emergency;

namespace bluebird {

    namespace main {

        public class mainClass {

            public static void Main(string[] args) {
                Console.Title = "bluebird interpreter";

                ping.pingClass ping = new ping.pingClass();
                help.helpClass help = new help.helpClass();
                debug.debugClass debug = new debug.debugClass();
                emergency.emergencyClass emergency = new emergency.emergencyClass();

                bool dumpExists = File.Exists("dump.dat");
                string memory = String.Empty;

                if (dumpExists) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("info: last exit was unclean, recovering data...");
                    string[] fileLines = File.ReadAllLines("dump.dat");
                    string newVal = String.Concat(fileLines);
                    memory = newVal;
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
                            if (memory != String.Empty) {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("info: data is already in memory slot, anything will be overwritten");
                            }

                            Console.Write("savetomem (what):");
                            Console.ResetColor();
                            memory = Console.ReadLine();
                            Console.WriteLine("value saved to memory slot");
                            break;
                        case "exit&":
                            Console.ResetColor();
                            System.Environment.Exit(0);
                            break;
                        case "help&":
                            help.help();
                            break;
                        case "?&":
                            help.help();
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
                                emergency.dump(memory);
                                emergency.die("user inflicted crash");
                                break;
                            } else {
                                Console.WriteLine("okay");
                                break;
                            }
                        case "thank you&":
                            Console.WriteLine("you're welcome");
                            break;
                        case "recallmem&":
                            if (memory == String.Empty) {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("error: no value to recall from memory slot");
                            } else {
                                Console.WriteLine("value is: \"{0}\"", memory);
                            }
                            break;
                        case "clearmem&":
                            if (memory == String.Empty) {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("error: nothing to clear anyway");
                                break;
                            } else {
                                memory = String.Empty;
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
                            bool fileExists = File.Exists(fileName);

                            if (!fileExists) {
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
                                emergency.dump(memory);
                                break;
                            } else if (dumpWhat == "custom") {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("please provide the custom data to be dumped:");
                                Console.ResetColor();
                                string customData = Console.ReadLine();
                                emergency.dump(customData);
                                break;
                            }
                            break;
                        case "hangthread&":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("are you sure you want to do this? (yes/no):");
                            Console.ResetColor();
                            string hangOption = Console.ReadLine();

                            if (hangOption == "yes") {
                                emergency.dump(memory);
                                Console.WriteLine("putting thread to sleep, goodnight...");
                                while (true) {
                                    Thread.Sleep(0);
                                }
                            } else {
                                Console.WriteLine("okay");
                                break;
                            }
                        case "ping&":
                            ping.invokePing();
                            ping.invokePing();
                            ping.invokePing();
                            ping.invokePing();
                            break;
                        case "cleardump&":
                            bool dumpExists2 = File.Exists("dump.dat");

                            if (!dumpExists2) {
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
                        // case "recalldump&":
                        //    add ability to move dump data to memory
                        //    break;
                        // case "list&":
                        //    list files using Directory.GetFiles()
                        //    break;
                        case "clear&":
                            Console.Clear();
                            break;
                        case "":
                            emergency.dump(memory);
                            emergency.die("data without the verification character at the end should never be processed");
                            break;
                        case "&":
                            break;
                        case null:
                            emergency.dump(memory);
                            emergency.die("null data entered, and no verification character was found in the string");
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
                            Console.WriteLine("error: \"{0}\" is not recognised as a valid command. check the spelling and try again", userIn);
                            break;
                    }
                }
            }
        }
    }
}
