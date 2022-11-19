using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.net.minecraft.input
{
    public class KeyMapAttribute : Attribute
    {
        public Keys[] Keys { get; set; }

        public KeyMapAttribute(params Keys[] keys)
        {
            Keys = keys;
        }
    }
}
