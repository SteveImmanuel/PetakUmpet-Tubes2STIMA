namespace Tubes2Stima
{
    partial class F2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.query_file = new System.Windows.Forms.TabPage();
            this.show_path_file = new System.Windows.Forms.Button();
            this.query_file_list = new System.Windows.Forms.ListBox();
            this.query_input = new System.Windows.Forms.TabPage();
            this.query_input_textbox = new System.Windows.Forms.TextBox();
            this.add_query_input = new System.Windows.Forms.Button();
            this.show_path_input = new System.Windows.Forms.Button();
            this.query_input_list = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.query_file.SuspendLayout();
            this.query_input.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.query_file);
            this.tabControl1.Controls.Add(this.query_input);
            this.tabControl1.Location = new System.Drawing.Point(500, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(485, 455);
            this.tabControl1.TabIndex = 1;
            // 
            // query_file
            // 
            this.query_file.Controls.Add(this.show_path_file);
            this.query_file.Controls.Add(this.query_file_list);
            this.query_file.Location = new System.Drawing.Point(4, 25);
            this.query_file.Name = "query_file";
            this.query_file.Padding = new System.Windows.Forms.Padding(3);
            this.query_file.Size = new System.Drawing.Size(477, 426);
            this.query_file.TabIndex = 1;
            this.query_file.Text = "Query (File)";
            this.query_file.UseVisualStyleBackColor = true;
            // 
            // show_path_file
            // 
            this.show_path_file.Location = new System.Drawing.Point(6, 6);
            this.show_path_file.Name = "show_path_file";
            this.show_path_file.Size = new System.Drawing.Size(450, 46);
            this.show_path_file.TabIndex = 1;
            this.show_path_file.Text = "Show Path";
            this.show_path_file.UseVisualStyleBackColor = true;
            this.show_path_file.Click += new System.EventHandler(this.show_path_file_Click);
            // 
            // query_file_list
            // 
            this.query_file_list.FormattingEnabled = true;
            this.query_file_list.ItemHeight = 16;
            this.query_file_list.Location = new System.Drawing.Point(6, 58);
            this.query_file_list.Name = "query_file_list";
            this.query_file_list.Size = new System.Drawing.Size(450, 340);
            this.query_file_list.TabIndex = 0;
            // 
            // query_input
            // 
            this.query_input.Controls.Add(this.query_input_textbox);
            this.query_input.Controls.Add(this.add_query_input);
            this.query_input.Controls.Add(this.show_path_input);
            this.query_input.Controls.Add(this.query_input_list);
            this.query_input.Location = new System.Drawing.Point(4, 25);
            this.query_input.Name = "query_input";
            this.query_input.Padding = new System.Windows.Forms.Padding(3);
            this.query_input.Size = new System.Drawing.Size(477, 426);
            this.query_input.TabIndex = 2;
            this.query_input.Text = "Query (Input)";
            this.query_input.UseVisualStyleBackColor = true;
            // 
            // query_input_textbox
            // 
            this.query_input_textbox.BackColor = System.Drawing.SystemColors.Window;
            this.query_input_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.query_input_textbox.Location = new System.Drawing.Point(6, 6);
            this.query_input_textbox.Name = "query_input_textbox";
            this.query_input_textbox.Size = new System.Drawing.Size(450, 34);
            this.query_input_textbox.TabIndex = 3;
            // 
            // add_query_input
            // 
            this.add_query_input.Location = new System.Drawing.Point(236, 71);
            this.add_query_input.Name = "add_query_input";
            this.add_query_input.Size = new System.Drawing.Size(220, 36);
            this.add_query_input.TabIndex = 2;
            this.add_query_input.Text = "Add Query";
            this.add_query_input.UseVisualStyleBackColor = true;
            this.add_query_input.Click += new System.EventHandler(this.add_query_input_Click);
            // 
            // show_path_input
            // 
            this.show_path_input.Location = new System.Drawing.Point(7, 71);
            this.show_path_input.Name = "show_path_input";
            this.show_path_input.Size = new System.Drawing.Size(223, 36);
            this.show_path_input.TabIndex = 1;
            this.show_path_input.Text = "Show Path";
            this.show_path_input.UseVisualStyleBackColor = true;
            this.show_path_input.Click += new System.EventHandler(this.show_path_input_Click);
            // 
            // query_input_list
            // 
            this.query_input_list.FormattingEnabled = true;
            this.query_input_list.ItemHeight = 16;
            this.query_input_list.Location = new System.Drawing.Point(6, 114);
            this.query_input_list.Name = "query_input_list";
            this.query_input_list.Size = new System.Drawing.Size(450, 276);
            this.query_input_list.TabIndex = 0;
            // 
            // F2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(982, 453);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "F2";
            this.ShowIcon = false;
            this.Text = "Visual";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.query_file.ResumeLayout(false);
            this.query_input.ResumeLayout(false);
            this.query_input.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage query_file;
        private System.Windows.Forms.TabPage query_input;
        private System.Windows.Forms.Button show_path_file;
        private System.Windows.Forms.ListBox query_file_list;
        private System.Windows.Forms.TextBox query_input_textbox;
        private System.Windows.Forms.Button add_query_input;
        private System.Windows.Forms.Button show_path_input;
        private System.Windows.Forms.ListBox query_input_list;
    }
}