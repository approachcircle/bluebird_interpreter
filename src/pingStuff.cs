using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace bluebird {
    namespace ping {
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

            public dynamic getms() {
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
    }
}