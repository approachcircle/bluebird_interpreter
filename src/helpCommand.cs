using System;
using System.IO;

namespace bluebird {
    namespace help {
        class helpClass {
            public void help(){
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
                    "ping: gets current network ping over the course of four packets from this computer to 8.8.8.8 (google.com)",
                    "cleardump: deletes any dump files that shouldn't be there, or that you would like to remove",
                    "list: lists all files and directories in the current directory",
                    "recalldump: moves data from a data dump into the memory slot"
                };

                for (int i = 0; i < cmds.Length; i++) {
                    Console.WriteLine(cmds[i]);
                }
            }
        }
    }
}