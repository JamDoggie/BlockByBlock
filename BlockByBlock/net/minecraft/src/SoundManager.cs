using BlockByBlock.java_extensions;
using BlockByBlock.sound;
using net.minecraft.client.entity;
using OpenTK.Mathematics;
using System;

namespace net.minecraft.src
{
    public class SoundManager
	{
		private bool InstanceFieldsInitialized = false;

		public SoundManager()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

		private void InitializeInstanceFields()
		{
			//ticksBeforeMusic = this.rand.Next(12000);
			ticksBeforeMusic = 10;
		}

		private static volatile SoundSystem sndSystem;
		private SoundPool soundPoolSounds = new SoundPool();
		private SoundPool soundPoolStreaming = new SoundPool();
		private SoundPool soundPoolMusic = new SoundPool();
		private int latestSoundID = 0;
		private GameSettings options;
		private static bool loaded = false;
		private RandomExtended rand = new RandomExtended();
		private int ticksBeforeMusic;
		internal Thread SoundThread;

		public virtual void loadSoundSettings(GameSettings gameSettings1)
		{
			this.soundPoolStreaming.isGetRandomSound = false;
			this.options = gameSettings1;
			if (!loaded && (gameSettings1 == null || gameSettings1.soundVolume != 0.0F || gameSettings1.musicVolume != 0.0F))
			{
				this.tryToSetLibraryAndCodecs();
			}

		}

		private void tryToSetLibraryAndCodecs()
		{
			float f1 = options.soundVolume;
			float f2 = options.musicVolume;
			options.soundVolume = 0.0F;
			options.musicVolume = 0.0F;
			options.saveOptions();
            
			SoundThread = new(() =>
			{
                sndSystem = new SoundSystem(null, null);
				sndSystem.RunThread();
            });
			SoundThread.Start();
			/*while(sndSystem == null)
			{
                // Block until the sound system is initialized.
            }*/

            options.soundVolume = f1;
			options.musicVolume = f2;
			options.saveOptions();

			loaded = true;
		}

		internal void tick()
		{
			//if (sndSystem != null)
			//	sndSystem.TickSoundSystem();
		}

		public virtual void onSoundOptionsChanged()
		{
			if (!loaded && (this.options.soundVolume != 0.0F || this.options.musicVolume != 0.0F))
			{
				this.tryToSetLibraryAndCodecs();
			}

			if (loaded)
			{
				if (this.options.musicVolume == 0.0F)
				{
					//sndSystem.Stop("BgMusic");
				}
				else
				{
					sndSystem.SetVolume("BgMusic", this.options.musicVolume);
				}
			}

		}

		public virtual void closeMinecraft()
		{
			if (loaded)
			{
                sndSystem.Cleanup();
                SoundThread.Join();
            }

		}

		public virtual void addSound(string string1, FileInfo file2)
		{
			this.soundPoolSounds.addSound(string1, file2);
		}

		public virtual void addStreaming(string string1, FileInfo file2)
		{
			this.soundPoolStreaming.addSound(string1, file2);
		}

		public virtual void addMusic(string string1, FileInfo file2)
		{
			this.soundPoolMusic.addSound(string1, file2);
		}

		public virtual void playRandomMusicIfReady()
		{
			if (loaded && this.options.musicVolume != 0.0F)
			{
				if (!sndSystem.Playing("BgMusic") && !sndSystem.Playing("streaming"))
				{
					if (this.ticksBeforeMusic > 0)
					{
						--this.ticksBeforeMusic;
						return;
					}

					SoundPoolEntry soundPoolEntry1 = this.soundPoolMusic.RandomSound;
					if (soundPoolEntry1 != null)
					{
						Console.WriteLine("Playing music");
						this.ticksBeforeMusic = this.rand.Next(12000) + 12000;
						sndSystem.BackgroundMusic("BgMusic", soundPoolEntry1.soundUrl, soundPoolEntry1.soundName, false);
						sndSystem.SetVolume("BgMusic", this.options.musicVolume);
						sndSystem.Play("BgMusic");
					}
				}

			}
		}

		public virtual void setListener(EntityLiving entityLiving1, float f2)
		{
			if (loaded && this.options.soundVolume != 0.0F)
			{
				if (entityLiving1 != null)
				{
					float f3 = entityLiving1.prevRotationYaw + (entityLiving1.rotationYaw - entityLiving1.prevRotationYaw) * f2;
					double d4 = entityLiving1.prevPosX + (entityLiving1.posX - entityLiving1.prevPosX) * (double)f2;
					double d6 = entityLiving1.prevPosY + (entityLiving1.posY - entityLiving1.prevPosY) * (double)f2;
					double d8 = entityLiving1.prevPosZ + (entityLiving1.posZ - entityLiving1.prevPosZ) * (double)f2;
					float f10 = MathHelper.cos(-f3 * 0.017453292F - (float)Math.PI);
					float f11 = MathHelper.sin(-f3 * 0.017453292F - (float)Math.PI);
					float f12 = -f11;
					float f13 = 0.0F;
					float f14 = -f10;
					float f15 = 0.0F;
					float f16 = 1.0F;
					float f17 = 0.0F;
					sndSystem.SetListenerPosition((float)d4, (float)d6, (float)d8);
					sndSystem.SetListenerOrientation(f12, f13, f14, f15, f16, f17);
				}
			}
		}

		public virtual void playStreaming(string string1, float f2, float f3, float f4, float f5, float f6)
		{
			if (loaded && (this.options.soundVolume != 0.0F || string.ReferenceEquals(string1, null)))
			{
				string string7 = "streaming";
				if (sndSystem.Playing("streaming"))
				{
					sndSystem.Stop("streaming");
				}

				if (!string.ReferenceEquals(string1, null))
				{
					SoundPoolEntry soundPoolEntry8 = this.soundPoolStreaming.getRandomSoundFromSoundPool(string1);
					if (soundPoolEntry8 != null && f5 > 0.0F)
					{
						if (sndSystem.Playing("BgMusic"))
						{
							sndSystem.Stop("BgMusic");
						}

						float f9 = 16.0F;
						sndSystem.NewStreamingSource(true, string7, soundPoolEntry8.soundUrl, soundPoolEntry8.soundName, false, new Vector3(f2, f3, f4), 2, f9 * 4.0F);
						sndSystem.SetVolume(string7, 0.5F * this.options.soundVolume);
						sndSystem.Play(string7);
					}

				}
			}
		}

		public virtual void playSound(string string1, float x, float y, float z, float f5, float f6)
		{
			if (loaded && this.options.soundVolume != 0.0F)
			{
				SoundPoolEntry? soundPoolEntry7 = this.soundPoolSounds.getRandomSoundFromSoundPool(string1);
				if (soundPoolEntry7 != null && f5 > 0.0F)
				{
					this.latestSoundID = (this.latestSoundID + 1) % 256;
					string sndIdentifier = "sound_" + this.latestSoundID;
					float f9 = 16.0F;
					if (f5 > 1.0F)
					{
						f9 *= f5;
					}
                    
					sndSystem.NewSource(f5 > 1.0F, sndIdentifier, soundPoolEntry7.soundUrl, soundPoolEntry7.soundName, false, new Vector3(x, y, z));
					sndSystem.SetPitch(sndIdentifier, f6);
					if (f5 > 1.0F)
					{
						f5 = 1.0F;
					}

					sndSystem.SetVolume(sndIdentifier, f5 * this.options.soundVolume);
					sndSystem.Play(sndIdentifier);
				}

			}
		}

		public virtual void playSoundFX(string string1, float f2, float f3)
		{
			if (loaded && this.options.soundVolume != 0.0F)
			{
				SoundPoolEntry? soundPoolEntry4 = this.soundPoolSounds.getRandomSoundFromSoundPool(string1);
				if (soundPoolEntry4 != null)
				{
					this.latestSoundID = (this.latestSoundID + 1) % 256;
					string string5 = "sound_" + this.latestSoundID;
					sndSystem.NewSource(false, string5, soundPoolEntry4.soundUrl, soundPoolEntry4.soundName, false, null);
					if (f2 > 1.0F)
					{
						f2 = 1.0F;
					}

					f2 *= 0.25F;
					sndSystem.SetPitch(string5, f3);
					sndSystem.SetVolume(string5, f2 * this.options.soundVolume);
					sndSystem.Play(string5);
				}

			}
		}
	}

}