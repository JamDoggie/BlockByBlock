using System;

namespace net.minecraft.src
{
	public class WorldType
	{
		public static readonly WorldType[] worldTypes = new WorldType[16];
		public static readonly WorldType DEFAULT = (new WorldType(0, "default", 1)).func_48631_f();
		public static readonly WorldType FLAT = new WorldType(1, "flat");
		public static readonly WorldType DEFAULT_1_1 = (new WorldType(8, "default_1_1", 0)).setCanBeCreated(false);
		private readonly string worldType;
		private readonly int generatorVersion;
		private bool canBeCreated;
		private bool field_48638_h;

		private WorldType(int i1, string string2) : this(i1, string2, 0)
		{
		}

		private WorldType(int i1, string string2, int i3)
		{
			this.worldType = string2;
			this.generatorVersion = i3;
			this.canBeCreated = true;
			worldTypes[i1] = this;
		}

		public virtual string func_48628_a()
		{
			return this.worldType;
		}

		public virtual string TranslateName
		{
			get
			{
				return "generator." + this.worldType;
			}
		}

		public virtual int GeneratorVersion
		{
			get
			{
				return this.generatorVersion;
			}
		}

		public virtual WorldType func_48629_a(int i1)
		{
			return this == DEFAULT && i1 == 0 ? DEFAULT_1_1 : this;
		}

		private WorldType setCanBeCreated(bool z1)
		{
			this.canBeCreated = z1;
			return this;
		}

		public virtual bool CanBeCreated
		{
			get
			{
				return this.canBeCreated;
			}
		}

		private WorldType func_48631_f()
		{
			this.field_48638_h = true;
			return this;
		}

		public virtual bool func_48626_e()
		{
			return this.field_48638_h;
		}

		public static WorldType parseWorldType(string string0)
		{
			for (int i1 = 0; i1 < worldTypes.Length; ++i1)
			{
				if (worldTypes[i1] != null && worldTypes[i1].worldType.Equals(string0, StringComparison.OrdinalIgnoreCase))
				{
					return worldTypes[i1];
				}
			}

			return null;
		}
	}

}