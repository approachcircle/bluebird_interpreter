using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Bluebird {
    namespace Networking {
        class PingClass {

            private Ping PingSender = new Ping();
            private PingOptions Options = new PingOptions();
            public void InvokePing() {
                Options.DontFragment = true;

                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes (data);
                int timeout = 120;
                string address = "8.8.8.8";

                try {
                    PingReply reply = PingSender.Send (address, timeout, buffer, Options);
                    if (reply.Status == IPStatus.Success) {
                        Console.WriteLine("pong! {0}ms, buffer_size={1}, TTL={2}, addr={3}", reply.RoundtripTime,reply.Buffer.Length,reply.Options.Ttl,reply.Address.ToString());
                    } else {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("error: ping failed, check your connection to {0} and try again", address);
                        Console.ResetColor();
                    }
                } catch (PingException) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: the address that the ping was supposed to be sent to has either been changed from its original value of 8.8.8.8 to an unreachable address, or the connection to the destination address has been blocked");
                    Console.WriteLine("error: address value is: {0}, should be 8.8.8.8", address);
                    Console.ResetColor();
                }
            }

            public dynamic GetPingMS() {
                Options.DontFragment = true;

                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes (data);
                int timeout = 120;
                string address = "8.8.8.8";

                try {
                    PingReply reply = PingSender.Send (address, timeout, buffer, Options);

                    if (reply.Status == IPStatus.Success) {
                        long ms = reply.RoundtripTime;
                        int rtbuffer = reply.Buffer.Length;
                        int rtttl = reply.Options.Ttl;
                        System.Net.IPAddress rtaddr = reply.Address;
                        return ms;
                    } else {
                        return "error: ping failed, check your connection to " + address + " and try again";
                    }

                } catch (PingException) {
                    return "error: the address that the ping was supposed to be sent to has either been changed from its original value of 8.8.8.8 to an unreachable address, or the connection to the destination address has been blocked";
                }
            }
        }

        public class DLSpeedClass {
            protected const string TempFile = "tempfile.tmp";
            private WebClient WebClientObject = new WebClient();
            public void InvokeDLSpeedtest() {
                Console.WriteLine("testing download speed...");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    WebClientObject.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", TempFile);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }
                
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(TempFile);
                long speedBPS = fileInfo.Length / sw.Elapsed.Seconds; //! one of these throw a DivideByZeroException sometimes
                double DoubleSpeedBPS = Convert.ToDouble(speedBPS);
                double DoubleSpeedMBPS = DoubleSpeedBPS / 1000000; //! one of these throw a DivideByZeroException sometimes
                Console.WriteLine("download speed is: '{0}' mbps (megabytes per second)", DoubleSpeedMBPS);
                File.Delete(TempFile);
            }

            public int GetDLSpeedBPS() {
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    WebClientObject.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", TempFile);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(TempFile);
                long speedBPS2 = fileInfo.Length / sw.Elapsed.Seconds;
                int IntSpeedBPS2 = Convert.ToInt32(speedBPS2);
                File.Delete(TempFile);
                return IntSpeedBPS2;
            }

            public int GetDLSpeedMBPS() {
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    WebClientObject.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", TempFile);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(TempFile);
                long speedBPS3 = fileInfo.Length / sw.Elapsed.Seconds;
                int IntSpeedBPS3 = Convert.ToInt32(speedBPS3);
                int IntSpeedMBPS3 = IntSpeedBPS3 / 1000000;
                File.Delete(TempFile);
                return IntSpeedMBPS3;
            }
        }
    }
}