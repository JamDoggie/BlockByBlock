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

		public virtual string getMD5String(string string1)
		{
			try
			{
				string string2 = this.field_27370_a + string1;
				MessageDigest messageDigest3 = MessageDigest.getInstance("MD5");
				messageDigest3.update(string2.GetBytes(), 0, string2.Length);
				return (new BigInteger(1, messageDigest3.digest())).toString(16);
			}
			catch (NoSuchAlgorithmException noSuchAlgorithmException4)
			{
				throw new Exception(noSuchAlgorithmException4);
			}
		}
	}

}