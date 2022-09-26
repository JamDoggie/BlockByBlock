using BlockByBlock.logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace net.minecraft.src
{

	public class ChatClickData
	{
        public static readonly Regex URLRegex = new Regex("^(?:(https?)://)?([-\\w_\\.]{2,}\\.[a-z]{2,3})(/\\S*)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly FontRenderer fontRenderer;
		private readonly ChatLine chatLine;
		private readonly int field_50093_d;
		private readonly int field_50094_e;
		private readonly string field_50091_f;
		private readonly string field_50092_g;

		public ChatClickData(FontRenderer fontRenderer1, ChatLine chatLine2, int i3, int i4)
		{
			this.fontRenderer = fontRenderer1;
			this.chatLine = chatLine2;
			this.field_50093_d = i3;
			this.field_50094_e = i4;
			this.field_50091_f = fontRenderer1.func_50107_a(chatLine2.message, i3);
			this.field_50092_g = this.func_50090_c();
		}

		public virtual string func_50088_a()
		{
			return this.field_50092_g;
		}
        
		/// <summary>
		/// PORTING TODO: The logic on this one was iffy. Check back on this method later.
		/// </summary>
		/// <returns></returns>
		public virtual Uri getURIFromChatLine()
		{
			string string1 = this.func_50088_a();
			if (string.ReferenceEquals(string1, null))
			{
				return null;
			}
			else
			{
				MatchCollection matcher2 = URLRegex.Matches(string1);
                
				if (URLRegex.IsMatch(string1))
				{
					try
					{
                        string string3 = matcher2[0].Groups[0].Value;
                        if (matcher2[0].Groups[1].Value == null)
						{
							string3 = "http://" + string3;
						}

						return new Uri(string3);
					}
					catch (UriFormatException e)
					{
						Logger.getLogger("Minecraft").log(Level.SEVERE, "Couldn\'t create URI from chat", e);
					}
				}

				return null;
			}
		}

		private string func_50090_c()
		{
			int i1 = this.field_50091_f.LastIndexOf(" ", this.field_50091_f.Length, StringComparison.Ordinal) + 1;
			if (i1 < 0)
			{
				i1 = 0;
			}

			int i2 = this.chatLine.message.IndexOf(" ", i1, StringComparison.Ordinal);
			if (i2 < 0)
			{
				i2 = this.chatLine.message.Length;
			}

			FontRenderer fontRenderer10000 = this.fontRenderer;
			return FontRenderer.func_52014_d(this.chatLine.message.Substring(i1, i2 - i1));
		}
	}
}