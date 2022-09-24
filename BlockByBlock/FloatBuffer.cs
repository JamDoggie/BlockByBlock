using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.src
{
    public class FloatBuffer : ByteBuffer
    {
        public float get(int index)
        {
            return getFloat(index);
        }
    }
}
