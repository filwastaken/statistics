namespace hw4
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
            variables_num = new NumericUpDown();
            startbt = new Button();
            calculate_bt = new Button();
            table = new TableLayoutPanel();
            label2 = new Label();
            extractions_num = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)variables_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)extractions_num).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 7);
            label1.Name = "label1";
            label1.Size = new Size(231, 32);
            label1.TabIndex = 0;
            label1.Text = "Number of variables";
            // 
            // variables_num
            // 
            variables_num.Location = new Point(252, 5);
            variables_num.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            variables_num.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            variables_num.Name = "variables_num";
            variables_num.Size = new Size(116, 39);
            variables_num.TabIndex = 1;
            variables_num.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // startbt
            // 
            startbt.Location = new Point(1258, 4);
            startbt.Name = "startbt";
            startbt.Size = new Size(150, 39);
            startbt.TabIndex = 2;
            startbt.Text = "Start";
            startbt.UseVisualStyleBackColor = true;
            startbt.Click += startbt_Click;
            // 
            // calculate_bt
            // 
            calculate_bt.Location = new Point(1258, 49);
            calculate_bt.Name = "calculate_bt";
            calculate_bt.Size = new Size(150, 39);
            calculate_bt.TabIndex = 3;
            calculate_bt.Text = "Calculate";
            calculate_bt.UseVisualStyleBackColor = true;
            calculate_bt.Visible = false;
            calculate_bt.Click += calculate_bt_Click;
            // 
            // table
            // 
            table.AutoScroll = true;
            table.ColumnCount = 4;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            table.Location = new Point(10, 300);
            table.Margin = new Padding(0);
            table.Name = "table";
            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            table.Size = new Size(1400, 400);
            table.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(405, 9);
            label2.Name = "label2";
            label2.Size = new Size(259, 32);
            label2.TabIndex = 5;
            label2.Text = "Per variable extractions";
            // 
            // extractions_num
            // 
            extractions_num.Location = new Point(670, 5);
            extractions_num.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            extractions_num.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            extractions_num.Name = "extractions_num";
            extractions_num.Size = new Size(128, 39);
            extractions_num.TabIndex = 6;
            extractions_num.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1420, 707);
            Controls.Add(extractions_num);
            Controls.Add(label2);
            Controls.Add(table);
            Controls.Add(calculate_bt);
            Controls.Add(startbt);
            Controls.Add(variables_num);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)variables_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)extractions_num).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown variables_num;
        private Button startbt;
        private Button calculate_bt;
        private TableLayoutPanel table;
        private Label label2;
        private NumericUpDown extractions_num;
    }
}