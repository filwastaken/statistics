namespace nUniform
{
    partial class nUniform
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
            nchooser = new NumericUpDown();
            kchooser = new NumericUpDown();
            bmContainer = new PictureBox();
            stopbt = new Button();
            startbt = new Button();
            ktext = new Label();
            ntext = new Label();
            animationCheckbox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)nchooser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kchooser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bmContainer).BeginInit();
            SuspendLayout();
            // 
            // nchooser
            // 
            nchooser.Location = new Point(137, 12);
            nchooser.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            nchooser.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nchooser.Name = "nchooser";
            nchooser.Size = new Size(64, 23);
            nchooser.TabIndex = 1;
            nchooser.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // kchooser
            // 
            kchooser.Location = new Point(501, 10);
            kchooser.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            kchooser.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            kchooser.Name = "kchooser";
            kchooser.Size = new Size(64, 23);
            kchooser.TabIndex = 3;
            kchooser.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // bmContainer
            // 
            bmContainer.Location = new Point(0, 40);
            bmContainer.Name = "bmContainer";
            bmContainer.Size = new Size(1062, 640);
            bmContainer.TabIndex = 0;
            bmContainer.TabStop = false;
            // 
            // stopbt
            // 
            stopbt.Cursor = Cursors.Hand;
            stopbt.Location = new Point(977, 12);
            stopbt.Name = "stopbt";
            stopbt.Size = new Size(75, 23);
            stopbt.TabIndex = 6;
            stopbt.Text = "Stop";
            stopbt.UseVisualStyleBackColor = true;
            stopbt.Click += stopbt_Click;
            // 
            // startbt
            // 
            startbt.Cursor = Cursors.Hand;
            startbt.Location = new Point(881, 12);
            startbt.Name = "startbt";
            startbt.Size = new Size(75, 23);
            startbt.TabIndex = 7;
            startbt.Text = "Start";
            startbt.UseVisualStyleBackColor = true;
            startbt.Click += startbt_Click;
            // 
            // ktext
            // 
            ktext.AutoSize = true;
            ktext.Location = new Point(389, 12);
            ktext.Name = "ktext";
            ktext.Size = new Size(96, 15);
            ktext.TabIndex = 8;
            ktext.Text = "Please, choose k:";
            // 
            // ntext
            // 
            ntext.AutoSize = true;
            ntext.Location = new Point(22, 14);
            ntext.Name = "ntext";
            ntext.Size = new Size(97, 15);
            ntext.TabIndex = 9;
            ntext.Text = "Please, choose n:";
            // 
            // animationCheckbox
            // 
            animationCheckbox.AutoSize = true;
            animationCheckbox.Location = new Point(776, 14);
            animationCheckbox.Name = "animationCheckbox";
            animationCheckbox.Size = new Size(82, 19);
            animationCheckbox.TabIndex = 10;
            animationCheckbox.Text = "Animation";
            animationCheckbox.UseVisualStyleBackColor = true;
            // 
            // nUniform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1064, 681);
            Controls.Add(animationCheckbox);
            Controls.Add(ntext);
            Controls.Add(ktext);
            Controls.Add(startbt);
            Controls.Add(stopbt);
            Controls.Add(kchooser);
            Controls.Add(nchooser);
            Controls.Add(bmContainer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "nUniform";
            Text = "nUniform";
            ((System.ComponentModel.ISupportInitialize)nchooser).EndInit();
            ((System.ComponentModel.ISupportInitialize)kchooser).EndInit();
            ((System.ComponentModel.ISupportInitialize)bmContainer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox bmContainer;
        public NumericUpDown nchooser;
        public NumericUpDown kchooser;
        public CheckBox animationCheckbox;
        public Button stopbt;
        public Button startbt;
        private Label ktext;
        private Label ntext;
    }
}