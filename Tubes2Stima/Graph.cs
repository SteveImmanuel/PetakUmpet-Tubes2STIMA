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

    public class Node{
        private List<Node> neighbor;
        private int weight;
        private int ID;
        private Boolean visited;
        
        public Node(int _ID){
            this.ID=_ID;
            this.visited=false;
            this.neighbor=new List<Node>();
            this.weight=0;
        }

        public Node(int _ID, double _posX, double _posY)
        {
            this.ID = _ID;
            this.visited = false;
            this.neighbor = new List<Node>();
            this.weight = 0;
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

        public void setWeight(int _weight){
            this.weight=_weight;
        }

        public int getWeight(){
            return this.weight;
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