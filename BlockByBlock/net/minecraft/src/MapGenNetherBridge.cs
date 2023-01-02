using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System.Collections;

namespace net.minecraft.src
{

    public class MapGenNetherBridge : MapGenStructure
	{
		private System.Collections.IList spawnList = new ArrayList();

		public MapGenNetherBridge()
		{
			this.spawnList.Add(new SpawnListEntry(typeof(EntityBlaze), 10, 2, 3));
			this.spawnList.Add(new SpawnListEntry(typeof(EntityPigZombie), 10, 4, 4));
			this.spawnList.Add(new SpawnListEntry(typeof(EntityMagmaCube), 3, 4, 4));
		}

		public virtual System.Collections.IList SpawnList
		{
			get
			{
				return this.spawnList;
			}
		}

		protected internal override bool canSpawnStructureAtCoords(int i1, int i2)
		{
			int i3 = i1 >> 4;
			int i4 = i2 >> 4;
			rand.SetSeed((long)(i3 ^ i4 << 4) ^ this.worldObj.Seed);
			this.rand.Next();
			return this.rand.Next(3) != 0 ? false : (i1 != (i3 << 4) + 4 + this.rand.Next(8) ? false : i2 == (i4 << 4) + 4 + this.rand.Next(8));
		}

		protected internal override StructureStart getStructureStart(int i1, int i2)
		{
			return new StructureNetherBridgeStart(this.worldObj, this.rand, i1, i2);
		}
	}

}