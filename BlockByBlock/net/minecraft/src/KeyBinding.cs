using System.Collections;

namespace net.minecraft.src
{

	public class KeyBinding
	{
		public static System.Collections.IList keybindArray = new ArrayList();
		public static IntHashMap hash = new IntHashMap();
		public string keyDescription;
		public int keyCode;
		public bool pressed;
		public int pressTime = 0;

		public static void onTick(int i0)
		{
			KeyBinding keyBinding1 = (KeyBinding)hash.lookup(i0);
			if (keyBinding1 != null)
			{
				++keyBinding1.pressTime;
			}

		}

		public static void setKeyBindState(int i0, bool z1)
		{
			KeyBinding keyBinding2 = (KeyBinding)hash.lookup(i0);
			if (keyBinding2 != null)
			{
				keyBinding2.pressed = z1;
			}

		}

		public static void unPressAllKeys()
		{
			System.Collections.IEnumerator iterator0 = keybindArray.GetEnumerator();

			while (iterator0.MoveNext())
			{
				KeyBinding keyBinding1 = (KeyBinding)iterator0.Current;
				keyBinding1.unpressKey();
			}

		}

		public static void resetKeyBindingArrayAndHash()
		{
			hash.clearMap();
			System.Collections.IEnumerator iterator0 = keybindArray.GetEnumerator();

			while (iterator0.MoveNext())
			{
				KeyBinding keyBinding1 = (KeyBinding)iterator0.Current;
				hash.addKey(keyBinding1.keyCode, keyBinding1);
			}

		}

		public KeyBinding(string string1, int i2)
		{
			this.keyDescription = string1;
			this.keyCode = i2;
			keybindArray.Add(this);
			hash.addKey(i2, this);
		}

		public virtual bool Pressed
		{
			get
			{
				if (this.pressTime == 0)
				{
					return false;
				}
				else
				{
					--this.pressTime;
					return true;
				}
			}
		}

		private void unpressKey()
		{
			this.pressTime = 0;
			this.pressed = false;
		}
	}

}