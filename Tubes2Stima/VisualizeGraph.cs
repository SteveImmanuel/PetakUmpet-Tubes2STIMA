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
        private List<Node> path = new List<Node>();
        private List<Command> LC;
        private Dictionary<Tuple<int,int>,Microsoft.Msagl.Drawing.Edge> dictEdge;

        public F2(ref Graph _G, ref List<Command> _LC)
        {

            InitializeComponent();
            G = _G;
            LC = _LC;
            dictEdge = new Dictionary<Tuple<int, int>, Microsoft.Msagl.Drawing.Edge>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            foreach (Edge E in G.allEdge)
            {
                graph.AddEdge((E.getFrom() + 1).ToString(), (E.getTo() + 1).ToString());
            }
            foreach (var E in graph.Edges)
            {
                Tuple<int,int> key = new Tuple<int,int>(Int32.Parse(E.Source) - 1, Int32.Parse(E.Target) - 1);
                dictEdge.Add(key, E);
                E.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }
            viewer.ToolBarIsVisible = false;
            viewer.PanButtonPressed = true;
            viewer.Graph = graph;
            Microsoft.Msagl.Drawing.Node nodeRaja = viewer.Graph.FindNode("1");
            if (nodeRaja != null)
            {
                nodeRaja.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                nodeRaja.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Fuchsia;
            }
            graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings();
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            viewer.Graph = graph;
            viewer.Size = new Size(586, 500);
            viewer.Location = new Point(0, 0);
            this.Controls.Add(viewer);
            foreach(Command C in LC)
            {
                this.query_file_list.Items.Add(C.getApproach().ToString() + " " + C.getX().ToString() + " " + C.getY().ToString());
            }
        }

        private void add_query_input_Click(object sender, EventArgs e)
        {
            String[] tuple;
            string input = query_input_textbox.Text;
            tuple = input.Split(' ');
            if (tuple.Count() == 3)
            {
                int type, to, from;
                try
                {
                    type = int.Parse(tuple[0]);
                    to = int.Parse(tuple[1]);
                    from = int.Parse(tuple[2]);
                    if(type == 0 || type == 1)
                    {
                        query_input_list.Items.Add(input);
                    }
                    else
                    {
                        MessageBox.Show("Tipe awal command hanya 0 atau 1", "Error add query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }catch
                {
                    MessageBox.Show("Query harus 3 integer", "Error add query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Query tidak valid", "Error add query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void show_path_input_Click(object sender, EventArgs e)
        {
            if (query_input_list.SelectedItem != null)
            {
                string selectedQuery = (string)query_input_list.SelectedItem;
                String[] tuple = selectedQuery.Split(' ');
                int type = int.Parse(tuple[0]);
                int to = int.Parse(tuple[1])-1;
                int from = int.Parse(tuple[2])-1;
                //Ganti semua shape path hasil query sebelumnya jadi normal lagi
                for (int i = 0; i < path.Count; i++)
                {
                    Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[i].getID().ToString());
                    tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
                    tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Transparent;
                    if (i < path.Count - 1)
                    {
                        Tuple<int, int> key = new Tuple<int, int>(path[i].getID() - 1, path[i + 1].getID() - 1);
                        Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                        if (dictEdge.ContainsKey(key))
                        {
                            dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                        }
                        else
                        {
                            dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                        }
                    }
                }
                path.Clear();
                Algorithm al = new Algorithm();
                G.generateWeight(G.getNode(0), 0);
                G.unvisitAll();
                Boolean found = false;
                try
                {
                    found=al.SearchPath(type, G.getNode(to), G.getNode(from), path);
                }
                catch
                {
                    MessageBox.Show("Node pada command lebih besar dari node yang ada", "Error Invalid Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (found)
                {
                    for (int i = 0; i < path.Count; i++)
                    {
                        Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[i].getID().ToString());
                        tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                        tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Cyan;
                        if (i < path.Count - 1)
                        {
                            Tuple<int, int> key = new Tuple<int, int>(path[i].getID() - 1, path[i + 1].getID() - 1);
                            Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                            if (dictEdge.ContainsKey(key))
                            {
                                dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            }
                            else
                            {
                                dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tidak ada jalur ditemukan", "Error show path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Microsoft.Msagl.Drawing.Node nodeRaja = viewer.Graph.FindNode("1");
                if (nodeRaja != null)
                {
                    nodeRaja.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                    nodeRaja.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Fuchsia;
                }
                
                G.unvisitAll();
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Pilih dahulu query dari list", "Error show path",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void show_path_file_Click(object sender, EventArgs e)
        {
            if (query_file_list.SelectedItem != null)
            {
                string selectedQuery = (string)query_file_list.SelectedItem;
                String[] tuple = selectedQuery.Split(' ');
                int type = int.Parse(tuple[0]);
                int to = int.Parse(tuple[1]) - 1;
                int from = int.Parse(tuple[2]) - 1;
                //Ganti semua shape path hasil query sebelumnya jadi normal lagi
                for (int i = 0; i < path.Count; i++)
                {
                    Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[i].getID().ToString());
                    tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
                    tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Transparent;
                    if (i < path.Count - 1)
                    {
                        Tuple<int, int> key = new Tuple<int, int>(path[i].getID() - 1, path[i + 1].getID() - 1);
                        Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                        if (dictEdge.ContainsKey(key))
                        {
                            dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                        }
                        else
                        {
                            dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                        }
                    }
                }
                path.Clear();
                Algorithm al = new Algorithm();
                G.generateWeight(G.getNode(0), 0);
                G.unvisitAll();
                Boolean found = false;
                try
                {
                    al.SearchPath(type, G.getNode(to), G.getNode(from), path);
                }
                catch
                {
                    MessageBox.Show("Node pada command lebih besar dari node yang ada", "Error Invalid Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (found)
                {
                    for (int i = 0; i < path.Count; i++)
                    {
                        Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[i].getID().ToString());
                        tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                        tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Cyan;
                        if (i < path.Count - 1)
                        {
                            Tuple<int, int> key = new Tuple<int, int>(path[i].getID() - 1, path[i + 1].getID() - 1);
                            Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                            if (dictEdge.ContainsKey(key))
                            {
                                dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            }
                            else
                            {
                                dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tidak ada jalur ditemukan", "Error show path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Microsoft.Msagl.Drawing.Node nodeRaja = viewer.Graph.FindNode("1");
                if (nodeRaja != null)
                {
                    nodeRaja.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                    nodeRaja.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                }
                G.unvisitAll();
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Pilih dahulu query dari list", "Error show path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
