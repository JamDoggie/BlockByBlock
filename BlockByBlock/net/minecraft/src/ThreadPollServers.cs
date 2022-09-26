using BlockByBlock.helpers;
using System;
using System.Net.Sockets;
using System.Threading;

namespace net.minecraft.src
{

	internal class ThreadPollServers
	{
		internal readonly ServerNBTStorage server;
		internal readonly GuiSlotServer serverSlotContainer;

		protected Thread thread;

		internal ThreadPollServers(GuiSlotServer guiSlotServer1, ServerNBTStorage serverNBTStorage2)
		{
			this.serverSlotContainer = guiSlotServer1;
			this.server = serverNBTStorage2;

			thread = new Thread(() => run());
		}

        public virtual void startThread()
        {
			thread.Start();
        }
        
		protected virtual void run()
		{
			bool z27 = false;

			{
				{
					{
						{
							{
								try
								{
									z27 = true;
									this.server.motd = "\u00a78Polling..";
									long j1 = JTime.NanoTime();
									GuiMultiplayer.pollServer(this.serverSlotContainer.parentGui, this.server);
									long j3 = JTime.NanoTime();
									this.server.lag = (j3 - j1) / 1000000L;
									z27 = false;
									goto label183Break;
								}
								catch (SocketException)
								{
									this.server.lag = -1L;
									this.server.motd = "\u00a74Can\'t reach server";
									z27 = false;
									goto label187Break;
								}
								catch (IOException)
								{
									this.server.lag = -1L;
									this.server.motd = "\u00a74Communication error";
									z27 = false;
									goto label186Break;
								}
								catch (Exception exception39)
								{
									this.server.lag = -1L;
									this.server.motd = "ERROR: " + exception39.GetType();
									z27 = false;
									goto label185Break;
								}
								finally
								{
									if (z27)
									{
										lock (GuiMultiplayer.Lock)
										{
											GuiMultiplayer.decrementThreadsPending();
										}
									}
								}

								lock (GuiMultiplayer.Lock)
								{
									GuiMultiplayer.decrementThreadsPending();
									return;
								}
							}
							label187Break:

							lock (GuiMultiplayer.Lock)
							{
								GuiMultiplayer.decrementThreadsPending();
								return;
							}
						}
						label186Break:

						lock (GuiMultiplayer.Lock)
						{
							GuiMultiplayer.decrementThreadsPending();
							return;
						}
					}
					label185Break:

					lock (GuiMultiplayer.Lock)
					{
						GuiMultiplayer.decrementThreadsPending();
						return;
					}
				}
				label184Break:

				lock (GuiMultiplayer.Lock)
				{
					GuiMultiplayer.decrementThreadsPending();
					return;
				}
			}
			label183Break:

			lock (GuiMultiplayer.Lock)
			{
				GuiMultiplayer.decrementThreadsPending();
			}

		}
	}

}