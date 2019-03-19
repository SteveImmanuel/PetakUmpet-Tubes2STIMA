using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Tubes2Stima
{
    class PetakUmpet
    {
        public static void ReadMap(string filename, Graph g)
        {
            try
            {
                int n;
                var map_file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                using (var map_file_reader = new StreamReader(map_file, Encoding.UTF8))
                {
                    n = int.Parse(map_file_reader.ReadLine());
                    for (int i = 0; i < n; i++)
                    {
                        g.addNode(new Node(i + 1));
                    }
                    for (int i = 0; i < n - 1; i++)
                    {
                        var temp = map_file_reader.ReadLine().Split(' ');
                        // Console.WriteLine("{0},{1}",temp[0],temp[1]);
                        g.getNode(int.Parse(temp[0]) - 1).addNeighbor(g.getNode(int.Parse(temp[1]) - 1));
                        g.getNode(int.Parse(temp[1]) - 1).addNeighbor(g.getNode(int.Parse(temp[0]) - 1));
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        /*public static void Main(String[] args){
            int stackSize = 1024*1024*15;
            Thread thread = new Thread(new ThreadStart(BigRecursion), stackSize);
            thread.Start();
        }*/

        public static void BigRecursion()
        {
            var watch = new System.Diagnostics.Stopwatch();
            Graph g = new Graph();
            Algorithm al = new Algorithm();
            ReadMap("10.txt", g);
            Console.WriteLine("Generating...");
            g.generateWeight(g.getNode(0), 0);
            g.unvisitAll();
            Console.WriteLine("Done");
            for (int i = 0; i < g.getNodeSize(); i++)
            {
                Console.WriteLine();
            }
            var temp2 = Console.ReadLine().Split(' ');
            int a = Convert.ToInt32(temp2[0]);
            int b = Convert.ToInt32(temp2[1]);
            int c = Convert.ToInt32(temp2[2]);

            watch.Start();
            if (al.SearchPath(a, g.getNode(b - 1), g.getNode(c - 1), new List<Node>()))
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
            watch.Stop();
            Console.WriteLine("{0} ms", watch.ElapsedMilliseconds);


            Environment.Exit(0);

        }
    }
}