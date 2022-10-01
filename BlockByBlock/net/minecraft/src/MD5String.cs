using System;
using System.Numerics;

namespace net.minecraft.src
{

	public class MD5String
	{
		private string field_27370_a;

		public MD5String(string string1)
		{
			this.field_27370_a = string1;
		}

		public virtual string getMD5String(string str)
		{
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) // PORTING TODO: Possible Java parity issue.
																									 // May need to use IKVM for full Java parity with world seeds.
																									 // This doesn't *seem* to be used for world gen though, so idk.
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				return Convert.ToHexString(hashBytes);
			}
		}
	}
}