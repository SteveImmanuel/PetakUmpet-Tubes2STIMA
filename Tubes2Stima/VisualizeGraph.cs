using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tubes2Stima
{
    public partial class F2 : Form
    {
        private Graph G;
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        private List<Node> path = new List<Node>();
        private List<Node> lastPath = new List<Node>();
        private List<Command> LC;
        private Dictionary<Tuple<int,int>,Microsoft.Msagl.Drawing.Edge> dictEdge;
        private System.Windows.Forms.Timer timerAnim;
        private bool isAnim = false;

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
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings();
            foreach (Edge E in G.allEdge)
            {
                graph.AddEdge((E.getFrom() + 1).ToString(), (E.getTo() + 1).ToString());
            }
            viewer.Graph = graph;
            foreach (var E in viewer.Graph.Edges)
            {
                Tuple<int,int> key = new Tuple<int,int>(Int32.Parse(E.Source) - 1, Int32.Parse(E.Target) - 1);
                dictEdge.Add(key, E);
                E.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }
            viewer.ToolBarIsVisible = false;
            viewer.PanButtonPressed = true;
            Microsoft.Msagl.Drawing.Node nodeRaja = viewer.Graph.FindNode("1");
            if (nodeRaja != null)
            {
                nodeRaja.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                nodeRaja.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Fuchsia;
            }
            viewer.Size = new Size(780, 500);
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

        private void reset_color_path()
        {
            //Ganti semua shape path hasil query sebelumnya jadi normal lagi
            for (int i = 0; i < lastPath.Count; i++)
            {
                Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(lastPath[i].getID().ToString());
                tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
                tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Transparent;
                if (i < lastPath.Count - 1)
                {
                    Tuple<int, int> key = new Tuple<int, int>(lastPath[i].getID() - 1, lastPath[i + 1].getID() - 1);
                    Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                    if (dictEdge.ContainsKey(key))
                    {
                        dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                    else
                    {
                        if (dictEdge.ContainsKey(keyInv))
                        {
                            dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                        }
                    }
                }
            }
            lastPath.Clear();
            this.Refresh();
        }

        private void show_path_animate(object sender, EventArgs e)
        {
            if (isAnim && path.Count > 0)
            {
                Microsoft.Msagl.Drawing.Node tempNode = viewer.Graph.FindNode(path[0].getID().ToString());
                tempNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                tempNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Cyan;
                if (path.Count > 1)
                {
                    Tuple<int, int> key = new Tuple<int, int>(path[0].getID() - 1, path[1].getID() - 1);
                    Tuple<int, int> keyInv = new Tuple<int, int>(key.Item2, key.Item1);
                    if (dictEdge.ContainsKey(key))
                    {
                        dictEdge[key].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                    else
                    {
                        if (dictEdge.ContainsKey(keyInv))
                        {
                            dictEdge[keyInv].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                        }
                    }
                    path.RemoveAt(0);
                }
                else
                {
                    path.Clear();
                    isAnim = false;
                }
            }
            this.Refresh();
        }

        private void show_path_input_Click(object sender, EventArgs e)
        {
            if (isAnim)
            {
                MessageBox.Show("Sedang menganimasikan path", "Error Show Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (query_input_list.SelectedItem != null)
                {
                    isAnim = false;
                    string selectedQuery = (string)query_input_list.SelectedItem;
                    String[] tuple = selectedQuery.Split(' ');
                    int type = int.Parse(tuple[0]);
                    int to = int.Parse(tuple[1]) - 1;
                    int from = int.Parse(tuple[2]) - 1;
                    reset_color_path();
                    Algorithm al = new Algorithm();
                    G.generateWeight(G.getNode(0), 0);
                    G.unvisitAll();
                    Boolean found = false;
                    try
                    {
                        int stackSize = 1024 * 1024 * 15;
                        Thread t = new Thread(() =>
                        {
                            found = al.SearchPath(type, G.getNode(to), G.getNode(from), path);
                            lastPath.Clear();
                            foreach (Node N in path)
                            {
                                lastPath.Add(N);
                            }
                            path.Reverse();
                        }, stackSize);
                        t.Start();
                        t.Join();
                    }
                    catch
                    {
                        MessageBox.Show("Node pada command lebih besar dari node yang ada", "Error Invalid Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (found)
                    {
                        isAnim = true;
                        timerAnim = new System.Windows.Forms.Timer();
                        timerAnim.Tick += new EventHandler(show_path_animate);
                        timerAnim.Interval = 6000 / path.Count; // in miliseconds
                        timerAnim.Start();
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
                    MessageBox.Show("Pilih dahulu query dari list", "Error show path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void show_path_file_Click(object sender, EventArgs e)
        {
            if (isAnim)
            {
                MessageBox.Show("Sedang menganimasikan path", "Error Show Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (query_file_list.SelectedItem != null)
                {
                    isAnim = false;
                    string selectedQuery = (string)query_file_list.SelectedItem;
                    String[] tuple = selectedQuery.Split(' ');
                    int type = int.Parse(tuple[0]);
                    int to = int.Parse(tuple[1]) - 1;
                    int from = int.Parse(tuple[2]) - 1;
                    //Ganti semua shape path hasil query sebelumnya jadi normal lagi
                    reset_color_path();
                    Algorithm al = new Algorithm();
                    G.generateWeight(G.getNode(0), 0);
                    G.unvisitAll();
                    Boolean found = false;
                    try
                    {
                        int stackSize = 1024 * 1024 * 15;
                        Thread t = new Thread(() =>
                        {
                            found = al.SearchPath(type, G.getNode(to), G.getNode(from), path);
                            lastPath.Clear();
                            foreach (Node N in path)
                            {
                                lastPath.Add(N);
                            }
                            path.Reverse();
                        }, stackSize);
                        t.Start();
                        t.Join();
                    }
                    catch
                    {
                        MessageBox.Show("Node pada command lebih besar dari node yang ada", "Error Invalid Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (found)
                    {
                        isAnim = true;
                        timerAnim = new System.Windows.Forms.Timer();
                        timerAnim.Tick += new EventHandler(show_path_animate);
                        timerAnim.Interval = 6000 / path.Count; // in miliseconds
                        timerAnim.Start();
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
}
