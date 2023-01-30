using System.Runtime.InteropServices;

namespace net.minecraft.client.world.render
{
    public unsafe class UnsafeByteBuffer : IDisposable
    {
        public nint Handle => _ptr;
        public int Size => _size;

        private readonly nint _ptr;
        private readonly int _size;

        public UnsafeByteBuffer(int size)
        {
            _ptr = Marshal.AllocHGlobal(size);
            _size = size;
        }
        
        /// <summary>
        /// WARNING: there is no bounds checking on this for performance reasons.
        /// Please be responsible.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                return ((byte*)_ptr)[index];
            }

            set
            {
                ((byte*)_ptr)[index] = value;
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(_ptr);
            GC.SuppressFinalize(this);
        }

        public void CopyTo(UnsafeByteBuffer newBuffer)
        {
            int amountToCopy = Math.Min(_size, newBuffer._size);

            for (int i = 0; i < amountToCopy; i++)
            {
                newBuffer[i] = this[i];
            }
        }
        
        public static void Copy(UnsafeByteBuffer source, int sourceOffset, UnsafeByteBuffer destination, int destinationOffset, int length)
        {
            if (sourceOffset < 0 || sourceOffset >= source._size)
                throw new IndexOutOfRangeException("Source offset out of range of source buffer.");

            if (destinationOffset < 0 || destinationOffset >= destination._size)
                throw new IndexOutOfRangeException("Destination offset out of range of destination buffer.");

            if (length < 0 || length > source._size - sourceOffset || length > destination._size - destinationOffset)
                throw new IndexOutOfRangeException("Length out of range of source or destination buffer.");

            for (int i = 0; i < length; i++)
            {
                destination[destinationOffset + i] = source[sourceOffset + i];
            }
        }

        // There is no check here to see if we've already disposed because we've told the GC to not
        // call the finalizer if/when Dispose is called using GC.SuppressFinalize(this)
        ~UnsafeByteBuffer()
        {
            Dispose();
        }
    }
}