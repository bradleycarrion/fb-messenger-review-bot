using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.Common
{
    public static class Requires
    {
        public static void NotNull<T>(T instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
    }
}
