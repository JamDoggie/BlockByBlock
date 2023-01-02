using System.Collections;

namespace net.minecraft.src
{

	public class ChunkProviderClient : IChunkProvider
	{
		private Chunk blankChunk;
		private LongHashMap chunkMapping { get; set; } = new LongHashMap();
		private System.Collections.IList field_889_c = new ArrayList();
		private World worldObj;

		public ChunkProviderClient(World world1)
		{
			this.blankChunk = new EmptyChunk(world1, 0, 0);
			this.worldObj = world1;
		}

		public virtual bool chunkExists(int i1, int i2)
		{
			return true;
		}

		/// <summary>
		/// I'm pretty sure this is just a method to unload a chunk at given chunk coordinates.
		/// PORTING TODO: name this method.
		/// </summary>
		/// <param name="i1"></param>
		/// <param name="i2"></param>
		public virtual void func_539_c(int i1, int i2)
		{
			Chunk chunk3 = this.provideChunk(i1, i2);
			if (!chunk3.Empty)
			{
				chunk3.onChunkUnload();
			}

			this.chunkMapping.remove(ChunkCoordIntPair.chunkXZ2Int(i1, i2));
			this.field_889_c.Remove(chunk3);
		}

		public virtual Chunk loadChunk(int i1, int i2)
		{
			Chunk chunk3 = new Chunk(this.worldObj, i1, i2);
			this.chunkMapping.add(ChunkCoordIntPair.chunkXZ2Int(i1, i2), chunk3);
			chunk3.isChunkLoaded = true;
			return chunk3;
		}

		public virtual Chunk provideChunk(int i1, int i2)
		{
			Chunk chunk3 = (Chunk)this.chunkMapping.getValueByKey(ChunkCoordIntPair.chunkXZ2Int(i1, i2));
			return chunk3 == null ? this.blankChunk : chunk3;
		}

		public virtual bool saveChunks(bool z1, IProgressUpdate iProgressUpdate2)
		{
			return true;
		}

		public virtual bool unload100OldestChunks()
		{
			return false;
		}

		public virtual bool canSave()
		{
			return false;
		}

		public virtual void populate(IChunkProvider iChunkProvider1, int i2, int i3)
		{
		}

		public virtual string makeString()
		{
			return "MultiplayerChunkCache: " + this.chunkMapping.NumHashElements;
		}

		public virtual System.Collections.IList getPossibleCreatures(EnumCreatureType enumCreatureType1, int i2, int i3, int i4)
		{
			return null;
		}

		public virtual ChunkPosition? findClosestStructure(World world1, string string2, int i3, int i4, int i5)
		{
			return null;
		}
	}

}