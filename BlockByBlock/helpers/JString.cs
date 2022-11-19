using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.helpers
{
    public class JString
    {
        private string _string;
        private int hash = 0;

        public string String => _string;

        public JString(string s)
        {
            _string = s;
        }

        public int hashCode()
        {
            int h = hash;
            if (h == 0 && String.Length > 0)
            {
                for (int i = 0; i < String.Length; i++)
                {
                    h = 31 * h + String[i];
                }
                hash = h;
            }
            return h;
        }
    }
}
