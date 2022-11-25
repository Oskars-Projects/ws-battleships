using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Extensions
{
    public static class CharExtension
    {
        public static int AsInt(this char c) 
        {
            return char.ToUpper(c) - 64; ; 
        }
    }
}
