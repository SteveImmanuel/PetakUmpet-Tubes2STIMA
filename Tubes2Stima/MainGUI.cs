/*Tugas Besar II [IF2211] Strategi Algoritma
Anggota Kelompok :
    Aditya Putra Santosa    / 13517013
    Steve Andreas Immanuel  / 13517039
    Leonardo                / 13517048
Nama File   : MainGUI.cs
Deskripsi   : Memanggil GUI utama (WiP)
*/
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Tubes2Stima
{
    public class F1 : Form
    {
        private Label title, map_label, command_label, from_map_title, from_command_title;
        private TextBox map_text, command_text, from_map_file, from_command_file;
        private Button button1, button2, load, reset, solvebtn, closebtn;
        private int Vertexes;
        private Graph G = null;

        public F1()
        {
            MainGUI();
        }

        private void MainGUI()
        {
            //initialize GUI
            this.Text = "Choose File for map and commands";
            this.Size = new Size(500, 550);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(100, 100, 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            //GUI CONTENTS
            //title = GUI title (Label)
            title = new Label();
            title.Text = "Hide and Seek!";
            title.BackColor = Color.FromArgb(230, 230, 230);
            title.AutoSize = false;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new Font("Futura LT", 35, FontStyle.Bold | FontStyle.Italic);
            title.BorderStyle = BorderStyle.FixedSingle;
            title.Size = new Size(500, 75);
            title.Location = new Point(0, 0);
            this.Controls.Add(title);
            //map_label = map below
            map_label = new Label();
            map_label.Text = "Insert .txt File for Map!";
            map_label.Font = new Font("Futura LT", 10);
            map_label.Size = new Size(200, 25);
            map_label.Location = new Point(50, 90);
            this.Controls.Add(map_label);
            //map_text = text for map
            map_text = new TextBox();
            map_text.Text = "";
            map_text.ReadOnly = true;
            map_text.Font = new Font("Futura LT", 8);
            map_text.Size = new Size(200, 25);
            map_text.Location = new Point(50, 115);
            this.Controls.Add(map_text);
            //button1 : take map file
            button1 = new Button();
            button1.Text = "Choose File";
            button1.Font = new Font("Futura LT", 10);
            button1.BackColor = Color.FromArgb(230, 230, 230);
            button1.Size = new Size(100, 25);
            button1.Location = new Point(255, 114);
            button1.Click += new System.EventHandler(this.AddMap);
            this.Controls.Add(button1);
            //command_label = command below
            command_label = new Label();
            command_label.Text = "Insert .txt File for Commands!";
            command_label.Font = new Font("Futura LT", 10);
            command_label.Size = new Size(200, 25);
            command_label.Location = new Point(50, 150);
            this.Controls.Add(command_label);
            //command_text = text for commands
            command_text = new TextBox();
            command_text.Text = "";
            command_text.ReadOnly = true;
            command_text.Font = new Font("Futura LT", 8);
            command_text.Size = new Size(200, 25);
            command_text.Location = new Point(50, 175);
            this.Controls.Add(command_text);
            //button2 = take command file
            button2 = new Button();
            button2.Text = "Choose File";
            button2.Font = new Font("Futura LT", 10);
            button2.BackColor = Color.FromArgb(230, 230, 230);
            button2.Size = new Size(100, 25);
            button2.Location = new Point(255, 174);
            button2.Click += new System.EventHandler(this.AddCommand);
            this.Controls.Add(button2);
            //load = button to load file name and print file contents to GUI
            load = new Button();
            load.Text = "Load";
            load.Font = new Font("Futura LT", 10);
            load.BackColor = Color.FromArgb(0, 255, 0);
            load.Size = new Size(100, 25);
            load.Location = new Point(355, 114);
            load.Click += new System.EventHandler(this.LoadBox);
            this.Controls.Add(load);
            //reset = button to reset map_text, command_text, from_map_file, and from_command_file
            reset = new Button();
            reset.Text = "Reset";
            reset.Font = new Font("Futura LT", 10);
            reset.BackColor = Color.FromArgb(255, 0, 0);
            reset.Size = new Size(100, 25);
            reset.Location = new Point(355, 174);
            reset.Click += new System.EventHandler(this.ResetBox);
            this.Controls.Add(reset);
            //from_map_title = label above map file contents
            from_map_title = new Label();
            from_map_title.Text = "Map File Contents :";
            from_map_title.Font = new Font("Futura LT", 8);
            from_map_title.Size = new Size(200, 25);
            from_map_title.Location = new Point(5, 225);
            this.Controls.Add(from_map_title);
            //from_map_file = label to show map file contents
            from_map_file = new TextBox();
            from_map_file.Text = "";
            from_map_file.Font = new Font("Futura LT", 8);
            from_map_file.ReadOnly = true;
            from_map_file.Multiline = true;
            from_map_file.ScrollBars = ScrollBars.Vertical;
            from_map_file.Size = new Size(235, 200);
            from_map_file.Location = new Point(0, 250);
            this.Controls.Add(from_map_file);
            //from_command_title = label above command file contents
            from_command_title = new Label();
            from_command_title.Text = "Command File Contents :";
            from_command_title.Font = new Font("Futura LT", 8);
            from_command_title.Size = new Size(200, 25);
            from_command_title.Location = new Point(255, 225);
            this.Controls.Add(from_command_title);
            //from_command_file = label to show command file contents
            from_command_file = new TextBox();
            from_command_file.Text = "";
            from_command_file.Font = new Font("Futura LT", 8);
            from_command_file.ReadOnly = true;
            from_command_file.Multiline = true;
            from_command_file.ScrollBars = ScrollBars.Vertical;
            from_command_file.Size = new Size(235, 200);
            from_command_file.Location = new Point(250, 250);
            this.Controls.Add(from_command_file);
            //solvebtn = button connecting this GUI to graph making GUI
            solvebtn = new Button();
            solvebtn.Text = "Solve";
            solvebtn.Font = new Font("Futura LT", 10);
            solvebtn.BackColor = Color.FromArgb(255, 105, 180);
            solvebtn.Size = new Size(100, 25);
            solvebtn.Location = new Point(15, this.Height - 75);
            solvebtn.Click += new System.EventHandler(this.Solve_Click);
            this.Controls.Add(solvebtn);
            //closebtn = close gui
            closebtn = new Button();
            closebtn.Text = "Close";
            closebtn.Font = new Font("Futura LT", 10);
            closebtn.BackColor = Color.FromArgb(230, 230, 230);
            closebtn.Size = new Size(100, 25);
            closebtn.Location = new Point(this.Width - 130, this.Height - 75);
            closebtn.Click += new System.EventHandler(this.Close_Click);
            this.Controls.Add(closebtn);
        }

        private void AddMap(object source, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../../../../InputFiles"));
                ofd.Filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.map_text.Text = ofd.FileName;
                }
            }
        }

        private void AddCommand(object source, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../../../../InputFiles"));
                ofd.Filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.command_text.Text = ofd.FileName;
                }
            }
        }

        private void LoadBox(object source, EventArgs e)
        {
            //show map contents          
            Vertexes = new int();
            G = new Graph();
            this.from_map_file.Text = ""; //assume correct input everytime
            string strText = "";
            ReadFile.ReadMap(this.map_text.Text, ref Vertexes, ref G, ref strText);
            this.from_map_file.Text += Vertexes;
            this.from_map_file.Text += strText;
            //show command contents
            int comnum = new int();
            List<Command> LC = new List<Command>();
            this.from_command_file.Text = ""; //assume correct input everytime
            ReadFile.ReadCommand(this.command_text.Text, ref comnum, ref LC);
            this.from_command_file.Text += comnum;
            for (int i = 0; i < comnum; i++)
            {
                this.from_command_file.Text += "\r\n" + LC[i].getApproach();
                this.from_command_file.Text += " " + LC[i].getX();
                this.from_command_file.Text += " " + LC[i].getY();
            }
        }

        private void ResetBox(object source, EventArgs e)
        {
            this.map_text.Text = "";
            this.command_text.Text = "";
            this.from_map_file.Text = "";
            this.from_command_file.Text = "";
            G.unvisitAll();
            G.unColorAll();
            G.allNode.Clear();
            G.allEdge.Clear();
        }

        private void Solve_Click(object source, EventArgs e)
        {
            //calling graph-drawing GUI
            //Run Visualization
            Form form = new Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            foreach(Edge E in G.allEdge)
            {
                graph.AddEdge(E.getFrom().ToString(), E.getTo().ToString());
            }
            foreach(var edge in graph.Edges)
            {
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.Width = 1366;
            form.Height = 768;
            form.ResumeLayout();

            form.ShowDialog();
        }

        private static void CallVisual(ref Graph G1)
        {
            //using (var visualGraph = new VisualizeGraph(ref G1, 500, 500))
            //{
            //    visualGraph.Run();
            //}
        }

        private void Close_Click(object source, EventArgs e)
        {
            Close();
        }

        [STAThread]
        public static void Main(String[] args)
        {
                Application.Run(new F1());
        }

    }

    public class Command
    {
        private int approach, X, Y;
        /* approach :
             0. Mendekati Vertex 1 (istana raja)
             1. Menjauhi Vertex 1 (istana raja)
           X : Tempat Jose Mengumpat
           Y : Tempat Ferdiant Mulai
        */

        //ctor
        public Command(int _approach, int _X, int _Y)
        {
            this.approach = _approach;
            this.X = _X;
            this.Y = _Y;
        }
        //getter
        public int getApproach()
        {
            return this.approach;
        }

        public int getX()
        {
            return this.X;
        }

        public int getY()
        {
            return this.Y;
        }
        //setter
        public void setApproach(int _approach)
        {
            this.approach = _approach;
        }

        public void setX(int _X)
        {
            this.X = _X;
        }

        public void setY(int _Y)
        {
            this.Y = _Y;
        }
    }
}