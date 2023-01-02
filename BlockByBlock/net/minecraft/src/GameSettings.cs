using net.minecraft.input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.IO;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class GameSettings
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			keyBindings = new KeyBinding[]{this.keyBindAttack, this.keyBindUseItem, this.keyBindForward, this.keyBindLeft, this.keyBindBack, this.keyBindRight, this.keyBindJump, this.keyBindSneak, this.keyBindDrop, this.keyBindInventory, this.keyBindChat, this.keyBindPlayerList, this.keyBindPickBlock};
		}

		private static readonly string[] RENDER_DISTANCES = new string[]{"options.renderDistance.far", "options.renderDistance.normal", "options.renderDistance.short", "options.renderDistance.tiny"};
		private static readonly string[] DIFFICULTIES = new string[]{"options.difficulty.peaceful", "options.difficulty.easy", "options.difficulty.normal", "options.difficulty.hard"};
		private static readonly string[] GUISCALES = new string[]{"options.guiScale.auto", "options.guiScale.small", "options.guiScale.normal", "options.guiScale.large"};
		private static readonly string[] PARTICLES = new string[]{"options.particles.all", "options.particles.decreased", "options.particles.minimal"};
		private static readonly string[] LIMIT_FRAMERATES = new string[]{"performance.max", "performance.balanced", "performance.powersaver"};
		public float musicVolume = 1.0F;
		public float soundVolume = 1.0F;
		public float mouseSensitivity = 0.5F;
		public bool invertMouse = false;
		public int renderDistance = 0;
		public bool viewBobbing = true;
		public bool advancedOpengl = false;
		public int limitFramerate = 1;
		public bool fancyGraphics = true;
		public bool ambientOcclusion { get; set; } = true;
		public bool clouds = true;
		public string skin = "Default";
		public KeyBinding keyBindForward = new KeyBinding("key.forward", 17);
		public KeyBinding keyBindLeft = new KeyBinding("key.left", 30);
		public KeyBinding keyBindBack = new KeyBinding("key.back", 31);
		public KeyBinding keyBindRight = new KeyBinding("key.right", 32);
		public KeyBinding keyBindJump = new KeyBinding("key.jump", 57);
		public KeyBinding keyBindInventory = new KeyBinding("key.inventory", 18);
		public KeyBinding keyBindDrop = new KeyBinding("key.drop", 16);
		public KeyBinding keyBindChat = new KeyBinding("key.chat", 20);
		public KeyBinding keyBindSneak = new KeyBinding("key.sneak", 42);
		public KeyBinding keyBindAttack = new KeyBinding("key.attack", -100);
		public KeyBinding keyBindUseItem = new KeyBinding("key.use", -99);
		public KeyBinding keyBindPlayerList = new KeyBinding("key.playerlist", 15);
		public KeyBinding keyBindPickBlock = new KeyBinding("key.pickItem", -98);
		public KeyBinding[] keyBindings;
		protected internal Minecraft mc;
		private FileInfo optionsFile;
		public int difficulty = 2;
		public bool hideGUI = false;
		public int thirdPersonView = 0;
		public bool showDebugInfo = false;
		public bool field_50119_G = false;
		public string lastServer = "";
		public bool noclip = false;
		public bool smoothCamera = false;
		public bool debugCamEnable = false;
		public float noclipRate = 1.0F;
		public float debugCamRate = 1.0F;
		public float fovSetting = 0.0F;
		public float gammaSetting = 0.0F;
		public int guiScale = 0;
		public int particleSetting = 0;
		public string language = "en_US";

		public GameSettings(Minecraft minecraft1, DirectoryInfo gameDirectory)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.mc = minecraft1;
			this.optionsFile = new FileInfo(gameDirectory + "/options.txt");
			this.loadOptions();
		}

		public GameSettings()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

		public virtual string getKeyBindingDescription(int i1)
		{
			StringTranslate stringTranslate2 = StringTranslate.Instance;
			return stringTranslate2.translateKey(this.keyBindings[i1].keyDescription);
		}

		public virtual string getOptionDisplayString(int i1)
		{
			int i2 = this.keyBindings[i1].keyCode;
			return getKeyDisplayString(i2);
		}
        
		public static string getKeyDisplayString(int i0)
		{
			return i0 < 0 ? StatCollector.translateToLocalFormatted("key.mouseButton", new object[]{i0 + 101}) : ((KeyCode)i0).ToString();
		}

		public virtual void setKeyBinding(int i1, int i2)
		{
			this.keyBindings[i1].keyCode = i2;
			this.saveOptions();
		}

		public virtual void setOptionFloatValue(EnumOptions enumOptions1, float f2)
		{
			if (enumOptions1 == EnumOptions.MUSIC)
			{
				this.musicVolume = f2;
				this.mc.sndManager.onSoundOptionsChanged();
			}

			if (enumOptions1 == EnumOptions.SOUND)
			{
				this.soundVolume = f2;
				this.mc.sndManager.onSoundOptionsChanged();
			}

			if (enumOptions1 == EnumOptions.SENSITIVITY)
			{
				this.mouseSensitivity = f2;
			}

			if (enumOptions1 == EnumOptions.FOV)
			{
				this.fovSetting = f2;
			}

			if (enumOptions1 == EnumOptions.GAMMA)
			{
				this.gammaSetting = f2;
			}

		}

		public virtual void setOptionValue(EnumOptions enumOptions1, int i2)
		{
			if (enumOptions1 == EnumOptions.INVERT_MOUSE)
			{
				this.invertMouse = !this.invertMouse;
			}

			if (enumOptions1 == EnumOptions.RENDER_DISTANCE)
			{
				this.renderDistance = this.renderDistance + i2 & 3;
			}

			if (enumOptions1 == EnumOptions.GUI_SCALE)
			{
				this.guiScale = this.guiScale + i2 & 3;
			}

			if (enumOptions1 == EnumOptions.PARTICLES)
			{
				this.particleSetting = (this.particleSetting + i2) % 3;
			}

			if (enumOptions1 == EnumOptions.VIEW_BOBBING)
			{
				this.viewBobbing = !this.viewBobbing;
			}

			if (enumOptions1 == EnumOptions.RENDER_CLOUDS)
			{
				this.clouds = !this.clouds;
			}

			if (enumOptions1 == EnumOptions.ADVANCED_OPENGL)
			{
				this.advancedOpengl = !this.advancedOpengl;
				this.mc.renderGlobal.loadRenderers();
			}

			if (enumOptions1 == EnumOptions.FRAMERATE_LIMIT)
			{
				this.limitFramerate = (this.limitFramerate + i2 + 3) % 3;
			}

			if (enumOptions1 == EnumOptions.DIFFICULTY)
			{
				this.difficulty = this.difficulty + i2 & 3;
			}

			if (enumOptions1 == EnumOptions.GRAPHICS)
			{
				this.fancyGraphics = !this.fancyGraphics;
				this.mc.renderGlobal.loadRenderers();
			}

			if (enumOptions1 == EnumOptions.AMBIENT_OCCLUSION)
			{
				this.ambientOcclusion = !this.ambientOcclusion;
				this.mc.renderGlobal.loadRenderers();
			}

			this.saveOptions();
		}

		public virtual float getOptionFloatValue(EnumOptions enumOptions1)
		{
			return enumOptions1 == EnumOptions.FOV ? this.fovSetting : (enumOptions1 == EnumOptions.GAMMA ? this.gammaSetting : (enumOptions1 == EnumOptions.MUSIC ? this.musicVolume : (enumOptions1 == EnumOptions.SOUND ? this.soundVolume : (enumOptions1 == EnumOptions.SENSITIVITY ? this.mouseSensitivity : 0.0F))));
		}

		public virtual bool getOptionOrdinalValue(EnumOptions enumOptions1)
		{
			switch (enumOptions1.innerEnumValue)
			{
			case EnumOptions.InnerEnum.INVERT_MOUSE:
				return invertMouse;
			case EnumOptions.InnerEnum.VIEW_BOBBING:
				return viewBobbing;
			case EnumOptions.InnerEnum.ADVANCED_OPENGL:
				return advancedOpengl;
			case EnumOptions.InnerEnum.AMBIENT_OCCLUSION:
				return ambientOcclusion;
			case EnumOptions.InnerEnum.RENDER_CLOUDS:
				return clouds;
			default:
				return false;
			}
		}

		private static string func_48571_a(string[] string0, int i1)
		{
			if (i1 < 0 || i1 >= string0.Length)
			{
				i1 = 0;
			}

			StringTranslate stringTranslate2 = StringTranslate.Instance;
			return stringTranslate2.translateKey(string0[i1]);
		}

		public virtual string getKeyBinding(EnumOptions enumOptions1)
		{
			StringTranslate stringTranslate2 = StringTranslate.Instance;
			string string3 = stringTranslate2.translateKey(enumOptions1.EnumString) + ": ";
			if (enumOptions1.EnumFloat)
			{
				float f5 = this.getOptionFloatValue(enumOptions1);
				return enumOptions1 == EnumOptions.SENSITIVITY ? (f5 == 0.0F ? string3 + stringTranslate2.translateKey("options.sensitivity.min") : (f5 == 1.0F ? string3 + stringTranslate2.translateKey("options.sensitivity.max") : string3 + (int)(f5 * 200.0F) + "%")) : (enumOptions1 == EnumOptions.FOV ? (f5 == 0.0F ? string3 + stringTranslate2.translateKey("options.fov.min") : (f5 == 1.0F ? string3 + stringTranslate2.translateKey("options.fov.max") : string3 + (int)(70.0F + f5 * 40.0F))) : (enumOptions1 == EnumOptions.GAMMA ? (f5 == 0.0F ? string3 + stringTranslate2.translateKey("options.gamma.min") : (f5 == 1.0F ? string3 + stringTranslate2.translateKey("options.gamma.max") : string3 + "+" + (int)(f5 * 100.0F) + "%")) : (f5 == 0.0F ? string3 + stringTranslate2.translateKey("options.off") : string3 + (int)(f5 * 100.0F) + "%")));
			}
			else if (enumOptions1.EnumBoolean)
			{
				bool z4 = this.getOptionOrdinalValue(enumOptions1);
				return z4 ? string3 + stringTranslate2.translateKey("options.on") : string3 + stringTranslate2.translateKey("options.off");
			}
			else
			{
				return enumOptions1 == EnumOptions.RENDER_DISTANCE ? string3 + func_48571_a(RENDER_DISTANCES, this.renderDistance) : (enumOptions1 == EnumOptions.DIFFICULTY ? string3 + func_48571_a(DIFFICULTIES, this.difficulty) : (enumOptions1 == EnumOptions.GUI_SCALE ? string3 + func_48571_a(GUISCALES, this.guiScale) : (enumOptions1 == EnumOptions.PARTICLES ? string3 + func_48571_a(PARTICLES, this.particleSetting) : (enumOptions1 == EnumOptions.FRAMERATE_LIMIT ? string3 + func_48571_a(LIMIT_FRAMERATES, this.limitFramerate) : (enumOptions1 == EnumOptions.GRAPHICS ? (this.fancyGraphics ? string3 + stringTranslate2.translateKey("options.graphics.fancy") : string3 + stringTranslate2.translateKey("options.graphics.fast")) : string3)))));
			}
		}

		public virtual void loadOptions()
		{
			try
			{
				if (!optionsFile.Exists)
				{
					return;
				}

				StreamReader bufferedReader1 = new StreamReader(new FileStream(optionsFile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
				string string2 = "";

				while (!string.ReferenceEquals((string2 = bufferedReader1.ReadLine()), null))
				{
					try
					{
						string[] string3 = string2.Split(":", true);
						if (string3[0].Equals("music"))
						{
							this.musicVolume = this.parseFloat(string3[1]);
						}

						if (string3[0].Equals("sound"))
						{
							this.soundVolume = this.parseFloat(string3[1]);
						}

						if (string3[0].Equals("mouseSensitivity"))
						{
							this.mouseSensitivity = this.parseFloat(string3[1]);
						}

						if (string3[0].Equals("fov"))
						{
							this.fovSetting = this.parseFloat(string3[1]);
						}

						if (string3[0].Equals("gamma"))
						{
							this.gammaSetting = this.parseFloat(string3[1]);
						}

						if (string3[0].Equals("invertYMouse"))
						{
							this.invertMouse = string3[1].Equals("true");
						}

						if (string3[0].Equals("viewDistance"))
						{
							this.renderDistance = int.Parse(string3[1]);
						}

						if (string3[0].Equals("guiScale"))
						{
							this.guiScale = int.Parse(string3[1]);
						}

						if (string3[0].Equals("particles"))
						{
							this.particleSetting = int.Parse(string3[1]);
						}

						if (string3[0].Equals("bobView"))
						{
							this.viewBobbing = string3[1].Equals("true");
						}

						if (string3[0].Equals("advancedOpengl"))
						{
							this.advancedOpengl = string3[1].Equals("true");
						}

						if (string3[0].Equals("fpsLimit"))
						{
							this.limitFramerate = int.Parse(string3[1]);
						}

						if (string3[0].Equals("difficulty"))
						{
							this.difficulty = int.Parse(string3[1]);
						}

						if (string3[0].Equals("fancyGraphics"))
						{
							this.fancyGraphics = string3[1].Equals("true");
						}

						if (string3[0].Equals("ao"))
						{
							this.ambientOcclusion = string3[1].Equals("true");
						}

						if (string3[0].Equals("clouds"))
						{
							this.clouds = string3[1].Equals("true");
						}

						if (string3[0].Equals("skin"))
						{
							this.skin = string3[1];
						}

						if (string3[0].Equals("lastServer") && string3.Length >= 2)
						{
							this.lastServer = string3[1];
						}

						if (string3[0].Equals("lang") && string3.Length >= 2)
						{
							this.language = string3[1];
						}

						for (int i4 = 0; i4 < this.keyBindings.Length; ++i4)
						{
							if (string3[0].Equals("key_" + this.keyBindings[i4].keyDescription))
							{
								this.keyBindings[i4].keyCode = int.Parse(string3[1]);
							}
						}
					}
					catch (Exception)
					{
						Console.WriteLine("Skipping bad option: " + string2);
					}
				}

				KeyBinding.resetKeyBindingArrayAndHash();
				bufferedReader1.Dispose();
			}
			catch (Exception exception6)
			{
				Console.WriteLine("Failed to load options");
				Console.WriteLine(exception6.ToString());
				Console.Write(exception6.StackTrace);
			}

		}

		private float parseFloat(string string1)
		{
			return string1.Equals("true") ? 1.0F : (string1.Equals("false") ? 0.0F : float.Parse(string1));
		}

		public virtual void saveOptions()
		{
			try
			{
				StreamWriter optionsWriter = new StreamWriter(new FileStream(optionsFile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
				optionsWriter.WriteLine("music:" + this.musicVolume);
				optionsWriter.WriteLine("sound:" + this.soundVolume);
				optionsWriter.WriteLine("invertYMouse:" + this.invertMouse);
				optionsWriter.WriteLine("mouseSensitivity:" + this.mouseSensitivity);
				optionsWriter.WriteLine("fov:" + this.fovSetting);
				optionsWriter.WriteLine("gamma:" + this.gammaSetting);
				optionsWriter.WriteLine("viewDistance:" + this.renderDistance);
				optionsWriter.WriteLine("guiScale:" + this.guiScale);
				optionsWriter.WriteLine("particles:" + this.particleSetting);
				optionsWriter.WriteLine("bobView:" + this.viewBobbing);
				optionsWriter.WriteLine("advancedOpengl:" + this.advancedOpengl);
				optionsWriter.WriteLine("fpsLimit:" + this.limitFramerate);
				optionsWriter.WriteLine("difficulty:" + this.difficulty);
				optionsWriter.WriteLine("fancyGraphics:" + this.fancyGraphics);
				optionsWriter.WriteLine("ao:" + this.ambientOcclusion);
				optionsWriter.WriteLine("clouds:" + this.clouds);
				optionsWriter.WriteLine("skin:" + this.skin);
				optionsWriter.WriteLine("lastServer:" + this.lastServer);
				optionsWriter.WriteLine("lang:" + this.language);

				for (int i2 = 0; i2 < this.keyBindings.Length; ++i2)
				{
					optionsWriter.WriteLine("key_" + this.keyBindings[i2].keyDescription + ":" + this.keyBindings[i2].keyCode);
				}
                
				optionsWriter.Dispose();
			}
			catch (Exception exception3)
			{
				Console.WriteLine("Failed to save options");
				Console.WriteLine(exception3.ToString());
				Console.Write(exception3.StackTrace);
			}

		}

		public virtual bool shouldRenderClouds()
		{
			return this.renderDistance < 2 && this.clouds;
		}
	}

}