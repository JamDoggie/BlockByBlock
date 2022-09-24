using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.src
{
    public class IntBuffer : ByteBuffer
    {
        public int get(int index)
        {
            return getInt(index);
        }
    }
}
