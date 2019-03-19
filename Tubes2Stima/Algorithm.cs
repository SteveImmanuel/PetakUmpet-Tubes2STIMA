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
        public Boolean SearchPath(int type, Node to, Node from, Graph g)
        {
            if (type == 0)
            {
                from.setVisited(true);
                if (from.neighborSize() == 0)
                {
                    return false;
                }
                else
                {
                    int i = 0;
                    Boolean found = false;
                    do
                    {
                        if (from.getNeighbor(i).getWeight() <= from.getWeight() && !from.getNeighbor(i).getVisited())
                        {
                            if (from.getNeighbor(i) == to)
                            {
                                Edge tempEdge = g.getEdge(from.getID() - 1, from.getNeighbor(i).getID() - 1);
                                tempEdge.setColor(1);
                                tempEdge.printInfo();
                                found = true;
                            }
                            else
                            {
                                Edge tempEdge = g.getEdge(from.getID() - 1, from.getNeighbor(i).getID() - 1);
                                tempEdge.setColor(1);
                                tempEdge.printInfo();
                                found = SearchPath(type, to, from.getNeighbor(i), g);
                                if (!found)
                                {
                                    tempEdge.setColor(0);
                                }
                            }
                        }
                        i++;
                    } while (i < from.neighborSize() && !found);
                    return found;
                }
            }
            else if (type == 1)
            {
                return false;

            }
            else
            {
                return false;
            }
        }
    }
}