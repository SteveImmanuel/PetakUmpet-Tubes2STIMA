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
            Console.Write(", X= " + this.pos.x);
            Console.WriteLine(", Y= " + this.pos.y);
        }

    }

    public class Edge
    {
        private int from;
        private int to;
        private int color;

        public Edge(int _from, int _to)
        {
            this.from = _from;
            this.to = _to;
            this.color = 0;
        }

        public int getFrom()
        {
            return this.from;
        }

        public int getTo()
        {
            return this.to;
        }
        public int getColor()
        {
            return this.color;
        }

        public void setFrom(int _from)
        {
            this.from = _from;
        }

        public void setTo(int _to)
        {
            this.to = _to;
        }
        public void setColor(int _color)
        {
            this.color = _color;
        }
        public void printInfo()
        {
            Console.WriteLine("From={0},To={1},Color={2}", from, to, color);
        }
    }

    public class Graph
    {
        public List<Node> allNode;
        public List<Edge> allEdge;
        private int maxWeight;

        public Graph(){
            this.allNode=new List<Node>();
            this.allEdge = new List<Edge>();
            this.maxWeight = -1;
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

        public void unColorAll()
        {
            for (int i = 0; i < this.allEdge.Count; i++)
            {
                this.allEdge[i].setColor(0);
            }
        }

        public void generateWeight(Node _node,int _weight){
            //Console.WriteLine("ID="+_node.getID());
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

        public void GeneratePosition(int width, int height)
        {
            if (maxWeight == -1)
            {
                maxWeight = getMaxWeight();
            }
            int dx = width / (maxWeight + 1);
            int dy = dx;
            int xAwal = dx / 2;
            int yAwal = xAwal;
            List<int> curY = new List<int>();
            for (int i = 0; i <= maxWeight; i++)
            {
                curY.Add(0);
            }
            foreach (var n in allNode)
            {
                double x = (xAwal + n.getWeight() * dx);
                double y = (yAwal + curY[n.getWeight()] * dy);
                n.pos = new Koordinat2D(x, y);
                curY[n.getWeight()]++;
                //Console.WriteLine("ID={0},X={1},Y={2}", n.getID(), n.pos.x, n.pos.y);
            }

        }

        public void PanPosition(int deltaX,int deltaY)
        {
            Koordinat2D delta = new Koordinat2D(deltaX, deltaY);
            foreach (var n in allNode)
            {
                n.pos = n.pos + delta;
            }
        }

        public int getMaxWeight()
        {
            //hanya dipanggil jika allnode sudah berisi
            int max = allNode[0].getWeight();
            for (int i = 0; i < allNode.Count; i++)
            {
                if (allNode[i].getWeight() > max)
                {
                    max = allNode[i].getWeight();
                }
            }
            return max;
        }

        public void normalizeKoordinat(int width, int height)
        {
            //Normalisasi Koordinat agar berada di rentang 0-width, 0-height
            //Cari maxX, maxY, minX, minY
            double maxX, maxY, minX, minY;
            maxX = getNode(0).pos.x;
            maxY = getNode(0).pos.y;
            minX = maxX;
            minY = maxY;
            foreach (var n in allNode)
            {
                if (n.pos.x > maxX)
                {
                    maxX = n.pos.x;
                }
                else if (n.pos.x < minX)
                {
                    minX = n.pos.x;
                }

                if (n.pos.y > maxY)
                {
                    maxY = n.pos.y;
                }
                else if (n.pos.y < minY)
                {
                    minY = n.pos.y;
                }
            }
            //Console.WriteLine("Normalize Koordinat");
            foreach (var n in allNode)
            {
                double x = (width * (n.pos.x - minX) / (maxX - minX));
                double y = (height * (n.pos.y - minY) / (maxY - minY));
                n.pos = new Koordinat2D(x, y);
                //Console.Write("ID= " + n.getID());
                //Console.Write(", X= " + n.getX());
                //Console.WriteLine(", Y= " + n.getY());
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

        public Edge getEdge(int from, int to)
        {
            Boolean found = false;
            int i = 0;
            while (i < allEdge.Count && !found)
            {
                if (allEdge[i].getFrom() == from && allEdge[i].getTo() == to || allEdge[i].getFrom() == to && allEdge[i].getTo() == from)
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            if (found)
            {
                return allEdge[i];
            }
            else
            {
                return new Edge(-1, -1);
            }
        }
    }
}