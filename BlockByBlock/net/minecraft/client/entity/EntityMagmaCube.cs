using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityMagmaCube : EntitySlime
    {
        public EntityMagmaCube(World world1) : base(world1)
        {
            texture = "/mob/lava.png";
            isImmuneToFire = true;
            landMovementFactor = 0.2F;
        }

        public override bool CanSpawnHere
        {
            get
            {
                return worldObj.difficultySetting > 0 && worldObj.checkIfAABBIsClear(boundingBox) && worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0 && !worldObj.isAnyLiquid(boundingBox);
            }
        }

        public override int TotalArmorValue
        {
            get
            {
                return SlimeSize * 3;
            }
        }

        public override int getBrightnessForRender(float f1)
        {
            return 15728880;
        }

        public override float getBrightness(float f1)
        {
            return 1.0F;
        }

        protected internal override string SlimeParticle
        {
            get
            {
                return "flame";
            }
        }

        protected internal override EntitySlime createInstance()
        {
            return new EntityMagmaCube(worldObj);
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.magmaCream.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = DropItemId;
            if (i3 > 0 && SlimeSize > 1)
            {
                int i4 = rand.Next(4) - 2;
                if (i2 > 0)
                {
                    i4 += rand.Next(i2 + 1);
                }

                for (int i5 = 0; i5 < i4; ++i5)
                {
                    dropItem(i3, 1);
                }
            }

        }

        public override bool Burning
        {
            get
            {
                return false;
            }
        }

        protected internal override int func_40131_af()
        {
            return base.func_40131_af() * 4;
        }

        protected internal override void func_40136_ag()
        {
            field_40139_a *= 0.9F;
        }

        protected internal override void jump()
        {
            motionY = 0.42F + SlimeSize * 0.1F;
            isAirBorne = true;
        }

        protected internal override void fall(float f1)
        {
        }

        protected internal override bool func_40137_ah()
        {
            return true;
        }

        protected internal override int func_40130_ai()
        {
            return base.func_40130_ai() + 2;
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.slime";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.slime";
            }
        }

        protected internal override string func_40138_aj()
        {
            return SlimeSize > 1 ? "mob.magmacube.big" : "mob.magmacube.small";
        }

        public override bool handleLavaMovement()
        {
            return false;
        }

        protected internal override bool func_40134_ak()
        {
            return true;
        }
    }

}