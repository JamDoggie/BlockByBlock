namespace TorchLiteDedicatedServer;

using System;
using TorchLite;

public class Program
{
    public static void Main(string[] args)
    {
        Server server = new Server();
        server.Start();
    }
}