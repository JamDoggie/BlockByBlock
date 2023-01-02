using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class VillageSiege
	{
		private World field_48582_a;
		private bool field_48580_b = false;
		private int field_48581_c = -1;
		private int field_48578_d;
		private int field_48579_e;
		private Village field_48576_f;
		private int field_48577_g;
		private int field_48583_h;
		private int field_48584_i;

		public VillageSiege(World world1)
		{
			this.field_48582_a = world1;
		}

		public virtual void tick()
		{
			bool z1 = false;
			if (z1)
			{
				if (this.field_48581_c == 2)
				{
					this.field_48578_d = 100;
					return;
				}
			}
			else
			{
				if (this.field_48582_a.Daytime)
				{
					this.field_48581_c = 0;
					return;
				}

				if (this.field_48581_c == 2)
				{
					return;
				}

				if (this.field_48581_c == 0)
				{
					float f2 = this.field_48582_a.getCelestialAngle(0.0F);
					if ((double)f2 < 0.5D || (double)f2 > 0.501D)
					{
						return;
					}

					this.field_48581_c = this.field_48582_a.rand.Next(10) == 0 ? 1 : 2;
					this.field_48580_b = false;
					if (this.field_48581_c == 2)
					{
						return;
					}
				}
			}

			if (!this.field_48580_b)
			{
				if (!this.func_48574_b())
				{
					return;
				}

				this.field_48580_b = true;
			}

			if (this.field_48579_e > 0)
			{
				--this.field_48579_e;
			}
			else
			{
				this.field_48579_e = 2;
				if (this.field_48578_d > 0)
				{
					this.func_48575_c();
					--this.field_48578_d;
				}
				else
				{
					this.field_48581_c = 2;
				}

			}
		}

		private bool func_48574_b()
		{
			System.Collections.IList list1 = this.field_48582_a.playerEntities;
			System.Collections.IEnumerator iterator2 = list1.GetEnumerator();

			Vec3D vec3D10;
			do
			{
				do
				{
					do
					{
						do
						{
							do
							{
								if (!iterator2.MoveNext())
								{
									return false;
								}

								EntityPlayer entityPlayer3 = (EntityPlayer)iterator2.Current;
								this.field_48576_f = this.field_48582_a.villageCollectionObj.findNearestVillage((int)entityPlayer3.posX, (int)entityPlayer3.posY, (int)entityPlayer3.posZ, 1);
							} while (this.field_48576_f == null);
						} while (this.field_48576_f.NumVillageDoors < 10);
					} while (this.field_48576_f.TicksSinceLastDoorAdding < 20);
				} while (this.field_48576_f.NumVillagers < 20);

				ChunkCoordinates chunkCoordinates4 = this.field_48576_f.Center;
				float f5 = (float)this.field_48576_f.VillageRadius;
				bool z6 = false;

				for (int i7 = 0; i7 < 10; ++i7)
				{
					this.field_48577_g = chunkCoordinates4.posX + (int)((double)(MathHelper.cos(this.field_48582_a.rand.NextSingle() * (float)Math.PI * 2.0F) * f5) * 0.9D);
					this.field_48583_h = chunkCoordinates4.posY;
					this.field_48584_i = chunkCoordinates4.posZ + (int)((double)(MathHelper.sin(this.field_48582_a.rand.NextSingle() * (float)Math.PI * 2.0F) * f5) * 0.9D);
					z6 = false;
					System.Collections.IEnumerator iterator8 = this.field_48582_a.villageCollectionObj.func_48554_b().GetEnumerator();

					while (iterator8.MoveNext())
					{
						Village village9 = (Village)iterator8.Current;
						if (village9 != this.field_48576_f && village9.isInRange(this.field_48577_g, this.field_48583_h, this.field_48584_i))
						{
							z6 = true;
							break;
						}
					}

					if (!z6)
					{
						break;
					}
				}

				if (z6)
				{
					return false;
				}

				vec3D10 = this.func_48572_a(this.field_48577_g, this.field_48583_h, this.field_48584_i);
			} while (vec3D10 == null);

			this.field_48579_e = 0;
			this.field_48578_d = 20;
			return true;
		}

		private bool func_48575_c()
		{
			Vec3D vec3D1 = this.func_48572_a(this.field_48577_g, this.field_48583_h, this.field_48584_i);
			if (vec3D1 == null)
			{
				return false;
			}
			else
			{
				EntityZombie entityZombie2;
				try
				{
					entityZombie2 = new EntityZombie(this.field_48582_a);
				}
				catch (Exception exception4)
				{
					Console.WriteLine(exception4.ToString());
					Console.Write(exception4.StackTrace);
					return false;
				}

				entityZombie2.setLocationAndAngles(vec3D1.xCoord, vec3D1.yCoord, vec3D1.zCoord, this.field_48582_a.rand.NextSingle() * 360.0F, 0.0F);
				this.field_48582_a.spawnEntityInWorld(entityZombie2);
				ChunkCoordinates chunkCoordinates3 = this.field_48576_f.Center;
				entityZombie2.setHomeArea(chunkCoordinates3.posX, chunkCoordinates3.posY, chunkCoordinates3.posZ, this.field_48576_f.VillageRadius);
				return true;
			}
		}

		private Vec3D func_48572_a(int i1, int i2, int i3)
		{
			for (int i4 = 0; i4 < 10; ++i4)
			{
				int i5 = i1 + this.field_48582_a.rand.Next(16) - 8;
				int i6 = i2 + this.field_48582_a.rand.Next(6) - 3;
				int i7 = i3 + this.field_48582_a.rand.Next(16) - 8;
				if (this.field_48576_f.isInRange(i5, i6, i7) && SpawnerAnimals.canCreatureTypeSpawnAtLocation(EnumCreatureType.monster, this.field_48582_a, i5, i6, i7))
				{
					return Vec3D.createVector((double)i5, (double)i6, (double)i7);
				}
			}

			return null;
		}
	}

}