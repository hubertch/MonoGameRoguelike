using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core
{
    public class ID
    {
        private int _number = 0;
        private static ID _instance = null;
        public static ID Instance
        {
            get
            {
                if (_instance == null) _instance = new ID();

                return _instance;
            }
        }

        public int GetNumber()
        {
            return ++_number;
        }
    }
}
