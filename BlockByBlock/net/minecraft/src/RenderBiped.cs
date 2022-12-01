using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class RenderBiped : RenderLiving
	{
		protected internal ModelBiped modelBipedMain;
		protected internal float field_40296_d;

		public RenderBiped(ModelBiped modelBiped1, float f2) : this(modelBiped1, f2, 1.0F)
		{
			this.modelBipedMain = modelBiped1;
		}

		public RenderBiped(ModelBiped modelBiped1, float f2, float f3) : base(modelBiped1, f2)
		{
			this.modelBipedMain = modelBiped1;
			this.field_40296_d = f3;
		}

		protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
			base.renderEquippedItems(entityLiving1, f2);
			ItemStack itemStack3 = entityLiving1.HeldItem;
			if (itemStack3 != null)
			{
                Minecraft.newRenderer.ModelMatrix.PushMatrix();
				this.modelBipedMain.bipedRightArm.postRender(0.0625F);
                Minecraft.newRenderer.ModelMatrix.Translate(-0.0625F, 0.4375F, 0.0625F);
				float f4;
				if (itemStack3.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemStack3.itemID].RenderType))
				{
					f4 = 0.5F;
                    Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.1875F, -0.3125F);
					f4 *= 0.75F;
                    Minecraft.newRenderer.ModelMatrix.Rotate(20.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Scale(f4, -f4, f4);
				}
				else if (itemStack3.itemID == Item.bow.shiftedIndex)
				{
					f4 = 0.625F;
					Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.125F, 0.3125F);
					Minecraft.newRenderer.ModelMatrix.Rotate(-20.0F, 0.0F, 1.0F, 0.0F);
					Minecraft.newRenderer.ModelMatrix.Scale(f4, -f4, f4);
					Minecraft.newRenderer.ModelMatrix.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				}
				else if (Item.itemsList[itemStack3.itemID].Full3D)
				{
					f4 = 0.625F;
					Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.1875F, 0.0F);
					Minecraft.newRenderer.ModelMatrix.Scale(f4, -f4, f4);
					Minecraft.newRenderer.ModelMatrix.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				}
				else
				{
					f4 = 0.375F;
					Minecraft.newRenderer.ModelMatrix.Translate(0.25F, 0.1875F, -0.1875F);
					Minecraft.newRenderer.ModelMatrix.Scale(f4, f4, f4);
					Minecraft.newRenderer.ModelMatrix.Rotate(60.0F, 0.0F, 0.0F, 1.0F);
					Minecraft.newRenderer.ModelMatrix.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Rotate(20.0F, 0.0F, 0.0F, 1.0F);
				}

				this.renderManager.itemRenderer.renderItem(entityLiving1, itemStack3, 0);
				if (itemStack3.Item.func_46058_c())
				{
					this.renderManager.itemRenderer.renderItem(entityLiving1, itemStack3, 1);
				}

                Minecraft.newRenderer.ModelMatrix.PopMatrix();
			}

		}
	}

}