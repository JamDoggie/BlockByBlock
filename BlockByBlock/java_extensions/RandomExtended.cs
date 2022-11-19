using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.java_extensions
{
    public class RandomExtended
    {
        private java.util.Random _internalRandom;

        public RandomExtended() : base()
        {
            _internalRandom = new();
        }

        public RandomExtended(long seed) 
        {
            _internalRandom = new java.util.Random(seed);
        }

        private double nextNextGaussian;
        private bool haveNextNextGaussian = false;

        public double NextGaussian()
        {
            return _internalRandom.nextGaussian();
        }

        public bool NextBool()
        {
            return _internalRandom.nextBoolean();
        }

        public double NextDouble()
        {
            return _internalRandom.nextDouble();
        }

        public float NextSingle()
        {
            return _internalRandom.nextFloat();
        }
        
        public long NextInt64()
        {
            return _internalRandom.nextLong();
        }

        public int Next(int upperBound)
        {
            return _internalRandom.nextInt(upperBound);
        }

        public int Next()
        {
            return _internalRandom.nextInt();
        }

        public void SetSeed(long seed)
        {
            _internalRandom.setSeed(seed);
        }
    }
}
