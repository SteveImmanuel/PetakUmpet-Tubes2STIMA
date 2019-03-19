using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tubes2Stima
{
    public partial class F2 : Form
    {
        private Graph G;
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer;

        public F2(ref Graph _G)
        {

            InitializeComponent();
            G = _G;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            foreach (Edge E in G.allEdge)
            {
                graph.AddEdge((E.getFrom()+1).ToString(), (E.getTo()+1).ToString());
            }
            foreach(var E in graph.Edges)
            {
                E.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }
            /*
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            */
            graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings();
            viewer.ToolBarIsVisible = false;
            viewer.PanButtonPressed = true;
            viewer.Graph = graph;
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Right;
            this.Controls.Add(viewer);
            this.ResumeLayout();


            //test button
            Button s = new Button();
            s.Text = "solve";
            s.Font = new Font("Futura LT", 10);
            s.BackColor = Color.FromArgb(0, 255, 0);
            s.Size = new Size(100, 25);
            s.Location = new Point(0, 0);
            s.Click += new System.EventHandler(this.testSolve);
            this.Controls.Add(s);

        }

        private void testSolve(object source, EventArgs e)
        {
            Algorithm al = new Algorithm();
            List<Node> path = new List<Node>();
            G.generateWeight(G.getNode(0), 0);
            G.unvisitAll();
            Boolean found = al.SearchPath(0, G.getNode(0), G.getNode(6), path);
            if (found)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[i].getID().ToString());
                    tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                    tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Cyan;


                    //yang ini bagian super stupid karena gw ga ketemu getedge
                    if (i < path.Count - 1)
                    {
                        foreach (var E in viewer.Graph.Edges)
                        {
                            if (E.Source==path[i].getID().ToString()&& E.Target == path[i+1].getID().ToString()|| E.Target == path[i].getID().ToString() && E.Source == path[i + 1].getID().ToString())
                            {
                                E.Attr.Color= Microsoft.Msagl.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            G.unvisitAll();
            this.Refresh();
        }
    }
}
