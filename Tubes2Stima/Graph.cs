/*
Tugas Besar II [IF2211] Strategi Algoritma
Anggota Kelompok :
    Aditya Putra Santosa    / 13517013
    Steve Andreas Immanuel  / 13517039
    Leonardo                / 13517048
Nama File   : Graph.cs
Deskripsi   : Representasi graf
*/
using System.Collections.Generic;
using System;

namespace Tubes2Stima{

    public class Koordinat2D
    {
        public double x, y;
        public Koordinat2D(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public void setZero()
        {
            x = 0;
            y = 0;
        }

        public double magnitude()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public void normalize()
        {
            double length = this.magnitude();
            if (Math.Abs(length) > 0.0001)
            {
                x = x / length;
                y = y / length;
            }
        }

        public static double distance(Koordinat2D K1, Koordinat2D K2)
        {
            return Math.Sqrt(Math.Pow(K1.x - K2.x, 2) + Math.Pow(K1.y - K2.y, 2));
        }

        public static Koordinat2D operator +(Koordinat2D K1, Koordinat2D K2)
        {
            Koordinat2D temp = new Koordinat2D(0, 0);
            temp.x = K1.x + K2.x;
            temp.y = K1.y + K2.y;
            return temp;
        }

        public static Koordinat2D operator -(Koordinat2D K1, Koordinat2D K2)
        {
            Koordinat2D temp = new Koordinat2D(0, 0);
            temp.x = K1.x - K2.x;
            temp.y = K1.y - K2.y;
            return temp;
        }

        public static Koordinat2D operator *(Koordinat2D K, double d)
        {
            Koordinat2D temp = new Koordinat2D(K.x, K.y);
            temp.x *= d;
            temp.y *= d;
            return temp;
        }

        public Koordinat2D copy()
        {
            return new Koordinat2D(x, y);
        }
    }

    public class Node{
        private List<Node> neighbor;
        public Koordinat2D pos, v, a;
        private Boolean isPosStatic; //isPosStatic = true jika node tidak ada bergerak di iterasi sebelumnya
        private int weight;
        private int ID;
        private Boolean visited;
        
        public Node(int _ID){
            this.ID=_ID;
            this.visited=false;
            this.neighbor=new List<Node>();
            this.weight=0;
            pos = new Koordinat2D(0, 0);
            v = new Koordinat2D(0, 0);
            a = new Koordinat2D(0, 0);
        }

        public Node(int _ID, double _posX, double _posY)
        {
            this.ID = _ID;
            this.visited = false;
            this.neighbor = new List<Node>();
            this.weight = 0;
            pos = new Koordinat2D(0, 0);
            v = new Koordinat2D(0, 0);
            a = new Koordinat2D(0, 0);
        }

        public void addNeighbor(Node _neighbor){
            this.neighbor.Add(_neighbor);
        }

        public Node getNeighbor(int i){
            return this.neighbor[i];
        }
        
        public int neighborSize(){
            return this.neighbor.Count;
        }

        public bool isNeighbor(Node _neighbor)
        {
            return this.neighbor.Contains(_neighbor);
        }

        public void updatePos(Koordinat2D force, double dt)
        {
            //a = F/m, m default = 2
            this.a = force * 0.5;
            this.v = this.v + (this.a * dt);
            this.pos = this.pos + (this.v * dt);
            this.a.setZero();
        }

        public void setWeight(int _weight){
            this.weight=_weight;
        }

        public int getWeight(){
            return this.weight;
        }

        public void setIsStatic(Boolean _isStatic)
        {
            this.isPosStatic = _isStatic;
        }

        public int getID(){
            return this.ID;
        }

        public Boolean getVisited(){
            return this.visited;
        }

        public void setVisited(Boolean value){
            this.visited=value;
        }

        public void printInfo(){
            Console.WriteLine("ID="+this.getID());
            Console.WriteLine("NeighborSize="+this.neighborSize());
            Console.WriteLine("Weight="+this.getWeight());
            Console.WriteLine("Visit="+this.getVisited());
            Console.Write("X= " + this.pos.x);
            Console.WriteLine(", Y= " + this.pos.y);
        }

    }

    public class Edge
    {
        private int from;
        private int to;

        public Edge(int _from, int _to)
        {
            this.from = _from;
            this.to = _to;
        }

        public int getFrom()
        {
            return this.from;
        }

        public int getTo()
        {
            return this.to;
        }

        public void setFrom(int _from)
        {
            this.from = _from;
        }

        public void setTo(int _to)
        {
            this.to = _to;
        }
        public void printInfo()
        {
            Console.WriteLine("From={0},To={1}", from, to);
        }
    }

    public class Graph
    {
        public List<Node> allNode;
        public List<Edge> allEdge;

        public Graph(){
            this.allNode=new List<Node>();
            this.allEdge = new List<Edge>();
        }

        public void addNode(Node _node){
            this.allNode.Add(_node);
        }

        public void addEdge(int awal, int akhir)
        {
            this.allNode[awal].addNeighbor(this.allNode[akhir]);
            this.allNode[akhir].addNeighbor(this.allNode[awal]);
            this.allEdge.Add(new Edge(awal, akhir));
        }

        public void unvisitAll(){
            for(int i=0;i<this.allNode.Count;i++){
                this.allNode[i].setVisited(false);
            }
        }

        public void generateWeight(Node _node,int _weight){
            _node.setVisited(true);
            if(_node.getID()==0){
                _node.setWeight(0);
            }else{
                _node.setWeight(_weight);
            }
            for(int i=0;i<_node.neighborSize();i++){
                if(!_node.getNeighbor(i).getVisited()){
                    generateWeight(_node.getNeighbor(i),_weight+1);
                }
            }
        }

        public Node getNode(int i){
            return this.allNode[i];
        }

        public Edge getEdge(int i)
        {
            return this.allEdge[i];
        }

        public int getNodeSize()
        {
            return this.allNode.Count;
        }

        public int getEdgeSize()
        {
            return this.allEdge.Count;
        }
    }
}