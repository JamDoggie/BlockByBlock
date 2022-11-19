using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TorchLite.game.entity;

namespace TorchLite.network
{
    /// <summary>
    /// Represents a connection to the server from a remote or local machine. This does not necessarily always represent a player, as it could
    /// just be a client that is temporarily connecting to the server to ping it and get information like the MOTD.
    /// </summary>
    public class Client
    {
        public TcpClient tcpClient { get; set; }

        public bool LoggedIn { get; set; } = false;

        public Player? Player { get; set; }

        private NetworkManager networkManager;

        public Client(TcpClient tcpClient, NetworkManager networkManager)
        {
            this.tcpClient = tcpClient;
            this.networkManager = networkManager;
        }
    }
}
