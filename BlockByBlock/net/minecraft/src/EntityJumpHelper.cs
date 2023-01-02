using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityJumpHelper
	{
		private EntityLiving entity;
		private bool isJumping = false;

		public EntityJumpHelper(EntityLiving entityLiving1)
		{
			this.entity = entityLiving1;
		}

		public virtual void setJumping()
		{
			this.isJumping = true;
		}

		public virtual void doJump()
		{
			this.entity.Jumping = this.isJumping;
			this.isJumping = false;
		}
	}

}