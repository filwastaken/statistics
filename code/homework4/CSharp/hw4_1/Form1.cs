using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace hw4
{
    public partial class Form1 : Form
    {

        private Font defaultFont = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
        private Random[] randoms;
        private NumericUpDown[] divisions;
        private double[] extractions;

        private Dictionary<String, int> extractionsDict = new Dictionary<String, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void startbt_Click(object sender, EventArgs e)
        {

            this.randoms = new Random[(int)this.variables_num.Value];
            this.extractions = new double[(int)this.variables_num.Value];
            this.divisions = new NumericUpDown[(int)this.variables_num.Value];
            for (int i = 0; i < randoms.Length; i++) { this.randoms[i] = new Random(); }

            for (int i = 0; i < this.variables_num.Value; i++)
            {
                // Creating new label and numeric and adding them to the winform
                var lblnew = new Label
                {
                    Location = new Point(25 + 200 * i, 80),
                    Text = "var " + (i + 1) + " divisions",
                    AutoSize = true,
                    BackColor = Color.White,
                    Font = this.defaultFont
                };

                var newnum = new NumericUpDown
                {
                    Location = new Point(50 + 200 * i, 120),
                    Minimum = 1,
                    Increment = 1,
                    DecimalPlaces = 0,
                    AutoSize = true,
                    BackColor = Color.White,
                    Font = this.defaultFont
                };

                Controls.Add(lblnew);
                Controls.Add(newnum);
                this.divisions[i] = newnum;
            }

            this.calculate_bt.Location = new Point(25 + 200 * (int)this.variables_num.Value, 110);
            this.calculate_bt.Visible = true;
            this.startbt.Enabled = false;
        }

        private void calculate_bt_Click(object sender, EventArgs e)
        {
            String key;
            RowStyle rowStyle = this.table.RowStyles[this.table.RowCount - 1];

            for (int j = 0; j < this.extractions_num.Value; j++)
            {
                key = "";

                for (int i = 0; i < this.randoms.Length; i++) this.extractions[i] = this.randoms[i].NextDouble();

                // I now need to take the corresponding groups to calculate the frequencies
                for (int i = 0; i < this.extractions.Length; i++)
                {
                    key += Math.Floor(this.extractions[i] * ((int)this.divisions[i].Value + 1));
                }

                // I can now increase the elements in the key by 1:
                if (this.extractionsDict.ContainsKey(key)) this.extractionsDict[key]++;
                else this.extractionsDict[key] = 1;
            }

            // I can now display the results
            // I can skip all of those who don't appear in the extracionsDict, since they have a frequency of 0.

            bool first = true;
            int abs = 0;
            double relative = 0.0, per = 0.0;
            String group;

            foreach (var dict_key in this.extractionsDict.Keys)
            {
                //increase panel rows count by one
                if (!first) this.table.RowCount++;
                else first = false;

                abs = this.extractionsDict[dict_key];
                relative = abs / (double)this.extractions_num.Value;
                per = relative * 100;
                group = "";

                foreach (var chr in dict_key) group += "Group " + chr + ", ";
                group = group.Remove(group.Length - 2);

                //add a new RowStyle as a copy of the previous one
                this.table.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));
                //add your three controls
                this.table.Controls.Add(new Label() { Text = group, AutoSize = true }, 0, this.table.RowCount - 1);
                this.table.Controls.Add(new Label() { Text = "" + abs, AutoSize = true }, 1, this.table.RowCount - 1);
                this.table.Controls.Add(new Label() { Text = "" + Math.Round(relative, 3), AutoSize = true }, 2, this.table.RowCount - 1);
                this.table.Controls.Add(new Label() { Text = "" + Math.Round(per, 3) + "%", AutoSize = true }, 3, this.table.RowCount - 1);
            }
        }
    }
}