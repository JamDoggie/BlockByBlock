namespace net.minecraft.src
{
	public class EntitySilverfish : EntityMob
	{
		private int allySummonCooldown;

		public EntitySilverfish(World world1) : base(world1)
		{
			this.texture = "/mob/silverfish.png";
			this.setSize(0.3F, 0.7F);
			this.moveSpeed = 0.6F;
			this.attackStrength = 1;
		}

		public override int MaxHealth
		{
			get
			{
				return 8;
			}
		}

		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override Entity findPlayerToAttack()
		{
			double d1 = 8.0D;
			return this.worldObj.getClosestVulnerablePlayerToEntity(this, d1);
		}

		protected internal override string LivingSound
		{
			get
			{
				return "mob.silverfish.say";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.silverfish.hit";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.silverfish.kill";
			}
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			if (this.allySummonCooldown <= 0 && damageSource1 is EntityDamageSource)
			{
				this.allySummonCooldown = 20;
			}

			return base.attackEntityFrom(damageSource1, i2);
		}

		protected internal override void attackEntity(Entity entity1, float f2)
		{
			if (this.attackTime <= 0 && f2 < 1.2F && entity1.boundingBox.maxY > this.boundingBox.minY && entity1.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				entity1.attackEntityFrom(DamageSource.causeMobDamage(this), this.attackStrength);
			}

		}

		protected internal override void playStepSound(int i1, int i2, int i3, int i4)
		{
			this.worldObj.playSoundAtEntity(this, "mob.silverfish.step", 1.0F, 1.0F);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
		}

		protected internal override int DropItemId
		{
			get
			{
				return 0;
			}
		}

		public override void onUpdate()
		{
			this.renderYawOffset = this.rotationYaw;
			base.onUpdate();
		}

		public override void updateEntityActionState()
		{
			base.updateEntityActionState();
			if (!this.worldObj.isRemote)
			{
				int i1;
				int i2;
				int i3;
				int i5;
				if (this.allySummonCooldown > 0)
				{
					--this.allySummonCooldown;
					if (this.allySummonCooldown == 0)
					{
						i1 = MathHelper.floor_double(this.posX);
						i2 = MathHelper.floor_double(this.posY);
						i3 = MathHelper.floor_double(this.posZ);
						bool z4 = false;

						for (i5 = 0; !z4 && i5 <= 5 && i5 >= -5; i5 = i5 <= 0 ? 1 - i5 : 0 - i5)
						{
							for (int i6 = 0; !z4 && i6 <= 10 && i6 >= -10; i6 = i6 <= 0 ? 1 - i6 : 0 - i6)
							{
								for (int i7 = 0; !z4 && i7 <= 10 && i7 >= -10; i7 = i7 <= 0 ? 1 - i7 : 0 - i7)
								{
									int i8 = this.worldObj.getBlockId(i1 + i6, i2 + i5, i3 + i7);
									if (i8 == Block.silverfish.blockID)
									{
										this.worldObj.playAuxSFX(2001, i1 + i6, i2 + i5, i3 + i7, Block.silverfish.blockID + (this.worldObj.getBlockMetadata(i1 + i6, i2 + i5, i3 + i7) << 12));
										this.worldObj.setBlockWithNotify(i1 + i6, i2 + i5, i3 + i7, 0);
										Block.silverfish.onBlockDestroyedByPlayer(this.worldObj, i1 + i6, i2 + i5, i3 + i7, 0);
										if (this.rand.NextBool())
										{
											z4 = true;
											break;
										}
									}
								}
							}
						}
					}
				}

				if (this.entityToAttack == null && !this.hasPath())
				{
					i1 = MathHelper.floor_double(this.posX);
					i2 = MathHelper.floor_double(this.posY + 0.5D);
					i3 = MathHelper.floor_double(this.posZ);
					int i9 = this.rand.Next(6);
					i5 = this.worldObj.getBlockId(i1 + Facing.offsetsXForSide[i9], i2 + Facing.offsetsYForSide[i9], i3 + Facing.offsetsZForSide[i9]);
					if (BlockSilverfish.getPosingIdByMetadata(i5))
					{
						this.worldObj.setBlockAndMetadataWithNotify(i1 + Facing.offsetsXForSide[i9], i2 + Facing.offsetsYForSide[i9], i3 + Facing.offsetsZForSide[i9], Block.silverfish.blockID, BlockSilverfish.getMetadataForBlockType(i5));
						this.spawnExplosionParticle();
						this.setDead();
					}
					else
					{
						this.updateWanderPath();
					}
				}
				else if (this.entityToAttack != null && !this.hasPath())
				{
					this.entityToAttack = null;
				}

			}
		}

		public override float getBlockPathWeight(int i1, int i2, int i3)
		{
			return this.worldObj.getBlockId(i1, i2 - 1, i3) == Block.stone.blockID ? 10.0F : base.getBlockPathWeight(i1, i2, i3);
		}

		protected internal override bool ValidLightLevel
		{
			get
			{
				return true;
			}
		}

		public override bool CanSpawnHere
		{
			get
			{
				if (base.CanSpawnHere)
				{
					EntityPlayer entityPlayer1 = this.worldObj.getClosestPlayerToEntity(this, 5.0D);
					return entityPlayer1 == null;
				}
				else
				{
					return false;
				}
			}
		}

		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.ARTHROPOD;
			}
		}
	}

}