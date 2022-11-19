using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TorchLite.network.protocol.legacy_protocol_29
{
    public struct Packet255DisconnectKick : IProtocolMessage
    {
        public int ID { get; set; } = 255;

        public string Reason;

        public Packet255DisconnectKick(string reason)
        {
            Reason = reason;
        }

        public void Read(BinaryReader packet)
        {
            Reason = packet.ReadString();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Reason);
        }

        public void HandleRecieved(NetworkManager networkManager, Client client)
        {

        }
    }
}
