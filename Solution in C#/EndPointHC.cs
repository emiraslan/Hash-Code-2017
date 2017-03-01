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
    class EndPointHC
    {
        public List<ConnectionWithLat> connectedCahce = new List<ConnectionWithLat>();
        public int latency;
        public List<RequestHC> requests = new List<RequestHC>();

    }

    class ConnectionWithLat
    {
        public int latency;
        public CacheHC to;
    }

}
