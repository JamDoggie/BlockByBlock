using BlockByBlock.helpers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace net.minecraft.client
{

	using AchievementList = net.minecraft.src.AchievementList;
	using AnvilSaveConverter = net.minecraft.src.AnvilSaveConverter;
	using AxisAlignedBB = net.minecraft.src.AxisAlignedBB;
	using Block = net.minecraft.src.Block;
	using ChunkCoordinates = net.minecraft.src.ChunkCoordinates;
	using ChunkProviderLoadOrGenerate = net.minecraft.src.ChunkProviderLoadOrGenerate;
	using ColorizerFoliage = net.minecraft.src.ColorizerFoliage;
	using ColorizerGrass = net.minecraft.src.ColorizerGrass;
	using ColorizerWater = net.minecraft.src.ColorizerWater;
	using EffectRenderer = net.minecraft.src.EffectRenderer;
	using EntityClientPlayerMP = net.minecraft.src.EntityClientPlayerMP;
	using EntityLiving = net.minecraft.src.EntityLiving;
	using EntityPlayer = net.minecraft.src.EntityPlayer;
	using EntityPlayerSP = net.minecraft.src.EntityPlayerSP;
	using EntityRenderer = net.minecraft.src.EntityRenderer;
	using EnumMovingObjectType = net.minecraft.src.EnumMovingObjectType;
	using EnumOS2 = net.minecraft.src.EnumOS2;
	using EnumOSMappingHelper = net.minecraft.src.EnumOSMappingHelper;
	using EnumOptions = net.minecraft.src.EnumOptions;
	using FontRenderer = net.minecraft.src.FontRenderer;
	using GLAllocation = net.minecraft.src.GLAllocation;
	using GameSettings = net.minecraft.src.GameSettings;
	using GameWindowListener = net.minecraft.src.GameWindowListener;
	using GuiAchievement = net.minecraft.src.GuiAchievement;
	using GuiChat = net.minecraft.src.GuiChat;
	using GuiConflictWarning = net.minecraft.src.GuiConflictWarning;
	using GuiConnecting = net.minecraft.src.GuiConnecting;
	using GuiErrorScreen = net.minecraft.src.GuiErrorScreen;
	using GuiGameOver = net.minecraft.src.GuiGameOver;
	using GuiIngame = net.minecraft.src.GuiIngame;
	using GuiIngameMenu = net.minecraft.src.GuiIngameMenu;
	using GuiInventory = net.minecraft.src.GuiInventory;
	using GuiMainMenu = net.minecraft.src.GuiMainMenu;
	using GuiMemoryErrorScreen = net.minecraft.src.GuiMemoryErrorScreen;
	using GuiScreen = net.minecraft.src.GuiScreen;
	using GuiSleepMP = net.minecraft.src.GuiSleepMP;
	using IChunkProvider = net.minecraft.src.IChunkProvider;
	using ISaveFormat = net.minecraft.src.ISaveFormat;
	using ISaveHandler = net.minecraft.src.ISaveHandler;
	using Item = net.minecraft.src.Item;
	using ItemBlock = net.minecraft.src.ItemBlock;
	using ItemRenderer = net.minecraft.src.ItemRenderer;
	using ItemStack = net.minecraft.src.ItemStack;
	using KeyBinding = net.minecraft.src.KeyBinding;
	using LoadingScreenRenderer = net.minecraft.src.LoadingScreenRenderer;
	using MathHelper = net.minecraft.src.MathHelper;
	using MinecraftError = net.minecraft.src.MinecraftError;
	using MinecraftException = net.minecraft.src.MinecraftException;
	using MinecraftImpl = net.minecraft.src.MinecraftImpl;
	using ModelBiped = net.minecraft.src.ModelBiped;
	using MouseHelper = net.minecraft.src.MouseHelper;
	using MovementInputFromOptions = net.minecraft.src.MovementInputFromOptions;
	using MovingObjectPosition = net.minecraft.src.MovingObjectPosition;
	using NetClientHandler = net.minecraft.src.NetClientHandler;
	using OpenGlCapsChecker = net.minecraft.src.OpenGlCapsChecker;
	using OpenGlHelper = net.minecraft.src.OpenGlHelper;
	using Packet3Chat = net.minecraft.src.Packet3Chat;
	using PlayerController = net.minecraft.src.PlayerController;
	using PlayerUsageSnooper = net.minecraft.src.PlayerUsageSnooper;
	using Profiler = net.minecraft.src.Profiler;
	using ProfilerResult = net.minecraft.src.ProfilerResult;
	using RenderBlocks = net.minecraft.src.RenderBlocks;
	using RenderEngine = net.minecraft.src.RenderEngine;
	using RenderGlobal = net.minecraft.src.RenderGlobal;
	using RenderManager = net.minecraft.src.RenderManager;
	using ScaledResolution = net.minecraft.src.ScaledResolution;
	using ScreenShotHelper = net.minecraft.src.ScreenShotHelper;
	using Session = net.minecraft.src.Session;
	using SoundManager = net.minecraft.src.SoundManager;
	using StatCollector = net.minecraft.src.StatCollector;
	using StatFileWriter = net.minecraft.src.StatFileWriter;
	using StatList = net.minecraft.src.StatList;
	using StatStringFormatKeyInv = net.minecraft.src.StatStringFormatKeyInv;
	using StringTranslate = net.minecraft.src.StringTranslate;
	using Teleporter = net.minecraft.src.Teleporter;
	using Tessellator = net.minecraft.src.Tessellator;
	using TextureCompassFX = net.minecraft.src.TextureCompassFX;
	using TextureFlamesFX = net.minecraft.src.TextureFlamesFX;
	using TextureLavaFX = net.minecraft.src.TextureLavaFX;
	using TextureLavaFlowFX = net.minecraft.src.TextureLavaFlowFX;
	using TexturePackList = net.minecraft.src.TexturePackList;
	using TexturePortalFX = net.minecraft.src.TexturePortalFX;
	using TextureWatchFX = net.minecraft.src.TextureWatchFX;
	using TextureWaterFX = net.minecraft.src.TextureWaterFX;
	using TextureWaterFlowFX = net.minecraft.src.TextureWaterFlowFX;
	using ThreadCheckHasPaid = net.minecraft.src.ThreadCheckHasPaid;
	using ThreadClientSleep = net.minecraft.src.ThreadClientSleep;
	using ThreadDownloadResources = net.minecraft.src.ThreadDownloadResources;
	using Timer = net.minecraft.src.Timer;
	using UnexpectedThrowable = net.minecraft.src.UnexpectedThrowable;
	using Vec3D = net.minecraft.src.Vec3D;
	using World = net.minecraft.src.World;
	using WorldProvider = net.minecraft.src.WorldProvider;
	using WorldRenderer = net.minecraft.src.WorldRenderer;
	using WorldSettings = net.minecraft.src.WorldSettings;
	using WorldType = net.minecraft.src.WorldType;

	using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    public abstract class Minecraft
	{
		private bool InstanceFieldsInitialized = false;
		
		private void InitializeInstanceFields()
		{
			guiAchievement = new GuiAchievement(this);
		}

		public static sbyte[] field_28006_b = new sbyte[10485760];
		private static Minecraft theMinecraft;
		public PlayerController playerController;
		private bool fullscreen = false;
		private bool hasCrashed = false;
		public int displayWidth;
		public int displayHeight;
		private OpenGlCapsChecker glCapabilities;
		private Timer timer = new Timer(20.0F);
		public World theWorld;
		public RenderGlobal renderGlobal;
		public EntityPlayerSP thePlayer;
		public EntityLiving renderViewEntity;
		public EffectRenderer effectRenderer;
		public Session session = null;
		public string minecraftUri;
		public bool hideQuitButton = false;
		public volatile bool isGamePaused = false;
		public RenderEngine renderEngine;
		public FontRenderer fontRenderer;
		public FontRenderer standardGalacticFontRenderer;
		public GuiScreen currentScreen = null;
		public LoadingScreenRenderer loadingScreen;
		public EntityRenderer entityRenderer;
		private ThreadDownloadResources downloadResourcesThread;
		private int ticksRan = 0;
		private int leftClickCounter = 0;
		private int tempDisplayWidth;
		private int tempDisplayHeight;
		public GuiAchievement guiAchievement;
		public GuiIngame ingameGUI;
		public bool skipRenderWorld = false;
		public ModelBiped playerModelBiped = new ModelBiped(0.0F);
		public MovingObjectPosition objectMouseOver = null;
		public GameSettings gameSettings;
		protected internal MinecraftApplet mcApplet;
		public SoundManager sndManager = new SoundManager();
		public MouseHelper mouseHelper;
		public TexturePackList texturePackList;
		public DirectoryInfo mcDataDir;
		private ISaveFormat saveLoader;
		public static long[] frameTimes = new long[512];
		public static long[] tickTimes = new long[512];
		public static int numRecordedFrameTimes = 0;
		public static long hasPaidCheckTime = 0L;
		private int rightClickDelayTimer = 0;
		public StatFileWriter statFileWriter;
		private string serverName;
		private int serverPort;
		private TextureWaterFX textureWaterFX = new TextureWaterFX();
		private TextureLavaFX textureLavaFX = new TextureLavaFX();
		private static DirectoryInfo minecraftDir = null;
		public volatile bool running = true;
		public string debug = "";
		internal long debugUpdateTime = DateTimeHelper.CurrentUnixTimeMillis();
		internal int fpsCounter = 0;
		internal bool isTakingScreenshot = false;
		internal long prevFrameTime = -1L;
		private string debugProfilerName = "root";
		public bool inGameHasFocus = false;
		public bool isRaining = false;
		internal long systemTime = DateTimeHelper.CurrentUnixTimeMillis();
		private int joinPlayerCounter = 0;

		public float MouseX => mcApplet.MousePosition.X;
		public float MouseY => mcApplet.MousePosition.Y;


		public Minecraft(NativeWindow window, MinecraftApplet minecraftApplet3, int displayWidth, int displayHeight, bool fullscreen)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			StatList.func_27360_a();
			this.tempDisplayHeight = displayHeight;
			this.fullscreen = fullscreen;
			this.mcApplet = minecraftApplet3;
			Packet3Chat.maxMessageLength = 32767;
			new ThreadClientSleep(this, "Timer hack thread");
			this.displayWidth = displayWidth;
			this.displayHeight = displayHeight;
			this.fullscreen = fullscreen;

			// PORTING TODO:
			//if (minecraftApplet3 == null || "true".Equals(minecraftApplet3.getParameter("stand-alone")))
			{
				this.hideQuitButton = false;
			}

			theMinecraft = this;
		}

		public virtual void onMinecraftCrash(UnexpectedThrowable unexpectedThrowable1)
		{
			this.hasCrashed = true;
			this.displayUnexpectedThrowable(unexpectedThrowable1);
		}

		public abstract void displayUnexpectedThrowable(UnexpectedThrowable unexpectedThrowable1);

		public virtual void setServer(string string1, int i2)
		{
			this.serverName = string1;
			this.serverPort = i2;
		}
        
		public virtual void startGame()
		{
			if (this.mcApplet != null)
			{
				GL.ClearColor(Color4.Black);

				if (this.fullscreen)
				{
					mcApplet.WindowState = WindowState.Fullscreen;
					this.displayWidth = Monitors.GetMonitorFromWindow(mcApplet).HorizontalResolution;
					this.displayHeight = Monitors.GetMonitorFromWindow(mcApplet).VerticalResolution;
					if (this.displayWidth <= 0)
					{
						this.displayWidth = 1;
					}

					if (this.displayHeight <= 0)
					{
						this.displayHeight = 1;
					}
				}
			}
            
			mcApplet.Title = "Minecraft 1.2.5";

			OpenGlHelper.initializeTextures();
			this.mcDataDir = MinecraftDir;
			this.saveLoader = new AnvilSaveConverter(new DirectoryInfo(mcDataDir + "/saves"));
			this.gameSettings = new GameSettings(this, this.mcDataDir);
			this.texturePackList = new TexturePackList(this, this.mcDataDir);
			this.renderEngine = new RenderEngine(this.texturePackList, this.gameSettings);
			this.loadScreen();
			this.fontRenderer = new FontRenderer(this.gameSettings, "/font/default.png", this.renderEngine, false);
			this.standardGalacticFontRenderer = new FontRenderer(this.gameSettings, "/font/alternate.png", this.renderEngine, false);
			if (!string.ReferenceEquals(this.gameSettings.language, null))
			{
				StringTranslate.Instance.Language = this.gameSettings.language;
				this.fontRenderer.UnicodeFlag = StringTranslate.Instance.Unicode;
				this.fontRenderer.BidiFlag = StringTranslate.isBidrectional(this.gameSettings.language);
			}

			ColorizerWater.WaterBiomeColorizer = this.renderEngine.getTextureContents("/misc/watercolor.png");
			ColorizerGrass.GrassBiomeColorizer = this.renderEngine.getTextureContents("/misc/grasscolor.png");
			ColorizerFoliage.getFoilageBiomeColorizer(this.renderEngine.getTextureContents("/misc/foliagecolor.png"));
			this.entityRenderer = new EntityRenderer(this);
			RenderManager.instance.itemRenderer = new ItemRenderer(this);
			this.statFileWriter = new StatFileWriter(this.session, this.mcDataDir);
			AchievementList.openInventory.setStatStringFormatter(new StatStringFormatKeyInv(this));
			this.loadScreen();
			this.mouseHelper = new MouseHelper(mcApplet);

			func_52004_D();
			this.checkGLError("Pre startup");
			GL.Enable(EnableCap.Texture2D);
			GL.ShadeModel(ShadingModel.Smooth);
			GL.ClearDepth(1.0D);
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);
			GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0.1F);
			GL.CullFace(CullFaceMode.Back);
			GL.MatrixMode(MatrixMode.Projection);

			GL.LoadIdentity();
			GL.MatrixMode(MatrixMode.Modelview);
			checkGLError("Startup");
			glCapabilities = new OpenGlCapsChecker();
			sndManager.loadSoundSettings(this.gameSettings);
			renderEngine.registerTextureFX(this.textureLavaFX);
			renderEngine.registerTextureFX(this.textureWaterFX);
			renderEngine.registerTextureFX(new TexturePortalFX());
			renderEngine.registerTextureFX(new TextureCompassFX(this));
			renderEngine.registerTextureFX(new TextureWatchFX(this));
			renderEngine.registerTextureFX(new TextureWaterFlowFX());
			renderEngine.registerTextureFX(new TextureLavaFlowFX());
			renderEngine.registerTextureFX(new TextureFlamesFX(0));
			renderEngine.registerTextureFX(new TextureFlamesFX(1));
			renderGlobal = new RenderGlobal(this, this.renderEngine);
			GL.Viewport(0, 0, this.displayWidth, this.displayHeight);
			effectRenderer = new EffectRenderer(this.theWorld, this.renderEngine);

			try
			{
				this.downloadResourcesThread = new ThreadDownloadResources(this.mcDataDir, this);
				this.downloadResourcesThread.Start();
			}
			catch (Exception)
			{
			}

			this.checkGLError("Post startup");
			this.ingameGUI = new GuiIngame(this);
			if (!string.ReferenceEquals(this.serverName, null))
			{
				this.displayGuiScreen(new GuiConnecting(this, this.serverName, this.serverPort));
			}
			else
			{
				this.displayGuiScreen(new GuiMainMenu());
			}

			this.loadingScreen = new LoadingScreenRenderer(this);
		}
        
		private void loadScreen()
		{
			ScaledResolution scaledResolution1 = new ScaledResolution(this.gameSettings, this.displayWidth, this.displayHeight);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0D, scaledResolution1.scaledWidthD, scaledResolution1.scaledHeightD, 0.0D, 1000.0D, 3000.0D);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate(0.0F, 0.0F, -2000.0F);
			GL.Viewport(0, 0, this.displayWidth, this.displayHeight);
			GL.ClearColor(0.0F, 0.0F, 0.0F, 0.0F);
			Tessellator tessellator2 = Tessellator.instance;
			GL.Disable(EnableCap.Lighting);
			GL.Enable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Fog);
			GL.BindTexture(TextureTarget.Texture2D, renderEngine.getTexture("/title/mojang.png"));
			tessellator2.startDrawingQuads();
			tessellator2.ColorOpaque_I = 0xFFFFFF;
			tessellator2.addVertexWithUV(0.0D, (double)this.displayHeight, 0.0D, 0.0D, 0.0D);
			tessellator2.addVertexWithUV((double)this.displayWidth, (double)this.displayHeight, 0.0D, 0.0D, 0.0D);
			tessellator2.addVertexWithUV((double)this.displayWidth, 0.0D, 0.0D, 0.0D, 0.0D);
			tessellator2.addVertexWithUV(0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
			tessellator2.draw();
			short s3 = 256;
			short s4 = 256;
			GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
			tessellator2.ColorOpaque_I = 0xFFFFFF;
			this.scaledTessellator((scaledResolution1.ScaledWidth - s3) / 2, (scaledResolution1.ScaledHeight - s4) / 2, 0, 0, s3, s4);
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Fog);
			GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0.1F);
			mcApplet.Context.SwapBuffers(); // PORTING TODO: pretty sure this is correct, but might be wrong.
		}

		public virtual void scaledTessellator(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			float f7 = 0.00390625F;
			float f8 = 0.00390625F;
			Tessellator tessellator9 = Tessellator.instance;
			tessellator9.startDrawingQuads();
			tessellator9.addVertexWithUV((double)(i1 + 0), (double)(i2 + i6), 0.0D, (double)((float)(i3 + 0) * f7), (double)((float)(i4 + i6) * f8));
			tessellator9.addVertexWithUV((double)(i1 + i5), (double)(i2 + i6), 0.0D, (double)((float)(i3 + i5) * f7), (double)((float)(i4 + i6) * f8));
			tessellator9.addVertexWithUV((double)(i1 + i5), (double)(i2 + 0), 0.0D, (double)((float)(i3 + i5) * f7), (double)((float)(i4 + 0) * f8));
			tessellator9.addVertexWithUV((double)(i1 + 0), (double)(i2 + 0), 0.0D, (double)((float)(i3 + 0) * f7), (double)((float)(i4 + 0) * f8));
			tessellator9.draw();
		}

		public static DirectoryInfo MinecraftDir
		{
			get
			{
				if (minecraftDir == null)
				{
					minecraftDir = getAppDir("minecraft");
				}
    
				return minecraftDir;
			}
		}

		public static DirectoryInfo getAppDir(string string0)
		{
			string string1 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			DirectoryInfo file2;
			switch (EnumOSMappingHelper.enumOSMappingArray[(int)Os])
			{
			case 1:
			case 2:
				file2 = new DirectoryInfo(string1 + '/' + '.' + string0 + '/');
				break;
			case 3:
				string? string3 = Environment.GetEnvironmentVariable("APPDATA");
				if (string3 != null)
				{
					file2 = new DirectoryInfo(string3 + "/." + string0 + '/');
				}
				else
				{
					file2 = new DirectoryInfo(string1 + '/' + '.' + string0 + '/');
				}
				break;
			case 4:
				file2 = new DirectoryInfo(string1 + "/Library/Application Support/" + string0);
				break;
			default:
				file2 = new DirectoryInfo(string1 + '/' + string0 + '/');
			break;
			}

			if (!file2.Exists)
			{
				file2.Create();

				if (!file2.Exists)
					throw new Exception("The working directory could not be created: " + file2);
				else
					return file2;
			}
			else
			{
				return file2;
			}
		}

		private static EnumOS2 Os
		{
			get
			{
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return EnumOS2.windows;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return EnumOS2.macos;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return EnumOS2.linux;
                }
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                {
					return EnumOS2.freebsd;
                }
                else
                {
                    return EnumOS2.unknown;
                }
            }
        }

		public virtual ISaveFormat SaveLoader
		{
			get
			{
				return this.saveLoader;
			}
		}

		public virtual void displayGuiScreen(GuiScreen guiScreen1)
		{
			if (!(this.currentScreen is GuiErrorScreen))
			{
				if (this.currentScreen != null)
				{
					this.currentScreen.onGuiClosed();
				}

				if (guiScreen1 is GuiMainMenu)
				{
					this.statFileWriter.func_27175_b();
				}

				this.statFileWriter.syncStats();
				if (guiScreen1 == null && this.theWorld == null)
				{
					guiScreen1 = new GuiMainMenu();
				}
				else if (guiScreen1 == null && this.thePlayer.Health <= 0)
				{
					guiScreen1 = new GuiGameOver();
				}

				if (guiScreen1 is GuiMainMenu)
				{
					this.gameSettings.showDebugInfo = false;
					this.ingameGUI.clearChatMessages();
				}

				this.currentScreen = (GuiScreen)guiScreen1;
				if (guiScreen1 != null)
				{
					this.setIngameNotInFocus();
					ScaledResolution scaledResolution2 = new ScaledResolution(this.gameSettings, this.displayWidth, this.displayHeight);
					int i3 = scaledResolution2.ScaledWidth;
					int i4 = scaledResolution2.ScaledHeight;
					((GuiScreen)guiScreen1).setWorldAndResolution(this, i3, i4);
					this.skipRenderWorld = false;
				}
				else
				{
					this.setIngameFocus();
				}

			}
		}

		private void checkGLError(string string1)
		{
			int i2 = (int)GLFW.GetError(out string error);
			
			if (i2 != 0)
			{
				Console.WriteLine("########## GL ERROR ##########");
				Console.WriteLine("@ " + string1);
				Console.WriteLine(i2 + ": " + error);
			}

		}

		public virtual void shutdownMinecraftApplet()
		{
			try
			{
				this.statFileWriter.func_27175_b();
				this.statFileWriter.syncStats();
				if (this.mcApplet != null)
				{
					this.mcApplet.clearApplet();
				}

				try
				{
					if (this.downloadResourcesThread != null)
					{
						this.downloadResourcesThread.closeMinecraft();
					}
				}
				catch (Exception)
				{
				}

				Console.WriteLine("Stopping!");

				try
				{
					this.changeWorld1((World)null);
				}
				catch (Exception)
				{
				}

				try
				{
					GLAllocation.deleteTexturesAndDisplayLists();
				}
				catch (Exception)
				{
				}

				this.sndManager.closeMinecraft();
			}
			finally
			{
				if (!this.hasCrashed)
				{
					Environment.Exit(0);
				}

			}

			System.GC.Collect();
		}

		public virtual void run()
		{
			this.running = true;

			try
			{
				this.startGame();
			}
			catch (Exception exception11)
			{
				Console.WriteLine(exception11.ToString());
				Console.Write(exception11.StackTrace);
				this.onMinecraftCrash(new UnexpectedThrowable("Failed to start game", exception11));
				return;
			}

			try
			{
				while (this.running)
				{
					try
					{
						this.runGameLoop();
					}
					catch (MinecraftException)
					{
						this.theWorld = null;
						this.changeWorld1((World)null);
						this.displayGuiScreen(new GuiConflictWarning());
					}
					catch (System.OutOfMemoryException)
					{
						this.freeMemory();
						this.displayGuiScreen(new GuiMemoryErrorScreen());
						System.GC.Collect(); // PORTING TODO: Come back to this, I'm pretty sure there's a better way to do GC collections.
					}
				}
			}
			catch (MinecraftError)
			{
			}
			catch (Exception throwable13)
			{
				this.freeMemory();
				Console.WriteLine(throwable13.ToString());
				Console.Write(throwable13.StackTrace);
				this.onMinecraftCrash(new UnexpectedThrowable("Unexpected error", throwable13));
			}
			finally
			{
				this.shutdownMinecraftApplet();
			}

		}

		private void runGameLoop()
		{
			if (this.mcApplet != null && !mcApplet.Exists)
			{
				this.running = false;
			}
			else
			{
				AxisAlignedBB.clearBoundingBoxPool();
				Vec3D.initialize();
				Profiler.startSection("root");
				if (mcApplet.IsExiting)
				{
					this.shutdown();
				}

				if (this.isGamePaused && this.theWorld != null)
				{
					float f1 = this.timer.renderPartialTicks;
					this.timer.updateTimer();
					this.timer.renderPartialTicks = f1;
				}
				else
				{
					this.timer.updateTimer();
				}

				long j6 = JTime.NanoTime();
				Profiler.startSection("tick");

				for (int i3 = 0; i3 < this.timer.elapsedTicks; ++i3)
				{
					++this.ticksRan;

					try
					{
						this.runTick();
					}
					catch (MinecraftException)
					{
						this.theWorld = null;
						this.changeWorld1((World)null);
						this.displayGuiScreen(new GuiConflictWarning());
					}
				}

				Profiler.endSection();
				long j7 = JTime.NanoTime() - j6;
				this.checkGLError("Pre render");
				RenderBlocks.fancyGrass = this.gameSettings.fancyGraphics;
				Profiler.startSection("sound");
				this.sndManager.setListener(this.thePlayer, this.timer.renderPartialTicks);
				Profiler.endStartSection("updatelights");
				if (this.theWorld != null)
				{
					this.theWorld.updatingLighting();
				}

				Profiler.endSection();
				Profiler.startSection("render");
				Profiler.startSection("display");
				GL.Enable(EnableCap.Texture2D);
				//if (!Keyboard.isKeyDown(Keyboard.KEY_F7))
				//{
				//	Display.update();
				//} // PORTING TODO: Not sure what this is for

				if (this.thePlayer != null && this.thePlayer.EntityInsideOpaqueBlock)
				{
					this.gameSettings.thirdPersonView = 0;
				}

				Profiler.endSection();
				if (!this.skipRenderWorld)
				{
					Profiler.startSection("gameMode");
					if (this.playerController != null)
					{
						this.playerController.PartialTime = this.timer.renderPartialTicks;
					}

					Profiler.endStartSection("gameRenderer");
					this.entityRenderer.updateCameraAndRender(this.timer.renderPartialTicks);
					Profiler.endSection();
				}

				GL.Flush();
				Profiler.endSection();
				if (!mcApplet.IsFocused && this.fullscreen)
				{
					this.toggleFullscreen();
				}

				Profiler.endSection();
				if (this.gameSettings.showDebugInfo && this.gameSettings.field_50119_G)
				{
					if (!Profiler.profilingEnabled)
					{
						Profiler.clearProfiling();
					}

					Profiler.profilingEnabled = true;
					this.displayDebugInfo(j7);
				}
				else
				{
					Profiler.profilingEnabled = false;
					this.prevFrameTime = JTime.NanoTime();
				}

				this.guiAchievement.updateAchievementWindow();
				Profiler.startSection("root");
				Thread.Yield();
				/*if (Keyboard.isKeyDown(Keyboard.KEY_F7))
				{
					Display.update();
				}*/ // PORTING TODO

				this.screenshotListener();
				if (mcApplet != null && !this.fullscreen && (mcApplet.Size.X != this.displayWidth || mcApplet.Size.Y != this.displayHeight))
				{
					this.displayWidth = mcApplet.Size.X;
					this.displayHeight = mcApplet.Size.Y;
					if (this.displayWidth <= 0)
					{
						this.displayWidth = 1;
					}

					if (this.displayHeight <= 0)
					{
						this.displayHeight = 1;
					}

					this.resize(this.displayWidth, this.displayHeight);
				}

				this.checkGLError("Post render");
				++this.fpsCounter;

				for (this.isGamePaused = !this.MultiplayerWorld && this.currentScreen != null && this.currentScreen.doesGuiPauseGame(); DateTimeHelper.CurrentUnixTimeMillis() >= this.debugUpdateTime + 1000L; this.fpsCounter = 0)
				{
					this.debug = this.fpsCounter + " fps, " + WorldRenderer.chunksUpdated + " chunk updates";
					WorldRenderer.chunksUpdated = 0;
					this.debugUpdateTime += 1000L;
				}

				Profiler.endSection();
			}
		}

		public virtual void freeMemory()
		{
			try
			{
				field_28006_b = new sbyte[0];
				this.renderGlobal.func_28137_f();
			}
			catch (Exception)
			{
			}

			try
			{
				System.GC.Collect();
				AxisAlignedBB.clearBoundingBoxes();
				Vec3D.clearVectorList();
			}
			catch (Exception)
			{
			}

			try
			{
				System.GC.Collect();
				this.changeWorld1((World)null);
			}
			catch (Exception)
			{
			}

			System.GC.Collect();
		}

		private void screenshotListener()
		{
			if (mcApplet.KeyboardState.IsKeyPressed(Keys.F2))
			{
				if (!this.isTakingScreenshot)
				{
					this.isTakingScreenshot = true;
					this.ingameGUI.addChatMessage(ScreenShotHelper.saveScreenshot(minecraftDir, this.displayWidth, this.displayHeight));
				}
			}
			else
			{
				this.isTakingScreenshot = false;
			}

		}

		private void updateDebugProfilerName(int i1)
		{
			System.Collections.IList list2 = Profiler.getProfilingData(this.debugProfilerName);
			if (list2 != null && list2.Count != 0)
			{
				ProfilerResult profilerResult3 = (ProfilerResult)list2.RemoveAndReturn(0);
				if (i1 == 0)
				{
					if (profilerResult3.name.Length > 0)
					{
						int i4 = this.debugProfilerName.LastIndexOf(".", StringComparison.Ordinal);
						if (i4 >= 0)
						{
							this.debugProfilerName = this.debugProfilerName.Substring(0, i4);
						}
					}
				}
				else
				{
					--i1;
					if (i1 < list2.Count && !((ProfilerResult)list2[i1]).name.Equals("unspecified"))
					{
						if (this.debugProfilerName.Length > 0)
						{
							this.debugProfilerName = this.debugProfilerName + ".";
						}

						this.debugProfilerName = this.debugProfilerName + ((ProfilerResult)list2[i1]).name;
					}
				}

			}
		}

		private void displayDebugInfo(long j1)
		{
			System.Collections.IList list3 = Profiler.getProfilingData(this.debugProfilerName);
			ProfilerResult profilerResult4 = (ProfilerResult)list3.RemoveAndReturn(0);
			long j5 = 16666666L;
			if (this.prevFrameTime == -1L)
			{
				this.prevFrameTime = JTime.NanoTime();
			}

			long j7 = JTime.NanoTime();
			tickTimes[numRecordedFrameTimes & frameTimes.Length - 1] = j1;
			frameTimes[numRecordedFrameTimes++ & frameTimes.Length - 1] = j7 - this.prevFrameTime;
			this.prevFrameTime = j7;
			GL.Clear(ClearBufferMask.DepthBufferBit);
			GL.MatrixMode(MatrixMode.Projection);
			GL.Enable(EnableCap.ColorMaterial);
			GL.LoadIdentity();
			GL.Ortho(0.0D, displayWidth, displayHeight, 0.0D, 1000.0D, 3000.0D);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate(0.0F, 0.0F, -2000.0F);
			GL.LineWidth(1.0F);
			GL.Disable(EnableCap.Texture2D);
			Tessellator tessellator9 = Tessellator.instance;
			tessellator9.startDrawing(7);
			int i10 = (int)(j5 / 200000L);
			tessellator9.ColorOpaque_I = 536870912;
			tessellator9.addVertex(0.0D, (double)(this.displayHeight - i10), 0.0D);
			tessellator9.addVertex(0.0D, (double)this.displayHeight, 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)this.displayHeight, 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)(this.displayHeight - i10), 0.0D);
			tessellator9.ColorOpaque_I = 0x20200000;
			tessellator9.addVertex(0.0D, (double)(this.displayHeight - i10 * 2), 0.0D);
			tessellator9.addVertex(0.0D, (double)(this.displayHeight - i10), 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)(this.displayHeight - i10), 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)(this.displayHeight - i10 * 2), 0.0D);
			tessellator9.draw();
			long j11 = 0L;

			int i13;
			for (i13 = 0; i13 < frameTimes.Length; ++i13)
			{
				j11 += frameTimes[i13];
			}

			i13 = (int)(j11 / 200000L / (long)frameTimes.Length);
			tessellator9.startDrawing(7);
			tessellator9.ColorOpaque_I = 0x20400000;
			tessellator9.addVertex(0.0D, (double)(this.displayHeight - i13), 0.0D);
			tessellator9.addVertex(0.0D, (double)this.displayHeight, 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)this.displayHeight, 0.0D);
			tessellator9.addVertex((double)frameTimes.Length, (double)(this.displayHeight - i13), 0.0D);
			tessellator9.draw();
			tessellator9.startDrawing(1);

			int i15;
			int i16;
			for (int i14 = 0; i14 < frameTimes.Length; ++i14)
			{
				i15 = (i14 - numRecordedFrameTimes & frameTimes.Length - 1) * 255 / frameTimes.Length;
				i16 = i15 * i15 / 255;
				i16 = i16 * i16 / 255;
				int i17 = i16 * i16 / 255;
				i17 = i17 * i17 / 255;
				if (frameTimes[i14] > j5)
				{
					tessellator9.ColorOpaque_I = (int)unchecked((int)0xFF000000) + i16 * 65536;
				}
				else
				{
					tessellator9.ColorOpaque_I = (int)unchecked((int)0xFF000000) + i16 * 256;
				}

				long j18 = frameTimes[i14] / 200000L;
				long j20 = tickTimes[i14] / 200000L;
				tessellator9.addVertex((double)((float)i14 + 0.5F), (double)((float)((long)this.displayHeight - j18) + 0.5F), 0.0D);
				tessellator9.addVertex((double)((float)i14 + 0.5F), (double)((float)this.displayHeight + 0.5F), 0.0D);
				tessellator9.ColorOpaque_I = (int)unchecked((int)0xFF000000) + i16 * 65536 + i16 * 256 + i16 * 1;
				tessellator9.addVertex((double)((float)i14 + 0.5F), (double)((float)((long)this.displayHeight - j18) + 0.5F), 0.0D);
				tessellator9.addVertex((double)((float)i14 + 0.5F), (double)((float)((long)this.displayHeight - (j18 - j20)) + 0.5F), 0.0D);
			}

			tessellator9.draw();
			short s26 = 160;
			i15 = this.displayWidth - s26 - 10;
			i16 = this.displayHeight - s26 * 2;
			GL.Enable(EnableCap.Blend);
			tessellator9.startDrawingQuads();
			tessellator9.setColorRGBA_I(0, 200);
			tessellator9.addVertex((double)((float)i15 - (float)s26 * 1.1F), (double)((float)i16 - (float)s26 * 0.6F - 16.0F), 0.0D);
			tessellator9.addVertex((double)((float)i15 - (float)s26 * 1.1F), (double)(i16 + s26 * 2), 0.0D);
			tessellator9.addVertex((double)((float)i15 + (float)s26 * 1.1F), (double)(i16 + s26 * 2), 0.0D);
			tessellator9.addVertex((double)((float)i15 + (float)s26 * 1.1F), (double)((float)i16 - (float)s26 * 0.6F - 16.0F), 0.0D);
			tessellator9.draw();
			GL.Disable(EnableCap.Blend);
			double d27 = 0.0D;

			int i21;
			for (int i19 = 0; i19 < list3.Count; ++i19)
			{
				ProfilerResult profilerResult29 = (ProfilerResult)list3[i19];
				i21 = MathHelper.floor_double(profilerResult29.sectionPercentage / 4.0D) + 1;
				tessellator9.startDrawing(6);
				tessellator9.ColorOpaque_I = profilerResult29.DisplayColor;
				tessellator9.addVertex((double)i15, (double)i16, 0.0D);

				int i22;
				float f23;
				float f24;
				float f25;
				for (i22 = i21; i22 >= 0; --i22)
				{
					f23 = (float)((d27 + profilerResult29.sectionPercentage * (double)i22 / (double)i21) * (double)(float)Math.PI * 2.0D / 100.0D);
					f24 = MathHelper.sin(f23) * (float)s26;
					f25 = MathHelper.cos(f23) * (float)s26 * 0.5F;
					tessellator9.addVertex((double)((float)i15 + f24), (double)((float)i16 - f25), 0.0D);
				}

				tessellator9.draw();
				tessellator9.startDrawing(5);
				tessellator9.ColorOpaque_I = (profilerResult29.DisplayColor & 16711422) >> 1;

				for (i22 = i21; i22 >= 0; --i22)
				{
					f23 = (float)((d27 + profilerResult29.sectionPercentage * (double)i22 / (double)i21) * (double)(float)Math.PI * 2.0D / 100.0D);
					f24 = MathHelper.sin(f23) * (float)s26;
					f25 = MathHelper.cos(f23) * (float)s26 * 0.5F;
					tessellator9.addVertex((double)((float)i15 + f24), (double)((float)i16 - f25), 0.0D);
					tessellator9.addVertex((double)((float)i15 + f24), (double)((float)i16 - f25 + 10.0F), 0.0D);
				}

				tessellator9.draw();
				d27 += profilerResult29.sectionPercentage;
			}

			string decimalFormat28 = "##0.00";
			GL.Enable(EnableCap.Texture2D);
			string string30 = "";
			if (!profilerResult4.name.Equals("unspecified"))
			{
				string30 = string30 + "[0] ";
			}

			if (profilerResult4.name.Length == 0)
			{
				string30 = string30 + "ROOT ";
			}
			else
			{
				string30 = string30 + profilerResult4.name + " ";
			}

			i21 = 0xFFFFFF;
			this.fontRenderer.drawStringWithShadow(string30, i15 - s26, i16 - s26 / 2 - 16, i21);
			this.fontRenderer.drawStringWithShadow(string30 = profilerResult4.globalPercentage.ToString(decimalFormat28) + "%", i15 + s26 - this.fontRenderer.getStringWidth(string30), i16 - s26 / 2 - 16, i21);

			for (int i32 = 0; i32 < list3.Count; ++i32)
			{
				ProfilerResult profilerResult31 = (ProfilerResult)list3[i32];
				string string33 = "";
				if (!profilerResult31.name.Equals("unspecified"))
				{
					string33 = string33 + "[" + (i32 + 1) + "] ";
				}
				else
				{
					string33 = string33 + "[?] ";
				}

				string33 = string33 + profilerResult31.name;
				this.fontRenderer.drawStringWithShadow(string33, i15 - s26, i16 + s26 / 2 + i32 * 8 + 20, profilerResult31.DisplayColor);
				this.fontRenderer.drawStringWithShadow(string33 = profilerResult31.sectionPercentage.ToString(decimalFormat28) + "%", i15 + s26 - 50 - this.fontRenderer.getStringWidth(string33), i16 + s26 / 2 + i32 * 8 + 20, profilerResult31.DisplayColor);
				this.fontRenderer.drawStringWithShadow(string33 = profilerResult31.globalPercentage.ToString(decimalFormat28) + "%", i15 + s26 - this.fontRenderer.getStringWidth(string33), i16 + s26 / 2 + i32 * 8 + 20, profilerResult31.DisplayColor);
			}

		}

		public virtual void shutdown()
		{
			this.running = false;
		}

		public virtual void setIngameFocus()
		{
			if (mcApplet.IsFocused)
			{
				if (!this.inGameHasFocus)
				{
					this.inGameHasFocus = true;
					this.mouseHelper.grabMouseCursor();
					this.displayGuiScreen((GuiScreen)null);
					this.leftClickCounter = 10000;
				}
			}
		}

		public virtual void setIngameNotInFocus()
		{
			if (this.inGameHasFocus)
			{
				KeyBinding.unPressAllKeys();
				this.inGameHasFocus = false;
				this.mouseHelper.ungrabMouseCursor();
			}
		}

		public virtual void displayInGameMenu()
		{
			if (this.currentScreen == null)
			{
				this.displayGuiScreen(new GuiIngameMenu());
			}
		}

		private void sendClickBlockToController(int i1, bool z2)
		{
			if (!z2)
			{
				this.leftClickCounter = 0;
			}

			if (i1 != 0 || this.leftClickCounter <= 0)
			{
				if (z2 && this.objectMouseOver != null && this.objectMouseOver.typeOfHit == EnumMovingObjectType.TILE && i1 == 0)
				{
					int i3 = this.objectMouseOver.blockX;
					int i4 = this.objectMouseOver.blockY;
					int i5 = this.objectMouseOver.blockZ;
					this.playerController.onPlayerDamageBlock(i3, i4, i5, this.objectMouseOver.sideHit);
					if (this.thePlayer.canPlayerEdit(i3, i4, i5))
					{
						this.effectRenderer.addBlockHitEffects(i3, i4, i5, this.objectMouseOver.sideHit);
						this.thePlayer.swingItem();
					}
				}
				else
				{
					this.playerController.resetBlockRemoving();
				}

			}
		}

		private void clickMouse(int i1)
		{
			if (i1 != 0 || this.leftClickCounter <= 0)
			{
				if (i1 == 0)
				{
					this.thePlayer.swingItem();
				}

				if (i1 == 1)
				{
					this.rightClickDelayTimer = 4;
				}

				bool z2 = true;
				ItemStack itemStack3 = this.thePlayer.inventory.CurrentItem;
				if (this.objectMouseOver == null)
				{
					if (i1 == 0 && this.playerController.NotCreative)
					{
						this.leftClickCounter = 10;
					}
				}
				else if (this.objectMouseOver.typeOfHit == EnumMovingObjectType.ENTITY)
				{
					if (i1 == 0)
					{
						this.playerController.attackEntity(this.thePlayer, this.objectMouseOver.entityHit);
					}

					if (i1 == 1)
					{
						this.playerController.interactWithEntity(this.thePlayer, this.objectMouseOver.entityHit);
					}
				}
				else if (this.objectMouseOver.typeOfHit == EnumMovingObjectType.TILE)
				{
					int i4 = this.objectMouseOver.blockX;
					int i5 = this.objectMouseOver.blockY;
					int i6 = this.objectMouseOver.blockZ;
					int i7 = this.objectMouseOver.sideHit;
					if (i1 == 0)
					{
						this.playerController.clickBlock(i4, i5, i6, this.objectMouseOver.sideHit);
					}
					else
					{
						int i9 = itemStack3 != null ? itemStack3.stackSize : 0;
						if (this.playerController.onPlayerRightClick(this.thePlayer, this.theWorld, itemStack3, i4, i5, i6, i7))
						{
							z2 = false;
							this.thePlayer.swingItem();
						}

						if (itemStack3 == null)
						{
							return;
						}

						if (itemStack3.stackSize == 0)
						{
							this.thePlayer.inventory.mainInventory[this.thePlayer.inventory.currentItem] = null;
						}
						else if (itemStack3.stackSize != i9 || this.playerController.InCreativeMode)
						{
							this.entityRenderer.itemRenderer.func_9449_b();
						}
					}
				}

				if (z2 && i1 == 1)
				{
					ItemStack itemStack10 = this.thePlayer.inventory.CurrentItem;
					if (itemStack10 != null && this.playerController.sendUseItem(this.thePlayer, this.theWorld, itemStack10))
					{
						this.entityRenderer.itemRenderer.func_9450_c();
					}
				}

			}
		}

		public virtual void toggleFullscreen()
		{
			try
			{
				this.fullscreen = !this.fullscreen;
				if (this.fullscreen)
				{
					mcApplet.WindowState = WindowState.Fullscreen;
					this.displayWidth = Monitors.GetMonitorFromWindow(mcApplet).HorizontalResolution;
					this.displayHeight = Monitors.GetMonitorFromWindow(mcApplet).VerticalResolution;
					if (this.displayWidth <= 0)
					{
						this.displayWidth = 1;
					}

					if (this.displayHeight <= 0)
					{
						this.displayHeight = 1;
					}
				}
				else
				{
					if (mcApplet != null)
					{
						this.displayWidth = mcApplet.Size.X;
						this.displayHeight = mcApplet.Size.Y;

						mcApplet.WindowState = WindowState.Normal;
					}

					if (this.displayWidth <= 0)
					{
						this.displayWidth = 1;
					}

					if (this.displayHeight <= 0)
					{
						this.displayHeight = 1;
					}
				}

				if (this.currentScreen != null)
				{
					this.resize(this.displayWidth, this.displayHeight);
				}
			}
			catch (Exception exception2)
			{
				Console.WriteLine(exception2.ToString());
				Console.Write(exception2.StackTrace);
			}

		}

		private void resize(int i1, int i2)
		{
			if (i1 <= 0)
			{
				i1 = 1;
			}

			if (i2 <= 0)
			{
				i2 = 1;
			}

			this.displayWidth = i1;
			this.displayHeight = i2;
			if (this.currentScreen != null)
			{
				ScaledResolution scaledResolution3 = new ScaledResolution(this.gameSettings, i1, i2);
				int i4 = scaledResolution3.ScaledWidth;
				int i5 = scaledResolution3.ScaledHeight;
				this.currentScreen.setWorldAndResolution(this, i4, i5);
			}

		}

		private void startThreadCheckHasPaid()
		{
			(new ThreadCheckHasPaid(this)).Start();
		}

		public virtual void runTick()
		{
			

			if (rightClickDelayTimer > 0)
			{
				--rightClickDelayTimer;
			}

			if (ticksRan == 6000)
			{
				startThreadCheckHasPaid();
			}

			Profiler.startSection("stats");
			statFileWriter.func_27178_d();
			Profiler.endStartSection("gui");
			if (!isGamePaused)
			{
				ingameGUI.updateTick();
			}

			Profiler.endStartSection("pick");
			entityRenderer.getMouseOver(1.0F);
			Profiler.endStartSection("centerChunkSource");
			int i3;
			if (thePlayer != null)
			{
				IChunkProvider iChunkProvider1 = theWorld.ChunkProvider;
				if (iChunkProvider1 is ChunkProviderLoadOrGenerate)
				{
					ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate2 = (ChunkProviderLoadOrGenerate)iChunkProvider1;
					i3 = MathHelper.floor_float((float)((int)thePlayer.posX)) >> 4;
					int i4 = MathHelper.floor_float((float)((int)thePlayer.posZ)) >> 4;
					chunkProviderLoadOrGenerate2.setCurrentChunkOver(i3, i4);
				}
			}

			Profiler.endStartSection("gameMode");
			if (!isGamePaused && theWorld != null)
			{
				playerController.updateController();
			}

			GL.BindTexture(TextureTarget.Texture2D, renderEngine.getTexture("/terrain.png"));
			Profiler.endStartSection("textures");
			if (!isGamePaused)
			{
				renderEngine.updateDynamicTextures();
			}

			if (currentScreen == null && thePlayer != null)
			{
				if (thePlayer.Health <= 0)
				{
					displayGuiScreen((GuiScreen)null);
				}
				else if (thePlayer.PlayerSleeping && theWorld != null && theWorld.isRemote)
				{
					displayGuiScreen(new GuiSleepMP());
				}
			}
			else if (currentScreen != null && currentScreen is GuiSleepMP && !thePlayer.PlayerSleeping)
			{
				displayGuiScreen((GuiScreen)null);
			}

			if (currentScreen != null)
			{
				leftClickCounter = 10000;
			}

			if (currentScreen != null)
			{
				currentScreen.handleInput();
				if (currentScreen != null)
				{
					currentScreen.guiParticles.update();
					currentScreen.updateScreen();
				}
			}

			NativeWindow.ProcessWindowEvents(false);

			if (currentScreen == null || currentScreen.allowUserInput)
			{
				while(mcApplet.NextMouseEvent())
                {
					if (mcApplet.CurrentMouseEvent() == null)
						continue; // This should never happen.

					MouseEvent e = mcApplet.CurrentMouseEvent()!.Value;

					KeyBinding.setKeyBindState((int)e.button - 100, e.IsPressed);

					long j5 = DateTimeHelper.CurrentUnixTimeMillis() - systemTime;
					if (j5 <= 200L)
					{
						i3 = e.scrollDelta;
						if (i3 != 0)
						{
							thePlayer.inventory.changeCurrentItem(i3);
							if (gameSettings.noclip)
							{
								if (i3 > 0)
								{
									i3 = 1;
								}

								if (i3 < 0)
								{
									i3 = -1;
								}

								gameSettings.noclipRate += (float)i3 * 0.25F;
							}
						}

						if (currentScreen == null)
						{
							if (!inGameHasFocus && e.IsPressed)
							{
								setIngameFocus();
							}
						}
						else if (currentScreen != null)
						{
							currentScreen.handleMouseInput();
						}
					}
				}
				

				if (leftClickCounter > 0)
				{
					--leftClickCounter;
				}

				Profiler.endStartSection("keyboard");

				while (true)
				{
					while (true)
					{
						do
						{
							if (!mcApplet.NextKeyEvent())
							{
								while (gameSettings.keyBindInventory.Pressed)
								{
									displayGuiScreen(new GuiInventory(thePlayer));
								}

								while (gameSettings.keyBindDrop.Pressed)
								{
									thePlayer.dropOneItem();
								}

								while (MultiplayerWorld && gameSettings.keyBindChat.Pressed)
								{
									displayGuiScreen(new GuiChat());
								}

								if (MultiplayerWorld && currentScreen == null && mcApplet.KeyboardState.IsKeyDown(Keys.Slash) || mcApplet.KeyboardState.IsKeyDown(Keys.KeyPadDivide))
								{
									displayGuiScreen(new GuiChat("/"));
								}

								if (thePlayer.UsingItem)
								{
									if (!gameSettings.keyBindUseItem.pressed)
									{
										playerController.onStoppedUsingItem(thePlayer);
									}

									while (true)
									{
										if (!gameSettings.keyBindAttack.Pressed)
										{
											while (gameSettings.keyBindUseItem.Pressed)
											{
											}

											while (gameSettings.keyBindPickBlock.Pressed)
											{
											}
											break;
										}
									}
								}
								else
								{
									while (gameSettings.keyBindAttack.Pressed)
									{
										clickMouse(0);
									}

									while (gameSettings.keyBindUseItem.Pressed)
									{
										clickMouse(1);
									}

									while (gameSettings.keyBindPickBlock.Pressed)
									{
										clickMiddleMouseButton();
									}
								}

								if (gameSettings.keyBindUseItem.pressed && rightClickDelayTimer == 0 && !thePlayer.UsingItem)
								{
									clickMouse(1);
								}

								sendClickBlockToController(0, currentScreen == null && gameSettings.keyBindAttack.pressed && inGameHasFocus);
								goto label361Break;
							}

							KeyBinding.setKeyBindState((int)mcApplet.CurrentKeyEvent().Value.e.Key, mcApplet.CurrentKeyEvent().Value.isPressed);
						} while (!mcApplet.CurrentKeyEvent().Value.isPressed);

						if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F11)
						{
							toggleFullscreen();
						}
						else
						{
							if (currentScreen != null)
							{
								currentScreen.handleKeyboardInput();
							}
							else
							{
								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.Escape)
								{
									displayInGameMenu();
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.S && mcApplet.KeyboardState.IsKeyDown(Keys.F3))
								{
									forceReload();
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.T && mcApplet.KeyboardState.IsKeyDown(Keys.F3))
								{
									renderEngine.refreshTextures();
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F && mcApplet.KeyboardState.IsKeyDown(Keys.F3))
								{
									bool z6 = mcApplet.KeyboardState.IsKeyDown(Keys.LeftShift) | mcApplet.KeyboardState.IsKeyDown(Keys.RightShift);
									gameSettings.setOptionValue(EnumOptions.RENDER_DISTANCE, z6 ? -1 : 1);
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.A && mcApplet.KeyboardState.IsKeyDown(Keys.F3))
								{
									renderGlobal.loadRenderers();
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F1)
								{
									gameSettings.hideGUI = !gameSettings.hideGUI;
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F3)
								{
									gameSettings.showDebugInfo = !gameSettings.showDebugInfo;
									gameSettings.field_50119_G = !GuiScreen.isShiftDown();
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F5)
								{
									++gameSettings.thirdPersonView;
									if (gameSettings.thirdPersonView > 2)
									{
										gameSettings.thirdPersonView = 0;
									}
								}

								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.F8)
								{
									gameSettings.smoothCamera = !gameSettings.smoothCamera;
								}
							}

							int i7;
							for (i7 = 0; i7 < 9; ++i7)
							{
								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.D1 + i7)
								{
									thePlayer.inventory.currentItem = i7;
								}
							}

							if (gameSettings.showDebugInfo && gameSettings.field_50119_G)
							{
								if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.D0)
								{
									updateDebugProfilerName(0);
								}

								for (i7 = 0; i7 < 9; ++i7)
								{
									if (mcApplet.CurrentKeyEvent().Value.e.Key == Keys.D1 + i7)
									{
										updateDebugProfilerName(i7 + 1);
									}
								}
							}
						}
					}
					label361Continue:;
				}
				label361Break:;

				foreach (KeyBinding binding in KeyBinding.keybindArray)
				{
					if (binding.pressed)
						KeyBinding.onTick(binding.keyCode);
				}
			}

			if (theWorld != null)
			{
				if (thePlayer != null)
				{
					++joinPlayerCounter;
					if (joinPlayerCounter == 30)
					{
						joinPlayerCounter = 0;
						theWorld.joinEntityInSurroundings(thePlayer);
					}
				}

				if (theWorld.WorldInfo.HardcoreModeEnabled)
				{
					theWorld.difficultySetting = 3;
				}
				else
				{
					theWorld.difficultySetting = gameSettings.difficulty;
				}

				if (theWorld.isRemote)
				{
					theWorld.difficultySetting = 1;
				}

				Profiler.endStartSection("gameRenderer");
				if (!isGamePaused)
				{
					entityRenderer.updateRenderer();
				}

				Profiler.endStartSection("levelRenderer");
				if (!isGamePaused)
				{
					renderGlobal.updateClouds();
				}

				Profiler.endStartSection("level");
				if (!isGamePaused)
				{
					if (theWorld.lightningFlash > 0)
					{
						--theWorld.lightningFlash;
					}

					theWorld.updateEntities();
				}

				if (!isGamePaused || MultiplayerWorld)
				{
					theWorld.setAllowedSpawnTypes(theWorld.difficultySetting > 0, true);
					theWorld.tick();
				}

				Profiler.endStartSection("animateTick");
				if (!isGamePaused && theWorld != null)
				{
					theWorld.randomDisplayUpdates(MathHelper.floor_double(thePlayer.posX), MathHelper.floor_double(thePlayer.posY), MathHelper.floor_double(thePlayer.posZ));
				}

				Profiler.endStartSection("particles");
				if (!isGamePaused)
				{
					effectRenderer.updateEffects();
				}
			}

			Profiler.endSection();
			systemTime = DateTimeHelper.CurrentUnixTimeMillis();
		}

		private void forceReload()
		{
			Console.WriteLine("FORCING RELOAD!");
			this.sndManager = new SoundManager();
			this.sndManager.loadSoundSettings(this.gameSettings);
			this.downloadResourcesThread.reloadResources();
		}

		public virtual bool MultiplayerWorld
		{
			get
			{
				return this.theWorld != null && this.theWorld.isRemote;
			}
		}

		public virtual void startWorld(string string1, string string2, WorldSettings worldSettings3)
		{
			this.changeWorld1((World)null);
			System.GC.Collect();
			if (this.saveLoader.isOldMapFormat(string1))
			{
				this.convertMapFormat(string1, string2);
			}
			else
			{
				if (this.loadingScreen != null)
				{
					this.loadingScreen.printText(StatCollector.translateToLocal("menu.switchingLevel"));
					this.loadingScreen.displayLoadingString("");
				}

				ISaveHandler iSaveHandler4 = this.saveLoader.getSaveLoader(string1, false);
				World world5 = null;
				world5 = new World(iSaveHandler4, string2, worldSettings3);
				if (world5.isNewWorld)
				{
					this.statFileWriter.readStat(StatList.createWorldStat, 1);
					this.statFileWriter.readStat(StatList.startGameStat, 1);
					this.changeWorld2(world5, StatCollector.translateToLocal("menu.generatingLevel"));
				}
				else
				{
					this.statFileWriter.readStat(StatList.loadWorldStat, 1);
					this.statFileWriter.readStat(StatList.startGameStat, 1);
					this.changeWorld2(world5, StatCollector.translateToLocal("menu.loadingLevel"));
				}
			}

		}

		public virtual void usePortal(int i1)
		{
			int i2 = this.thePlayer.dimension;
			this.thePlayer.dimension = i1;
			theWorld.setEntityDead(thePlayer);
			this.thePlayer.isDead = false;
			double d3 = this.thePlayer.posX;
			double d5 = this.thePlayer.posZ;
			double d7 = 1.0D;
			if (i2 > -1 && this.thePlayer.dimension == -1)
			{
				d7 = 0.125D;
			}
			else if (i2 == -1 && this.thePlayer.dimension > -1)
			{
				d7 = 8.0D;
			}

			d3 *= d7;
			d5 *= d7;
			World world9;
			if (this.thePlayer.dimension == -1)
			{
				this.thePlayer.setLocationAndAngles(d3, this.thePlayer.posY, d5, this.thePlayer.rotationYaw, this.thePlayer.rotationPitch);
				if (this.thePlayer.EntityAlive)
				{
					this.theWorld.updateEntityWithOptionalForce(this.thePlayer, false);
				}

				world9 = null;
				world9 = new World(this.theWorld, WorldProvider.getProviderForDimension(this.thePlayer.dimension));
				this.changeWorld(world9, "Entering the Nether", this.thePlayer);
			}
			else if (this.thePlayer.dimension == 0)
			{
				if (this.thePlayer.EntityAlive)
				{
					this.thePlayer.setLocationAndAngles(d3, this.thePlayer.posY, d5, this.thePlayer.rotationYaw, this.thePlayer.rotationPitch);
					this.theWorld.updateEntityWithOptionalForce(this.thePlayer, false);
				}

				world9 = null;
				world9 = new World(this.theWorld, WorldProvider.getProviderForDimension(this.thePlayer.dimension));
				if (i2 == -1)
				{
					this.changeWorld(world9, "Leaving the Nether", this.thePlayer);
				}
				else
				{
					this.changeWorld(world9, "Leaving the End", this.thePlayer);
				}
			}
			else
			{
				world9 = null;
				world9 = new World(this.theWorld, WorldProvider.getProviderForDimension(this.thePlayer.dimension));
				ChunkCoordinates chunkCoordinates10 = world9.EntrancePortalLocation;
				d3 = (double)chunkCoordinates10.posX;
				this.thePlayer.posY = (double)chunkCoordinates10.posY;
				d5 = (double)chunkCoordinates10.posZ;
				this.thePlayer.setLocationAndAngles(d3, this.thePlayer.posY, d5, 90.0F, 0.0F);
				if (this.thePlayer.EntityAlive)
				{
					world9.updateEntityWithOptionalForce(this.thePlayer, false);
				}

				this.changeWorld(world9, "Entering the End", this.thePlayer);
			}

			this.thePlayer.worldObj = this.theWorld;
			Console.WriteLine("Teleported to " + this.theWorld.worldProvider.worldType);
			if (this.thePlayer.EntityAlive && i2 < 1)
			{
				this.thePlayer.setLocationAndAngles(d3, this.thePlayer.posY, d5, this.thePlayer.rotationYaw, this.thePlayer.rotationPitch);
				this.theWorld.updateEntityWithOptionalForce(this.thePlayer, false);
				(new Teleporter()).placeInPortal(this.theWorld, this.thePlayer);
			}

		}

		public virtual void exitToMainMenu(string string1)
		{
			this.theWorld = null;
			this.changeWorld2((World)null, string1);
		}

		public virtual void changeWorld1(World world1)
		{
			this.changeWorld2(world1, "");
		}

		public virtual void changeWorld2(World world1, string string2)
		{
			this.changeWorld(world1, string2, (EntityPlayer)null);
		}

		public virtual void changeWorld(World world1, string string2, EntityPlayer entityPlayer3)
		{
			this.statFileWriter.func_27175_b();
			this.statFileWriter.syncStats();
			this.renderViewEntity = null;
			if (this.loadingScreen != null)
			{
				this.loadingScreen.printText(string2);
				this.loadingScreen.displayLoadingString("");
			}

			this.sndManager.playStreaming((string)null, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F);
			if (this.theWorld != null)
			{
				this.theWorld.saveWorldIndirectly(this.loadingScreen);
			}

			this.theWorld = world1;
			if (world1 != null)
			{
				if (this.playerController != null)
				{
					this.playerController.onWorldChange(world1);
				}

				if (!this.MultiplayerWorld)
				{
					if (entityPlayer3 == null)
					{
						this.thePlayer = (EntityPlayerSP)world1.func_4085_a(typeof(EntityPlayerSP));
					}
				}
				else if (this.thePlayer != null)
				{
					this.thePlayer.preparePlayerToSpawn();
					if (world1 != null)
					{
						world1.spawnEntityInWorld(this.thePlayer);
					}
				}

				if (!world1.isRemote)
				{
					this.preloadWorld(string2);
				}

				if (this.thePlayer == null)
				{
					this.thePlayer = (EntityPlayerSP)this.playerController.createPlayer(world1);
					this.thePlayer.preparePlayerToSpawn();
					this.playerController.flipPlayer(this.thePlayer);
				}

				this.thePlayer.movementInput = new MovementInputFromOptions(this.gameSettings);
				if (this.renderGlobal != null)
				{
					this.renderGlobal.changeWorld(world1);
				}

				if (this.effectRenderer != null)
				{
					this.effectRenderer.clearEffects(world1);
				}

				if (entityPlayer3 != null)
				{
					world1.func_6464_c();
				}

				IChunkProvider iChunkProvider4 = world1.ChunkProvider;
				if (iChunkProvider4 is ChunkProviderLoadOrGenerate)
				{
					ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate5 = (ChunkProviderLoadOrGenerate)iChunkProvider4;
					int i6 = MathHelper.floor_float((float)((int)this.thePlayer.posX)) >> 4;
					int i7 = MathHelper.floor_float((float)((int)this.thePlayer.posZ)) >> 4;
					chunkProviderLoadOrGenerate5.setCurrentChunkOver(i6, i7);
				}

				world1.spawnPlayerWithLoadedChunks(this.thePlayer);
				this.playerController.func_6473_b(this.thePlayer);
				if (world1.isNewWorld)
				{
					world1.saveWorldIndirectly(this.loadingScreen);
				}

				this.renderViewEntity = this.thePlayer;
			}
			else
			{
				this.saveLoader.flushCache();
				this.thePlayer = null;
			}

			System.GC.Collect();
			this.systemTime = 0L;
		}

		private void convertMapFormat(string string1, string string2)
		{
			this.loadingScreen.printText("Converting World to " + this.saveLoader.FormatName);
			this.loadingScreen.displayLoadingString("This may take a while :)");
			this.saveLoader.convertMapFormat(string1, this.loadingScreen);
			this.startWorld(string1, string2, new WorldSettings(0L, 0, true, false, WorldType.DEFAULT));
		}

		private void preloadWorld(string string1)
		{
			if (this.loadingScreen != null)
			{
				this.loadingScreen.printText(string1);
				this.loadingScreen.displayLoadingString(StatCollector.translateToLocal("menu.generatingTerrain"));
			}

			short s2 = 128;
			if (this.playerController.func_35643_e())
			{
				s2 = 64;
			}

			int i3 = 0;
			int i4 = s2 * 2 / 16 + 1;
			i4 *= i4;
			IChunkProvider iChunkProvider5 = this.theWorld.ChunkProvider;
			ChunkCoordinates chunkCoordinates6 = this.theWorld.SpawnPoint;
			if (this.thePlayer != null)
			{
				chunkCoordinates6.posX = (int)this.thePlayer.posX;
				chunkCoordinates6.posZ = (int)this.thePlayer.posZ;
			}

			if (iChunkProvider5 is ChunkProviderLoadOrGenerate)
			{
				ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate7 = (ChunkProviderLoadOrGenerate)iChunkProvider5;
				chunkProviderLoadOrGenerate7.setCurrentChunkOver(chunkCoordinates6.posX >> 4, chunkCoordinates6.posZ >> 4);
			}

			for (int i10 = -s2; i10 <= s2; i10 += 16)
			{
				for (int i8 = -s2; i8 <= s2; i8 += 16)
				{
					if (this.loadingScreen != null)
					{
						this.loadingScreen.LoadingProgress = i3++ * 100 / i4;
					}

					this.theWorld.getBlockId(chunkCoordinates6.posX + i10, 64, chunkCoordinates6.posZ + i8);
					if (!this.playerController.func_35643_e())
					{
						while (this.theWorld.updatingLighting())
						{
						}
					}
				}
			}

			if (!this.playerController.func_35643_e())
			{
				if (this.loadingScreen != null)
				{
					this.loadingScreen.displayLoadingString(StatCollector.translateToLocal("menu.simulating"));
				}

				bool z9 = true;
				this.theWorld.dropOldChunks();
			}

		}

		public virtual void installResource(string string1, FileInfo file2)
		{
			int i3 = string1.IndexOf("/", StringComparison.Ordinal);
			string string4 = string1.Substring(0, i3);
			string1 = string1.Substring(i3 + 1);
			if (string4.Equals("sound", StringComparison.OrdinalIgnoreCase))
			{
				this.sndManager.addSound(string1, file2);
			}
			else if (string4.Equals("newsound", StringComparison.OrdinalIgnoreCase))
			{
				this.sndManager.addSound(string1, file2);
			}
			else if (string4.Equals("streaming", StringComparison.OrdinalIgnoreCase))
			{
				this.sndManager.addStreaming(string1, file2);
			}
			else if (string4.Equals("music", StringComparison.OrdinalIgnoreCase))
			{
				this.sndManager.addMusic(string1, file2);
			}
			else if (string4.Equals("newmusic", StringComparison.OrdinalIgnoreCase))
			{
				this.sndManager.addMusic(string1, file2);
			}

		}

		public virtual string debugInfoRenders()
		{
			return this.renderGlobal.DebugInfoRenders;
		}

		public virtual string EntityDebug
		{
			get
			{
				return this.renderGlobal.DebugInfoEntities;
			}
		}

		public virtual string WorldProviderName
		{
			get
			{
				return this.theWorld.ProviderName;
			}
		}

		public virtual string debugInfoEntities()
		{
			return "P: " + this.effectRenderer.Statistics + ". T: " + this.theWorld.DebugLoadedEntities;
		}

		public virtual void respawn(bool z1, int i2, bool z3)
		{
			if (!this.theWorld.isRemote && !this.theWorld.worldProvider.canRespawnHere())
			{
				this.usePortal(0);
			}

			ChunkCoordinates chunkCoordinates4 = null;
			ChunkCoordinates chunkCoordinates5 = null;
			bool z6 = true;
			if (this.thePlayer != null && !z1)
			{
				chunkCoordinates4 = this.thePlayer.SpawnChunk;
				if (chunkCoordinates4 != null)
				{
					chunkCoordinates5 = EntityPlayer.verifyRespawnCoordinates(this.theWorld, chunkCoordinates4);
					if (chunkCoordinates5 == null)
					{
						this.thePlayer.addChatMessage("tile.bed.notValid");
					}
				}
			}

			if (chunkCoordinates5 == null)
			{
				chunkCoordinates5 = this.theWorld.SpawnPoint;
				z6 = false;
			}

			IChunkProvider iChunkProvider7 = this.theWorld.ChunkProvider;
			if (iChunkProvider7 is ChunkProviderLoadOrGenerate)
			{
				ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate8 = (ChunkProviderLoadOrGenerate)iChunkProvider7;
				chunkProviderLoadOrGenerate8.setCurrentChunkOver(chunkCoordinates5.posX >> 4, chunkCoordinates5.posZ >> 4);
			}

			this.theWorld.setSpawnLocation();
			this.theWorld.updateEntityList();
			int i10 = 0;
			if (this.thePlayer != null)
			{
				i10 = this.thePlayer.entityId;
				theWorld.setEntityDead(thePlayer);
			}

			EntityPlayerSP entityPlayerSP9 = this.thePlayer;
			this.renderViewEntity = null;
			this.thePlayer = (EntityPlayerSP)this.playerController.createPlayer(this.theWorld);
			if (z3)
			{
				this.thePlayer.copyPlayer(entityPlayerSP9);
			}

			this.thePlayer.dimension = i2;
			this.renderViewEntity = this.thePlayer;
			this.thePlayer.preparePlayerToSpawn();
			if (z6)
			{
				this.thePlayer.SpawnChunk = chunkCoordinates4;
				this.thePlayer.setLocationAndAngles((double)((float)chunkCoordinates5.posX + 0.5F), (double)((float)chunkCoordinates5.posY + 0.1F), (double)((float)chunkCoordinates5.posZ + 0.5F), 0.0F, 0.0F);
			}

			this.playerController.flipPlayer(this.thePlayer);
			this.theWorld.spawnPlayerWithLoadedChunks(this.thePlayer);
			this.thePlayer.movementInput = new MovementInputFromOptions(this.gameSettings);
			this.thePlayer.entityId = i10;
			this.thePlayer.func_6420_o();
			this.playerController.func_6473_b(this.thePlayer);
			this.preloadWorld(StatCollector.translateToLocal("menu.respawning"));
			if (this.currentScreen is GuiGameOver)
			{
				this.displayGuiScreen(null);
			}

		}
        
		public static void startMainThread1(string string0, string string1)
		{
			startMainThread(string0, string1, null);
		}

		public static void startMainThread(string string0, string string1, string string2)
		{
			bool fullscreen = false;

			NativeWindowSettings windowSettings = new()
			{
				Size = new Vector2i(854, 480)
			};

			MinecraftApplet applet = new(windowSettings);

			MinecraftImpl minecraftImpl7 = new MinecraftImpl(applet, 854, 480, fullscreen);
			Thread thread8 = new Thread(() => minecraftImpl7.run());
			thread8.Priority = ThreadPriority.Highest;
			minecraftImpl7.minecraftUri = "www.minecraft.net";
			if (!string.ReferenceEquals(string0, null) && !string.ReferenceEquals(string1, null))
			{
				minecraftImpl7.session = new Session(string0, string1);
			}
			else
			{
				minecraftImpl7.session = new Session("Player" + DateTimeHelper.CurrentUnixTimeMillis() % 1000L, "");
			}

			if (!string.ReferenceEquals(string2, null))
			{
				string[] string9 = string2.Split(":", true);
				minecraftImpl7.setServer(string9[0], int.Parse(string9[1]));
			}
            
			thread8.Start();
		}

		public virtual NetClientHandler SendQueue
		{
			get
			{
				return this.thePlayer is EntityClientPlayerMP ? ((EntityClientPlayerMP)this.thePlayer).sendQueue : null;
			}
		}
        
		public static void Main(string[] string0)
		{
			string string1 = null;
			string string2 = null;
			string1 = "Player" + DateTimeHelper.CurrentUnixTimeMillis() % 1000L;
			if (string0.Length > 0)
			{
				string1 = string0[0];
			}

			string2 = "-";
			if (string0.Length > 1)
			{
				string2 = string0[1];
			}

			startMainThread1(string1, string2);
		}

		public static bool GuiEnabled
		{
			get
			{
				return theMinecraft == null || !theMinecraft.gameSettings.hideGUI;
			}
		}

		public static bool FancyGraphicsEnabled
		{
			get
			{
				return theMinecraft != null && theMinecraft.gameSettings.fancyGraphics;
			}
		}

		public static bool AmbientOcclusionEnabled
		{
			get
			{
				return theMinecraft != null && theMinecraft.gameSettings.ambientOcclusion;
			}
		}

		public static bool DebugInfoEnabled
		{
			get
			{
				return theMinecraft != null && theMinecraft.gameSettings.showDebugInfo;
			}
		}

        public int MouseScrollDelta
        {
			get
            {
				int scrollDelta = 0;

				if (mcApplet.MouseState.ScrollDelta.Y > 0)
					scrollDelta = 1;

				if (mcApplet.MouseState.ScrollDelta.Y < 0)
					scrollDelta = -1;

				return scrollDelta;
			}
        }

        public virtual bool lineIsCommand(string string1)
		{
			if (string1.StartsWith("/", StringComparison.Ordinal))
			{
				;
			}

			return false;
		}

		private void clickMiddleMouseButton()
		{
			if (this.objectMouseOver != null)
			{
				bool z1 = this.thePlayer.capabilities.isCreativeMode;
				int i2 = this.theWorld.getBlockId(this.objectMouseOver.blockX, this.objectMouseOver.blockY, this.objectMouseOver.blockZ);
				if (!z1)
				{
					if (i2 == Block.grass.blockID)
					{
						i2 = Block.dirt.blockID;
					}

					if (i2 == Block.stairDouble.blockID)
					{
						i2 = Block.stairSingle.blockID;
					}

					if (i2 == Block.bedrock.blockID)
					{
						i2 = Block.stone.blockID;
					}
				}

				int i3 = 0;
				bool z4 = false;
				if (Item.itemsList[i2] != null && Item.itemsList[i2].HasSubtypes)
				{
					i3 = this.theWorld.getBlockMetadata(this.objectMouseOver.blockX, this.objectMouseOver.blockY, this.objectMouseOver.blockZ);
					z4 = true;
				}

				if (Item.itemsList[i2] != null && Item.itemsList[i2] is ItemBlock)
				{
					Block block5 = Block.blocksList[i2];
					int i6 = block5.idDropped(i3, this.thePlayer.worldObj.rand, 0);
					if (i6 > 0)
					{
						i2 = i6;
					}
				}

				this.thePlayer.inventory.setCurrentItem(i2, i3, z4, z1);
				if (z1)
				{
					int i7 = this.thePlayer.inventorySlots.inventorySlots.Count - 9 + this.thePlayer.inventory.currentItem;
					this.playerController.sendSlotPacket(this.thePlayer.inventory.getStackInSlot(this.thePlayer.inventory.currentItem), i7);
				}
			}

		}

		public static string func_52003_C()
		{
			return "1.2.5";
		}

		public static void func_52004_D()
		{
			GCMemoryInfo gcMemoryInfo = GC.GetGCMemoryInfo();
			long installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
			double physicalMemory = installedMemory / 1048576.0D;

			PlayerUsageSnooper playerUsageSnooper0 = new PlayerUsageSnooper("client");
			playerUsageSnooper0.func_52022_a("version", func_52003_C());
			playerUsageSnooper0.func_52022_a("os_name", Environment.OSVersion);
			playerUsageSnooper0.func_52022_a("os_version", Environment.OSVersion.Version);
            playerUsageSnooper0.func_52022_a("os_architecture", Environment.Is64BitOperatingSystem ? "x86_64" : "x86");
            playerUsageSnooper0.func_52022_a("memory_total", physicalMemory + "MB");
            playerUsageSnooper0.func_52022_a("memory_max", (Process.GetCurrentProcess().WorkingSet64 / 1000000) + "MB");
            playerUsageSnooper0.func_52022_a("java_version", Environment.Version); // hehehe
			playerUsageSnooper0.func_52022_a("opengl_version", GL.GetString(StringName.Version));
			playerUsageSnooper0.func_52022_a("opengl_vendor", GL.GetString(StringName.Vendor));
			playerUsageSnooper0.func_52021_a();
		}
	}

}