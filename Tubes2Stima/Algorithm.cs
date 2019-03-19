/*
Tugas Besar II [IF2211] Strategi Algoritma
Anggota Kelompok :
    Aditya Putra Santosa    / 13517013
    Steve Andreas Immanuel  / 13517039
    Leonardo                / 13517048
Nama File   : Algorithm.cs
Deskripsi   : Pemecah masalah dengan DFS dan BackTrack
*/
using System.Collections.Generic;
using System;

namespace Tubes2Stima
{
    class Algorithm
    {
        public Boolean SearchPath(int type, Node to, Node from, List<Node> path)
        {
            from.setVisited(true);
            if (from.neighborSize() == 0)
            {
                return false;
            }
            else
            {
                path.Add(from);
                // for(int j=0;j<path.Count;j++){
                //     Console.Write(path[j].getID()+" ");
                // }
                // Console.WriteLine();
                int initialSize = path.Count;
                Boolean found = false;
                int i = 0;
                do
                {
                    if (path.Count > initialSize)
                    {
                        // Console.Write("initial size={0},sizenow={1}",initialSize,path.Count);
                        path.RemoveRange(initialSize, path.Count - initialSize);
                        // for(int j=0;j<path.Count;j++){
                        //     Console.Write(path[j].getID()+" ");
                        // }
                        // Console.WriteLine();
                    }
                    if (!from.getNeighbor(i).getVisited())
                    {
                        if ((type == 0 && from.getNeighbor(i).getWeight() < from.getWeight()) || (type == 1 && from.getNeighbor(i).getWeight() > from.getWeight()))
                        {
                            if (from.getNeighbor(i) == to)
                            {
                                found = true;
                                path.Add(from.getNeighbor(i));
                            }
                            else
                            {
                                found = SearchPath(type, to, from.getNeighbor(i), path);
                            }
                        }
                    }
                    i++;
                } while (i < from.neighborSize() && !found);
                // if(!found){
                //     path.Clear();
                // }
                return found;
            }
        }
    }
}