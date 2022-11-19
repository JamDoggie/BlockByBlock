using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TorchLite.network;
using TorchLite.network.protocol.communicators;
using TorchLite.network.protocol.legacy_protocol_29;

namespace TorchLite
{
    public class Server
    {
        public const int TPSTarget = 20;
        public NetworkManager NetworkManager { get; set; }
        public Thread NetworkThread { get; set; }

        public List<Client> Clients { get; set; } = new();

        internal static double TPSMSInterval
        {
            get
            {
                return 1000d / TPSTarget;
            }
        }
        
        private DateTime? lastFrameTime = null;
        private DateTime? lastTick = null;
        private double timeSinceLastTick = 0;

        public Server()
        {
            NetworkManager = new(this);
            NetworkThread = new(() => NetworkManager.Start());
        }

        public void Start()
        {
            NetworkThread.Start();

            while (true)
            {
                if (lastFrameTime != null)
                {
                    DateTime currentTime = DateTime.Now; // We use this as the time at the start of the frame.

                    if (lastTick == null)
                    {
                        lastTick = currentTime;
                    }

                    timeSinceLastTick = (currentTime - lastTick.Value).TotalMilliseconds;

                    if (timeSinceLastTick >= TPSMSInterval)
                    {
                        if (timeSinceLastTick >= TPSMSInterval * 10)
                        {
                            Log($"Can't keep up. Is the server overloaded? Running {timeSinceLastTick}ms behind.", Severity.WARNING);
                        }

                        lastTick = currentTime;
                    }
                }

                lastFrameTime = DateTime.Now;
                Thread.Sleep(1);
            }
        }

        public void Stop()
        {
            
        }

        public void Log(string message, Severity severity = Severity.INFO)
        {
            switch (severity)
            {
                case Severity.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case Severity.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case Severity.CRITICAL:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write($"[{severity}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }

    public enum Severity
    {
        INFO,
        WARNING,
        CRITICAL
    }
}
