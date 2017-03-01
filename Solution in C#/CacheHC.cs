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
    class CacheHC
    {
        public static int capacity;
        public HashSet<VideoHC> cachedVideos = new HashSet<VideoHC>();
        public int index;
        public int freeSpace => (capacity - CurrentSize);
        public int CurrentSize => cachedVideos.Sum(x => x.size);
    }
}
