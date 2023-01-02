using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumOptions
	{
		public static readonly EnumOptions MUSIC = new EnumOptions("MUSIC", InnerEnum.MUSIC, "options.music", true, false);
		public static readonly EnumOptions SOUND = new EnumOptions("SOUND", InnerEnum.SOUND, "options.sound", true, false);
		public static readonly EnumOptions INVERT_MOUSE = new EnumOptions("INVERT_MOUSE", InnerEnum.INVERT_MOUSE, "options.invertMouse", false, true);
		public static readonly EnumOptions SENSITIVITY = new EnumOptions("SENSITIVITY", InnerEnum.SENSITIVITY, "options.sensitivity", true, false);
		public static readonly EnumOptions FOV = new EnumOptions("FOV", InnerEnum.FOV, "options.fov", true, false);
		public static readonly EnumOptions GAMMA = new EnumOptions("GAMMA", InnerEnum.GAMMA, "options.gamma", true, false);
		public static readonly EnumOptions RENDER_DISTANCE = new EnumOptions("RENDER_DISTANCE", InnerEnum.RENDER_DISTANCE, "options.renderDistance", false, false);
		public static readonly EnumOptions VIEW_BOBBING = new EnumOptions("VIEW_BOBBING", InnerEnum.VIEW_BOBBING, "options.viewBobbing", false, true);
		public static readonly EnumOptions ADVANCED_OPENGL = new EnumOptions("ADVANCED_OPENGL", InnerEnum.ADVANCED_OPENGL, "options.advancedOpengl", false, true);
		public static readonly EnumOptions FRAMERATE_LIMIT = new EnumOptions("FRAMERATE_LIMIT", InnerEnum.FRAMERATE_LIMIT, "options.framerateLimit", false, false);
		public static readonly EnumOptions DIFFICULTY = new EnumOptions("DIFFICULTY", InnerEnum.DIFFICULTY, "options.difficulty", false, false);
		public static readonly EnumOptions GRAPHICS = new EnumOptions("GRAPHICS", InnerEnum.GRAPHICS, "options.graphics", false, false);
		public static readonly EnumOptions AMBIENT_OCCLUSION = new EnumOptions("AMBIENT_OCCLUSION", InnerEnum.AMBIENT_OCCLUSION, "options.ao", false, true);
		public static readonly EnumOptions GUI_SCALE = new EnumOptions("GUI_SCALE", InnerEnum.GUI_SCALE, "options.guiScale", false, false);
		public static readonly EnumOptions RENDER_CLOUDS = new EnumOptions("RENDER_CLOUDS", InnerEnum.RENDER_CLOUDS, "options.renderClouds", false, true);
		public static readonly EnumOptions PARTICLES = new EnumOptions("PARTICLES", InnerEnum.PARTICLES, "options.particles", false, false);

		private static readonly List<EnumOptions> valueList = new List<EnumOptions>();

		static EnumOptions()
		{
			valueList.Add(MUSIC);
			valueList.Add(SOUND);
			valueList.Add(INVERT_MOUSE);
			valueList.Add(SENSITIVITY);
			valueList.Add(FOV);
			valueList.Add(GAMMA);
			valueList.Add(RENDER_DISTANCE);
			valueList.Add(VIEW_BOBBING);;
			valueList.Add(ADVANCED_OPENGL);
			valueList.Add(FRAMERATE_LIMIT);
			valueList.Add(DIFFICULTY);
			valueList.Add(GRAPHICS);
			valueList.Add(AMBIENT_OCCLUSION);
			valueList.Add(GUI_SCALE);
			valueList.Add(RENDER_CLOUDS);
			valueList.Add(PARTICLES);
		}

		public enum InnerEnum
		{
			MUSIC,
			SOUND,
			INVERT_MOUSE,
			SENSITIVITY,
			FOV,
			GAMMA,
			RENDER_DISTANCE,
			VIEW_BOBBING,
			ADVANCED_OPENGL,
			FRAMERATE_LIMIT,
			DIFFICULTY,
			GRAPHICS,
			AMBIENT_OCCLUSION,
			GUI_SCALE,
			RENDER_CLOUDS,
			PARTICLES
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private readonly bool enumFloat;
		private readonly bool enumBoolean;
		private readonly string enumString;

		public static EnumOptions getEnumOptions(int i0)
		{
			EnumOptions[] enumOptions1 = values();
			int i2 = enumOptions1.Length;

			for (int i3 = 0; i3 < i2; ++i3)
			{
				EnumOptions enumOptions4 = enumOptions1[i3];
				if (enumOptions4.returnEnumOrdinal() == i0)
				{
					return enumOptions4;
				}
			}

			return null;
		}

		private EnumOptions(string name, InnerEnum innerEnum, string string3, bool z4, bool z5)
		{
			this.enumString = string3;
			this.enumFloat = z4;
			this.enumBoolean = z5;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public bool EnumFloat
		{
			get
			{
				return this.enumFloat;
			}
		}

		public bool EnumBoolean
		{
			get
			{
				return this.enumBoolean;
			}
		}

		public int returnEnumOrdinal()
		{
			return this.ordinal();
		}

		public string EnumString
		{
			get
			{
				return this.enumString;
			}
		}

		public static EnumOptions[] values()
		{
			return valueList.ToArray();
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static EnumOptions valueOf(string name)
		{
			foreach (EnumOptions enumInstance in EnumOptions.valueList)
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}