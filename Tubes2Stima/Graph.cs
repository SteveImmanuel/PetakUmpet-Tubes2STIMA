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
using System.Linq;

namespace Tubes2Stima{
    public class Node{
        private List<Node> neighbor;
        private float posX, posY;
        private Boolean isPosStatic; //isPosStatic = true jika node tidak ada bergerak di iterasi sebelumnya
        private int weight;
        private int ID;
        private Boolean visited;
        
        public Node(int _ID){
            this.ID=_ID;
            this.visited=false;
            this.neighbor=new List<Node>();
            this.weight=0;
            this.posX = 0;
            this.posY = 0;
        }

        public Node(int _ID, float _posX, float _posY)
        {
            this.ID = _ID;
            this.visited = false;
            this.neighbor = new List<Node>();
            this.weight = 0;
            this.posX = _posX;
            this.posY = _posY;
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

        public float getX()
        {
            return this.posX;
        }

        public float getY()
        {
            return this.posY;
        }

        public void setX(float _posX)
        {
            this.posX = _posX;
        }

        public void setY(float _posY)
        {
            this.posY = _posY;
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
            Console.Write(", X= " + this.getX());
            Console.WriteLine(", Y= " + this.getY());
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


    public class Graph{
        private List<Node> allNode;
        private List<Edge> allEdge;
        private int maxWeight;

        public Graph(){
            this.allNode = new List<Node>();
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
            int dx = width / (maxWeight+1);
            int dy = dx;
            int xAwal = dx/2;
            int yAwal = xAwal;
            List<int> curY = new List<int>();
            for(int i = 0; i <= maxWeight; i++)
            {
                curY.Add(0);
            }
            foreach (var n in allNode)
            {
                n.setX(xAwal + n.getWeight() * dx);
                n.setY(yAwal + curY[n.getWeight()] * dy);
                curY[n.getWeight()]++;
                //Console.WriteLine("ID={0},X={1},Y={2}", n.getID(), n.getX(), n.getY());
            }

        }

        public void PanPosition(int deltaX,int deltaY)
        {
            foreach (var n in allNode)
            {
                n.setX(n.getX()+deltaX);
                n.setY(n.getY()+deltaY);
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
        public Node getNode(int i){
            return this.allNode[i];
        }

        public Edge getEdge(int i)
        {
            return this.allEdge[i];
        }
        
        public int getNodeSize(){
            return this.allNode.Count;
        }

        public int getEdgeSize()
        {
            return this.allEdge.Count;
        }

        public Edge getEdge(int from,int to)
        {
            Boolean found = false;
            int i = 0;
            while (i < allEdge.Count && !found)
            {
                if(allEdge[i].getFrom()==from&& allEdge[i].getTo() == to|| allEdge[i].getFrom() == to && allEdge[i].getTo() == from)
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            if (found){
                return allEdge[i];
            }
            else
            {
                return new Edge(-1, -1);
            }
        }
    }
}