using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TorchLite.network.protocol
{
    /// <summary>
    /// Represents a raw message to be sent to or recieved from a client. This is formatted according to the specification of the protocol we're working with.
    /// </summary>
    public interface IProtocolMessage
    {
        int ID { get; set; }
        
        void Read(BinaryReader packet);
        
        void Write(BinaryWriter writer);

        void HandleRecieved(NetworkManager networkManager, Client client);
    }
}
