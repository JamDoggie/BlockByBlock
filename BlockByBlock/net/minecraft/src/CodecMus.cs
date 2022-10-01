/*using System.IO;

namespace net.minecraft.src
{

	using CodecJOrbis = paulscode.sound.codecs.CodecJOrbis;

	// PORTING TODO: sound

	public class CodecMus : CodecJOrbis
	{
		// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		// ORIGINAL LINE: protected java.io.InputStream openInputStream() throws java.io.IOException
		protected internal virtual Stream openInputStream()
		{
			return new MusInputStream(this, this.url, this.urlConnection.getInputStream());
		}
	}

}*/ // PORTING TODO: Pretty sure this is unnecessary. Remove later.