using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	internal class StructureStrongholdStones : StructurePieceBlockSelector
	{
		private StructureStrongholdStones()
		{
		}

		public override void selectBlocks(RandomExtended random1, int i2, int i3, int i4, bool z5)
		{
			if (!z5)
			{
				selectedBlockId = 0;
				selectedBlockMetaData = 0;
			}
			else
			{
				selectedBlockId = Block.stoneBrick.blockID;
				float f6 = random1.NextSingle();
				if (f6 < 0.2F)
				{
					selectedBlockMetaData = 2;
				}
				else if (f6 < 0.5F)
				{
					selectedBlockMetaData = 1;
				}
				else if (f6 < 0.55F)
				{
					selectedBlockId = Block.silverfish.blockID;
					selectedBlockMetaData = 2;
				}
				else
				{
					selectedBlockMetaData = 0;
				}
			}

		}

		internal StructureStrongholdStones(StructureStrongholdPieceWeight2 structureStrongholdPieceWeight21) : this()
		{
		}
	}

}