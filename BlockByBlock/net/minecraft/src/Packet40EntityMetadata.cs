using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet40EntityMetadata : Packet
	{
		public int entityId;
		private System.Collections.IList metadata;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			metadata = DataWatcher.readWatchableObjects(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			DataWatcher.writeObjectsInListToStream(metadata, dataOutputStream1);
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
				return metadata;
			}
		}
	}

}