namespace OrderedFreq
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            quantc_key = new RadioButton();
            quantc_asc = new RadioButton();
            quantc_desc = new RadioButton();
            quantd_desc = new RadioButton();
            quantd_key = new RadioButton();
            quantd_asc = new RadioButton();
            quali_desc = new RadioButton();
            quali_alpha = new RadioButton();
            quali_asc = new RadioButton();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            label7 = new Label();
            label8 = new Label();
            quantd_inter = new NumericUpDown();
            quantc_inter = new NumericUpDown();
            calc_bt = new Button();
            qual_grid = new DataGridView();
            quantd_grid = new DataGridView();
            quantc_grid = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)quantd_inter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)quantc_inter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)qual_grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)quantd_grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)quantc_grid).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(170, 54);
            label1.TabIndex = 0;
            label1.Text = "Hobbies";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 382);
            label2.Name = "label2";
            label2.Size = new Size(94, 54);
            label2.TabIndex = 1;
            label2.Text = "Age";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(45, 741);
            label3.Name = "label3";
            label3.Size = new Size(150, 54);
            label3.TabIndex = 2;
            label3.Text = "Weight";
            // 
            // quantc_key
            // 
            quantc_key.AutoSize = true;
            quantc_key.Location = new Point(133, 56);
            quantc_key.Name = "quantc_key";
            quantc_key.Size = new Size(84, 36);
            quantc_key.TabIndex = 3;
            quantc_key.TabStop = true;
            quantc_key.Text = "Key";
            quantc_key.UseVisualStyleBackColor = true;
            quantc_key.Click += quantc_key_click;
            // 
            // quantc_asc
            // 
            quantc_asc.AutoSize = true;
            quantc_asc.Location = new Point(3, 3);
            quantc_asc.Name = "quantc_asc";
            quantc_asc.Size = new Size(156, 36);
            quantc_asc.TabIndex = 4;
            quantc_asc.TabStop = true;
            quantc_asc.Text = "Ascendant";
            quantc_asc.UseVisualStyleBackColor = true;
            quantc_asc.Click += quantc_asc_click;
            // 
            // quantc_desc
            // 
            quantc_desc.AutoSize = true;
            quantc_desc.Location = new Point(191, 3);
            quantc_desc.Name = "quantc_desc";
            quantc_desc.Size = new Size(171, 36);
            quantc_desc.TabIndex = 5;
            quantc_desc.TabStop = true;
            quantc_desc.Text = "Descendant";
            quantc_desc.UseVisualStyleBackColor = true;
            quantc_desc.Click += quantc_desc_click;
            // 
            // quantd_desc
            // 
            quantd_desc.AutoSize = true;
            quantd_desc.Location = new Point(211, 13);
            quantd_desc.Name = "quantd_desc";
            quantd_desc.Size = new Size(171, 36);
            quantd_desc.TabIndex = 6;
            quantd_desc.TabStop = true;
            quantd_desc.Text = "Descendant";
            quantd_desc.UseVisualStyleBackColor = true;
            quantd_desc.Click += quantd_desc_click;
            // 
            // quantd_key
            // 
            quantd_key.AutoSize = true;
            quantd_key.Location = new Point(144, 55);
            quantd_key.Name = "quantd_key";
            quantd_key.Size = new Size(84, 36);
            quantd_key.TabIndex = 7;
            quantd_key.TabStop = true;
            quantd_key.Text = "Key";
            quantd_key.UseVisualStyleBackColor = true;
            quantd_key.Click += quantd_key_click;
            // 
            // quantd_asc
            // 
            quantd_asc.AutoSize = true;
            quantd_asc.Location = new Point(12, 13);
            quantd_asc.Name = "quantd_asc";
            quantd_asc.Size = new Size(156, 36);
            quantd_asc.TabIndex = 8;
            quantd_asc.TabStop = true;
            quantd_asc.Text = "Ascendant";
            quantd_asc.UseVisualStyleBackColor = true;
            quantd_asc.Click += quantd_asc_click;
            // 
            // quali_desc
            // 
            quali_desc.AutoSize = true;
            quali_desc.Location = new Point(175, 41);
            quali_desc.Name = "quali_desc";
            quali_desc.Size = new Size(171, 36);
            quali_desc.TabIndex = 9;
            quali_desc.Text = "Descendant";
            quali_desc.UseVisualStyleBackColor = true;
            quali_desc.Click += quali_desc_click;
            // 
            // quali_alpha
            // 
            quali_alpha.AutoSize = true;
            quali_alpha.Location = new Point(13, 69);
            quali_alpha.Name = "quali_alpha";
            quali_alpha.Size = new Size(158, 36);
            quali_alpha.TabIndex = 10;
            quali_alpha.Text = "Alphabetic";
            quali_alpha.UseVisualStyleBackColor = true;
            quali_alpha.Click += quali_alpha_click;
            // 
            // quali_asc
            // 
            quali_asc.AutoSize = true;
            quali_asc.Location = new Point(13, 13);
            quali_asc.Name = "quali_asc";
            quali_asc.Size = new Size(156, 36);
            quali_asc.TabIndex = 11;
            quali_asc.Text = "Ascendant";
            quali_asc.UseVisualStyleBackColor = true;
            quali_asc.Click += quali_asc_click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(177, 835);
            label4.Name = "label4";
            label4.Size = new Size(113, 32);
            label4.TabIndex = 12;
            label4.Text = "Order by:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(158, 106);
            label5.Name = "label5";
            label5.Size = new Size(113, 32);
            label5.TabIndex = 13;
            label5.Text = "Order by:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(167, 485);
            label6.Name = "label6";
            label6.Size = new Size(113, 32);
            label6.TabIndex = 14;
            label6.Text = "Order by:";
            // 
            // panel1
            // 
            panel1.Controls.Add(quantd_asc);
            panel1.Controls.Add(quantd_desc);
            panel1.Controls.Add(quantd_key);
            panel1.Location = new Point(42, 520);
            panel1.Name = "panel1";
            panel1.Size = new Size(400, 100);
            panel1.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Controls.Add(quantc_asc);
            panel2.Controls.Add(quantc_desc);
            panel2.Controls.Add(quantc_key);
            panel2.Location = new Point(67, 874);
            panel2.Name = "panel2";
            panel2.Size = new Size(371, 101);
            panel2.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.Controls.Add(quali_asc);
            panel3.Controls.Add(quali_desc);
            panel3.Controls.Add(quali_alpha);
            panel3.Location = new Point(42, 141);
            panel3.Name = "panel3";
            panel3.Size = new Size(357, 116);
            panel3.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(275, 382);
            label7.Name = "label7";
            label7.Size = new Size(103, 32);
            label7.TabIndex = 18;
            label7.Text = "Intervals";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(326, 737);
            label8.Name = "label8";
            label8.Size = new Size(103, 32);
            label8.TabIndex = 19;
            label8.Text = "Intervals";
            // 
            // quantd_inter
            // 
            quantd_inter.Location = new Point(259, 427);
            quantd_inter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            quantd_inter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            quantd_inter.Name = "quantd_inter";
            quantd_inter.Size = new Size(135, 39);
            quantd_inter.TabIndex = 20;
            quantd_inter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // quantc_inter
            // 
            quantc_inter.Location = new Point(300, 772);
            quantc_inter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            quantc_inter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            quantc_inter.Name = "quantc_inter";
            quantc_inter.Size = new Size(138, 39);
            quantc_inter.TabIndex = 21;
            quantc_inter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // calc_bt
            // 
            calc_bt.Location = new Point(650, 1060);
            calc_bt.Name = "calc_bt";
            calc_bt.Size = new Size(150, 46);
            calc_bt.TabIndex = 22;
            calc_bt.Text = "Calculate";
            calc_bt.UseVisualStyleBackColor = true;
            calc_bt.Click += calc_bt_Click;
            // 
            // qual_grid
            // 
            qual_grid.BackgroundColor = Color.White;
            qual_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            qual_grid.Location = new Point(471, 19);
            qual_grid.Name = "qual_grid";
            qual_grid.RowHeadersWidth = 82;
            qual_grid.RowTemplate.Height = 41;
            qual_grid.Size = new Size(989, 321);
            qual_grid.TabIndex = 23;
            // 
            // quantd_grid
            // 
            quantd_grid.BackgroundColor = Color.White;
            quantd_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            quantd_grid.Location = new Point(471, 382);
            quantd_grid.Name = "quantd_grid";
            quantd_grid.RowHeadersWidth = 82;
            quantd_grid.RowTemplate.Height = 41;
            quantd_grid.Size = new Size(989, 319);
            quantd_grid.TabIndex = 24;
            // 
            // quantc_grid
            // 
            quantc_grid.BackgroundColor = Color.White;
            quantc_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            quantc_grid.Location = new Point(471, 737);
            quantc_grid.Name = "quantc_grid";
            quantc_grid.RowHeadersWidth = 82;
            quantc_grid.RowTemplate.Height = 41;
            quantc_grid.Size = new Size(989, 302);
            quantc_grid.TabIndex = 25;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(1474, 1119);
            Controls.Add(quantc_grid);
            Controls.Add(quantd_grid);
            Controls.Add(qual_grid);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(calc_bt);
            Controls.Add(quantc_inter);
            Controls.Add(quantd_inter);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)quantd_inter).EndInit();
            ((System.ComponentModel.ISupportInitialize)quantc_inter).EndInit();
            ((System.ComponentModel.ISupportInitialize)qual_grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)quantd_grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)quantc_grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private RadioButton quantc_key;
        private RadioButton quantc_asc;
        private RadioButton quantc_desc;
        private RadioButton quantd_desc;
        private RadioButton quantd_key;
        private RadioButton quantd_asc;
        private RadioButton quali_desc;
        private RadioButton quali_alpha;
        private RadioButton quali_asc;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label7;
        private Label label8;
        private NumericUpDown quantd_inter;
        private NumericUpDown quantc_inter;
        private Button calc_bt;
        private DataGridView qual_grid;
        private DataGridView quantd_grid;
        private DataGridView quantc_grid;
    }
}