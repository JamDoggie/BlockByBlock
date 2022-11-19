using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.sound
{
    public class AudioSource
    {
        /// <summary>
        /// The sound that this AudioSource is playing.
        /// </summary>
        public Sound ActiveSound { get; set; }
        public bool IsPriority { get; set; } = false;
        public bool IsActive
        {
            get
            {
                AL.GetSource(ALSource, ALGetSourcei.SourceState, out int state);

                if ((ALSourceState)state == ALSourceState.Stopped || (ALSourceState)state == ALSourceState.Initial)
                    return false;

                return true;
            }
        }

        internal int[] QueueBuffers { get; set; } = new int[4];
        internal int ALSource { get; set; }

        private int _currentStreamPosition = 0;

        private IntPtr _streamProcessBuffer;

        public AudioSource(Sound sound, Vector3? position, float volume = 1.0f, float pitch = 1.0f)
        {
            ActiveSound = sound;
            AL.GenBuffers(QueueBuffers.Length, QueueBuffers);
            ALSource = AL.GenSource();
            

            for (int i = 0; i < QueueBuffers.Length; i++)
            {
                int buffer = QueueBuffers[i];
                
                BufferNext(buffer, Math.Min(SoundSystem.BufferChunkSize, (int)(ActiveSound.FileLengthAsShortBytes - _currentStreamPosition)));
            }

            AL.SourceQueueBuffers(ALSource, QueueBuffers);

            Volume(volume);
            Pitch(pitch);

            Position(position);

            _streamProcessBuffer = Marshal.AllocHGlobal(sizeof(int) * QueueBuffers.Length);
        }

        ~AudioSource()
        {
            Marshal.FreeHGlobal(_streamProcessBuffer);
        }

        internal int BufferNext(int buffer, int amount)
        {
            int readCount = Math.Min(amount, (int)(ActiveSound.FileLengthAsShortBytes - _currentStreamPosition));

            if (readCount > 0)
            {
                ActiveSound.Get(buffer, _currentStreamPosition, readCount);
                _currentStreamPosition += readCount;
            }

            return readCount;
        }

        internal unsafe void Tick()
        {
            if (!IsActive)
                return;
            
            AL.GetSource(ALSource, ALGetSourcei.BuffersProcessed, out int buffersProcessed);
            if (buffersProcessed > 0 && _currentStreamPosition < ActiveSound.FileLengthAsShortBytes)
            {
                int* bufferPtr = (int*)_streamProcessBuffer.ToPointer();

                AL.SourceUnqueueBuffers(ALSource, buffersProcessed, bufferPtr);
                for (int i = 0; i < buffersProcessed; i++)
                {
                    int buffer = bufferPtr[i];
                    BufferNext(buffer, Math.Min(SoundSystem.BufferChunkSize, (int)(ActiveSound.FileLengthAsShortBytes - _currentStreamPosition)));
                }
                AL.SourceQueueBuffers(ALSource, buffersProcessed, bufferPtr);
            }
        }

        /// <summary>
        /// Plays the sound from the current position in the stream.
        /// </summary>
        public void Play()
        {
            AL.SourcePlay(ALSource);
        }

        /// <summary>
        /// Pauses the sound at the current position in the stream.
        /// </summary>
        public void Pause()
        {
            AL.SourcePause(ALSource);
        }

        /// <summary>
        /// Pauses the sound and resets the position in the stream back to the beginning.
        /// </summary>
        public void Stop()
        {
            AL.SourceStop(ALSource);
        }

        public void Volume(float vol)
        {
            AL.Source(ALSource, ALSourcef.Gain, vol);
        }

        public void Pitch(float pitch)
        {
            AL.Source(ALSource, ALSourcef.Pitch, pitch);
        }

        public void Loop(bool loop)
        {
            AL.Source(ALSource, ALSourceb.Looping, loop);
        }

        public void Position(Vector3? pos)
        {
            if (pos != null)
            {
                Vector3 position = pos.Value;
                AL.Source(ALSource, ALSource3f.Position, ref position);
                AL.Source(ALSource, ALSourceb.SourceRelative, false);
            }
            else
            {
                Vector3 position = Vector3.Zero;
                AL.Source(ALSource, ALSource3f.Position, ref position);
                AL.Source(ALSource, ALSourceb.SourceRelative, true);
            }
        }

        public void Reset(Sound sound)
        {
            Stop();

            ActiveSound = sound;

            AL.SourceUnqueueBuffers(ALSource, QueueBuffers);

            _currentStreamPosition = 0;

            AL.DeleteBuffers(QueueBuffers);
            AL.GenBuffers(QueueBuffers);

            for (int i = 0; i < QueueBuffers.Length; i++)
            {
                int buffer = QueueBuffers[i];

                BufferNext(buffer, Math.Min(SoundSystem.BufferChunkSize, (int)(ActiveSound.FileLengthAsShortBytes - _currentStreamPosition)));
            }

            AL.SourceQueueBuffers(ALSource, QueueBuffers);
            
            Pitch(1.0f);
            Volume(1.0f);
        }
    }
}
