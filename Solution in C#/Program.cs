using System;
using System.Collections.Generic;
using System.IO;
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
    class Program
    {
        public static List<CacheHC> caches;
        public static List<EndPointHC> endPoints;
        public static List<VideoHC> videos;
        public static List<RequestHC> requests;
        public static string path = $@"C:\Users\aslan\Desktop\problemsss\kittens.in";



        public static StreamReader reader = new StreamReader(path);

        static string[] parts;
        static int currentPart;
        static string getNextPart()
        {
            if (parts == null || parts.Length == 0 || currentPart == parts.Length)
            {
                currentPart = 0;
                string line = reader.ReadLine();
                parts = line.Split(new char[] { ' ' });
            }
            return parts[currentPart++];
        }

        static int readInt()
        {
            return Convert.ToInt32(getNextPart());
        }
        static void Main222(string[] args)
        {
            //algo1(); // by latency
            algo2(); // by cache optimized
        }



        private static void algo2()
        {
            HashSet<CacheHC> usedCaches = new HashSet<CacheHC>();
            #region Reader
            int numVideos = readInt();
            videos = new List<VideoHC>(numVideos);
            int numEndPoints = readInt();
            endPoints = new List<EndPointHC>(numEndPoints);
            int numRequests = readInt();
            requests = new List<RequestHC>(numRequests);
            int numCaches = readInt();
            caches = new List<CacheHC>(numCaches);


            for (int i = 0; i < numCaches; i++)
            {
                CacheHC temp = new CacheHC();
                temp.index = i;
                caches.Add(temp);
            }



            CacheHC.capacity = readInt();

            for (int i = 0; i < numVideos; i++)
            {
                int size = readInt();

                videos.Add(new VideoHC()
                {
                    index = i,
                    size = size
                });

            }

            for (int iEndP = 0; iEndP < numEndPoints; iEndP++)
            {
                int latency = readInt();
                var endP = new EndPointHC();
                endP.latency = latency;

                int numConnectedCaches = readInt();

                for (int iConnectedCache = 0; iConnectedCache < numConnectedCaches; iConnectedCache++)
                {
                    int q = readInt();
                    var con = new ConnectionWithLat
                    {
                        to = caches[q],
                        latency = readInt()
                    };

                    endP.connectedCahce.Add(con);
                }
                endPoints.Add(endP);
            }

            for (int i = 0; i < numRequests; i++)
            {
                RequestHC rqst = new RequestHC();

                rqst.video = videos[readInt()];
                int to = readInt();
                rqst.count = readInt();

                endPoints[to].requests.Add(rqst);
            }

            #endregion

            sortByvideoCoibut(numEndPoints);

            for (int i = 0; i < numEndPoints; i++)
            {
                SortRequestsAtEndPoint2(i);
                SortCachesByLatencyAtEndPoint(i);
            }

            endPoints.Sort((y, x) => x.requests[0].video.weight);

            for (int i = 0; i < numEndPoints; i++)
            {
                var endP = endPoints[i];

                foreach (var rqst in endP.requests)
                {
                    var vid = rqst.video;
                    //if video can fit in any of cache servers
                    foreach (var cache in endP.connectedCahce)
                    {
                        if (cache.to.freeSpace >= vid.size)
                        {
                            cache.to.cachedVideos.Add(vid);
                            break;
                        }
                    }
                }

                usedCaches.AddRange(endP.connectedCahce.Select(x => x.to));
            }

            #region Writer
            StreamWriter sw = new StreamWriter(path + ".out");
                sw.WriteLine(usedCaches.Count);
               // var a = usedCaches.OrderByDescending(x => x.index);

                foreach (var i in usedCaches)
                {

                    sw.Write(i.index + " ");
                    int j = 0;
                    foreach (var vid in i.cachedVideos)

                    {
                        sw.Write(vid.index + ((j == i.cachedVideos.Count - 1) ? "\n" : " "));
                        j++;
                    }
                    sw.Flush();
                }

                #endregion
        }

        private static void SortRequestsAtEndPoint2(int index)
        {
            endPoints[index].requests.Sort((y, x) => x.video.weight.CompareTo(y.video.weight));
        }

        private static void sortByvideoCoibut(int numEndPoints)
        {
            for (int i = 0; i < numEndPoints; i++)
            {
                var endP = endPoints[i];

                foreach (var rqst in endP.requests)
                {
                    var vid = rqst.video;
                    vid.weight++;
                }
            }
        }

        private static void algo1()
        {
            HashSet<CacheHC> usedCaches = new HashSet<CacheHC>();
            #region Reader
            int numVideos = readInt();
            videos = new List<VideoHC>(numVideos);
            int numEndPoints = readInt();
            endPoints = new List<EndPointHC>(numEndPoints);
            int numRequests = readInt();
            requests = new List<RequestHC>(numRequests);
            int numCaches = readInt();
            caches = new List<CacheHC>(numCaches);


            for (int i = 0; i < numCaches; i++)
            {
                CacheHC temp = new CacheHC();
                temp.index = i;
                caches.Add(temp);
            }



            CacheHC.capacity = readInt();

            for (int i = 0; i < numVideos; i++)
            {
                int size = readInt();

                videos.Add(new VideoHC()
                {
                    index = i,
                    size = size
                });

            }

            for (int iEndP = 0; iEndP < numEndPoints; iEndP++)
            {
                int latency = readInt();
                var endP = new EndPointHC();
                endP.latency = latency;

                int numConnectedCaches = readInt();

                for (int iConnectedCache = 0; iConnectedCache < numConnectedCaches; iConnectedCache++)
                {
                    int q = readInt();
                    var con = new ConnectionWithLat
                    {
                        to = caches[q],
                        latency = readInt()
                    };

                    endP.connectedCahce.Add(con);
                }
                endPoints.Add(endP);
            }

            for (int i = 0; i < numRequests; i++)
            {
                RequestHC rqst = new RequestHC();

                rqst.video = videos[readInt()];
                int to = readInt();
                rqst.count = readInt();

                endPoints[to].requests.Add(rqst);
            }

            #endregion



            for (int i = 0; i < numEndPoints; i++)
            {
                SortRequestsAtEndPoint(i);
                SortCachesByLatencyAtEndPoint(i);
                var endP = endPoints[i];

                foreach (var rqst in endP.requests)
                {
                    var vid = rqst.video;
                    //if video can fit in any of cache servers
                    foreach (var cache in endP.connectedCahce)
                    {
                        if (cache.to.freeSpace >= vid.size)
                        {
                            cache.to.cachedVideos.Add(vid);
                            break;
                        }
                    }
                }

                usedCaches.AddRange(endP.connectedCahce.Select(x => x.to));
            }




            #region Writer
            StreamWriter sw = new StreamWriter(path + ".out");
            sw.WriteLine(usedCaches.Count);
            var a = usedCaches.OrderByDescending(x => x.index);

            foreach (var i in a)
            {

                sw.Write(i.index + " ");
                int j = 0;
                foreach (var vid in i.cachedVideos)

                {
                    sw.Write(vid.index + ((j == i.cachedVideos.Count - 1) ? "\n" : " "));
                    j++;
                }
                sw.Flush();
            }

            #endregion
        }

        private static void SortRequestsAtEndPoint(int index)
        {
            endPoints[index].requests.Sort((y, x) => x.count.CompareTo(y.count));
        }

        private static void SortCachesByLatencyAtEndPoint(int index)
        {
            endPoints[index].connectedCahce.Sort((x, y) => x.latency.CompareTo(y.latency));
        }
    }
}
