using BlockByBlock.java_extensions;
using System;
using System.Collections;
using System.Linq;

namespace net.minecraft.src
{

	public abstract class MapGenStructure : MapGenBase
	{
		protected internal Hashtable coordMap = new Hashtable();

		public override void generate(IChunkProvider iChunkProvider1, World world2, int i3, int i4, sbyte[] b5)
		{
			base.generate(iChunkProvider1, world2, i3, i4, b5);
		}

		protected internal override void recursiveGenerate(World world1, int i2, int i3, int i4, int i5, sbyte[] b6)
		{
			if (!this.coordMap.ContainsKey(ChunkCoordIntPair.chunkXZ2Int(i2, i3)))
			{
				this.rand.Next();
				if (this.canSpawnStructureAtCoords(i2, i3))
				{
					StructureStart structureStart7 = this.getStructureStart(i2, i3);
					this.coordMap[ChunkCoordIntPair.chunkXZ2Int(i2, i3)] = structureStart7;
				}

			}
		}

		public virtual bool generateStructuresInChunk(World world1, RandomExtended random2, int i3, int i4)
		{
			int i5 = (i3 << 4) + 8;
			int i6 = (i4 << 4) + 8;
			bool z7 = false;
			System.Collections.IEnumerator iterator8 = this.coordMap.Values.GetEnumerator();

			while (iterator8.MoveNext())
			{
				StructureStart structureStart9 = (StructureStart)iterator8.Current;
				if (structureStart9.SizeableStructure && structureStart9.BoundingBox.intersectsWith(i5, i6, i5 + 15, i6 + 15))
				{
					structureStart9.generateStructure(world1, random2, new StructureBoundingBox(i5, i6, i5 + 15, i6 + 15));
					z7 = true;
				}
			}

			return z7;
		}

		public virtual bool func_40483_a(int i1, int i2, int i3)
		{
			System.Collections.IEnumerator iterator4 = this.coordMap.Values.GetEnumerator();

			while (true)
			{
				StructureStart structureStart5;
				do
				{
					do
					{
						if (!iterator4.MoveNext())
						{
							return false;
						}

						structureStart5 = (StructureStart)iterator4.Current;
					} while (!structureStart5.SizeableStructure);
				} while (!structureStart5.BoundingBox.intersectsWith(i1, i3, i1, i3));

				System.Collections.IEnumerator iterator6 = structureStart5.Components.GetEnumerator();

				while (iterator6.MoveNext())
				{
					StructureComponent structureComponent7 = (StructureComponent)iterator6.Current;
					if (structureComponent7.BoundingBox.isVecInside(i1, i2, i3))
					{
						return true;
					}
				}
			}
		}

		public virtual ChunkPosition? getNearestInstance(World world1, int i2, int i3, int i4)
		{
			this.worldObj = world1;
			rand.SetSeed(world1.Seed);
			long j5 = this.rand.NextInt64();
			long j7 = this.rand.NextInt64();
			long j9 = (long)(i2 >> 4) * j5;
			long j11 = (long)(i4 >> 4) * j7;
            rand.SetSeed(j9 ^ j11 ^ world1.Seed);
            this.recursiveGenerate(world1, i2 >> 4, i4 >> 4, 0, 0, (sbyte[])null);
			double d13 = double.MaxValue;
			ChunkPosition? chunkPosition15 = null;
			System.Collections.IEnumerator iterator16 = this.coordMap.Values.GetEnumerator();

			ChunkPosition chunkPosition19;
			int i20;
			int i21;
			int i22;
			double d23;
			while (iterator16.MoveNext())
			{
				StructureStart structureStart17 = (StructureStart)iterator16.Current;
				if (structureStart17.SizeableStructure)
				{
					StructureComponent structureComponent18 = (StructureComponent)structureStart17.Components.ToList()[0];
					chunkPosition19 = structureComponent18.Center;
					i20 = chunkPosition19.x - i2;
					i21 = chunkPosition19.y - i3;
					i22 = chunkPosition19.z - i4;
					d23 = (double)(i20 + i20 * i21 * i21 + i22 * i22);
					if (d23 < d13)
					{
						d13 = d23;
						chunkPosition15 = chunkPosition19;
					}
				}
			}

			if (chunkPosition15 != null)
			{
				return chunkPosition15;
			}
			else
			{
				System.Collections.IList list25 = this.func_40482_a();
				if (list25 != null)
				{
					ChunkPosition? chunkPosition26 = null;
					System.Collections.IEnumerator iterator27 = list25.GetEnumerator();

					while (iterator27.MoveNext())
					{
						chunkPosition19 = (ChunkPosition)iterator27.Current;
						i20 = chunkPosition19.x - i2;
						i21 = chunkPosition19.y - i3;
						i22 = chunkPosition19.z - i4;
						d23 = (double)(i20 + i20 * i21 * i21 + i22 * i22);
						if (d23 < d13)
						{
							d13 = d23;
							chunkPosition26 = chunkPosition19;
						}
					}

					return chunkPosition26;
				}
				else
				{
					return null;
				}
			}
		}

		protected internal virtual System.Collections.IList func_40482_a()
		{
			return null;
		}

		protected internal abstract bool canSpawnStructureAtCoords(int i1, int i2);

		protected internal abstract StructureStart getStructureStart(int i1, int i2);
	}

}