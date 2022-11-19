
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace net.minecraft.src
{

	public class GuiMultiplayer : GuiScreen
	{
		private static int threadsPending = 0;
		private static object @lock = new object();
		private GuiScreen parentScreen;
		private GuiSlotServer serverSlotContainer;
		private System.Collections.IList serverList = new ArrayList();
		private int selectedServer = -1;
		private GuiButton buttonEdit;
		private GuiButton buttonSelect;
		private GuiButton buttonDelete;
		private bool deleteClicked = false;
		private bool addClicked = false;
		private bool editClicked = false;
		private bool directClicked = false;
		private string lagTooltip = null;
		private ServerNBTStorage tempServer = null;

		public GuiMultiplayer(GuiScreen guiScreen1)
		{
			this.parentScreen = guiScreen1;
		}

		public override void updateScreen()
		{
		}

		public override void initGui()
		{
			this.loadServerList();
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Clear();
			this.serverSlotContainer = new GuiSlotServer(this);
			this.initGuiControls();
		}

		private void loadServerList()
		{
			try
			{
				if (!File.Exists(mc.mcDataDir + "/servers.dat"))
					File.Create(mc.mcDataDir + "/servers.dat");

				NBTTagCompound? nBTTagCompound1 = CompressedStreamTools.read(new FileInfo(mc.mcDataDir + "/servers.dat"));
				NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("servers");
				this.serverList.Clear();

				for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
				{
					this.serverList.Add(ServerNBTStorage.createServerNBTStorage((NBTTagCompound)nBTTagList2.tagAt(i3)));
				}
			}
			catch (Exception exception4)
			{
				Console.WriteLine(exception4.ToString());
				Console.Write(exception4.StackTrace);
			}

		}

		private void saveServerList()
		{
			try
			{
				NBTTagList nBTTagList1 = new NBTTagList();

				for (int i2 = 0; i2 < this.serverList.Count; ++i2)
				{
					nBTTagList1.appendTag(((ServerNBTStorage)this.serverList[i2]).CompoundTag);
				}

				NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
				nBTTagCompound4.setTag("servers", nBTTagList1);
				CompressedStreamTools.safeWrite(nBTTagCompound4, new FileInfo(mc.mcDataDir + "/servers.dat"));
			}
			catch (Exception exception3)
			{
				Console.WriteLine(exception3.ToString());
				Console.Write(exception3.StackTrace);
			}

		}

		public virtual void initGuiControls()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Add(this.buttonEdit = new GuiButton(7, this.width / 2 - 154, this.height - 28, 70, 20, stringTranslate1.translateKey("selectServer.edit")));
			this.controlList.Add(this.buttonDelete = new GuiButton(2, this.width / 2 - 74, this.height - 28, 70, 20, stringTranslate1.translateKey("selectServer.delete")));
			this.controlList.Add(this.buttonSelect = new GuiButton(1, this.width / 2 - 154, this.height - 52, 100, 20, stringTranslate1.translateKey("selectServer.select")));
			this.controlList.Add(new GuiButton(4, this.width / 2 - 50, this.height - 52, 100, 20, stringTranslate1.translateKey("selectServer.direct")));
			this.controlList.Add(new GuiButton(3, this.width / 2 + 4 + 50, this.height - 52, 100, 20, stringTranslate1.translateKey("selectServer.add")));
			this.controlList.Add(new GuiButton(8, this.width / 2 + 4, this.height - 28, 70, 20, stringTranslate1.translateKey("selectServer.refresh")));
			this.controlList.Add(new GuiButton(0, this.width / 2 + 4 + 76, this.height - 28, 75, 20, stringTranslate1.translateKey("gui.cancel")));
			bool z2 = this.selectedServer >= 0 && this.selectedServer < this.serverSlotContainer.Size;
			this.buttonSelect.enabled = z2;
			this.buttonEdit.enabled = z2;
			this.buttonDelete.enabled = z2;
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(false);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 2)
				{
					string string2 = ((ServerNBTStorage)this.serverList[this.selectedServer]).name;
					if (!string.ReferenceEquals(string2, null))
					{
						this.deleteClicked = true;
						StringTranslate stringTranslate3 = StringTranslate.Instance;
						string string4 = stringTranslate3.translateKey("selectServer.deleteQuestion");
						string string5 = "\'" + string2 + "\' " + stringTranslate3.translateKey("selectServer.deleteWarning");
						string string6 = stringTranslate3.translateKey("selectServer.deleteButton");
						string string7 = stringTranslate3.translateKey("gui.cancel");
						GuiYesNo guiYesNo8 = new GuiYesNo(this, string4, string5, string6, string7, this.selectedServer);
						this.mc.displayGuiScreen(guiYesNo8);
					}
				}
				else if (guiButton1.id == 1)
				{
					this.joinServer(this.selectedServer);
				}
				else if (guiButton1.id == 4)
				{
					this.directClicked = true;
					this.mc.displayGuiScreen(new GuiScreenServerList(this, this.tempServer = new ServerNBTStorage(StatCollector.translateToLocal("selectServer.defaultName"), "")));
				}
				else if (guiButton1.id == 3)
				{
					this.addClicked = true;
					this.mc.displayGuiScreen(new GuiScreenAddServer(this, this.tempServer = new ServerNBTStorage(StatCollector.translateToLocal("selectServer.defaultName"), "")));
				}
				else if (guiButton1.id == 7)
				{
					this.editClicked = true;
					ServerNBTStorage serverNBTStorage9 = (ServerNBTStorage)this.serverList[this.selectedServer];
					this.mc.displayGuiScreen(new GuiScreenAddServer(this, this.tempServer = new ServerNBTStorage(serverNBTStorage9.name, serverNBTStorage9.host)));
				}
				else if (guiButton1.id == 0)
				{
					this.mc.displayGuiScreen(this.parentScreen);
				}
				else if (guiButton1.id == 8)
				{
					this.mc.displayGuiScreen(new GuiMultiplayer(this.parentScreen));
				}
				else
				{
					this.serverSlotContainer.actionPerformed(guiButton1);
				}

			}
		}

		public override void confirmClicked(bool z1, int i2)
		{
			if (this.deleteClicked)
			{
				this.deleteClicked = false;
				if (z1)
				{
					this.serverList.RemoveAt(i2);
					this.saveServerList();
				}

				this.mc.displayGuiScreen(this);
			}
			else if (this.directClicked)
			{
				this.directClicked = false;
				if (z1)
				{
					this.joinServer(this.tempServer);
				}
				else
				{
					this.mc.displayGuiScreen(this);
				}
			}
			else if (this.addClicked)
			{
				this.addClicked = false;
				if (z1)
				{
					this.serverList.Add(this.tempServer);
					this.saveServerList();
				}

				this.mc.displayGuiScreen(this);
			}
			else if (this.editClicked)
			{
				this.editClicked = false;
				if (z1)
				{
					ServerNBTStorage serverNBTStorage3 = (ServerNBTStorage)this.serverList[this.selectedServer];
					serverNBTStorage3.name = this.tempServer.name;
					serverNBTStorage3.host = this.tempServer.host;
					this.saveServerList();
				}

				this.mc.displayGuiScreen(this);
			}

		}

		private int parseIntWithDefault(string string1, int i2)
		{
			try
			{
				return int.Parse(string1.Trim());
			}
			catch (Exception)
			{
				return i2;
			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (c1 == (char)13)
			{
				this.actionPerformed((GuiButton)this.controlList[2]);
			}

		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.lagTooltip = null;
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawDefaultBackground();
			this.serverSlotContainer.drawScreen(i1, i2, f3);
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("multiplayer.title"), this.width / 2, 20, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
			if (!string.ReferenceEquals(this.lagTooltip, null))
			{
				this.func_35325_a(this.lagTooltip, i1, i2);
			}

		}

		private void joinServer(int i1)
		{
			this.joinServer((ServerNBTStorage)this.serverList[i1]);
		}

		private void joinServer(ServerNBTStorage serverNBTStorage1)
		{
			string string2 = serverNBTStorage1.host;
			string[] string3 = string2.Split(":", true);
			if (string2.StartsWith("[", StringComparison.Ordinal))
			{
				int i4 = string2.IndexOf("]", StringComparison.Ordinal);
				if (i4 > 0)
				{
					string string5 = string2.Substring(1, i4 - 1);
					string string6 = string2.Substring(i4 + 1).Trim();
					if (string6.StartsWith(":", StringComparison.Ordinal) && string6.Length > 0)
					{
						string6 = string6.Substring(1);
						string3 = new string[]{string5, string6};
					}
					else
					{
						string3 = new string[]{string5};
					}
				}
			}

			if (string3.Length > 2)
			{
				string3 = new string[]{string2};
			}

			this.mc.displayGuiScreen(new GuiConnecting(this.mc, string3[0], string3.Length > 1 ? this.parseIntWithDefault(string3[1], 25565) : 25565));
		}
        
		private void pollServer(ServerNBTStorage serverNBTStorage1)
		{
			string hostAddress = serverNBTStorage1.host;
			string[] addressSections = hostAddress.Split(":", true);
			if (hostAddress.StartsWith("[", StringComparison.Ordinal))
			{
				int i4 = hostAddress.IndexOf("]", StringComparison.Ordinal);
				if (i4 > 0)
				{
					string string5 = hostAddress.Substring(1, i4 - 1);
					string string6 = hostAddress.Substring(i4 + 1).Trim();
					if (string6.StartsWith(":", StringComparison.Ordinal) && string6.Length > 0)
					{
						string6 = string6.Substring(1);
						addressSections = new string[]{string5, string6};
					}
					else
					{
						addressSections = new string[]{string5};
					}
				}
			}

			if (addressSections.Length > 2)
			{
				addressSections = new string[]{hostAddress};
			}

			string ipAddress = addressSections[0];
			int port = addressSections.Length > 1 ? parseIntWithDefault(addressSections[1], 25565) : 25565;
			Socket serverSocket = null;
			BinaryReader reader = null;
			BinaryWriter writer = null;

			try
			{
				if (ipAddress == "localhost")
					ipAddress = "127.0.0.1";

				IPHostEntry host = Dns.GetHostEntry(ipAddress);
				IPAddress ip = host.AddressList[0];
				IPEndPoint endPoint = new(ip, port);

                serverSocket = new(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				serverSocket.NoDelay = true;
				serverSocket.SendTimeout = 30000;
				serverSocket.ReceiveTimeout = 30000;
				serverSocket.Blocking = true;
				Console.WriteLine("Pre connect");
                serverSocket.Connect(endPoint);
				Console.WriteLine("Post connect");

				NetworkStream networkStream = new(serverSocket);

				reader = new BinaryReader(networkStream);
				writer = new BinaryWriter(networkStream);

                Console.WriteLine("Writing");
                writer.Write((byte)254);
                Console.WriteLine("Reading");
                if (reader.ReadByte() != 255)
                {
                    throw new IOException("Bad message");
                }
                Console.WriteLine("readpacket");
                string string9 = Packet.readString(reader, 256);
                char[] c10 = string9.ToCharArray();
                Console.WriteLine("donereadpacket");

                int i11;
				for (i11 = 0; i11 < c10.Length; ++i11)
				{
					if (c10[i11] != (char)167 && ChatAllowedCharacters.allowedCharacters.IndexOf(c10[i11]) < 0)
					{
						c10[i11] = (char)63;
					}
				}

				string9 = new string(c10);
				addressSections = string9.Split("\u00a7", true);
				string9 = addressSections[0];
				i11 = -1;
				int i12 = -1;

				try
				{
					i11 = int.Parse(addressSections[1]);
					i12 = int.Parse(addressSections[2]);
				}
				catch (Exception)
				{
				}

				serverNBTStorage1.motd = "\u00a77" + string9;
				if (i11 >= 0 && i12 > 0)
				{
					serverNBTStorage1.playerCount = "\u00a77" + i11 + "\u00a78/\u00a77" + i12;
				}
				else
				{
					serverNBTStorage1.playerCount = "\u00a78???";
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
                
			}
			finally
			{
				serverSocket?.Disconnect(false);
				serverSocket?.Close();
                reader?.Dispose();
                writer?.Dispose();
            }

		}

		protected internal virtual void func_35325_a(string string1, int i2, int i3)
		{
			if (!string.ReferenceEquals(string1, null))
			{
				int i4 = i2 + 12;
				int i5 = i3 - 12;
				int i6 = this.fontRenderer.getStringWidth(string1);
				this.drawGradientRect(i4 - 3, i5 - 3, i4 + i6 + 3, i5 + 8 + 3, -1073741824, -1073741824);
				this.fontRenderer.drawStringWithShadow(string1, i4, i5, -1);
			}
		}

		internal static System.Collections.IList getServerList(GuiMultiplayer guiMultiplayer0)
		{
			return guiMultiplayer0.serverList;
		}

		internal static int setSelectedServer(GuiMultiplayer guiMultiplayer0, int i1)
		{
			return guiMultiplayer0.selectedServer = i1;
		}

		internal static int getSelectedServer(GuiMultiplayer guiMultiplayer0)
		{
			return guiMultiplayer0.selectedServer;
		}

		internal static GuiButton getButtonSelect(GuiMultiplayer guiMultiplayer0)
		{
			return guiMultiplayer0.buttonSelect;
		}

		internal static GuiButton getButtonEdit(GuiMultiplayer guiMultiplayer0)
		{
			return guiMultiplayer0.buttonEdit;
		}

		internal static GuiButton getButtonDelete(GuiMultiplayer guiMultiplayer0)
		{
			return guiMultiplayer0.buttonDelete;
		}

		internal static void joinServer(GuiMultiplayer guiMultiplayer0, int i1)
		{
			guiMultiplayer0.joinServer(i1);
		}

		internal static object Lock
		{
			get
			{
				return @lock;
			}
		}

		internal static int ThreadsPending
		{
			get
			{
				return threadsPending;
			}
		}

		internal static int incrementThreadsPending()
		{
			return threadsPending++;
		}

		internal static void pollServer(GuiMultiplayer guiMultiplayer0, ServerNBTStorage serverNBTStorage1)
		{
			guiMultiplayer0.pollServer(serverNBTStorage1);
		}

		internal static int decrementThreadsPending()
		{
			return threadsPending--;
		}

		internal static string setTooltipText(GuiMultiplayer guiMultiplayer0, string string1)
		{
			return guiMultiplayer0.lagTooltip = string1;
		}
	}

}