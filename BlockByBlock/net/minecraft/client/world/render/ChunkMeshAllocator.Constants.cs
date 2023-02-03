using net.minecraft.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.client.world.render
{
    public partial class ChunkMeshAllocator
    {
        public const int DefaultBufferVerticeSize = 50000000 * 32;
        public const int BufferIncreaseStep = 1000000 * 32;
    }
}
