using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2Stima
{
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
            x = x / length;
            y = y / length;
        }

        public static double distance(Koordinat2D K1, Koordinat2D K2)
        {
            return Math.Sqrt(Math.Pow(K1.x - K2.x, 2) + Math.Pow(K1.y - K2.y, 2));
        }

        public static Koordinat2D operator+ (Koordinat2D K1, Koordinat2D K2)
        {
            Koordinat2D temp = new Koordinat2D(0,0);
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
    }

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
            position = new Koordinat2D(_n.getX(), _n.getY());
            isi = _n;
        }

        public void update()
        {
            velocity += acceleration;
            position += velocity;
            acceleration.setZero();
            isi.setX(position.x);
            isi.setY(position.y);
        }

        public void interact(Body b)
        {
            this.applyForce(b.attract(this));
        }

        public void applyForce(Koordinat2D force)
        {
            acceleration += new Koordinat2D(force.x / mass, force.y / mass);
        }

        public double normKoor(double min, double max, double val, double delta)
        {
            return (min + delta) + (max - min - 2 * delta) * (val - min) / (max - min);
        }

        public Koordinat2D attract(Body b)
        {
            Koordinat2D forc = position - b.position;
            double distance = forc.magnitude();
            distance = normKoor(-50.0, 50.0, distance, 0);
            forc.normalize();
            double strenght = (G * mass * mass) / (distance * distance);
            return new Koordinat2D(forc.x * strenght, forc.y * strenght);
        }

        public void addBody(Body body)
        {
            double m = mass + body.mass;
            double x = (position.x * mass + body.position.x * body.mass) / m;
            double y = (position.y * mass + body.position.y * body.mass) / m;
            mass = m;
            position = new Koordinat2D(x, y);
        }

        private Koordinat2D CopyVector(Koordinat2D vec)
        {
            return new Koordinat2D(vec.x, vec.y);
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
