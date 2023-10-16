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
        private bool animated;

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

        public void StartPick(int n, int k, bool animated)
        {
            this.nPicks = n;
            this.kGroups = k;
            this.animated = animated;

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
                this.winForm.nchooser.Enabled = false;
                this.winForm.kchooser.Enabled = false;
                this.winForm.animationCheckbox.Enabled = false;
                this.winForm.startbt.Enabled = false;
                this.winForm.stopbt.Enabled = true;
            });

            this.winForm.grph.Clear(Color.White);

            int hr_size = this.winForm.bitmap.Width / this.kGroups;

            decimal index = 0;
            while (this.allowed && index < this.nPicks)
            {

                int randNumber = rand.Next(0, this.kGroups);
                this.heights[randNumber]++;
                this.displayedHeight[randNumber]++;

                // In case the animation is required I can do all the following calculations
                // Otherwile I can do all the drawings outside of this while loop, to make that amount go from n to k
                if (this.animated)
                {
                    sw.Start();

                    this.winForm.Invoke(new MethodInvoker(delegate ()
                    {
                        int clmHeight = this.winForm.bitmap.Height - this.displayedHeight[randNumber];

                        // In case the columns go over the amount of space defined for the PictureBox, I can move them all down by 250px
                        if (clmHeight <= 0)
                        {
                            this.winForm.grph.Clear(Color.White);
                            for (int i = 0; i < this.displayedHeight.Count(); i++)
                            {
                                this.displayedHeight[i] -= 250;
                                this.winForm.grph.FillRectangle(this.brsh[i % brsh.Count()], hr_size * i, this.winForm.bitmap.Height - this.displayedHeight[i], hr_size, this.winForm.bitmap.Height);
                            }
                        }
                        else
                        // Else I can just draw the rectangle as is
                        {
                            this.winForm.grph.FillRectangle(this.brsh[randNumber % brsh.Count()], hr_size * randNumber, this.winForm.bitmap.Height - this.displayedHeight[randNumber], hr_size, this.winForm.bitmap.Height);
                            this.winForm.bmContainer.Refresh();
                        }
                    }));

                    sw.Stop();
                    // Max ~55fps (delta time calculation)
                    double sleeptime = 18.20 - sw.ElapsedMilliseconds;
                    if (sleeptime > 0) Thread.Sleep(sw.Elapsed);
                    sw.Reset();

                }
                index++;
            }

            if(!this.animated)
            {
                // Resizing loop
                int diffHeight = 1;
                for (int i = 0; i < this.displayedHeight.Count(); )
                {
                    if ((int) this.displayedHeight[i]/diffHeight > this.winForm.bitmap.Height) diffHeight++;
                    else i++;
                }

                // Unanimented loop
                for (int i = 0; i < this.displayedHeight.Count(); i++)
                {
                    // Resizing all heigths. If none are higher than the screen, diffHeight should be 1
                    this.displayedHeight[i] = (int)this.displayedHeight[i] / diffHeight;

                    this.winForm.Invoke(new MethodInvoker(delegate () {
                        // I can now draw the rectangle
                        this.winForm.grph.FillRectangle(this.brsh[i % brsh.Count()], hr_size * i, this.winForm.bitmap.Height - this.displayedHeight[i], hr_size, this.displayedHeight[i]);
                    }));
                }

                this.winForm.Invoke(new MethodInvoker(delegate () { this.winForm.bmContainer.Refresh(); }));
            }


            // Reset UI to default values (Thread  safe)
            this.allowed = true;
            this.winForm.BeginInvoke((Action) delegate () {
                this.winForm.stopbt.Enabled = false;
                this.winForm.startbt.Enabled = true;
                this.winForm.nchooser.Enabled = true;
                this.winForm.kchooser.Enabled = true;
                this.winForm.animationCheckbox.Enabled = true;
            });

            return;
        }
    }
}
