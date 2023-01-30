using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.client.world.render
{
    public interface IAllocator
    {
        /// <summary>
        /// Allocates the given memory into the memory buffer.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>the ID of the memory block as a 32-bit integer.</returns>
        public int AllocateData(Span<byte> data);
        
        /// <summary>
        /// Unallocates the data at the given ID, & wipes the data from the buffer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>whether or not an allocated segment was found and the operation was successful.</returns>
        public bool FreeData(int id);
        
        public void FrameUpdate();
    }
}
