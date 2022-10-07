using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet7UseEntity : Packet
	{
		public int playerEntityId;
		public int targetEntity;
		public int isLeftClick;

		public Packet7UseEntity()
		{
		}

		public Packet7UseEntity(int i1, int i2, int i3)
		{
			playerEntityId = i1;
			targetEntity = i2;
			isLeftClick = i3;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			playerEntityId = dataInputStream1.ReadInt32BigEndian();
			targetEntity = dataInputStream1.ReadInt32BigEndian();
			isLeftClick = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(playerEntityId);
			dataOutputStream1.WriteBigEndian(targetEntity);
			dataOutputStream1.Write((sbyte)isLeftClick);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUseEntity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}