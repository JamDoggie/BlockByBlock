namespace net.minecraft.src
{

	public class Packet40EntityMetadata : Packet
	{
		public int entityId;
		private System.Collections.IList metadata;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.metadata = DataWatcher.readWatchableObjects(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			DataWatcher.writeObjectsInListToStream(this.metadata, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityMetadata(this);
		}

		public override int PacketSize
		{
			get
			{
				return 5;
			}
		}

		public virtual System.Collections.IList Metadata
		{
			get
			{
				return this.metadata;
			}
		}
	}

}