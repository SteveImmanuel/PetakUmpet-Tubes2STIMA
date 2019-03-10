using System.Collections.Generic;
using System;

namespace Tubes2Stima{
    class PetakUmpet{
        public static void Main(String[] args){
            int n;
            Graph g=new Graph();
            Algorithm al=new Algorithm();
            n=Convert.ToInt32(Console.ReadLine());
            for(int i=0;i<n;i++){
                g.addNode(new Node(i+1));
            }
            for(int i=0;i<n-1;i++){
                var temp=Console.ReadLine().Split(' ');
                g.getNode(Convert.ToInt32(temp[0])-1).addNeighbor(g.getNode(Convert.ToInt32(temp[1])-1));
                g.getNode(Convert.ToInt32(temp[1])-1).addNeighbor(g.getNode(Convert.ToInt32(temp[0])-1));
            }
            g.generateWeight(g.getNode(0),0);
            g.unvisitAll();
            for(int i=0;i<n;i++){
                g.getNode(i).printInfo();
                Console.WriteLine();
            }
            var temp2=Console.ReadLine().Split(' ');
            int a=Convert.ToInt32(temp2[0]);
            int b=Convert.ToInt32(temp2[1]);
            int c=Convert.ToInt32(temp2[2]);
            List<Node> path=new List<Node>();
            if(al.SearchPath(a,g.getNode(b-1),g.getNode(c-1),path)){
                Console.WriteLine("YES");
            }else{
                Console.WriteLine("NO");
            }
            Console.Write("Path=");
            for(int i=0;i<path.Count;i++){
                Console.Write(path[i].getID()+" ");
            }
            Console.WriteLine();

            Environment.Exit(0);
        }
    }
}