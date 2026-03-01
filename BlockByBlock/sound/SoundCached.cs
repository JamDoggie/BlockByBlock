
using NAudio.Dsp;
using NVorbis;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.sound
{
    /// <summary>
    /// Represents a sound that is directly loaded from a file. This should only be used for smaller sounds that won't take as much space in memory.
    /// There should only be one of these objects per precached sound loaded in memory at a time. This means that if, for example, you have a character hurt sound, that sound 
    /// should only ever be loaded once into memory, then reused later. Treat this as essentially just a buffer.
    /// </summary>
    public class SoundCached : Sound
    {
        public override int Channels { get; set; } = 1;
        public override long FileLengthInSamples => _currentBufferSize;

        public override long FileLengthAsShortBytes => _currentBufferByteSize;

        public int Frequency { get; protected set; }

        private float[] _buffer;
        private float[] _bufferDownMixed;
        private IntPtr _bufferPtr;
        private readonly int _currentBufferByteSize;
        private readonly int _currentBufferSize;
        
        private byte[] resamplerBuffer;

        public unsafe SoundCached(string path)
        {
            FileInfo file = new FileInfo(path);

            using (VorbisReader reader = new VorbisReader(path))
            {
                // Read the whole audio file into a buffer.
                Channels = 1;
                Frequency = reader.SampleRate;
                _buffer = new float[reader.TotalSamples * reader.Channels];

                reader.ReadSamples(_buffer, 0, _buffer.Length);

                // Downmix to mono if the file is stereo.
                if (reader.Channels == 2 && Channels == 1)
                {
                    _bufferDownMixed = new float[reader.TotalSamples];
                    for (int i = 0; i < _bufferDownMixed.Length; i++)
                    {
                        _bufferDownMixed[i] = (_buffer[i * 2] + _buffer[i * 2 + 1]) / 2;
                    }
                    
                    _buffer = _bufferDownMixed;
                }

                // Store the audio in an unmanaged buffer.
                _currentBufferSize = _buffer.Length;
                _currentBufferByteSize = _buffer.Length * sizeof(short);
                _bufferPtr = Marshal.AllocHGlobal(_currentBufferByteSize);

                for (int i = 0; i < _buffer.Length; i++)
                {
                    ByteShortUnion union = new();
                    union.sh = (short)(_buffer[i] * 32768);

                    ((byte*)(_bufferPtr.ToPointer()))[i * sizeof(short)] = union.b0;
                    ((byte*)(_bufferPtr.ToPointer()))[i * sizeof(short) + 1] = union.b1;
                }
            }
        }

        public override int Get(int buffer, int offset, int length)
        {
            length = Math.Max(0, length);
            
            if (offset < _currentBufferByteSize)
            {
                if (length + offset > _currentBufferByteSize)
                {
                    Console.WriteLine("SoundCached: Likely heap corruption!");
                }

                AL.BufferData(buffer, Format, _bufferPtr + offset, length, Frequency);

                return length;
            }

            return 0;
        }

        ~SoundCached()
        {
            Marshal.FreeHGlobal(_bufferPtr); // Make sure to free our pointer when we're done with it!! This may still be C#, but we have to stay responsible...
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct ByteFloatUnion
    {
        [FieldOffset(0)] public byte b0;
        [FieldOffset(1)] public byte b1;
        [FieldOffset(2)] public byte b2;
        [FieldOffset(3)] public byte b3;

        [FieldOffset(0)]
        public float f;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct ByteShortUnion
    {
        [FieldOffset(0)] public byte b0;
        [FieldOffset(1)] public byte b1;

        [FieldOffset(0)]
        public short sh;
    }
}
