namespace argo.saj
{

	internal sealed class PositionTrackingPushbackReader : ThingWithPosition
	{
		private readonly StringReader pushbackReader;
		private int characterCount = 0;
		private int lineCount = 1;
		private bool lastCharacterWasCarriageReturn = false;

		public PositionTrackingPushbackReader(StringReader reader1)
		{
			this.pushbackReader = reader1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void unread(char c1) throws java.io.IOException
		public void unread(char c1)
		{
			--this.characterCount;
			if (this.characterCount < 0)
			{
				this.characterCount = 0;
			}

			this.pushbackReader.unread(c1);
		}

		public void uncount(char[] c1)
		{
			this.characterCount -= c1.Length;
			if (this.characterCount < 0)
			{
				this.characterCount = 0;
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public int read() throws java.io.IOException
		public int read()
		{
			int i1 = this.pushbackReader.read();
			this.updateCharacterAndLineCounts(i1);
			return i1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public int read(char[] c1) throws java.io.IOException
		public int read(char[] c1)
		{
			int i2 = this.pushbackReader.read(c1);
			char[] c3 = c1;
			int i4 = c1.Length;

			for (int i5 = 0; i5 < i4; ++i5)
			{
				char c6 = c3[i5];
				this.updateCharacterAndLineCounts(c6);
			}

			return i2;
		}

		private void updateCharacterAndLineCounts(int i1)
		{
			if (13 == i1)
			{
				this.characterCount = 0;
				++this.lineCount;
				this.lastCharacterWasCarriageReturn = true;
			}
			else
			{
				if (10 == i1 && !this.lastCharacterWasCarriageReturn)
				{
					this.characterCount = 0;
					++this.lineCount;
				}
				else
				{
					++this.characterCount;
				}

				this.lastCharacterWasCarriageReturn = false;
			}

		}

		public int Column
		{
			get
			{
				return this.characterCount;
			}
		}

		public int Row
		{
			get
			{
				return this.lineCount;
			}
		}
	}

}