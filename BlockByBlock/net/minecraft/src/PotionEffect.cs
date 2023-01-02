using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class PotionEffect
	{
		private int potionID;
		private int duration;
		private int amplifier;

		public PotionEffect(int i1, int i2, int i3)
		{
			this.potionID = i1;
			this.duration = i2;
			this.amplifier = i3;
		}

		public PotionEffect(PotionEffect potionEffect1)
		{
			this.potionID = potionEffect1.potionID;
			this.duration = potionEffect1.duration;
			this.amplifier = potionEffect1.amplifier;
		}

		public virtual void combine(PotionEffect potionEffect1)
		{
			if (this.potionID != potionEffect1.potionID)
			{
				Console.Error.WriteLine("This method should only be called for matching effects!");
			}

			if (potionEffect1.amplifier > this.amplifier)
			{
				this.amplifier = potionEffect1.amplifier;
				this.duration = potionEffect1.duration;
			}
			else if (potionEffect1.amplifier == this.amplifier && this.duration < potionEffect1.duration)
			{
				this.duration = potionEffect1.duration;
			}

		}

		public virtual int PotionID
		{
			get
			{
				return this.potionID;
			}
		}

		public virtual int Duration
		{
			get
			{
				return this.duration;
			}
		}

		public virtual int Amplifier
		{
			get
			{
				return this.amplifier;
			}
		}

		public virtual bool onUpdate(EntityLiving entityLiving1)
		{
			if (this.duration > 0)
			{
				if (Potion.potionTypes[this.potionID].isReady(this.duration, this.amplifier))
				{
					this.performEffect(entityLiving1);
				}

				this.deincrementDuration();
			}

			return this.duration > 0;
		}

		private int deincrementDuration()
		{
			return --this.duration;
		}

		public virtual void performEffect(EntityLiving entityLiving1)
		{
			if (this.duration > 0)
			{
				Potion.potionTypes[this.potionID].performEffect(entityLiving1, this.amplifier);
			}

		}

		public virtual string EffectName
		{
			get
			{
				return Potion.potionTypes[this.potionID].Name;
			}
		}

		public override int GetHashCode()
		{
			return this.potionID;
		}

		public override string ToString()
		{
			string string1 = "";
			if (this.Amplifier > 0)
			{
				string1 = this.EffectName + " x " + (this.Amplifier + 1) + ", Duration: " + this.Duration;
			}
			else
			{
				string1 = this.EffectName + ", Duration: " + this.Duration;
			}

			return Potion.potionTypes[this.potionID].Usable ? "(" + string1 + ")" : string1;
		}

		public override bool Equals(object object1)
		{
			if (!(object1 is PotionEffect))
			{
				return false;
			}
			else
			{
				PotionEffect potionEffect2 = (PotionEffect)object1;
				return this.potionID == potionEffect2.potionID && this.amplifier == potionEffect2.amplifier && this.duration == potionEffect2.duration;
			}
		}
	}

}