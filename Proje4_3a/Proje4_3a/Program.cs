using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje4_3a
{
    class Program
    {
        public static int INFINITY = 1000;
        static void Main(string[] args)
        {
            int N = 5;  // 5 köşeli graph
            int SRC = 0;  // Başlangıç köşesi, bu köşeden diğer tüm köşelere olan en kısa yollar bulunacak

            int[,] cost = {
            { INFINITY,    5,    3, INFINITY,    2},
            { INFINITY, INFINITY,    2,    6, INFINITY},
            { INFINITY,    1, INFINITY,    2, INFINITY},
            { INFINITY, INFINITY, INFINITY, INFINITY, INFINITY},
            { INFINITY,    6,   10,    4,    INFINITY}  };

            int[] distances = new int[N];  // Kaynak köşesinden(0) diğer tüm köşelere olan en kısa yollar bu dizide tutulacak

            Distance(N, cost, distances, SRC);

            for (int i = 0; i < distances.Length; ++i)
                if (distances[i] != INFINITY)
                    Console.WriteLine(distances[i]);
                else Console.WriteLine("INFINITY");

            Console.ReadLine();
        }

        public static void Distance(int N, int[,] cost, int[] D, int src)
        {

            int w, v, min;

            bool[] visited = new bool[N];  // Seçilen köşeden bu köşeye olan en kısa yol bulundu mu?

            //initialization of D[], visited[] and previous[] arrays according to src node
            for (v = 0; v < N; v++)
            {
                if (v != src)
                {
                    visited[v] = false;
                    D[v] = cost[src, v];
                }
                else
                {
                    visited[v] = true;
                    D[v] = 0;
                }

            }

            // Searching for shortest paths
            for (int i = 0; i < N; ++i)
            {
                min = INFINITY;
                for (w = 0; w < N; w++)
                    if (!visited[w])
                        if (D[w] < min)
                        {
                            v = w;
                            min = D[w];
                        }

                visited[v] = true;

                for (w = 0; w < N; w++)
                    if (!visited[w])
                        if (min + cost[v, w] < D[w])
                        {
                            D[w] = min + cost[v, w];
                        }
            }
        }
    }
}