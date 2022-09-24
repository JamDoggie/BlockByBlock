namespace net.minecraft.src
{
	using GL11 = org.lwjgl.opengl.GL11;

	public class RenderGiantZombie : RenderLiving
	{
		private float scale;

		public RenderGiantZombie(ModelBase modelBase1, float f2, float f3) : base(modelBase1, f2 * f3)
		{
			this.scale = f3;
		}

		protected internal virtual void preRenderScale(EntityGiantZombie entityGiantZombie1, float f2)
		{
			GL11.glScalef(this.scale, this.scale, this.scale);
		}

		protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
		{
			this.preRenderScale((EntityGiantZombie)entityLiving1, f2);
		}
	}

}