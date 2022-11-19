using System;
using System.Collections.Generic;
using System.Text;

namespace TorchLite.network.protocol.communicators
{
    /// <summary>
    /// The goal of this class is to essentially act as a wall between the state of the game world and the players over the network.
    /// This class is responsible for communicating game state changes to the clients, communicating the state of entities to the clients, etc.
    /// This is also responsible for keeping the connection alive, handling encryption, logging in and authentication, etc.
    /// 
    /// <para>
    /// All of this is of course done according to the protocol that this class represents.
    /// </para>
    /// </summary>
    public interface ICommunicator
    {
        void Tick();

        void RecievePacket(IProtocolMessage message);
    }
}
