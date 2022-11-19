using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using TorchLite.network.protocol.communicators;
using TorchLite.network.protocol.legacy_protocol_29;
using System.Threading;
using TorchLite.network.protocol;
using System.IO;
using BlockByBlockShared.networking;
using TorchLite.network;
using System.Threading.Tasks;

namespace TorchLite
{
    public class NetworkManager
    {
        public int Port { get; set; } = 25565;
        public ICommunicator Communicator { get; set; } = new Protocol29Communicator(); // Temporary, we could theoretically use a different communicator per connected client.
        public Dictionary<int, Type> Packets { get; set; } = new(); // int = id
        public bool ServerRunning { get; set; } = true;

        private Server server;

        private TcpListener tcpListener;
        private MemoryStream netOut = new();

        public NetworkManager(Server server)
        {
            this.server = server;
            tcpListener = new(IPAddress.Any, Port);

            // Register all objects that derive from IProtocolMessage into the Packets dictionary
            foreach (Type type in typeof(IProtocolMessage).Assembly.GetTypes())
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue;
                }

                if (type.GetInterface(nameof(IProtocolMessage)) != null)
                {
                    IProtocolMessage? instance = (IProtocolMessage?)Activator.CreateInstance(type);

                    if (instance != null)
                        Packets.Add(instance.ID, type);
                }
            }
        }

        public void Start()
        {
            tcpListener.Start();

            server.Log($"Starting server on port {Port}");

            while (ServerRunning)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                server.Log($"Recieved connection from {client.Client.RemoteEndPoint}");

                new Task((() => { ClientConnected(client, this); })).Start();
            }
        }

        private void ClientConnected(TcpClient tcpClient, NetworkManager manager)
        {
            Client client = new Client(tcpClient, manager);

            server.Clients.Add(client);

            NetworkStream netStream = tcpClient.GetStream();
            BinaryReaderNet reader = new BinaryReaderNet(netStream, ByteOrder.BIG_ENDIAN);

            while (true)
            {
                if (!netStream.DataAvailable)
                {
                    Thread.Sleep(1);
                    continue;
                }    

                int packetId = reader.ReadByte();

                // Process incoming packets from this client.
                bool packetFound = false;
                IProtocolMessage? packet = null;

                foreach (KeyValuePair<int, Type> pair in Packets)
                {
                    if (packetId == pair.Key)
                    {
                        packetFound = true;
                        packet = (IProtocolMessage?)Activator.CreateInstance(pair.Value);
                        break;
                    }
                }    

                if (!packetFound)
                {
#if DEBUG
                    server.Log($"Recieved unknown packet id {packetId}", Severity.WARNING);
#endif
                    continue;
                }

                if (packet != null)
                {
                    packet.Read(reader);
                    packet.HandleRecieved(this, client);
                }

                Thread.Sleep(1);
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        /// <summary>
        /// Broadcasts the packet to every client connected to the server.
        /// </summary>
        /// <param name="message"></param>
        public void SendPacket(IProtocolMessage message)
        {
            foreach (Client client in server.Clients)
            {
                BinaryWriterNet writer = new(client.tcpClient.GetStream());
                writer.Write((byte)message.ID);
                message.Write(writer);
            }
        }

        /// <summary>
        /// Sends the packet to the specified client.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        public void SendPacket(IProtocolMessage message, Client client)
        {
            BinaryWriterNet writer = new(client.tcpClient.GetStream());
            writer.Write((byte)message.ID);
            message.Write(writer);
        }
    }
}
