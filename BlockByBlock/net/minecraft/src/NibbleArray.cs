using System;
using System.Runtime.CompilerServices;

namespace net.minecraft.src
{
    public class NibbleArray
    {
        public readonly sbyte[] data;
        private readonly int depthBits;
        private readonly int depthBitsPlusFour;

        public NibbleArray(int i1, int i2)
        {
            this.data = new sbyte[i1 >> 1];
            this.depthBits = i2;
            this.depthBitsPlusFour = i2 + 4;
        }

        public NibbleArray(sbyte[] b1, int i2)
        {
            this.data = b1;
            this.depthBits = i2;
            this.depthBitsPlusFour = i2 + 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int get(int i1, int i2, int i3)
        {
            int i4 = i2 << this.depthBitsPlusFour | i3 << this.depthBits | i1;
            int i5 = i4 >> 1;
            int i6 = i4 & 1;
            return i6 == 0 ? this.data[i5] & 0xF : this.data[i5] >> 4 & 0xF;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void set(int i1, int i2, int i3, int i4)
        {
            int i5 = i2 << this.depthBitsPlusFour | i3 << this.depthBits | i1;
            int i6 = i5 >> 1;
            int i7 = i5 & 1;
            if (i7 == 0)
            {
                this.data[i6] = (sbyte)((this.data[i6] & 0xF0) | (i4 & 0xF));
            }
            else
            {
                this.data[i6] = (sbyte)((this.data[i6] & 0x0F) | ((i4 & 0xF) << 4));
            }
        }
    }
}