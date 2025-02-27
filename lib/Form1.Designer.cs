namespace lib
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            menuStrip1 = new MenuStrip();
            назадToolStripMenuItem = new ToolStripMenuItem();
            button4 = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Snap ITC", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            button1.Location = new Point(100, 135);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(234, 60);
            button1.TabIndex = 0;
            button1.Text = "Выданные книги";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.HighlightText;
            button2.FlatStyle = FlatStyle.System;
            button2.Font = new Font("Snap ITC", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            button2.ImageAlign = ContentAlignment.BottomLeft;
            button2.Location = new Point(100, 54);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(234, 56);
            button2.TabIndex = 1;
            button2.Text = "Учет книг";
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.Desktop;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.Red;
            button3.Location = new Point(12, 319);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(139, 31);
            button3.TabIndex = 2;
            button3.Text = "Должники";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.WindowText;
            menuStrip1.Dock = DockStyle.Bottom;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { назадToolStripMenuItem });
            menuStrip1.Location = new Point(0, 354);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(452, 30);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // назадToolStripMenuItem
            // 
            назадToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            назадToolStripMenuItem.ForeColor = SystemColors.ButtonFace;
            назадToolStripMenuItem.Name = "назадToolStripMenuItem";
            назадToolStripMenuItem.RightToLeft = RightToLeft.No;
            назадToolStripMenuItem.Size = new Size(67, 24);
            назадToolStripMenuItem.Text = "Выход";
            назадToolStripMenuItem.Click += назадToolStripMenuItem_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Snap ITC", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            button4.Location = new Point(100, 225);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(234, 60);
            button4.TabIndex = 5;
            button4.Text = "Картотека";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(452, 384);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Библиотека";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem назадToolStripMenuItem;
        private Button button4;
    }
}
