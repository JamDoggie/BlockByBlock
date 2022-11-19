using NVorbis;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ikvm.extensions;
using BlockByBlock.helpers;
using NVorbis.Ogg;

namespace BlockByBlock.sound
{
    public class SoundStreamed : Sound
    {
        public override int Channels { get; set; }

        public override long FileLengthInSamples => _reader.TotalSamples;

        public override long FileLengthAsShortBytes => _reader.TotalSamples * sizeof(short);

        private VorbisReader _reader;

        public SoundStreamed(string path)
        {
            FileInfo fileInfo = new(path);

            if (fileInfo.Extension == ".mus")
            {
                /*using (MusInputStream input = new MusInputStream(path, FileMode.Open))
                {
                    using (FileStream testFile = new FileStream("test.ogg", FileMode.OpenOrCreate))
                    {
                        byte[] buff = new byte[testFile.Length];
                        input.Read(buff, 0, buff.Length);
                
                        testFile.Write(buff, 0, buff.Length);
                    }
                }*/
                
                _reader = new VorbisReader(new MusInputStream(path, FileMode.Open));
            }
            else
            {
                _reader = new VorbisReader(new FileStream(path, FileMode.Open));
            }

            Channels = _reader.Channels;
        }

        ~SoundStreamed()
        {
            _reader.Dispose();
        }

        public unsafe override int Get(int buffer, int offset, int length)
        {
            float* chunkBuffer = stackalloc float[length * Channels];
            short* shortChunkBuffer = stackalloc short[length * Channels];

            if (offset >= _reader.TotalSamples)
                return 0;
            
            if (_reader.SamplePosition != offset)
                _reader.SeekTo(offset);

            int read = _reader.ReadSamples(new Span<float>(chunkBuffer, length * Channels));

            // Convert float samples to short
            for (int i = 0; i < read; i++)
            {
                ByteShortUnion union = new();
                union.sh = (short)(chunkBuffer[i] * 32768);

                ((byte*)shortChunkBuffer)[i * sizeof(short)] = union.b0;
                ((byte*)shortChunkBuffer)[i * sizeof(short) + 1] = union.b1;
            }

            // Buffer it
            if (read * sizeof(short) > length * Channels * sizeof(short))
            {
                Console.WriteLine("SoundStreamed: Likely heap corruption!");
            }

            AL.BufferData(buffer, Format, shortChunkBuffer, read * sizeof(short), SoundSystem.SamplingFrequency);

            return read;
        }
    }
}
