using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TorchLite.network.protocol.legacy_protocol_29
{
    public struct Packet254ServerListPing : IProtocolMessage
    {
        public int ID { get; set; } = 254;

        public Packet254ServerListPing()
        {
            
        }

        public void Read(BinaryReader packet)
        {
            
        }

        public void Write(BinaryWriter writer)
        {
            
        }

        public void HandleRecieved(NetworkManager networkManager, Client client)
        {
            networkManager.SendPacket(new Packet255DisconnectKick("A really cool server§69§420"));
        }
    }
}
