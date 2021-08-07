using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace bluebird {
    namespace networking {
        class pingClass {
            public void invokePing() {
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

            public dynamic getPingMS() {
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
            protected const string tmpf = "tempfile.tmp";
            WebClient wcli = new WebClient();
            public void invokeDLSpeedtest() {
                Console.WriteLine("testing download speed...");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    wcli.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", tmpf);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }
                
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(tmpf);
                long speedBPS = fileInfo.Length / sw.Elapsed.Seconds;
                double DSpeedBPS = Convert.ToDouble(speedBPS);
                double DSpeedMBPS = DSpeedBPS / 1000000;
                Console.WriteLine("download speed is: '{0}' mbps (megabytes per second)", DSpeedMBPS);
                File.Delete(tmpf);
            }

            public int getDLSpeedBPS() {
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    wcli.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", tmpf);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(tmpf);
                long speedBPS2 = fileInfo.Length / sw.Elapsed.Seconds;
                int ISpeedBPS2 = Convert.ToInt32(speedBPS2);
                File.Delete(tmpf);
                return ISpeedBPS2;
            }

            public int getDLSpeedMBPS() {
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                try {
                    wcli.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", tmpf);
                    sw.Stop();
                } catch (WebException) {
                    sw.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("error: there is a problem with your internet connection. check your connection and try again");
                    Console.ResetColor();
                }

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(tmpf);
                long speedBPS3 = fileInfo.Length / sw.Elapsed.Seconds;
                int ISpeedBPS3 = Convert.ToInt32(speedBPS3);
                int ISpeedMBPS3 = ISpeedBPS3 / 1000000;
                File.Delete(tmpf);
                return ISpeedMBPS3;
            }
        }
    }
}