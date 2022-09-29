using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.src
{
    public class ShortBuffer : ByteBuffer
    {
        public short get(short index)
        {
            return getShort(index);
        }
    }
}
