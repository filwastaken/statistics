using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nUniform
{
    internal class RandomPicker
    {
        private Random rand;
        public bool allowed;
        private Stopwatch sw;

        private Thread thread;
        private int nPicks;
        private int kGroups;

        private readonly nUniform winForm;

        private List<int> heights;
        private List<int> displayedHeight;
        private readonly List<Brush> brsh = new List<Brush>{ Brushes.Black, Brushes.Red, Brushes.Pink, Brushes.Green, Brushes.Blue, Brushes.Brown, Brushes.Purple, Brushes.Orange, Brushes.Yellow, Brushes.Aquamarine };

        public RandomPicker(Graphics g, nUniform winform)
        {
            this.winForm = winform;
            
            this.rand = new Random();
            this.allowed = true;
            this.sw = new Stopwatch();
            

            this.heights = new List<int>();
        }

        public void StartPick(int n, int k)
        {
            this.nPicks = n;
            this.kGroups = k;

            this.thread = new Thread(new ThreadStart(startThreadPick));
            this.heights = Enumerable.Repeat(0, this.kGroups).ToList();
            this.displayedHeight = Enumerable.Repeat(0, this.kGroups).ToList();

            // Starting thread
            this.thread.Start();
        }

        private void startThreadPick()
        {
            // Disabling nUniform UI items (Thread safe)
            this.winForm.BeginInvoke((Action) delegate () {
                this.winForm.startbt.Enabled = false;
                this.winForm.stopbt.Enabled = true;
                this.winForm.nchooser.Enabled = false;
                this.winForm.kchooser.Enabled = false;
            });

            this.winForm.grph.Clear(Color.White);

            int hr_size = this.winForm.bitmap.Width / this.kGroups;

            decimal index = 0;
            while(this.allowed && index < this.nPicks)
            {
                sw.Start();

                int randNumber = rand.Next(0, 100);
                int group = randNumber % this.kGroups;
                this.heights[group]++;
                this.displayedHeight[group]++;

                this.winForm.Invoke(new MethodInvoker(delegate ()
                {
                    int clmHeight = this.winForm.bitmap.Height - this.displayedHeight[group];
                    
                    // In case the columns go over the amount of space defined for the PictureBox, I can move them all down by 50px
                    if (clmHeight <= 0)
                    {
                        this.winForm.grph.Clear(Color.White);
                        for(int i = 0; i<this.displayedHeight.Count(); i++)
                        {
                            this.displayedHeight[i] -= 250;
                            this.winForm.grph.FillRectangle(this.brsh[i % brsh.Count()], hr_size * i, this.winForm.bitmap.Height - this.displayedHeight[i], hr_size, this.winForm.bitmap.Height);
                        }
                    } else
                    // Else I can just draw the rectangle a little higher
                    {
                        this.winForm.grph.FillRectangle(this.brsh[group % brsh.Count()], hr_size * group, this.winForm.bitmap.Height - this.displayedHeight[group], hr_size, this.winForm.bitmap.Height);
                        this.winForm.bmContainer.Refresh();
                    }
                }));

                /*
                 * this.winForm.BeginInvoke((Action) delegate () {
                 * });
                 */

                sw.Stop();
                // Max ~55fps (delta time calculation)
                double sleeptime = 18.20 - sw.ElapsedMilliseconds;
                if (sleeptime > 0) Thread.Sleep(sw.Elapsed);

                sw.Reset();

                index++;
            }

            // Reset UI to default values (Thread  safe)
            this.allowed = true;
            this.winForm.BeginInvoke((Action) delegate () {
                this.winForm.stopbt.Enabled = false;
                this.winForm.startbt.Enabled = true;
                this.winForm.nchooser.Enabled = true;
                this.winForm.kchooser.Enabled = true;
            });

            return;
        }
    }
}
