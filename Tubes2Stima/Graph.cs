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
            /*
            //Update posisi semua node sesuai algo force directed
            Boolean allStatic = true;
            foreach(var n in allNode)
            {
                double vX = 0;
                double vY = 0;
                for (int i = 0; i < n.neighborSize(); i++)
                {
                    //Hitung gaya hooke (gaya tarik)
                    double kHooke = 40;
                    vX += -1 * kHooke * (n.getX() - n.getNeighbor(i).getX()) / 100;
                    vY += -1 * kHooke * (n.getY() - n.getNeighbor(i).getY()) / 100;
                }
                foreach (var nCoul in allNode)
                {
                    if(n.getID() != nCoul.getID())
                    {
                        //Hitung gaya coulomb (gaya repulsif)
                        double kCoulomb = 200;///((n.getWeight()+1) * (nCoul.getWeight()+1));
                        double fCoulomb = kCoulomb / (Math.Pow(n.getX()/100 - nCoul.getX()/100, 2) + Math.Pow(n.getY()/100 - nCoul.getY()/100, 2));
                        vX += fCoulomb * (n.getX() - nCoul.getX()) / 100;
                        vY += fCoulomb * (n.getY() - nCoul.getY()) / 100;
                    }
                }
                double epsilon = 10;
                if (Math.Abs(vX) <= epsilon && Math.Abs(vY) <= epsilon)
                {
                    n.setIsStatic(true);
                }
                else
                {
                    allStatic = false;
                    n.setIsStatic(false);
                    n.setvX(vX);
                    n.setvY(vY);
                }
            }
            foreach(var n in allNode)
            {
                double dt = 1.0/15;
                n.updatePos(dt);
            }
            this.normalizeKoordinat(width, height);
            return !allStatic;
            */
            return true;
        }

        public void ForceDirected(int width, int height, bool langsung)
        {
            
            //Force Directed Graph Drawing algorithm
            //kalo langsung=false, hanya menyiapkan nodenya saja
            //harus panggil updatePosFD tiap tick kalo langsung=false
            int xAwal = 0;
            int yAwal = 0;
            int dx = width / ((int)Math.Sqrt(allNode.Count));
            int dy = dx;
            foreach (var n in allNode)
            {
                n.pos = new Koordinat2D(xAwal, yAwal);
                xAwal += dx;
                if (xAwal > width)
                {
                    xAwal = 0;
                    yAwal += dy;
                }
            }
            /*
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
                    
                    iterasi -= 1;
                    temp = this.updatePosFD(width, height);
                }
                this.normalizeKoordinat(width, height);
            }
            */
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
        
        public int getSize(){
            return allNode.Count;
        }
    }
}