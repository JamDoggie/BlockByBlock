using BlockByBlock.sound;
using System.Runtime.InteropServices;

namespace net.minecraft.client.world.render
{
    public unsafe class UnsafeByteBuffer : IDisposable
    {
        public nint Handle => _hGlobal;
        public virtual int Size => _size;
        public readonly byte* Pointer;

        protected readonly nint _hGlobal;
        protected readonly int _size;

        public UnsafeByteBuffer(int size)
        {
            _hGlobal = Marshal.AllocHGlobal(size);
            Pointer = (byte*)_hGlobal;
            _size = size;
        }
        
        /// <summary>
        /// WARNING: there is no bounds checking on this for performance reasons.
        /// This just accesses the raw internal pointer. Please be responsible, and do your own bounds checking
        /// if necessary.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual byte this[int index]
        {
            get
            {
                return Pointer[index];
            }

            set
            {
                Pointer[index] = value;
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(_hGlobal);
            GC.SuppressFinalize(this);
        }

        public void CopyBytesTo(UnsafeByteBuffer newBuffer)
        {
            int amountToCopy = Math.Min(_size, newBuffer._size);

            for (int i = 0; i < amountToCopy; i++)
            {
                newBuffer[i] = this[i];
            }
        }
        
        public static void CopyBytes(UnsafeByteBuffer source, int sourceOffset, UnsafeByteBuffer destination, int destinationOffset, int length)
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

    public unsafe class UnsafeIntBuffer : UnsafeByteBuffer
    {
        public UnsafeIntBuffer(int size) : base(size * sizeof(int))
        {
        }

        public override int Size => _size / sizeof(int);
        
        public int this[int index]
        {
            get
            {
                return ((int*)Pointer)[index];
            }

            set
            {
                ((int*)Pointer)[index] = value;
            }
        }
    }
}