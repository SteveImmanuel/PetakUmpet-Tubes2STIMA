/*Tugas Besar II [IF2211] Strategi Algoritma
Anggota Kelompok :
    Aditya Putra Santosa    / 13517013
    Steve Andreas Immanuel  / 13517039
    Leonardo                / 13517048
Nama File   : Classes.cs
Deskripsi   : Deklarasi Kelas Edge, Graph (List of Edge), dan Command
*/
using System;
using System.Collections.Generic;

namespace Tubes2Stima {
    public class Edge {
        private int from;
        private int to;
        
        //ctor
        public Edge(int _from, int _to) {
            this.from = _from;
            this.to = _to;
        }

        public int getFrom(){
            return this.from;
        }

        public int getTo(){
            return this.to;
        }

        public void setFrom(int _from){
            this.from = _from;
        }

        public void setTo(int _to){
            this.to = _to;
        }
    }

    //List of Command - Graph
    public class Graph {
        private int edgenum;
        private List<Edge> listOfEdge;
        
        //ctor
        public Graph(){
            this.edgenum = 0;
            this.listOfEdge = new List<Edge>();
        }

        public Edge getEdge(int idx){
            return this.listOfEdge[idx];
        }

        public int getEdgeNum(){
            return this.edgenum;
        }

        public void AddEdge(Edge E){
            this.listOfEdge.Add(E);
            this.edgenum += 1;
        }
    }

    public class Command {
        private int approach, X, Y;
        /* approach :
             0. Menjauhi Vertex 1
             1. Mendekati Vertex 1

           X : Tempat Ferdiant Mulai
           Y : Tempat Jose mengumpat 
        */

        //ctor
        public Command(int _approach, int _X, int _Y){
            this.approach = _approach;
            this.X = _X;
            this.Y = _Y;
        }
        //getter
        public int getApproach(){
            return this.approach;
        }

        public int getX(){
            return this.X;
        }

        public int getY(){
            return this.Y;
        }
        //setter
        public void setApproach(int _approach){
            this.approach = _approach;
        }

        public void setX(int _X){
            this.X = _X;
        }

        public void setY(int _Y){
            this.Y = _Y;
        }
    }
}