using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.java_extensions
{
    public class RandomExtended : Random
    {
        public RandomExtended() : base()
        {
        }

        public RandomExtended(int seed) : base(seed)
        {
        }

        public RandomExtended(long seed) : base((int)((seed / long.MaxValue) * int.MaxValue)) // PORTING TODO: this is very temporary and jank. This will be removed when I remake java random in this class.
        {
            
        }

        private double nextNextGaussian;
        private bool haveNextNextGaussian = false;

        /// <summary>
        /// <para>
        /// [This method is based off of the java.util.Random.nextGaussian() method.]
        /// Returns the next, Gaussian ("normally") distributed double value with mean 0.0 and standard deviation 1.0 from this random number generator's sequence.
        /// </para>
        /// 
        /// <para>
        /// The general contract of nextGaussian is that one double value, chosen from (approximately) the usual normal distribution with mean 0.0 and standard deviation 1.0, is pseudorandomly generated and returned.
        /// </para>
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public double NextGaussian()
        {
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                
                do
                {
                    v1 = 2 * NextDouble() - 1;   // between -1.0 and 1.0
                    v2 = 2 * NextDouble() - 1;   // between -1.0 and 1.0
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1 || s == 0);
                
                double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }

        public bool NextBool()
        {
            return Next(2) == 1;
        }
    }
}
