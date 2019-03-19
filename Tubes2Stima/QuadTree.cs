using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2Stima
{
    

    public class Quad
    {
        public Koordinat2D position;
        public Koordinat2D size;
        public Koordinat2D gravityCenter;
        public double mass;

        public Quad(Koordinat2D position, double size, Koordinat2D gravCenter, double mass)
        {
            this.position = position;
            this.size = new Koordinat2D(size, size);
            this.gravityCenter = gravCenter;
            this.mass = mass;
        }


    }

    public class Body
    {
        public double mass;
        public Koordinat2D position;
        private Koordinat2D velocity;
        private Koordinat2D acceleration;
        private Node isi;
        private double G = 0.04f;

        public Body(Node _n)
        {
            mass = 2.0f;
            velocity = new Koordinat2D(0, 0);
            acceleration = new Koordinat2D(0, 0);
            position = _n.pos;
            isi = _n;
        }

        public void update()
        {
            double dt = 1 / 60.0;
            velocity += acceleration * dt;
            position += velocity * dt;
            acceleration.setZero();
            isi.pos = position;
        }

        public void interact(Body b)
        {
            this.applyForce(b.attract(this));
        }

        public void applyForce(Koordinat2D force)
        {
            acceleration += new Koordinat2D(force.x / mass, force.y / mass);
        }

        public double pembatas(double min, double max, double val, double delta)
        {
            if(val < min-delta)
            {
                return min;
            }
            else if(val > max+delta)
            {
                return max;
            }
            else
            {
                return val;
            }
        }

        public Koordinat2D attract(Body b)
        {
            Koordinat2D forc = position - b.position;
            double distance = forc.magnitude();
            distance = pembatas(50.0,250.0, distance, 0);
            forc.normalize();
            //double strenght = -(G * mass * mass) / (distance * distance);
            double hooke = -G * (distance-50);
            double kCoulomb = 200;
            double coulomb = kCoulomb/(distance*distance);
            double strength = hooke + coulomb;
            return new Koordinat2D(forc.x * strength, forc.y * strength);
        }

        public void addBody(Body body)
        {
            double m = mass + body.mass;
            double x = (position.x * mass + body.position.x * body.mass) / m;
            double y = (position.y * mass + body.position.y * body.mass) / m;
            mass = m;
            position.x = x;
            position.y = y;
        }
    }

    public class QuadNode
    {

        private List<Body> bodys = new List<Body>();
        private Body averageBody = null;
        private Koordinat2D center;
        private double size;
        private int level;

        private int MAX_OBJECTS = 1;
        private int MAX_LEVELS = 15;

        private QuadNode[] childs;

        public QuadNode(int level, Koordinat2D center, double size)
        {
            this.center = center;
            this.size = size;
            this.level = level;
            childs = new QuadNode[4];
        }

        public void interact(Body body, float complexity)
        {
            if (averageBody == null)
                return;
            double distance = Koordinat2D.distance(body.position, averageBody.position);
            if (distance == 0)
                return;

            if (size / distance > complexity && childs[0] != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    childs[i].interact(body, complexity);
                }
            }
            else
            {
                body.interact(averageBody);
            }
        }

        public void addBody(Body body)
        {

            if (averageBody == null)
            {
                averageBody = body;
            }
            else
            {
                averageBody.addBody(body);
            }

            if (childs[0] != null)
            {
                //Cari seharusnya dia ditaruh di kuadran mana
                int index = getSplitIndex(body);
                if (index != -1)
                {
                    childs[index].addBody(body);

                    return;
                }
            }
            if (!bodys.Contains(body))
            {
                bodys.Add(body);
            }

            if (bodys.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (childs[0] == null)
                {
                    split();
                }
                foreach (Body bo in bodys)
                {
                    int index = getSplitIndex(bo);
                    if (index != -1)
                    {
                        childs[index].addBody(body);
                    }
                }
                bodys.Clear();
            }
        }

        public bool contains(Body body)
        {
            if (body.position.x >= center.x + (size / 2f))
                return false;
            if (body.position.x <= center.x - (size / 2f))
                return false;
            if (body.position.y >= center.y + (size / 2f))
                return false;
            if (body.position.y <= center.y - (size / 2f))
                return false;
            return true;
        }

        private int getSplitIndex(Body body)
        {
            for (int i = 0; i < 4; i++)
            {
                if (childs[i].contains(body))
                {
                    return i;
                }
            }
            return -1;
        }

        private void split()
        {
            double newSize = size / 2f;
            childs[0] = new QuadNode(level + 1, new Koordinat2D(center.x - newSize / 2f, center.y + newSize / 2f), newSize);
            childs[1] = new QuadNode(level + 1, new Koordinat2D(center.x + newSize / 2f, center.y + newSize / 2f), newSize);
            childs[2] = new QuadNode(level + 1, new Koordinat2D(center.x - newSize / 2f, center.y - newSize / 2f), newSize);
            childs[3] = new QuadNode(level + 1, new Koordinat2D(center.x + newSize / 2f, center.y - newSize / 2f), newSize);
        }

        public void getAllQuad(List<Quad> quads)
        {
            if (averageBody == null)
            {
                quads.Add(new Quad(center, size, new Koordinat2D(0,0), 0));
            }
            else
            {
                quads.Add(new Quad(center, size, averageBody.position, averageBody.mass));
            }
            if (childs[0] == null)
                return;
            for (int i = 0; i < 4; i++)
            {
                childs[i].getAllQuad(quads);
            }
        }
    }
}
