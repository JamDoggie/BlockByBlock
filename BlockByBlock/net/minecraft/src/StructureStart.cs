using System;
using System.Collections.Generic;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public abstract class StructureStart
	{
		protected internal List<StructureComponent> components = new List<StructureComponent>();
		protected internal StructureBoundingBox boundingBox;

		public virtual StructureBoundingBox BoundingBox
		{
			get
			{
				return this.boundingBox;
			}
		}

		public virtual List<StructureComponent> Components
		{
			get
			{
				return components;
			}
		}

		public virtual void generateStructure(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
            for (int i = components.Count - 1; i >= 0; i--)
            {
                StructureComponent structureComponent5 = components[i];
                if (structureComponent5.BoundingBox.intersectsWith(structureBoundingBox3) && !structureComponent5.addComponentParts(world1, random2, structureBoundingBox3))
                {
                    components.Remove(structureComponent5);
                }
            }
        }

		protected internal virtual void updateBoundingBox()
		{
			this.boundingBox = StructureBoundingBox.NewBoundingBox;
			System.Collections.IEnumerator iterator1 = this.components.GetEnumerator();

			while (iterator1.MoveNext())
			{
				StructureComponent structureComponent2 = (StructureComponent)iterator1.Current;
				this.boundingBox.expandTo(structureComponent2.BoundingBox);
			}

		}

		protected internal virtual void markAvailableHeight(World world1, RandomExtended random2, int i3)
		{
			int i4 = 63 - i3;
			int i5 = this.boundingBox.YSize + 1;
			if (i5 < i4)
			{
				i5 += random2.Next(i4 - i5);
			}

			int i6 = i5 - this.boundingBox.maxY;
			this.boundingBox.offset(0, i6, 0);
			System.Collections.IEnumerator iterator7 = this.components.GetEnumerator();

			while (iterator7.MoveNext())
			{
				StructureComponent structureComponent8 = (StructureComponent)iterator7.Current;
				structureComponent8.BoundingBox.offset(0, i6, 0);
			}

		}

		protected internal virtual void setRandomHeight(World world1, RandomExtended random2, int i3, int i4)
		{
			int i5 = i4 - i3 + 1 - this.boundingBox.YSize;
			bool z6 = true;
			int i10;
			if (i5 > 1)
			{
				i10 = i3 + random2.Next(i5);
			}
			else
			{
				i10 = i3;
			}

			int i7 = i10 - this.boundingBox.minY;
			this.boundingBox.offset(0, i7, 0);
			System.Collections.IEnumerator iterator8 = this.components.GetEnumerator();

			while (iterator8.MoveNext())
			{
				StructureComponent structureComponent9 = (StructureComponent)iterator8.Current;
				structureComponent9.BoundingBox.offset(0, i7, 0);
			}

		}

		public virtual bool SizeableStructure
		{
			get
			{
				return true;
			}
		}
	}

}