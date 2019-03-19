using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2Stima
{
    public class BarnesHut
    {
        private BarnesHut[] kuadran;
        private double width, height;
        private Koordinat2D center;
        private double avgMass = 0;
        private Koordinat2D avgPos;
        private int numOfParticle = 0;
        private List<Node> isi = new List<Node>();
        private int level;
        public BarnesHut(int _l, Koordinat2D _c, double _w, double _h)
        {
            level = _l;
            width = _w;
            height = _h;
            center = new Koordinat2D(_c.x, _c.y);
            avgPos = center.copy();
            kuadran = new BarnesHut[4];
        }

        public int getKuadran(Koordinat2D posNode)
        {
            if (posNode.x >= center.x)
            {
                if (posNode.y >= center.y)
                {
                    //Kuadran 1
                    return 0;
                }
                else
                {
                    //Kuadran 4
                    return 3;
                }
            }
            else
            {
                if (posNode.y >= center.y)
                {
                    //Kuadran 2
                    return 1;
                }
                else
                {
                    //Kuadran 3
                    return 2;
                }
            }
        }

        public void allocateKuadran(int noKuadran)
        { 
            switch(noKuadran)
            {
                case 0:
                    if (kuadran[0] == null)
                    {
                        kuadran[0] = new BarnesHut(level+1,new Koordinat2D(center.x + width / 2.0, center.y + height / 2.0), width / 2.0, height / 2.0);
                    }
                        break;
                case 1:
                    if (kuadran[1] == null)
                    {
                        kuadran[1] = new BarnesHut(level + 1,new Koordinat2D(center.x - width / 2.0, center.y + height / 2.0), width / 2.0, height / 2.0);
                    }
                    break;
                case 2:
                    if (kuadran[2] == null)
                    {
                        kuadran[2] = new BarnesHut(level + 1, new Koordinat2D(center.x - width / 2.0, center.y - height / 2.0), width / 2.0, height / 2.0);
                    }
                    break;
                case 3:
                    if (kuadran[3] == null)
                    {
                        kuadran[3] = new BarnesHut(level + 1, new Koordinat2D(center.x + width / 2.0, center.y - height / 2.0), width / 2.0, height / 2.0);
                    }
                    break;
                default:
                    break;
            }
            
            
            
           
        }

        public void addNodeSimulation(Node _n)
        {
            //Max Level = 10
            if (level > 10)
            {
                isi.Add(_n);
                double m = avgMass + 2.0;
                avgPos.x = (avgPos.x * avgMass + _n.pos.x * 2.0) / m;
                avgPos.y = (avgPos.y * avgMass + _n.pos.y * 2.0) / m;
                avgMass = m;
            }
            else
            {
                if (numOfParticle > 1)
                {
                    int noKuadran = this.getKuadran(_n.pos);
                    this.allocateKuadran(noKuadran);
                    kuadran[noKuadran].addNodeSimulation(_n);
                    double m = avgMass + 2.0;
                    avgPos.x = (avgPos.x * avgMass + _n.pos.x * 2.0) / m;
                    avgPos.y = (avgPos.y * avgMass + _n.pos.y * 2.0) / m;
                    avgMass = m;
                }
                else if (numOfParticle == 1)
                {
                    int noKuadranIsi = this.getKuadran(isi[0].pos);
                    this.allocateKuadran(noKuadranIsi);
                    kuadran[noKuadranIsi].addNodeSimulation(isi[0]);
                    isi.Clear();
                    int noKuadran = this.getKuadran(_n.pos);
                    this.allocateKuadran(noKuadran);
                    kuadran[noKuadran].addNodeSimulation(_n);
                    double m = avgMass + 2.0;
                    avgPos.x = (avgPos.x * avgMass + _n.pos.x * 2.0) / m;
                    avgPos.y = (avgPos.y * avgMass + _n.pos.y * 2.0) / m;
                    avgMass = m;
                }
                else if (numOfParticle == 0 && isi.Count() == 0)
                {
                    isi.Add(_n);
                    avgMass = 2;
                    avgPos = new Koordinat2D(_n.pos.x, _n.pos.y);
                }
            }
            numOfParticle += 1;
        }

        //Get force antara quadtree ini dengan node _n
        //force akan digunakan untuk update node _n
        public Koordinat2D getForce(Node _n, double tetha)
        {
            Koordinat2D force = new Koordinat2D(0, 0);
            double fScalar = 0;
            if (numOfParticle == 1)
            {
                if (isi[0] != _n)
                {
                    force = _n.pos - isi[0].pos;
                    double distance = force.magnitude();
                    force.normalize();
                    if (_n.isNeighbor(isi[0]))
                    {
                        //Hukum hooke
                        double kHooke = 20;
                        fScalar += -kHooke * (distance-400) / 100.0;
                    }
                    //Hukum coulomb
                    /*
                    double kCoulomb = 500;
                    fScalar += kCoulomb * 2.0 * 2.0 / (distance / 100.0 * distance / 100.0);
                    */
                    double kHooke2 = 25;
                    fScalar += kHooke2 * (distance-400) / 100.0;
                    return force * fScalar;
                }
                else
                {
                    return force;
                }
            }
            else
            {
                //Thresold untuk menentukan apakah menggunakkan avgMass & pos
                //Atau menggunakkan tiap kuadran untuk menghitung force total
                double d = new Koordinat2D(width, height).magnitude();
                force = avgPos - _n.pos;
                double distance = force.magnitude();
                force.normalize();
                if(d/distance < tetha)
                {
                    /*
                    double kCoulomb = 10;
                    fScalar += kCoulomb * avgMass * 2.0 / (distance / 100.0 * distance / 100.0);
                    */
                    double kHooke2 = 25;
                    fScalar += kHooke2 * (distance - 400) / 100.0;
                    return force * fScalar;
                }
                else
                {
                    force.setZero();
                    foreach(BarnesHut B in kuadran)
                    {
                        if (B != null)
                        {
                            force = force + B.getForce(_n, tetha);
                        }
                    }
                    return force;
                }
            }
        }
    }
}
