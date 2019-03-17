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
        private double posX;
        private double posY;
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

        public Node(int _ID, double _posX, double _posY)
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

        public double getX()
        {
            return this.posX;
        }

        public double getY()
        {
            return this.posY;
        }

        public void setX(double _posX)
        {
            this.posX = _posX;
        }

        public void setY(double _posY)
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
    
    public class Graph{
        private List<Node> allNode;

        public Graph(){
            this.allNode=new List<Node>();
        }

        public void addNode(Node _node){
            this.allNode.Add(_node);
        }

        public void addEdge(int awal, int akhir)
        {
            this.allNode[awal].addNeighbor(this.allNode[akhir]);
            this.allNode[akhir].addNeighbor(this.allNode[awal]);
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

        public Boolean updatePosFD(int width, int height)
        {
            //Update posisi semua node sesuai algo force directed
            Boolean allStatic = true;
            foreach(var n in allNode)
            {
                double vX = 0;
                double vY = 0;
                for (int i = 0; i < n.neighborSize(); i++)
                {
                    //Hitung gaya hooke (gaya tarik)
                    double kHooke = 2;
                    vX += -1 * kHooke * (n.getX() - n.getNeighbor(i).getX());
                    vY += -1 * kHooke * (n.getY() - n.getNeighbor(i).getY());
                }
                foreach (var nCoul in allNode)
                {
                    if(n.getID() != nCoul.getID())
                    {
                        //Hitung gaya coulomb (gaya repulsif)
                        double kCoulomb = 500;///((n.getWeight()+1) * (nCoul.getWeight()+1));
                        double fCoulomb = kCoulomb / (Math.Pow(n.getX() - nCoul.getX(), 2) + Math.Pow(n.getY() - nCoul.getY(), 2));
                        vX += fCoulomb * (n.getX() - nCoul.getX());
                        vY += fCoulomb * (n.getY() - nCoul.getY());
                    }
                }
                double epsilon = 0.005;
                if (Math.Abs(vX) <= epsilon && Math.Abs(vY) <= epsilon)
                {
                    n.setIsStatic(true);
                }
                else
                {
                    allStatic = false;
                    double dt = 60;
                    n.setIsStatic(false);
                    n.setX(n.getX() + vX / dt);
                    n.setY(n.getY() + vY / dt);
                }
            }
            this.normalizeKoordinat(width, height);
            return allStatic;
        }

        public void ForceDirected(int width, int height, bool langsung)
        {
            //Force Directed Graph Drawing algorithm
            //kalo langsung=false, hanya menyiapkan nodenya saja
            //harus panggil updatePosFD tiap tick kalo langsung=false
            int xAwal = 0;
            int yAwal = 0;
            int dx = width / ((int)Math.Sqrt(allNode.Capacity));
            int dy = dx;
            foreach (var n in allNode)
            {
                n.setX(xAwal);
                n.setY(yAwal);
                xAwal += dx;
                if (xAwal > width)
                {
                    yAwal += dy;
                }
            }
            //Console.WriteLine("Done set 0 semua");
            if (langsung)
            {
                int iterasi = 1000;
                Boolean temp = false;
                while (!temp && iterasi > 0)
                {
                    /*
                    Console.WriteLine("Lagi Simulasi");
                    foreach (var n in allNode)
                    {
                        Console.Write("ID= " + n.getID());
                        Console.Write(", X= " + n.getX());
                        Console.WriteLine(", Y= " + n.getY());
                    }
                    */
                    iterasi -= 1;
                    temp = this.updatePosFD(width, height);
                }
                this.normalizeKoordinat(width, height);
            }
        }

        public void normalizeKoordinat(int width, int height)
        {
            //Normalisasi Koordinat agar berada di rentang 0-width, 0-height
            //Cari maxX, maxY, minX, minY
            double maxX, maxY, minX, minY;
            maxX = getNode(0).getX();
            maxY = getNode(0).getY();
            minX = maxX;
            minY = maxY;
            foreach (var n in allNode)
            {
                if (n.getX() > maxX)
                {
                    maxX = n.getX();
                }
                else if (n.getX() < minX)
                {
                    minX = n.getX();
                }

                if (n.getY() > maxY)
                {
                    maxY = n.getY();
                }
                else if (n.getY() < minY)
                {
                    minY = n.getY();
                }
            }
            //Console.WriteLine("Normalize Koordinat");
            foreach (var n in allNode)
            {
                n.setX(width * (n.getX() - minX) / (maxX - minX));
                n.setY(height * (n.getY() - minY) / (maxY - minY));
                //Console.Write("ID= " + n.getID());
                //Console.Write(", X= " + n.getX());
                //Console.WriteLine(", Y= " + n.getY());
            }
        }
        public Node getNode(int i){
            return this.allNode[i];
        }
        
        public int getSize(){
            return allNode.Count;
        }
    }
}