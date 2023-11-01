using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using static System.Windows.Forms.LinkLabel;

namespace OrderedFreq
{
    public partial class Form1 : Form
    {

        private Dictionary<String, int> qual_cnt = new Dictionary<String, int>();
        private Dictionary<String, int> quantc_cnt = new Dictionary<String, int>();
        private Dictionary<String, int> quantd_cnt = new Dictionary<String, int>();

        private string[] lines;

        public Form1()
        {
            InitializeComponent();
            this.quali_alpha.Checked = false;
            this.quali_asc.Checked = false;
            this.quali_desc.Checked = false;
            this.quantd_key.Checked = false;
            this.quantd_asc.Checked = false;
            this.quantd_desc.Checked = false;
            this.quantc_key.Checked = false;
            this.quantc_asc.Checked = false;
            this.quantc_desc.Checked = false;
        }

        // Qualitive ordering
        private void quali_asc_click(object sender, EventArgs e) { AscOrder(this.qual_cnt, this.qual_grid); }
        private void quali_alpha_click(object sender, EventArgs e) { AlphaOrder(this.qual_cnt, this.qual_grid); }
        private void quali_desc_click(object sender, EventArgs e) { DescOrder(this.qual_cnt, this.qual_grid); }

        // Quantitive discrete ordering
        private void quantd_asc_click(object sender, EventArgs e) { AscOrder(this.quantd_cnt, this.quantd_grid); }
        private void quantd_desc_click(object sender, EventArgs e) { DescOrder(this.quantd_cnt, this.quantd_grid); }
        private void quantd_key_click(object sender, EventArgs e) { AlphaOrder(this.quantd_cnt, this.quantd_grid); }

        // Quantitive continuous ordering
        private void quantc_asc_click(object sender, EventArgs e) { AscOrder(this.quantc_cnt, this.quantc_grid); }
        private void quantc_desc_click(object sender, EventArgs e) { DescOrder(this.quantc_cnt, this.quantc_grid); }
        private void quantc_key_click(object sender, EventArgs e) { AlphaOrder(this.quantc_cnt, this.quantc_grid); }

        private void calc_bt_Click(object sender, EventArgs e)
        {
            this.quantd_inter.Enabled = false;
            this.quantc_inter.Enabled = false;
            this.calc_bt.Enabled = false;

            String key = "";

            int DiscreteIntervals = (int)this.quantd_inter.Value;
            int ContinuousIntervals = (int)this.quantc_inter.Value;

            // Getting the csv online
            var client = new System.Net.WebClient();
            String data = client.DownloadString("https://bluecheese-fil.github.io/src/hw2/survey/professional_life.csv");

            List<String> ages = new List<String>();
            List<String> weights = new List<String>();
            List<String> hobbies = new List<String>();

            // Changing the grid to accomodate for the incoming data
            quantc_grid.Rows.Clear();
            quantc_grid.Columns.Clear();
            quantc_grid.Columns.Add("Weight", "Weight (kg)");
            quantc_grid.Columns.Add("AbsoluteFrequency", "Absolute Frequency");
            quantc_grid.Columns.Add("RelativeFrequency", "Relative Frequency");
            quantc_grid.Columns.Add("PercentageFrequency", "Percentage Frequency");

            quantd_grid.Rows.Clear();
            quantd_grid.Columns.Clear();
            quantd_grid.Columns.Add("Age", "Age (years)");
            quantd_grid.Columns.Add("AbsoluteFrequency", "Absolute Frequency");
            quantd_grid.Columns.Add("RelativeFrequency", "Relative Frequency");
            quantd_grid.Columns.Add("PercentageFrequency", "Percentage Frequency");

            qual_grid.Rows.Clear();
            qual_grid.Columns.Clear();
            qual_grid.Columns.Add("Main Hobbies", "Main Hobbies");
            qual_grid.Columns.Add("AbsoluteFrequency", "Absolute Frequency");
            qual_grid.Columns.Add("RelativeFrequency", "Relative Frequency");
            qual_grid.Columns.Add("PercentageFrequency", "Percentage Frequency");

            // Getting data from csv file
            this.lines = data.Split('\n');
            String[] headers = this.lines[0].Split(',');
            for (int i = 1; i < this.lines.Length; i++) //skipping the header
            {
                String[] currentLine = this.lines[i].Split(',');
                ages.Add(currentLine[Array.IndexOf(headers, "Age")]);
                weights.Add(currentLine[Array.IndexOf(headers, "Weight")]);
                hobbies.Add(currentLine[Array.IndexOf(headers, "Main hobbies\r")]); // Main hobbies is the last element
            }

            // Min and max ages
            double ageMax = ages.Max(a => double.Parse(a));
            double ageMin = ages.Min(a => double.Parse(a));

            // Min and max weights
            double weightMax = weights.Max(w => double.Parse(w));
            double weightMin = weights.Min(w => double.Parse(w));

            double age_dim = (ageMax - ageMin) / DiscreteIntervals;
            double weight_dim = (weightMax - weightMin) / ContinuousIntervals;

            for (int i = 0; i < ages.Count; i++)
            {
                for (double j = ageMin; j < ageMax; j += age_dim)
                {
                    double start = j;
                    double end = start + age_dim;

                    if (double.Parse(ages[i]) >= start && double.Parse(ages[i]) < end)
                    {
                        if (quantd_cnt.ContainsKey($"{start}-{end}")) quantd_cnt[$"{start}-{end}"]++;
                        else quantd_cnt[$"{start}-{end}"] = 1;
                    }

                    if (end == ageMax && double.Parse(ages[i]) == end)
                    {
                        if (quantd_cnt.ContainsKey($"{start}-{end}")) quantd_cnt[$"{start}-{end}"]++;
                        else quantd_cnt[$"{start}-{end}"] = 1;
                    }
                }
            }

            for (int i = 0; i < weights.Count; i++)
            {
                for (double j = weightMin; j < weightMax; j += weight_dim)
                {
                    double start = j;
                    double end = start + weight_dim;
                    start = Math.Round(start, 2);
                    end = Math.Round(end, 2);

                    if (double.Parse(weights[i]) >= start && double.Parse(weights[i]) < end)
                    {
                        if (quantc_cnt.ContainsKey($"{start}-{end}")) quantc_cnt[$"{start}-{end}"]++;
                        else quantc_cnt[$"{start}-{end}"] = 1;
                    }
                    if (end == weightMax && double.Parse(weights[i]) == end)
                    {
                        if (quantc_cnt.ContainsKey($"{start}-{end}")) quantc_cnt[$"{start}-{end}"]++;
                        else quantc_cnt[$"{start}-{end}"] = 1;
                    }
                }
            }

            foreach (var s in hobbies)
            {
                if (!this.qual_cnt.ContainsKey(s)) this.qual_cnt[s] = 1;
                else this.qual_cnt[s]++;
            }

            // I can now insert the data inside the grids
            double relFreq, percFreq;
            foreach (var val in quantd_cnt)
            {
                relFreq = (double)val.Value / this.lines.Count();
                percFreq = relFreq * 100;
                quantd_grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }

            foreach (var val in quantc_cnt)
            {
                relFreq = (double)val.Value / this.lines.Count();
                percFreq = relFreq * 100;
                quantc_grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }

            foreach (var val in qual_cnt)
            {
                relFreq = (double)val.Value / this.lines.Count();
                percFreq = relFreq * 100;
                qual_grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }
        }

        // I can now do the ordering functions
        private void AlphaOrder(Dictionary<String, int> dict, DataGridView grid)
        {
            double relFreq, percFreq;

            var keys = dict.Keys.ToList();
            keys.Sort(); // Sorting keys by alphabetical

            var sorted = new Dictionary<String, int>();
            foreach (var k in keys) sorted[k] = dict[k];

            grid.Rows.Clear();

            foreach (var val in sorted)
            {
                relFreq = (double)val.Value / lines.Count();
                percFreq = relFreq * 100;

                grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }
        }

        private void AscOrder(Dictionary<String, int> dict, DataGridView grid)
        {
            double relFreq, percFreq;

            var sortedEntries = dict.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            grid.Rows.Clear();

            foreach (var val in sortedEntries)
            {
                relFreq = (double)val.Value / lines.Count();
                percFreq = relFreq * 100;

                grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }
        }

        private void DescOrder(Dictionary<String, int> dict, DataGridView grid)
        {
            double relFreq, percFreq;

            var sortedEntries = dict.OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            grid.Rows.Clear();

            foreach (var val in sortedEntries)
            {
                relFreq = (double)val.Value / lines.Count();
                percFreq = relFreq * 100;

                grid.Rows.Add(val.Key, val.Value, relFreq.ToString("F4"), percFreq.ToString("F2") + "%");
            }
        }
    }
}