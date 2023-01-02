using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityDragon : EntityDragonBase
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            dragonPartArray = new EntityDragonPart[] { dragonPartHead = new EntityDragonPart(this, "head", 6.0F, 6.0F), dragonPartBody = new EntityDragonPart(this, "body", 8.0F, 8.0F), dragonPartTail1 = new EntityDragonPart(this, "tail", 4.0F, 4.0F), dragonPartTail2 = new EntityDragonPart(this, "tail", 4.0F, 4.0F), dragonPartTail3 = new EntityDragonPart(this, "tail", 4.0F, 4.0F), dragonPartWing1 = new EntityDragonPart(this, "wing", 4.0F, 4.0F), dragonPartWing2 = new EntityDragonPart(this, "wing", 4.0F, 4.0F) };
        }

        public double targetX;
        public double targetY;
        public double targetZ;
        // JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
        // ORIGINAL LINE: public double[][] field_40162_d = new double[64][3];
        public double[][] field_40162_d = RectangularArrays.RectangularDoubleArray(64, 3);
        public int field_40164_e = -1;
        public EntityDragonPart[] dragonPartArray;
        public EntityDragonPart dragonPartHead;
        public EntityDragonPart dragonPartBody;
        public EntityDragonPart dragonPartTail1;
        public EntityDragonPart dragonPartTail2;
        public EntityDragonPart dragonPartTail3;
        public EntityDragonPart dragonPartWing1;
        public EntityDragonPart dragonPartWing2;
        public float field_40173_aw = 0.0F;
        public float field_40172_ax = 0.0F;
        public bool field_40163_ay = false;
        public bool field_40161_az = false;
        private Entity target;
        public int field_40178_aA = 0;
        public EntityEnderCrystal healingEnderCrystal = null;

        public EntityDragon(World world1) : base(world1)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            maxHealth = 200;
            EntityHealth = maxHealth;
            texture = "/mob/enderdragon/ender.png";
            SetSize(16.0F, 8.0F);
            noClip = true;
            isImmuneToFire = true;
            targetY = 100.0D;
            ignoreFrustumCheck = true;
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new int?(maxHealth));
        }

        public virtual double[] func_40160_a(int i1, float f2)
        {
            if (health <= 0)
            {
                f2 = 0.0F;
            }

            f2 = 1.0F - f2;
            int i3 = field_40164_e - i1 * 1 & 63;
            int i4 = field_40164_e - i1 * 1 - 1 & 63;
            double[] d5 = new double[3];
            double d6 = field_40162_d[i3][0];

            double d8;
            for (d8 = field_40162_d[i4][0] - d6; d8 < -180.0D; d8 += 360.0D)
            {
            }

            while (d8 >= 180.0D)
            {
                d8 -= 360.0D;
            }

            d5[0] = d6 + d8 * (double)f2;
            d6 = field_40162_d[i3][1];
            d8 = field_40162_d[i4][1] - d6;
            d5[1] = d6 + d8 * (double)f2;
            d5[2] = field_40162_d[i3][2] + (field_40162_d[i4][2] - field_40162_d[i3][2]) * (double)f2;
            return d5;
        }

        public override void onLivingUpdate()
        {
            field_40173_aw = field_40172_ax;
            if (!worldObj.isRemote)
            {
                dataWatcher.updateObject(16, health);
            }

            float f1;
            float f3;
            float f26;
            if (health <= 0)
            {
                f1 = (rand.NextSingle() - 0.5F) * 8.0F;
                f26 = (rand.NextSingle() - 0.5F) * 4.0F;
                f3 = (rand.NextSingle() - 0.5F) * 8.0F;
                worldObj.spawnParticle("largeexplode", posX + (double)f1, posY + 2.0D + (double)f26, posZ + (double)f3, 0.0D, 0.0D, 0.0D);
            }
            else
            {
                updateDragonEnderCrystal();
                f1 = 0.2F / (MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ) * 10.0F + 1.0F);
                f1 *= (float)Math.Pow(2.0D, motionY);
                if (field_40161_az)
                {
                    field_40172_ax += f1 * 0.5F;
                }
                else
                {
                    field_40172_ax += f1;
                }

                while (rotationYaw >= 180.0F)
                {
                    rotationYaw -= 360.0F;
                }

                while (rotationYaw < -180.0F)
                {
                    rotationYaw += 360.0F;
                }

                if (field_40164_e < 0)
                {
                    for (int i2 = 0; i2 < field_40162_d.Length; ++i2)
                    {
                        field_40162_d[i2][0] = rotationYaw;
                        field_40162_d[i2][1] = posY;
                    }
                }

                if (++field_40164_e == field_40162_d.Length)
                {
                    field_40164_e = 0;
                }

                field_40162_d[field_40164_e][0] = rotationYaw;
                field_40162_d[field_40164_e][1] = posY;
                double d4;
                double d6;
                double d8;
                double d25;
                float f31;
                if (worldObj.isRemote)
                {
                    if (newPosRotationIncrements > 0)
                    {
                        d25 = posX + (newPosX - posX) / newPosRotationIncrements;
                        d4 = posY + (newPosY - posY) / newPosRotationIncrements;
                        d6 = posZ + (newPosZ - posZ) / newPosRotationIncrements;

                        for (d8 = newRotationYaw - rotationYaw; d8 < -180.0D; d8 += 360.0D)
                        {
                        }

                        while (d8 >= 180.0D)
                        {
                            d8 -= 360.0D;
                        }

                        rotationYaw = (float)(rotationYaw + d8 / newPosRotationIncrements);
                        rotationPitch = (float)(rotationPitch + (newRotationPitch - rotationPitch) / newPosRotationIncrements);
                        --newPosRotationIncrements;
                        SetPosition(d25, d4, d6);
                        setRotation(rotationYaw, rotationPitch);
                    }
                }
                else
                {
                    d25 = targetX - posX;
                    d4 = targetY - posY;
                    d6 = targetZ - posZ;
                    d8 = d25 * d25 + d4 * d4 + d6 * d6;
                    if (target != null)
                    {
                        targetX = target.posX;
                        targetZ = target.posZ;
                        double d10 = targetX - posX;
                        double d12 = targetZ - posZ;
                        double d14 = Math.Sqrt(d10 * d10 + d12 * d12);
                        double d16 = (double)0.4F + d14 / 80.0D - 1.0D;
                        if (d16 > 10.0D)
                        {
                            d16 = 10.0D;
                        }

                        targetY = target.boundingBox.minY + d16;
                    }
                    else
                    {
                        targetX += rand.NextGaussian() * 2.0D;
                        targetZ += rand.NextGaussian() * 2.0D;
                    }

                    if (field_40163_ay || d8 < 100.0D || d8 > 22500.0D || isCollidedHorizontally || isCollidedVertically)
                    {
                        func_41006_aA();
                    }

                    d4 /= (double)MathHelper.sqrt_double(d25 * d25 + d6 * d6);
                    f31 = 0.6F;
                    if (d4 < (double)-f31)
                    {
                        d4 = (double)-f31;
                    }

                    if (d4 > (double)f31)
                    {
                        d4 = (double)f31;
                    }

                    for (motionY += (float)(d4 * (double)0.1F); rotationYaw < -180.0F; rotationYaw += 360.0F)
                    {
                    }

                    while (rotationYaw >= 180.0F)
                    {
                        rotationYaw -= 360.0F;
                    }

                    double d11 = 180.0D - Math.Atan2(d25, d6) * 180.0D / (double)(float)Math.PI;

                    double d13;
                    for (d13 = d11 - rotationYaw; d13 < -180.0D; d13 += 360.0D)
                    {
                    }

                    while (d13 >= 180.0D)
                    {
                        d13 -= 360.0D;
                    }

                    if (d13 > 50.0D)
                    {
                        d13 = 50.0D;
                    }

                    if (d13 < -50.0D)
                    {
                        d13 = -50.0D;
                    }

                    Vec3D vec3D15 = Vec3D.createVector(targetX - posX, targetY - posY, targetZ - posZ).normalize();
                    Vec3D vec3D39 = Vec3D.createVector((double)MathHelper.sin(rotationYaw * (float)Math.PI / 180.0F), motionY, (double)-MathHelper.cos(rotationYaw * (float)Math.PI / 180.0F)).normalize();
                    float f17 = (float)(vec3D39.dotProduct(vec3D15) + 0.5D) / 1.5F;
                    if (f17 < 0.0F)
                    {
                        f17 = 0.0F;
                    }

                    randomYawVelocity *= 0.8F;
                    float f18 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ) * 1.0F + 1.0F;
                    double d19 = Math.Sqrt(motionX * motionX + motionZ * motionZ) * 1.0D + 1.0D;
                    if (d19 > 40.0D)
                    {
                        d19 = 40.0D;
                    }

                    randomYawVelocity = (float)(randomYawVelocity + d13 * ((double)0.7F / d19 / (double)f18));
                    rotationYaw += randomYawVelocity * 0.1F;
                    float f21 = (float)(2.0D / (d19 + 1.0D));
                    float f22 = 0.06F;
                    moveFlying(0.0F, -1.0F, f22 * (f17 * f21 + (1.0F - f21)));
                    if (field_40161_az)
                    {
                        moveEntity(motionX * (double)0.8F, motionY * (double)0.8F, motionZ * (double)0.8F);
                    }
                    else
                    {
                        moveEntity(motionX, motionY, motionZ);
                    }

                    Vec3D vec3D23 = Vec3D.createVector(motionX, motionY, motionZ).normalize();
                    float f24 = (float)(vec3D23.dotProduct(vec3D39) + 1.0D) / 2.0F;
                    f24 = 0.8F + 0.15F * f24;
                    motionX *= f24;
                    motionZ *= f24;
                    motionY *= 0.91F;
                }

                renderYawOffset = rotationYaw;
                dragonPartHead.width = dragonPartHead.height = 3.0F;
                dragonPartTail1.width = dragonPartTail1.height = 2.0F;
                dragonPartTail2.width = dragonPartTail2.height = 2.0F;
                dragonPartTail3.width = dragonPartTail3.height = 2.0F;
                dragonPartBody.height = 3.0F;
                dragonPartBody.width = 5.0F;
                dragonPartWing1.height = 2.0F;
                dragonPartWing1.width = 4.0F;
                dragonPartWing2.height = 3.0F;
                dragonPartWing2.width = 4.0F;
                f26 = (float)(func_40160_a(5, 1.0F)[1] - func_40160_a(10, 1.0F)[1]) * 10.0F / 180.0F * (float)Math.PI;
                f3 = MathHelper.cos(f26);
                float f27 = -MathHelper.sin(f26);
                float f5 = rotationYaw * (float)Math.PI / 180.0F;
                float f28 = MathHelper.sin(f5);
                float f7 = MathHelper.cos(f5);
                dragonPartBody.onUpdate();
                dragonPartBody.setLocationAndAngles(posX + (double)(f28 * 0.5F), posY, posZ - (double)(f7 * 0.5F), 0.0F, 0.0F);
                dragonPartWing1.onUpdate();
                dragonPartWing1.setLocationAndAngles(posX + (double)(f7 * 4.5F), posY + 2.0D, posZ + (double)(f28 * 4.5F), 0.0F, 0.0F);
                dragonPartWing2.onUpdate();
                dragonPartWing2.setLocationAndAngles(posX - (double)(f7 * 4.5F), posY + 2.0D, posZ - (double)(f28 * 4.5F), 0.0F, 0.0F);
                if (!worldObj.isRemote)
                {
                    func_41007_az();
                }

                if (!worldObj.isRemote && maxHurtTime == 0)
                {
                    collideWithEntities(worldObj.getEntitiesWithinAABBExcludingEntity(this, dragonPartWing1.boundingBox.expand(4.0D, 2.0D, 4.0D).offset(0.0D, -2.0D, 0.0D)));
                    collideWithEntities(worldObj.getEntitiesWithinAABBExcludingEntity(this, dragonPartWing2.boundingBox.expand(4.0D, 2.0D, 4.0D).offset(0.0D, -2.0D, 0.0D)));
                    attackEntitiesInList(worldObj.getEntitiesWithinAABBExcludingEntity(this, dragonPartHead.boundingBox.expand(1.0D, 1.0D, 1.0D)));
                }

                double[] d29 = func_40160_a(5, 1.0F);
                double[] d9 = func_40160_a(0, 1.0F);
                f31 = MathHelper.sin(rotationYaw * (float)Math.PI / 180.0F - randomYawVelocity * 0.01F);
                float f33 = MathHelper.cos(rotationYaw * (float)Math.PI / 180.0F - randomYawVelocity * 0.01F);
                dragonPartHead.onUpdate();
                dragonPartHead.setLocationAndAngles(posX + (double)(f31 * 5.5F * f3), posY + (d9[1] - d29[1]) * 1.0D + (double)(f27 * 5.5F), posZ - (double)(f33 * 5.5F * f3), 0.0F, 0.0F);

                for (int i30 = 0; i30 < 3; ++i30)
                {
                    EntityDragonPart entityDragonPart32 = null;
                    if (i30 == 0)
                    {
                        entityDragonPart32 = dragonPartTail1;
                    }

                    if (i30 == 1)
                    {
                        entityDragonPart32 = dragonPartTail2;
                    }

                    if (i30 == 2)
                    {
                        entityDragonPart32 = dragonPartTail3;
                    }

                    double[] d34 = func_40160_a(12 + i30 * 2, 1.0F);
                    float f35 = rotationYaw * (float)Math.PI / 180.0F + simplifyAngle(d34[0] - d29[0]) * (float)Math.PI / 180.0F * 1.0F;
                    float f37 = MathHelper.sin(f35);
                    float f36 = MathHelper.cos(f35);
                    float f38 = 1.5F;
                    float f40 = (i30 + 1) * 2.0F;
                    entityDragonPart32.onUpdate();
                    entityDragonPart32.setLocationAndAngles(posX - (double)((f28 * f38 + f37 * f40) * f3), posY + (d34[1] - d29[1]) * 1.0D - (double)((f40 + f38) * f27) + 1.5D, posZ + (double)((f7 * f38 + f36 * f40) * f3), 0.0F, 0.0F);
                }

                if (!worldObj.isRemote)
                {
                    field_40161_az = destroyBlocksInAABB(dragonPartHead.boundingBox) | destroyBlocksInAABB(dragonPartBody.boundingBox);
                }

            }
        }

        private void updateDragonEnderCrystal()
        {
            if (healingEnderCrystal != null)
            {
                if (healingEnderCrystal.isDead)
                {
                    if (!worldObj.isRemote)
                    {
                        attackEntityFromPart(dragonPartHead, DamageSource.explosion, 10);
                    }

                    healingEnderCrystal = null;
                }
                else if (ticksExisted % 10 == 0 && health < maxHealth)
                {
                    ++health;
                }
            }

            if (rand.Next(10) == 0)
            {
                float f1 = 32.0F;
                System.Collections.IList list2 = worldObj.getEntitiesWithinAABB(typeof(EntityEnderCrystal), boundingBox.expand((double)f1, (double)f1, (double)f1));
                EntityEnderCrystal entityEnderCrystal3 = null;
                double d4 = double.MaxValue;
                System.Collections.IEnumerator iterator6 = list2.GetEnumerator();

                while (iterator6.MoveNext())
                {
                    Entity entity7 = (Entity)iterator6.Current;
                    double d8 = entity7.getDistanceSqToEntity(this);
                    if (d8 < d4)
                    {
                        d4 = d8;
                        entityEnderCrystal3 = (EntityEnderCrystal)entity7;
                    }
                }

                healingEnderCrystal = entityEnderCrystal3;
            }

        }

        private void func_41007_az()
        {
        }

        private void collideWithEntities(System.Collections.IList list1)
        {
            double d2 = (dragonPartBody.boundingBox.minX + dragonPartBody.boundingBox.maxX) / 2.0D;
            double d4 = (dragonPartBody.boundingBox.minZ + dragonPartBody.boundingBox.maxZ) / 2.0D;
            System.Collections.IEnumerator iterator6 = list1.GetEnumerator();

            while (iterator6.MoveNext())
            {
                Entity entity7 = (Entity)iterator6.Current;
                if (entity7 is EntityLiving)
                {
                    double d8 = entity7.posX - d2;
                    double d10 = entity7.posZ - d4;
                    double d12 = d8 * d8 + d10 * d10;
                    entity7.addVelocity(d8 / d12 * 4.0D, (double)0.2F, d10 / d12 * 4.0D);
                }
            }

        }

        private void attackEntitiesInList(System.Collections.IList list1)
        {
            for (int i2 = 0; i2 < list1.Count; ++i2)
            {
                Entity entity3 = (Entity)list1[i2];
                if (entity3 is EntityLiving)
                {
                    entity3.attackEntityFrom(DamageSource.causeMobDamage(this), 10);
                }
            }

        }

        private void func_41006_aA()
        {
            field_40163_ay = false;
            if (rand.Next(2) == 0 && worldObj.playerEntities.Count > 0)
            {
                target = (Entity)worldObj.playerEntities[rand.Next(worldObj.playerEntities.Count)];
            }
            else
            {
                bool z1 = false;

                do
                {
                    targetX = 0.0D;
                    targetY = 70.0F + rand.NextSingle() * 50.0F;
                    targetZ = 0.0D;
                    targetX += rand.NextSingle() * 120.0F - 60.0F;
                    targetZ += rand.NextSingle() * 120.0F - 60.0F;
                    double d2 = posX - targetX;
                    double d4 = posY - targetY;
                    double d6 = posZ - targetZ;
                    z1 = d2 * d2 + d4 * d4 + d6 * d6 > 100.0D;
                } while (!z1);

                target = null;
            }

        }

        private float simplifyAngle(double d1)
        {
            while (d1 >= 180.0D)
            {
                d1 -= 360.0D;
            }

            while (d1 < -180.0D)
            {
                d1 += 360.0D;
            }

            return (float)d1;
        }

        private bool destroyBlocksInAABB(AxisAlignedBB axisAlignedBB1)
        {
            int i2 = MathHelper.floor_double(axisAlignedBB1.minX);
            int i3 = MathHelper.floor_double(axisAlignedBB1.minY);
            int i4 = MathHelper.floor_double(axisAlignedBB1.minZ);
            int i5 = MathHelper.floor_double(axisAlignedBB1.maxX);
            int i6 = MathHelper.floor_double(axisAlignedBB1.maxY);
            int i7 = MathHelper.floor_double(axisAlignedBB1.maxZ);
            bool z8 = false;
            bool z9 = false;

            for (int i10 = i2; i10 <= i5; ++i10)
            {
                for (int i11 = i3; i11 <= i6; ++i11)
                {
                    for (int i12 = i4; i12 <= i7; ++i12)
                    {
                        int i13 = worldObj.getBlockId(i10, i11, i12);
                        if (i13 != 0)
                        {
                            if (i13 != Block.obsidian.blockID && i13 != Block.whiteStone.blockID && i13 != Block.bedrock.blockID)
                            {
                                z9 = true;
                                worldObj.setBlockWithNotify(i10, i11, i12, 0);
                            }
                            else
                            {
                                z8 = true;
                            }
                        }
                    }
                }
            }

            if (z9)
            {
                double d16 = axisAlignedBB1.minX + (axisAlignedBB1.maxX - axisAlignedBB1.minX) * (double)rand.NextSingle();
                double d17 = axisAlignedBB1.minY + (axisAlignedBB1.maxY - axisAlignedBB1.minY) * (double)rand.NextSingle();
                double d14 = axisAlignedBB1.minZ + (axisAlignedBB1.maxZ - axisAlignedBB1.minZ) * (double)rand.NextSingle();
                worldObj.spawnParticle("largeexplode", d16, d17, d14, 0.0D, 0.0D, 0.0D);
            }

            return z8;
        }

        public override bool attackEntityFromPart(EntityDragonPart entityDragonPart1, DamageSource damageSource2, int i3)
        {
            if (entityDragonPart1 != dragonPartHead)
            {
                i3 = i3 / 4 + 1;
            }

            float f4 = rotationYaw * (float)Math.PI / 180.0F;
            float f5 = MathHelper.sin(f4);
            float f6 = MathHelper.cos(f4);
            targetX = posX + (double)(f5 * 5.0F) + (double)((rand.NextSingle() - 0.5F) * 2.0F);
            targetY = posY + (double)(rand.NextSingle() * 3.0F) + 1.0D;
            targetZ = posZ - (double)(f6 * 5.0F) + (double)((rand.NextSingle() - 0.5F) * 2.0F);
            target = null;
            if (damageSource2.Entity is EntityPlayer || damageSource2 == DamageSource.explosion)
            {
                superAttackFrom(damageSource2, i3);
            }

            return true;
        }

        protected internal override void onDeathUpdate()
        {
            ++field_40178_aA;
            if (field_40178_aA >= 180 && field_40178_aA <= 200)
            {
                float f1 = (rand.NextSingle() - 0.5F) * 8.0F;
                float f2 = (rand.NextSingle() - 0.5F) * 4.0F;
                float f3 = (rand.NextSingle() - 0.5F) * 8.0F;
                worldObj.spawnParticle("hugeexplosion", posX + (double)f1, posY + 2.0D + (double)f2, posZ + (double)f3, 0.0D, 0.0D, 0.0D);
            }

            int i4;
            int i5;
            if (!worldObj.isRemote && field_40178_aA > 150 && field_40178_aA % 5 == 0)
            {
                i4 = 1000;

                while (i4 > 0)
                {
                    i5 = EntityXPOrb.getXPSplit(i4);
                    i4 -= i5;
                    worldObj.spawnEntityInWorld(new EntityXPOrb(worldObj, posX, posY, posZ, i5));
                }
            }

            moveEntity(0.0D, (double)0.1F, 0.0D);
            renderYawOffset = rotationYaw += 20.0F;
            if (field_40178_aA == 200)
            {
                i4 = 10000;

                while (i4 > 0)
                {
                    i5 = EntityXPOrb.getXPSplit(i4);
                    i4 -= i5;
                    worldObj.spawnEntityInWorld(new EntityXPOrb(worldObj, posX, posY, posZ, i5));
                }

                createEnderPortal(MathHelper.floor_double(posX), MathHelper.floor_double(posZ));
                onEntityDeath();
                setDead();
            }

        }

        private void createEnderPortal(int i1, int i2)
        {
            sbyte b3 = 64;
            BlockEndPortal.bossDefeated = true;
            sbyte b4 = 4;

            for (int i5 = b3 - 1; i5 <= b3 + 32; ++i5)
            {
                for (int i6 = i1 - b4; i6 <= i1 + b4; ++i6)
                {
                    for (int i7 = i2 - b4; i7 <= i2 + b4; ++i7)
                    {
                        double d8 = i6 - i1;
                        double d10 = i7 - i2;
                        double d12 = (double)MathHelper.sqrt_double(d8 * d8 + d10 * d10);
                        if (d12 <= b4 - 0.5D)
                        {
                            if (i5 < b3)
                            {
                                if (d12 <= b4 - 1 - 0.5D)
                                {
                                    worldObj.setBlockWithNotify(i6, i5, i7, Block.bedrock.blockID);
                                }
                            }
                            else if (i5 > b3)
                            {
                                worldObj.setBlockWithNotify(i6, i5, i7, 0);
                            }
                            else if (d12 > b4 - 1 - 0.5D)
                            {
                                worldObj.setBlockWithNotify(i6, i5, i7, Block.bedrock.blockID);
                            }
                            else
                            {
                                worldObj.setBlockWithNotify(i6, i5, i7, Block.endPortal.blockID);
                            }
                        }
                    }
                }
            }

            worldObj.setBlockWithNotify(i1, b3 + 0, i2, Block.bedrock.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 1, i2, Block.bedrock.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 2, i2, Block.bedrock.blockID);
            worldObj.setBlockWithNotify(i1 - 1, b3 + 2, i2, Block.torchWood.blockID);
            worldObj.setBlockWithNotify(i1 + 1, b3 + 2, i2, Block.torchWood.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 2, i2 - 1, Block.torchWood.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 2, i2 + 1, Block.torchWood.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 3, i2, Block.bedrock.blockID);
            worldObj.setBlockWithNotify(i1, b3 + 4, i2, Block.dragonEgg.blockID);
            BlockEndPortal.bossDefeated = false;
        }

        protected internal override void despawnEntity()
        {
        }

        public override Entity[] Parts
        {
            get
            {
                return dragonPartArray;
            }
        }

        public override bool canBeCollidedWith()
        {
            return false;
        }

        public virtual int func_41010_ax()
        {
            return dataWatcher.getWatchableObjectInt(16);
        }
    }

}