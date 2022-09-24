using System;

namespace net.minecraft.src
{
	public class StructureNetherBridgePieceWeight
	{
		public Type field_40699_a;
		public readonly int field_40697_b;
		public int field_40698_c;
		public int field_40695_d;
		public bool field_40696_e;

		public StructureNetherBridgePieceWeight(Type class1, int i2, int i3, bool z4)
		{
			this.field_40699_a = class1;
			this.field_40697_b = i2;
			this.field_40695_d = i3;
			this.field_40696_e = z4;
		}

		public StructureNetherBridgePieceWeight(Type class1, int i2, int i3) : this(class1, i2, i3, false)
		{
		}

		public virtual bool func_40693_a(int i1)
		{
			return this.field_40695_d == 0 || this.field_40698_c < this.field_40695_d;
		}

		public virtual bool func_40694_a()
		{
			return this.field_40695_d == 0 || this.field_40698_c < this.field_40695_d;
		}
	}

}