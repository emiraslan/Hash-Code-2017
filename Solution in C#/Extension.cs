
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Solution Authors: Orkhan Alikhanov and Amiraslan Bakhshili
 * Team: Cybersteins
 * Score: 1753399  
 *  
 */

namespace HashCode2017
{
    public static class Extensions
    {
        public static bool AddRange<T>(this HashSet<T> @this, IEnumerable<T> items)
        {
            bool allAdded = true;
            foreach (T item in items)
            {
                allAdded &= @this.Add(item);
            }
            return allAdded;
        }
    }
}

