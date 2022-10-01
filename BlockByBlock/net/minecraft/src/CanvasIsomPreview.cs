/*using System;
using System.Collections.Generic;
using System.Threading;

namespace net.minecraft.src
{

	public class CanvasIsomPreview : Canvas, KeyListener, MouseListener, MouseMotionListener, ThreadStart
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			workDir = this.WorkingDirectory;
		}

		private int currentRender = 0;
		private int zoom = 2;
		private bool showHelp = true;
		private World level;
		private File workDir;
		private bool running = true;
		private System.Collections.IList zonesToRender = Collections.synchronizedList(new LinkedList());
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
//ORIGINAL LINE: private IsoImageBuffer[][] zoneMap = new IsoImageBuffer[64][64];
		private IsoImageBuffer[][] zoneMap = RectangularArrays.RectangularIsoImageBufferArray(64, 64);
		private int field_1785_i;
		private int field_1784_j;
		private int field_1783_k;
		private int field_1782_l;

		public virtual File WorkingDirectory
		{
			get
			{
				if (this.workDir == null)
				{
					this.workDir = this.getWorkingDirectory("minecraft");
				}
    
				return this.workDir;
			}
		}

		public virtual File getWorkingDirectory(string string1)
		{
			string string2 = System.getProperty("user.home", ".");
			File file3;
			switch (OsMap.osValues[Platform.ordinal()])
			{
			case 1:
			case 2:
				file3 = new File(string2, '.' + string1 + '/');
				break;
			case 3:
				string string4 = Environment.GetEnvironmentVariable("APPDATA");
				if (!string.ReferenceEquals(string4, null))
				{
					file3 = new File(string4, "." + string1 + '/');
				}
				else
				{
					file3 = new File(string2, '.' + string1 + '/');
				}
				break;
			case 4:
				file3 = new File(string2, "Library/Application Support/" + string1);
				break;
			default:
				file3 = new File(string2, string1 + '/');
			break;
			}

			if (!file3.exists() && !file3.mkdirs())
			{
				throw new Exception("The working directory could not be created: " + file3);
			}
			else
			{
				return file3;
			}
		}

		private static EnumOS1 Platform
		{
			get
			{
				string string0 = System.getProperty("os.name").ToLower();
				return string0.Contains("win") ? EnumOS1.windows : (string0.Contains("mac") ? EnumOS1.macos : (string0.Contains("solaris") ? EnumOS1.solaris : (string0.Contains("sunos") ? EnumOS1.solaris : (string0.Contains("linux") ? EnumOS1.linux : (string0.Contains("unix") ? EnumOS1.linux : EnumOS1.unknown)))));
			}
		}

		public CanvasIsomPreview()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			for (int i1 = 0; i1 < 64; ++i1)
			{
				for (int i2 = 0; i2 < 64; ++i2)
				{
					this.zoneMap[i1][i2] = new IsoImageBuffer((World)null, i1, i2);
				}
			}

			this.addMouseListener(this);
			this.addMouseMotionListener(this);
			this.addKeyListener(this);
			this.setFocusable(true);
			this.requestFocus();
			this.setBackground(Color.red);
		}

		public virtual void loadLevel(string string1)
		{
			this.field_1785_i = this.field_1784_j = 0;
			this.level = new World(new SaveHandler(new File(this.workDir, "saves"), string1, false), string1, new WorldSettings((new Random()).nextLong(), 0, true, false, WorldType.DEFAULT));
			this.level.skylightSubtracted = 0;
			System.Collections.IList list2 = this.zonesToRender;
			lock (this.zonesToRender)
			{
				this.zonesToRender.Clear();

				for (int i3 = 0; i3 < 64; ++i3)
				{
					for (int i4 = 0; i4 < 64; ++i4)
					{
						this.zoneMap[i3][i4].init(this.level, i3, i4);
					}
				}

			}
		}

		private int Brightness
		{
			set
			{
				System.Collections.IList list2 = this.zonesToRender;
				lock (this.zonesToRender)
				{
					this.level.skylightSubtracted = value;
					this.zonesToRender.Clear();
    
					for (int i3 = 0; i3 < 64; ++i3)
					{
						for (int i4 = 0; i4 < 64; ++i4)
						{
							this.zoneMap[i3][i4].init(this.level, i3, i4);
						}
					}
    
				}
			}
		}

		public virtual void start()
		{
			(new ThreadRunIsoClient(this)).Start();

			for (int i1 = 0; i1 < 8; ++i1)
			{
				(new Thread(this)).Start();
			}

		}

		public virtual void stop()
		{
			this.running = false;
		}

		private IsoImageBuffer getZone(int i1, int i2)
		{
			int i3 = i1 & 63;
			int i4 = i2 & 63;
			IsoImageBuffer isoImageBuffer5 = this.zoneMap[i3][i4];
			if (isoImageBuffer5.x == i1 && isoImageBuffer5.y == i2)
			{
				return isoImageBuffer5;
			}
			else
			{
				System.Collections.IList list6 = this.zonesToRender;
				lock (this.zonesToRender)
				{
					this.zonesToRender.Remove(isoImageBuffer5);
				}

				isoImageBuffer5.init(i1, i2);
				return isoImageBuffer5;
			}
		}

		public virtual void run()
		{
			TerrainTextureManager terrainTextureManager1 = new TerrainTextureManager();

			while (this.running)
			{
				IsoImageBuffer isoImageBuffer2 = null;
				System.Collections.IList list3 = this.zonesToRender;
				lock (this.zonesToRender)
				{
					if (this.zonesToRender.Count > 0)
					{
						isoImageBuffer2 = (IsoImageBuffer)this.zonesToRender.RemoveAndReturn(0);
					}
				}

				if (isoImageBuffer2 != null)
				{
					if (this.currentRender - isoImageBuffer2.lastVisible < 2)
					{
						terrainTextureManager1.render(isoImageBuffer2);
						this.repaint();
					}
					else
					{
						isoImageBuffer2.addedToRenderQueue = false;
					}
				}

				try
				{
					Thread.Sleep(2L);
				}
				catch (InterruptedException interruptedException5)
				{
					Console.WriteLine(interruptedException5.ToString());
					Console.Write(interruptedException5.StackTrace);
				}
			}

		}

		public virtual void update(Graphics graphics1)
		{
		}

		public virtual void paint(Graphics graphics1)
		{
		}

		public virtual void render()
		{
			BufferStrategy bufferStrategy1 = this.getBufferStrategy();
			if (bufferStrategy1 == null)
			{
				this.createBufferStrategy(2);
			}
			else
			{
				this.render((Graphics2D)bufferStrategy1.getDrawGraphics());
				bufferStrategy1.show();
			}
		}

		public virtual void render(Graphics2D graphics2D1)
		{
			++this.currentRender;
			AffineTransform affineTransform2 = graphics2D1.getTransform();
			graphics2D1.setClip(0, 0, this.getWidth(), this.getHeight());
			graphics2D1.setRenderingHint(RenderingHints.KEY_INTERPOLATION, RenderingHints.VALUE_INTERPOLATION_NEAREST_NEIGHBOR);
			graphics2D1.translate(this.getWidth() / 2, this.getHeight() / 2);
			graphics2D1.scale((double)this.zoom, (double)this.zoom);
			graphics2D1.translate(this.field_1785_i, this.field_1784_j);
			if (this.level != null)
			{
				ChunkCoordinates chunkCoordinates3 = this.level.SpawnPoint;
				graphics2D1.translate(-(chunkCoordinates3.posX + chunkCoordinates3.posZ), -(-chunkCoordinates3.posX + chunkCoordinates3.posZ) + 64);
			}

			Rectangle rectangle17 = graphics2D1.getClipBounds();
			graphics2D1.setColor(new Color(-15724512));
			graphics2D1.fillRect(rectangle17.x, rectangle17.y, rectangle17.width, rectangle17.height);
			sbyte b4 = 16;
			sbyte b5 = 3;
			int i6 = rectangle17.x / b4 / 2 - 2 - b5;
			int i7 = (rectangle17.x + rectangle17.width) / b4 / 2 + 1 + b5;
			int i8 = rectangle17.y / b4 - 1 - b5 * 2;
			int i9 = (rectangle17.y + rectangle17.height + 16 + 128) / b4 + 1 + b5 * 2;

			int i10;
			for (i10 = i8; i10 <= i9; ++i10)
			{
				for (int i11 = i6; i11 <= i7; ++i11)
				{
					int i12 = i11 - (i10 >> 1);
					int i13 = i11 + (i10 + 1 >> 1);
					IsoImageBuffer isoImageBuffer14 = this.getZone(i12, i13);
					isoImageBuffer14.lastVisible = this.currentRender;
					if (!isoImageBuffer14.rendered)
					{
						if (!isoImageBuffer14.addedToRenderQueue)
						{
							isoImageBuffer14.addedToRenderQueue = true;
							this.zonesToRender.Add(isoImageBuffer14);
						}
					}
					else
					{
						isoImageBuffer14.addedToRenderQueue = false;
						if (!isoImageBuffer14.noContent)
						{
							int i15 = i11 * b4 * 2 + (i10 & 1) * b4;
							int i16 = i10 * b4 - 128 - 16;
							graphics2D1.drawImage(isoImageBuffer14.image, i15, i16, (ImageObserver)null);
						}
					}
				}
			}

			if (this.showHelp)
			{
				graphics2D1.setTransform(affineTransform2);
				i10 = this.getHeight() - 32 - 4;
				graphics2D1.setColor(new Color(int.MinValue, true));
				graphics2D1.fillRect(4, this.getHeight() - 32 - 4, this.getWidth() - 8, 32);
				graphics2D1.setColor(Color.WHITE);
				string string18 = "F1 - F5: load levels   |   0-9: Set time of day   |   Space: return to spawn   |   Double click: zoom   |   Escape: hide this text";
				graphics2D1.drawString(string18, this.getWidth() / 2 - graphics2D1.getFontMetrics().stringWidth(string18) / 2, i10 + 20);
			}

			graphics2D1.dispose();
		}

		public virtual void mouseDragged(MouseEvent mouseEvent1)
		{
			int i2 = mouseEvent1.getX() / this.zoom;
			int i3 = mouseEvent1.getY() / this.zoom;
			this.field_1785_i += i2 - this.field_1783_k;
			this.field_1784_j += i3 - this.field_1782_l;
			this.field_1783_k = i2;
			this.field_1782_l = i3;
			this.repaint();
		}

		public virtual void mouseMoved(MouseEvent mouseEvent1)
		{
		}

		public virtual void mouseClicked(MouseEvent mouseEvent1)
		{
			if (mouseEvent1.getClickCount() == 2)
			{
				this.zoom = 3 - this.zoom;
				this.repaint();
			}

		}

		public virtual void mouseEntered(MouseEvent mouseEvent1)
		{
		}

		public virtual void mouseExited(MouseEvent mouseEvent1)
		{
		}

		public virtual void mousePressed(MouseEvent mouseEvent1)
		{
			int i2 = mouseEvent1.getX() / this.zoom;
			int i3 = mouseEvent1.getY() / this.zoom;
			this.field_1783_k = i2;
			this.field_1782_l = i3;
		}

		public virtual void mouseReleased(MouseEvent mouseEvent1)
		{
		}

		public virtual void keyPressed(KeyEvent keyEvent1)
		{
			if (keyEvent1.getKeyCode() == 48)
			{
				this.Brightness = 11;
			}

			if (keyEvent1.getKeyCode() == 49)
			{
				this.Brightness = 10;
			}

			if (keyEvent1.getKeyCode() == 50)
			{
				this.Brightness = 9;
			}

			if (keyEvent1.getKeyCode() == 51)
			{
				this.Brightness = 7;
			}

			if (keyEvent1.getKeyCode() == 52)
			{
				this.Brightness = 6;
			}

			if (keyEvent1.getKeyCode() == 53)
			{
				this.Brightness = 5;
			}

			if (keyEvent1.getKeyCode() == 54)
			{
				this.Brightness = 3;
			}

			if (keyEvent1.getKeyCode() == 55)
			{
				this.Brightness = 2;
			}

			if (keyEvent1.getKeyCode() == 56)
			{
				this.Brightness = 1;
			}

			if (keyEvent1.getKeyCode() == 57)
			{
				this.Brightness = 0;
			}

			if (keyEvent1.getKeyCode() == 112)
			{
				this.loadLevel("World1");
			}

			if (keyEvent1.getKeyCode() == 113)
			{
				this.loadLevel("World2");
			}

			if (keyEvent1.getKeyCode() == 114)
			{
				this.loadLevel("World3");
			}

			if (keyEvent1.getKeyCode() == 115)
			{
				this.loadLevel("World4");
			}

			if (keyEvent1.getKeyCode() == 116)
			{
				this.loadLevel("World5");
			}

			if (keyEvent1.getKeyCode() == 32)
			{
				this.field_1785_i = this.field_1784_j = 0;
			}

			if (keyEvent1.getKeyCode() == 27)
			{
				this.showHelp = !this.showHelp;
			}

			this.repaint();
		}

		public virtual void keyReleased(KeyEvent keyEvent1)
		{
		}

		public virtual void keyTyped(KeyEvent keyEvent1)
		{
		}

		internal static bool isRunning(CanvasIsomPreview canvasIsomPreview0)
		{
			return canvasIsomPreview0.running;
		}
	}

}*/