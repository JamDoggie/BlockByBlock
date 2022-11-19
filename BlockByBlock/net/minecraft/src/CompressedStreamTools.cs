using System.IO;
using System.IO.Compression;

namespace net.minecraft.src
{

	public class CompressedStreamTools
	{
		public static NBTTagCompound readCompressed(Stream inputStream0)
		{
			using(BinaryReader dataInputStream1 = new BinaryReader(new GZipStream(inputStream0, CompressionMode.Decompress)))
            {
				NBTTagCompound nBTTagCompound2 = read(dataInputStream1);

				return nBTTagCompound2;
			}
		}
        
		public static void writeCompressed(NBTTagCompound nBTTagCompound0, Stream outputStream1)
		{
			using (BinaryWriter dataOutputStream2 = new BinaryWriter(new GZipStream(outputStream1, CompressionMode.Compress)))
            {
				write(nBTTagCompound0, dataOutputStream2);
			}
		}

		public static NBTTagCompound decompress(byte[] b0)
		{
			using (BinaryReader dataInputStream1 = new BinaryReader(new GZipStream(new MemoryStream(b0), CompressionMode.Decompress)))
            {
				NBTTagCompound nBTTagCompound2 = read(dataInputStream1);
				return nBTTagCompound2;
			}
		}
        
		public static byte[] compress(NBTTagCompound nBTTagCompound0)
		{
			MemoryStream byteArrayOutputStream1 = new MemoryStream();
			
			using (BinaryWriter dataOutputStream2 = new BinaryWriter(new GZipStream(byteArrayOutputStream1, CompressionMode.Compress)))
            {
				write(nBTTagCompound0, dataOutputStream2);
				return byteArrayOutputStream1.ToArray();
			}
		}
        

		/// <summary>
		/// Safely writes a given NBTTagCompound to a file by first creating a temporary file, writing to it, then replacing the file that already exists if there is one.
		/// </summary>
		/// <param name="nBTTagCompound0"></param>
		/// <param name="file1"></param>
		/// <exception cref="IOException"></exception>
		public static void safeWrite(NBTTagCompound nBTTagCompound0, FileInfo file1)
		{
			FileInfo file2 = new FileInfo(file1.FullName + "_tmp");
			if (file2.Exists)
			{
				file2.Delete();
			}

			write(nBTTagCompound0, file2);
            
			if (file1.Exists)
			{
				file1.Delete();
			}

			if (file1.Exists)
			{
				throw new IOException("Failed to delete " + file1);
			}
			else
			{
                file2.MoveTo(file1.FullName, true);
            }
		}
        
		public static void write(NBTTagCompound nBTTagCompound0, FileInfo file1)
		{
			using (BinaryWriter dataOutputStream2 = new BinaryWriter(new FileStream(file1.FullName, FileMode.Create, FileAccess.Write)))
            {
				write(nBTTagCompound0, dataOutputStream2);
			}
		}
        
		public static NBTTagCompound? read(FileInfo file0)
		{
			if (!file0.Exists)
			{
				return null;
			}
			else
			{
				using (BinaryReader dataInputStream1 = new BinaryReader(new FileStream(file0.FullName, FileMode.OpenOrCreate, FileAccess.Read)))
                {
					NBTTagCompound nBTTagCompound2 = read(dataInputStream1);
					return nBTTagCompound2;
				}
			}
		}
        
		public static NBTTagCompound read(BinaryReader dataInput0)
		{
			NBTBase nBTBase1 = NBTBase.readNamedTag(dataInput0);
			if (nBTBase1 is NBTTagCompound)
			{
				return (NBTTagCompound)nBTBase1;
			}
			else
			{
				throw new IOException("Root tag must be a named compound tag");
			}
		}

		public static void write(NBTTagCompound nBTTagCompound0, BinaryWriter dataOutput1)
		{
			NBTBase.writeNamedTag(nBTTagCompound0, dataOutput1);
		}
	}

}