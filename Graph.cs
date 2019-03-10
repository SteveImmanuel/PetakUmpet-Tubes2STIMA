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

        public void addNeighbor(Node _neighbor){
            this.neighbor.Add(_neighbor);
        }

        public Node getNeighbor(int i){
            return this.neighbor[i];
        }
        
        public int neighborSize(){
            return this.neighbor.Count;
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

        public void printInfo(){
            Console.WriteLine("ID="+this.getID());
            Console.WriteLine("NeighborSize="+this.neighborSize());
            Console.WriteLine("Weight="+this.getWeight());
            Console.WriteLine("Visit="+this.getVisited());
        }

    }

    // public class Edge{
    //     private int from;
    //     private int to;
    //     public edge(int _from,int _to){
    //         this.from=_from;
    //         this.to=_to;
    //     }
    // }
    
    public class Graph{
        private List<Node> allNode;
        // private List<Edge> allEdge;

        public Graph(){
            this.allNode=new List<Node>();
            // this.allEdge=new List<Edge>();
        }

        public void addNode(Node _node){
            this.allNode.Add(_node);
        }

        // public void addEdge(Edge _edge){
        //     this.allEdge.Add(_edge);
        // }

        public void unvisitAll(){
            for(int i=0;i<this.allNode.Count;i++){
                this.allNode[i].setVisited(false);
            }
        }
        public void generateWeight(Node _node,int _weight){
            Console.WriteLine("ID="+_node.getID());
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

    }




}