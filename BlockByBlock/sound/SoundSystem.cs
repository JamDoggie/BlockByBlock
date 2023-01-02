using com.sun.tools.@internal.xjc.reader.gbind;
using net.minecraft.client;
using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class goes out to Paul. Shoutouts Paul!!!!

namespace BlockByBlock.sound
{
    /// <summary>
    /// This is essentially a recreation of pauls soundsystem (or atleast the necessary parts). Figured it'd be easier to just make my own OpenAL wrapper
    /// instead of porting some random java library Notch decided to use. Plus, this could technically be made into an interface so that other
    /// sound libraries could be hooked up like SDL or FMOD.
    /// </summary>
    public class SoundSystem
    {
        public string OpenALVersion { get; private set; }
        public string OpenALVendor { get; private set; }
        public string OpenALRenderer { get; private set; }
        internal Dictionary<string, Sound> CachedSounds { get; private set; } = new();
        internal Dictionary<string, AudioSource> AudioSources { get; private set; } = new();
        internal  ConcurrentBag<AudioSource> SoundPool { get; private set; }
        
        internal int SoundPoolSize { get; private set; }

        public const int SamplingFrequency = 44100;

        private ALDevice _outputDevice;
        private ALContext _context;
        internal const int BufferChunkSize = 2048;
        private DateTime _lastBufferCheckTime = DateTime.Now;
        private int _bufferCheckIntervalMS = 1;
        private volatile AudioSource? _musicSource;
        internal volatile object Lock = new();
        private volatile bool shouldCleanup = false;
        private List<Action> _tasks = new();

        /// <summary>
        /// Initializes the sound system with a default output and input device.
        /// </summary>
        /// <param name="outputDevice">The OpenAL provided output device name, or null if default.</param>
        /// <param name="inputDevice">The OpenAL provided input device name, or null if default.</param>
        public SoundSystem(string? outputDevice, string? inputDevice)
        {
            _outputDevice = ALC.OpenDevice(outputDevice);
            _context = ALC.CreateContext(_outputDevice, (int[]?)null);
            ALC.MakeContextCurrent(_context);

            OpenALVersion = AL.Get(ALGetString.Version);
            OpenALVendor = AL.Get(ALGetString.Vendor);
            OpenALRenderer = AL.Get(ALGetString.Renderer);

            Console.WriteLine($"Initializing OpenAL version {OpenALVersion} by {OpenALVendor} on {OpenALRenderer}");
            ALC.GetInteger(_outputDevice, AlcGetInteger.AttributesSize, 1, out int attribSize);

            int monoSources = 64; // Default to 64 mono sources if we somehow fail at getting the right number of sources from OpenAL.

            unsafe
            {
                int* attribs = stackalloc int[attribSize];
                ALC.GetInteger(_outputDevice, AlcGetInteger.AllAttributes, attribSize, attribs);
                for (int i = 0; i < attribSize; i += 2)
                {
                    if (attribs[i] == (int)AlcContextAttributes.MonoSources)
                    {
                        Console.WriteLine($"{attribs[i + 1]} mono sources available to use!");
                        monoSources = attribs[i + 1];
                        break;
                    }
                }
            }
            
            // Create our sound pool.
            SoundPool = new ConcurrentBag<AudioSource>();
            SoundPoolSize = Math.Max(1, monoSources - 4); // Leave some buffer space for music and other possible non negotiable sounds.
        }

        public void RunThread()
        {
            while (true)
            {
                lock(Lock)
                {
                    while (_tasks.Count > 0)
                    {
                        _tasks[0]();
                        _tasks.RemoveAt(0);
                    }

                    TickSoundSystem();
                    if (shouldCleanup)
                    {
                        shouldCleanup = false;
                        break;
                    }
                }
                Thread.Sleep(1);
            }

            // Cleanup
            if (_context != ALContext.Null)
            {
                ALC.MakeContextCurrent(ALContext.Null);

                ALC.DestroyContext(_context);
            }

            if (_outputDevice != ALDevice.Null)
            {
                ALC.CloseDevice(_outputDevice);
            }
        }

        public void TickSoundSystem()
        {
            lock(Lock)
            {
                foreach (KeyValuePair<string, AudioSource> pair in AudioSources)
                {
                    pair.Value.Tick();
                }

                if (_musicSource != null)
                    _musicSource.Tick();
            }
        }

        internal void NewSource(bool priority, string sourcename, Uri soundUrl, string identifier, bool loop, Vector3? pos)
        {
            lock(Lock)
            {
                _tasks.Add(() => 
                {
                    CachedSounds.TryGetValue(Path.GetFullPath(soundUrl.LocalPath), out Sound? sound);
                    if (sound == null)
                    {
                        sound = new SoundCached(Path.GetFullPath(soundUrl.LocalPath));
                        CachedSounds.Add(Path.GetFullPath(soundUrl.LocalPath), sound);
                    }

                    AudioSource? source = GetAudioSourceFromPool(sound, pos);

                    if (source != null)
                    {
                        source.Loop(loop);
                        AudioSources[sourcename] = source;
                        source.IsPriority = priority;
                        source.Play();
                    }
                });
            }
        }

        internal AudioSource? GetAudioSourceFromPool(Sound sound, Vector3? position)
        {
            AudioSource? retSource = null;
            if (SoundPool.Count < SoundPoolSize)
            {
                // We still have an empty slot in the pool, create and return a new AudioSource.
                retSource = new AudioSource(sound, position);
                SoundPool.Add(retSource);
            }
            else
            {
                bool foundSource = false;

                // First, we check if there are any audio sources that are done playing.
                // If we find one that is done, repurpose it and use it.
                foreach(AudioSource source in SoundPool)
                {
                    if (!source.IsActive)
                    {
                        source.Reset(sound);

                        source.Position(position);

                        retSource = source;
                        foundSource = true;
                        break;
                    }
                }

                // Now it gets a little hairy. Our sound pool is completely filled with active sounds, so we need to force stop one.
                // What we'll do is axe and repurpose an audio source based on how far away it is from the listener.
                // We'll also factor in AudioSource.IsPriority

                if (!foundSource)
                {
                    float biggestRemovalFactor = 0;
                    AudioSource? biggestRemovalSource = null;

                    foreach (AudioSource source in SoundPool)
                    {
                        AL.GetSource(source.ALSource, ALSource3f.Position, out Vector3 sourcePos);
                        AL.GetListener(ALListener3f.Position, out Vector3 pos);

                        float removalFactor = Vector3.DistanceSquared(sourcePos, pos);
                        if (source.IsPriority)
                            removalFactor /= 2;

                        if (removalFactor > biggestRemovalFactor)
                        {
                            biggestRemovalFactor = removalFactor;
                            biggestRemovalSource = source;
                        }
                    }

                    if (biggestRemovalSource == null)
                        biggestRemovalSource = SoundPool.First();

                    Console.WriteLine("Too many active sounds! Force stopping an existing sound and repooling.");

                    biggestRemovalSource.Reset(sound);

                    biggestRemovalSource.Position(position);

                    retSource = biggestRemovalSource;

                    for (int i = AudioSources.Count - 1; i >= 0; i--)
                    {
                        KeyValuePair<string, AudioSource> pair = AudioSources.ElementAt(i);
                        if (pair.Value == retSource)
                        {
                            AudioSources.Remove(pair.Key); // Clean up this audio source's previous sound ID association.
                            break;
                        }
                    }
                }
            }

            return retSource;
        }

        public void SetPitch(string string8, float f6)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    AudioSources.TryGetValue(string8, out AudioSource? source);

                    source?.Pitch(f6);
                });
            }
        }

        public void SetVolume(string string8, float v)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    AudioSources.TryGetValue(string8, out AudioSource? source);

                    source?.Volume(v);
                });
            }
        }

        public void Play(string string8)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    AudioSources.TryGetValue(string8, out AudioSource? source);

                    source?.Play();
                });
            }
        }

        public void SetListenerPosition(float d4, float d6, float d8)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    AL.Listener(ALListener3f.Position, d4, d6, d8);
                });
            }
        }

        public void SetListenerOrientation(float f12, float f13, float f14, float f15, float f16, float f17)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    Vector3 look = new Vector3(f12, f13, f14);
                    Vector3 up = new Vector3(f15, f16, f17);
                    AL.Listener(ALListenerfv.Orientation, ref look, ref up);
                });
            }
        }

        internal void Cleanup()
        {
            shouldCleanup = true;
        }

        internal bool Playing(string v)
        {
            lock (Lock)
            {
                AudioSources.TryGetValue(v, out AudioSource? source);

                return source != null ? source.IsActive : false;
            }
        }

        internal void BackgroundMusic(string sourceName, Uri soundUrl, string soundName, bool v2)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    Console.WriteLine($"Playing music: {soundUrl}");

                    CachedSounds.TryGetValue(Path.GetFullPath(soundUrl.LocalPath), out Sound? sound);
                    if (sound == null)
                    {
                        sound = new SoundStreamed(Path.GetFullPath(soundUrl.LocalPath));
                        CachedSounds.Add(Path.GetFullPath(soundUrl.LocalPath), sound);
                    }

                    if (_musicSource != null)
                    {
                        _musicSource.Reset(sound);
                    }
                    else
                    {
                        AL.GetListener(ALListener3f.Position, out Vector3 vec);
                        _musicSource = new AudioSource(sound, vec);
                    }

                    AudioSources[sourceName] = _musicSource;

                    
                    _musicSource.Play();
                });
            }
        }

        public void Stop(string v)
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    AudioSources.TryGetValue(v, out AudioSource? source);

                    source?.Stop();
                });
            }
        }

        // I think v3 is supposed to be channels, and v4 is supposed to be hearing range. Not sure.
        internal void NewStreamingSource(bool priority, string sourceName, Uri soundUrl, string soundName, bool loop, Vector3 pos, int v3, float v4)                                                                                                                                          
        {
            lock (Lock)
            {
                _tasks.Add(() =>
                {
                    CachedSounds.TryGetValue(Path.GetFullPath(soundUrl.LocalPath), out Sound? sound);
                    if (sound == null)
                    {
                        sound = new SoundStreamed(Path.GetFullPath(soundUrl.LocalPath));
                        CachedSounds.Add(Path.GetFullPath(soundUrl.LocalPath), sound);
                    }
                    
                    AudioSource? source = GetAudioSourceFromPool(sound, pos);

                    if (source != null)
                    {
                        source.Loop(loop);
                        AudioSources[sourceName] = source;
                        source.IsPriority = priority;
                        source.Play();
                    }
                });
            }
        }
    }
}
