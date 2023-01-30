using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.net.minecraft.render
{
    public struct VertexBuffer
    {
        public int SizeInBytes;
        public int VertexCount;
        public int GLHandle;
        public int DrawMode;

        public bool HasColor;
        public bool HasBrightness;
        public bool HasTexture;
        public bool HasNormal;

        public VertexBuffer(int sizeInBytes, int elements, int glHandle, int drawMode, bool hasBrightness, bool hasColor, bool hasTexture, bool hasNormal)
        {
            SizeInBytes = sizeInBytes;
            GLHandle = glHandle;
            VertexCount = elements;
            HasBrightness = hasBrightness;
            HasColor = hasColor;
            HasTexture = hasTexture;
            HasNormal = hasNormal;
            DrawMode = drawMode;
        }
    }
}
