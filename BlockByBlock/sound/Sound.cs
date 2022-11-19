using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.sound
{
    /// <summary>
    /// <para>
    /// Represents a (logically) stateless file representing a sound file located on the end user's storage.
    /// This object is similar to a sort of random access stream, where no state regarding position in the underlying stream is stored.
    /// Storing states that, for example, help optimize sequencial reads from the underlying stream are permitted; but this should be clearly outlined if this is the case.
    /// </para>
    /// 
    /// <para>
    /// The expectation is that the actual state regarding position in the stream at the time of the sound being played should be stored in an AudioSource, 
    /// and this Sound object (whether it be preloaded or streamed directly) should be cached and only created once per sound file.
    /// </para>
    /// </summary>
    public abstract class Sound
    {
        /// <summary>
        /// The number of channels that are stored in the underlying audio file.
        /// </summary>
        public abstract int Channels { get; set; }

        /// <summary>
        /// The full length of the sound, in raw PCM samples. Samples are assumed to be 32-bit floats by default.
        /// </summary>
        public abstract long FileLengthInSamples { get; }

        /// <summary>
        /// The full length of the sound in bytes. Samples are assumed to be 32-bit floats by default.
        /// </summary>
        public abstract long FileLengthAsShortBytes { get; }

        internal ALFormat Format => Channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16;

        /// <summary>
        /// Buffers the chunk of audio into the given buffer at the position and length given.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns>The amount of samples successfully buffered.</returns>
        public abstract int Get(int buffer, int offset, int length);
    }
}
