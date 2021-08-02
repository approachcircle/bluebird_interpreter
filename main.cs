using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;

public class mainClass
{
    public static void invokeping() {
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();
        options.DontFragment = true;
        string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        byte[] buffer = Encoding.ASCII.GetBytes (data);
        int timeout = 120;
        string address = "8.8.8.8";
        try {
            PingReply reply = pingSender.Send (address, timeout, buffer, options);
            if (reply.Status == IPStatus.Success) {
                Console.WriteLine("pong! {0}ms, buffer={1}, TTL={2}, addr={3}", reply.RoundtripTime,reply.Buffer.Length,reply.Options.Ttl,reply.Address.ToString());
            } else {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("error: ping failed, check your connection to {0} and try again", address);
            }
        } catch (PingException) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("error: the address that the ping was supposed to be sent to has either been changed from its original value of 8.8.8.8 to an unreachable address, or the connection to the destination address has been blocked");
            Console.WriteLine("error: address value is: {0}, should be 8.8.8.8", address);
        }
    }
    public static void dump(string data) {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("info: bluebird just called the function to do an emergency dump of data...");
        if (data == "") {
            Console.WriteLine("info: no data found in memory/provided to be dumped");
            Console.ResetColor();
        } else {
            Console.WriteLine("info: dumping data...");
            File.WriteAllText("dump.dat", data);
            Console.WriteLine("info: data dumped");
            Console.ResetColor();
        }
    }
    public static void die(string why = "no reason for crash provided") {
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("crashing...");
        throw new Exception(why);
    }

    public static void help(){
        string[] cmds = {
            "put: allows you to write text to the screen",
            "get: allows you to get input",
            "savetomem: allows you to get input, store it to the memory slot, then choose whether to display it afterwards or not",
            "help: displays these entries",
            "?: is an alias for help",
            "exit: exits bluebird",
            "retin: returns the command that you just input (this should ALWAYS return the value of \"retin&\")",
            "crash: throws an exception that the program does not handle",
            "clear: clears the console output",
            "thank you: you're welcome",
            "recallmem: recalls the value stored in the memory slot",
            "clearmem: clears the value stored in the memory slot",
            "delete: allows you to delete a file with a specified filename",
            "dump: allows you to dump custom data, or dump the data straight from memory, if there is any",
            "hangthread: puts the main thread to sleep indefinitely (hangs the program forever)",
            "ping: gets current network ping in a single packet from this computer to 8.8.8.8 (google.com)"
        };
        for (int i = 0; i < cmds.Length; i++) {
            Console.WriteLine(cmds[i]);
        }
    }

    public static void Main(string[] args)
    {
        Console.Title = "bluebird interpreter";
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
                    Console.Write("are you sure you want to do this? (yes/no):");
                    Console.ResetColor();
                    string crashOption = Console.ReadLine();
                    if (crashOption == "yes") {
                        dump(valueStore);
                        die("user inflicted crash");
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
                        dump(valueStore);
                        break;
                    } else if (dumpWhat == "custom") {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("please provide the custom data to be dumped:");
                        Console.ResetColor();
                        string customData = Console.ReadLine();
                        dump(customData);
                        break;
                    }
                    break;
                case "hangthread&":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("are you sure you want to do this? (yes/no):");
                    Console.ResetColor();
                    string hangOption = Console.ReadLine();
                    if (hangOption == "yes") {
                        dump(valueStore);
                        Console.WriteLine("putting thread to sleep, goodnight...");
                        while (true) {
                            Thread.Sleep(0);
                        }
                    } else {
                        Console.WriteLine("okay");
                        break;
                    }
                case "ping&":
                    invokeping();
                    break;
                case "clear&":
                    Console.Clear();
                    break;
                case "":
                    dump(valueStore);
                    die("data without the verification character at the end should never be processed");
                    break;
                case "&":
                    break;
                case null:
                    dump(valueStore);
                    die("data without the verification character at the end should never be processed");
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
