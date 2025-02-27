namespace lib
{
    partial class СardIndex
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СardIndex));
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            dataGridView1 = new DataGridView();
            libDbContextBindingSource = new BindingSource(components);
            libDbContextBindingSource1 = new BindingSource(components);
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)libDbContextBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)libDbContextBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            toolStrip1.Location = new Point(0, 419);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(919, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(55, 24);
            toolStripButton1.Text = "Назад";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(904, 327);
            dataGridView1.TabIndex = 1;
            // 
            // libDbContextBindingSource
            // 
            libDbContextBindingSource.DataSource = typeof(Data.LibDbContext);
            // 
            // libDbContextBindingSource1
            // 
            libDbContextBindingSource1.DataSource = typeof(Data.LibDbContext);
            // 
            // button1
            // 
            button1.Location = new Point(694, 345);
            button1.Name = "button1";
            button1.Size = new Size(213, 29);
            button1.TabIndex = 2;
            button1.Text = "Просмотреть историю";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 345);
            button2.Name = "button2";
            button2.Size = new Size(110, 29);
            button2.TabIndex = 3;
            button2.Text = "Выбрать";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(436, 345);
            button3.Name = "button3";
            button3.Size = new Size(194, 29);
            button3.TabIndex = 4;
            button3.Text = "Удалить студента";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // СardIndex
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(919, 446);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(toolStrip1);
            Name = "СardIndex";
            Text = "Картотека ";
            Load += СardIndex_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)libDbContextBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)libDbContextBindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private DataGridView dataGridView1;
        private BindingSource libDbContextBindingSource;
        private BindingSource libDbContextBindingSource1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}