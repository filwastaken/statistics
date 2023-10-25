using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace pAttacks
{
    internal class Attack
    {
        private Random rand;
        private AttackForm atkForm;
        private Stopwatch sw;

        private List<Pen> pens;

        private Pen axis_pen;
        private Pen company_pen;

        private Pen rect1_pen;
        private Pen rect2_pen;
        private Pen rect3_pen;
        private Pen rect4_pen;

        private Brush histo_pen;

        public Thread thread;
        private int nAttacks;
        public int picking;
        public int mSystems;
        private double pAttacks;
        public int aHistogram;
        private int intervalsNumber;

        private List<List<Point>> zero_points;
        private List<List<Point>> one_points;
        private List<List<Point>> divided;
        private List<List<Point>> normalized;

        private List<List<float>> zero_points_Y;
        private List<List<float>> divided_Y;
        private List<List<float>> normalized_Y;

        private Dictionary<int, int> countNormalized;
        private Dictionary<int, int> countZero_points;
        private Dictionary<int, int> countDivided;

        private Dictionary<int, int> countNormalized_end;
        private Dictionary<int, int> countZero_points_end;
        private Dictionary<int, int> countDivided_end;

        private int scaling = 5;

        private Rectangle normalized_histo;
        private Rectangle zero_histo;
        private Rectangle divided_histo;

        private Rectangle normalized_histo_end;
        private Rectangle zero_histo_end;
        private Rectangle divided_histo_end;

        private int barHeight = 10;

        public Attack(AttackForm atkForm)
        {
            this.atkForm = atkForm;
            this.rand = new Random();
            this.sw = new Stopwatch();

            this.pens = new List<Pen>();
            this.pens.Add(new Pen(Color.Olive, 3));
            this.pens.Add(new Pen(Color.Red, 3));
            this.pens.Add(new Pen(Color.Blue, 3));
            this.pens.Add(new Pen(Color.Green, 3));
            this.pens.Add(new Pen(Color.Orange, 3));
            this.pens.Add(new Pen(Color.Aqua, 3));
            this.pens.Add(new Pen(Color.Coral, 3));
            this.pens.Add(new Pen(Color.Purple, 3));
            this.pens.Add(new Pen(Color.Salmon, 3));
            this.pens.Add(new Pen(Color.SandyBrown, 3));

            this.histo_pen = new SolidBrush(Color.FromArgb(150, Color.Maroon));

            this.axis_pen = new Pen(Color.DarkGray, 2);
            this.company_pen = new Pen(Color.LightGray, 1);

            this.rect1_pen = new Pen(Color.LightBlue, 4);
            this.rect2_pen = new Pen(Color.LightGreen, 4);
            this.rect3_pen = new Pen(Color.LightPink, 4);
            this.rect4_pen = new Pen(Color.DarkBlue, 4);


            this.zero_points = new List<List<Point>>();
            this.one_points = new List<List<Point>>();
            this.divided = new List<List<Point>>();
            this.normalized = new List<List<Point>>();

            this.zero_points_Y = new List<List<float>>();
            this.divided_Y = new List<List<float>>();
            this.normalized_Y = new List<List<float>>();

            this.countNormalized = new Dictionary<int, int>();
            this.countZero_points = new Dictionary<int, int>();
            this.countDivided = new Dictionary<int, int>();

            this.countNormalized_end = new Dictionary<int, int>();
            this.countZero_points_end = new Dictionary<int, int>();
            this.countDivided_end = new Dictionary<int, int>();

            this.normalized_histo = new Rectangle(0, 0, 0, this.barHeight);
            this.zero_histo = new Rectangle(0, 0, 0, this.barHeight);
            this.divided_histo = new Rectangle(0, 0, 0, this.barHeight);

            this.normalized_histo_end = new Rectangle(0, 0, 0, this.barHeight);
            this.zero_histo_end = new Rectangle(0, 0, 0, this.barHeight);
            this.divided_histo_end = new Rectangle(0, 0, 0, this.barHeight);

            this.picking = -1;
            this.nAttacks = 1;
            this.pAttacks = 0;
            this.mSystems = 1;
            this.aHistogram = 1;
            this.intervalsNumber = 20;

            this.thread = new Thread(new ThreadStart(StartThreadedAttacks));

            // Starting thread
            this.thread.Start();
        }

        public void startAttacks(int n, double p, int m, int a)
        {
            this.zero_points.Clear();
            this.one_points.Clear();
            this.divided.Clear();
            this.normalized.Clear();

            this.zero_points_Y.Clear();
            this.divided_Y.Clear();
            this.normalized_Y.Clear();

            this.countNormalized.Clear();
            this.countZero_points.Clear();
            this.countDivided.Clear();

            this.countNormalized_end.Clear();
            this.countZero_points_end.Clear();
            this.countDivided_end.Clear();

            for (int i = 0; i < m; i++)
            {
                this.zero_points.Add(new List<Point>());
                this.zero_points[i].Add(new Point(0, 0));

                this.one_points.Add(new List<Point>());
                this.one_points[i].Add(new Point(0, 0));

                this.divided.Add(new List<Point>());
                this.divided[i].Add(new Point(0, 0));

                this.normalized.Add(new List<Point>());
                this.normalized[i].Add(new Point(0, 0));

                this.zero_points_Y.Add(new List<float>());
                this.zero_points_Y[i].Add(0);

                this.divided_Y.Add(new List<float>());
                this.divided_Y[i].Add(0);

                this.normalized_Y.Add(new List<float>());
                this.normalized_Y[i].Add(0);


            }

            this.pAttacks = p;
            this.nAttacks = n;
            this.mSystems = m;
            this.picking = 0;
            this.aHistogram = a;
        }

        public void cancelAttack()
        {
            this.picking = -1;
            this.nAttacks = 0;
            this.pAttacks = 0;
            this.mSystems = 1;
            this.aHistogram = 1;

            // Reset UI to default values (Thread  safe)
            this.atkForm.BeginInvoke((Action)delegate () {
                this.atkForm.cancBT.Enabled = false;
                this.atkForm.startBT.Enabled = true;
                this.atkForm.nValue.Enabled = true;
                this.atkForm.pValue.Enabled = true;
            });
            
        }

        private void StartThreadedAttacks()
        {
            int posX = 0; int posY = 0, index = 0;

            float inizio = 0, fine = 0;
            float factorXHistogram2 = 0, factorXHistogram3 = 0, factorXHistogram4 = 0;

            int x = 0, y = 0;

            while (this.atkForm.running)
            {
                sw.Start();

                if (this.picking != -1 && this.picking < this.nAttacks)
                {
                    this.atkForm.BeginInvoke((Action)delegate ()
                    {
                        this.atkForm.nValue.Enabled = false;
                        this.atkForm.pValue.Enabled = false;
                        this.atkForm.startBT.Enabled = false;
                        this.atkForm.cancBT.Enabled = true;
                    });

                    if(this.picking == 0)
                    {
                        for (int c = 0; c < this.intervalsNumber; c++)
                        {
                            countNormalized[c] = 0;
                            countZero_points[c] = 0;
                            countDivided[c] = 0;

                            countNormalized_end[c] = 0;
                            countZero_points_end[c] = 0;
                            countDivided_end[c] = 0;
                        }
                    }

                    for (int i = 0; i < this.mSystems; i++)
                    {
                        double randNumber = rand.NextDouble();

                        Point newZero = this.zero_points[i].Last();
                        Point newOne = this.one_points[i].Last();
                        Point newDivided = this.divided[i].Last();
                        Point newNormal = this.normalized[i].Last();

                        float newZero_Y = this.zero_points_Y[i].Last();
                        float newDivided_Y = this.divided_Y[i].Last();
                        float newNormal_Y = this.normalized_Y[i].Last();

                        newZero.X++; newOne.X++; newDivided.X++; newNormal.X++;

                        newOne.Y += (randNumber <= this.pAttacks ? -1 : 1);
                        newZero.Y += (randNumber <= this.pAttacks ? -1 : 0);
                        newDivided.Y += (randNumber <= this.pAttacks ? -1 : 0); newDivided.Y = (int)(newDivided.Y / (this.picking + 1));
                        newNormal.Y += (randNumber <= this.pAttacks ? -1 : 0); newNormal.Y = (int)(newNormal.Y / Math.Sqrt(this.picking + 1));

                        newZero_Y += (randNumber <= this.pAttacks ? 1 : 0);
                        newDivided_Y += (float)(randNumber <= this.pAttacks ? 1 : 0); newDivided_Y = (float)(newDivided_Y / (this.picking + 1));
                        newNormal_Y += (float)(randNumber <= this.pAttacks ? 1 : 0); newNormal_Y = (float)(newNormal_Y / Math.Sqrt(this.picking + 1));

                        this.zero_points[i].Add(newZero);
                        this.one_points[i].Add(newOne);
                        this.divided[i].Add(newDivided);
                        this.normalized[i].Add(newNormal);

                        this.zero_points_Y[i].Add(newZero_Y);

                        this.divided_Y[i].Add(newDivided_Y);
                        normalized_Y[i].Add(newNormal_Y);

                        for (var c = 0; c < this.intervalsNumber; c++)
                        {
                            inizio = (float)c / this.intervalsNumber;
                            fine = (float)(c + 1) / this.intervalsNumber;

                            inizio *= 10;
                            fine *= 10;

                            if (newZero.X == this.aHistogram && newZero_Y >= inizio && newZero_Y < fine) countZero_points[c] += 1;
                            if (newDivided.X == this.aHistogram && newDivided_Y >= inizio && newDivided_Y < fine) countDivided[c] += 1;
                            if (newNormal.X == this.aHistogram && newNormal_Y >= inizio && newNormal_Y < fine) countNormalized[c] += 1;
                            if (newZero.X == this.nAttacks && newZero_Y >= inizio && newZero_Y < fine) countZero_points_end[c] += 1;
                            if (newDivided.X == this.nAttacks && newDivided_Y >= inizio && newDivided_Y < fine) countDivided_end[c] += 1;
                            if (newNormal.X == this.nAttacks && newNormal_Y >= inizio && newNormal_Y < fine) countNormalized_end[c] += 1;
                        }
                    }

                    this.picking += 1;
                }

                if (this.picking == this.nAttacks)
                {
                    // Reset UI to default values (Thread  safe)
                    this.atkForm.BeginInvoke((Action)delegate () {
                        this.atkForm.cancBT.Enabled = false;
                        this.atkForm.startBT.Enabled = true;
                        this.atkForm.nValue.Enabled = true;
                        this.atkForm.pValue.Enabled = true;
                    });

                    this.picking = -1;
                }

                // Drawing all the things
                this.atkForm.grph.Clear(Color.Transparent);
                try
                {
                    this.atkForm.Invoke(new MethodInvoker(delegate ()
                    {

                        // Draw rectangles
                        this.atkForm.grph.DrawRectangle(this.rect1_pen, this.atkForm.rect1);
                        this.atkForm.grph.DrawRectangle(this.rect2_pen, this.atkForm.rect2);
                        this.atkForm.grph.DrawRectangle(this.rect3_pen, this.atkForm.rect3);
                        this.atkForm.grph.DrawRectangle(this.rect4_pen, this.atkForm.rect4);

                        // Draw axis in rectangles

                        // - Horizontal

                        //      -- r1
                        posY = this.atkForm.rect1.Y + (int)this.atkForm.rect1.Height / 2 + this.atkForm.r1move.Y;
                        if (!(posY < this.atkForm.rect1.Top || posY > this.atkForm.rect1.Bottom)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect1.Left, posY, this.atkForm.rect1.Right, posY);

                        // smaller lines
                        index = 0;
                        while (posY - index * this.atkForm.r1zoom > this.atkForm.rect1.Top)
                        {
                            if (posY - index * this.atkForm.r1zoom < this.atkForm.rect1.Bottom) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY - index * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY - index * this.atkForm.r1zoom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posY + index * this.atkForm.r1zoom < this.atkForm.rect1.Bottom)
                        {
                            if (posY + index * this.atkForm.r1zoom > this.atkForm.rect1.Top) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY + index * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY + index * this.atkForm.r1zoom);
                            index += this.scaling;
                        }

                        //      -- r2
                        posY = this.atkForm.rect2.Y + (int)this.atkForm.rect2.Height / 2 + this.atkForm.r2move.Y;
                        if (!(posY < this.atkForm.rect2.Top || posY > this.atkForm.rect2.Bottom)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect2.Left, posY, this.atkForm.rect2.Right, posY);

                        // smaller lines
                        index = 0;
                        while (posY - index * this.atkForm.r2zoom > this.atkForm.rect2.Top)
                        {
                            if (posY - index * this.atkForm.r2zoom < this.atkForm.rect2.Bottom) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect2.Left, posY - index * this.atkForm.r2zoom, this.atkForm.rect2.Right, posY - index * this.atkForm.r2zoom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posY + index * this.atkForm.r2zoom < this.atkForm.rect2.Bottom)
                        {
                            if (posY + index * this.atkForm.r2zoom > this.atkForm.rect2.Top) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect2.Left, posY + index * this.atkForm.r2zoom, this.atkForm.rect2.Right, posY + index * this.atkForm.r2zoom);
                            index += this.scaling;
                        }

                        //      -- r3
                        posY = this.atkForm.rect3.Y + (int)this.atkForm.rect3.Height / 2 + this.atkForm.r3move.Y;
                        if (!(posY < this.atkForm.rect3.Top || posY > this.atkForm.rect3.Bottom)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect3.Left, posY, this.atkForm.rect3.Right, posY);

                        // smaller lines
                        index = 0;
                        while (posY - index * this.atkForm.r3zoom > this.atkForm.rect3.Top)
                        {
                            if (posY - index * this.atkForm.r3zoom < this.atkForm.rect3.Bottom) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect3.Left, posY - index * this.atkForm.r3zoom, this.atkForm.rect3.Right, posY - index * this.atkForm.r3zoom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posY + index * this.atkForm.r3zoom < this.atkForm.rect3.Bottom)
                        {
                            if (posY + index * this.atkForm.r3zoom > this.atkForm.rect3.Top) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect3.Left, posY + index * this.atkForm.r3zoom, this.atkForm.rect3.Right, posY + index * this.atkForm.r3zoom);
                            index += this.scaling;
                        }

                        //      -- r4
                        posY = this.atkForm.rect4.Y + (int)this.atkForm.rect4.Height / 2 + this.atkForm.r4move.Y;
                        if (!(posY < this.atkForm.rect4.Top || posY > this.atkForm.rect4.Bottom)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect4.Left, posY, this.atkForm.rect4.Right, posY);

                        // smaller lines
                        index = 0;
                        while (posY - index * this.atkForm.r4zoom > this.atkForm.rect4.Top)
                        {
                            if (posY - index * this.atkForm.r4zoom < this.atkForm.rect4.Bottom) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect4.Left, posY - index * this.atkForm.r4zoom, this.atkForm.rect4.Right, posY - index * this.atkForm.r4zoom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posY + index * this.atkForm.r4zoom < this.atkForm.rect4.Bottom)
                        {
                            if (posY + index * this.atkForm.r4zoom > this.atkForm.rect4.Top) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect4.Left, posY + index * this.atkForm.r4zoom, this.atkForm.rect4.Right, posY + index * this.atkForm.r4zoom);
                            index += this.scaling;
                        }

                        // - Vertical

                        //      -- r1
                        posX = this.atkForm.rect1.X + this.atkForm.r1move.X + (int)this.atkForm.rect1.Width / 2;
                        if (!(posX < this.atkForm.rect1.Left || posX > this.atkForm.rect1.Right)) this.atkForm.grph.DrawLine(this.axis_pen, posX, this.atkForm.rect1.Top, posX, this.atkForm.rect1.Bottom);

                        // smaller lines
                        index = 0;
                        while (posX - index * this.atkForm.r1zoom > this.atkForm.rect1.Left)
                        {
                            if (posX - index * this.atkForm.r1zoom < this.atkForm.rect1.Right) this.atkForm.grph.DrawLine(this.company_pen, posX - index * this.atkForm.r1zoom, this.atkForm.rect1.Top, posX - index * this.atkForm.r1zoom, this.atkForm.rect1.Bottom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posX + index * this.atkForm.r1zoom < this.atkForm.rect1.Right)
                        {
                            if (posX + index * this.atkForm.r1zoom > this.atkForm.rect1.Left) this.atkForm.grph.DrawLine(this.company_pen, posX + index * this.atkForm.r1zoom, this.atkForm.rect1.Top, posX + index * this.atkForm.r1zoom, this.atkForm.rect1.Bottom);
                            index += this.scaling;
                        }

                        //      -- r2
                        posX = this.atkForm.rect2.X + this.atkForm.r2move.X + (int)this.atkForm.rect2.Width / 2;
                        if (!(posX < this.atkForm.rect2.Left || posX > this.atkForm.rect2.Right)) this.atkForm.grph.DrawLine(this.axis_pen, posX, this.atkForm.rect2.Top, posX, this.atkForm.rect2.Bottom);

                        // smaller lines
                        index = 0;
                        while (posX - index * this.atkForm.r2zoom > this.atkForm.rect2.Left)
                        {
                            if (posX - index * this.atkForm.r2zoom < this.atkForm.rect2.Right) this.atkForm.grph.DrawLine(this.company_pen, posX - index * this.atkForm.r2zoom, this.atkForm.rect2.Top, posX - index * this.atkForm.r2zoom, this.atkForm.rect2.Bottom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posX + index * this.atkForm.r2zoom < this.atkForm.rect2.Right)
                        {
                            if (posX + index * this.atkForm.r2zoom > this.atkForm.rect2.Left) this.atkForm.grph.DrawLine(this.company_pen, posX + index * this.atkForm.r2zoom, this.atkForm.rect2.Top, posX + index * this.atkForm.r2zoom, this.atkForm.rect2.Bottom);
                            index += this.scaling;
                        }

                        //      -- r3
                        posX = this.atkForm.rect3.X + this.atkForm.r3move.X + (int)this.atkForm.rect3.Width / 2;
                        if (!(posX < this.atkForm.rect3.Left || posX > this.atkForm.rect3.Right)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect3.Left + this.atkForm.r3move.X + (int)this.atkForm.rect3.Width / 2, this.atkForm.rect3.Top, this.atkForm.rect3.Left + this.atkForm.r3move.X + (int)this.atkForm.rect3.Width / 2, this.atkForm.rect3.Bottom);

                        // smaller lines
                        index = 0;
                        while (posX - index * this.atkForm.r3zoom > this.atkForm.rect3.Left)
                        {
                            if (posX - index * this.atkForm.r3zoom < this.atkForm.rect3.Right) this.atkForm.grph.DrawLine(this.company_pen, posX - index * this.atkForm.r3zoom, this.atkForm.rect3.Top, posX - index * this.atkForm.r3zoom, this.atkForm.rect3.Bottom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posX + index * this.atkForm.r3zoom < this.atkForm.rect3.Right)
                        {
                            if (posX + index * this.atkForm.r3zoom > this.atkForm.rect3.Left) this.atkForm.grph.DrawLine(this.company_pen, posX + index * this.atkForm.r3zoom, this.atkForm.rect3.Top, posX + index * this.atkForm.r3zoom, this.atkForm.rect3.Bottom);
                            index += this.scaling;
                        }

                        //      -- r4
                        posX = this.atkForm.rect4.X + this.atkForm.r4move.X + (int)this.atkForm.rect4.Width / 2;
                        if (!(posX < this.atkForm.rect4.Left || posX > this.atkForm.rect4.Right)) this.atkForm.grph.DrawLine(this.axis_pen, posX, this.atkForm.rect4.Top, posX, this.atkForm.rect4.Bottom);

                        // smaller lines
                        index = 0;
                        while (posX - index * this.atkForm.r4zoom > this.atkForm.rect4.Left)
                        {
                            if (posX - index * this.atkForm.r4zoom < this.atkForm.rect4.Right) this.atkForm.grph.DrawLine(this.company_pen, posX - index * this.atkForm.r4zoom, this.atkForm.rect4.Top, posX - index * this.atkForm.r4zoom, this.atkForm.rect4.Bottom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posX + index * this.atkForm.r4zoom < this.atkForm.rect4.Right)
                        {
                            if (posX + index * this.atkForm.r4zoom > this.atkForm.rect4.Left) this.atkForm.grph.DrawLine(this.company_pen, posX + index * this.atkForm.r4zoom, this.atkForm.rect4.Top, posX + index * this.atkForm.r4zoom, this.atkForm.rect4.Bottom);
                            index += this.scaling;
                        }

                        // Drawing graphs in the rectangles
                        // Drawing first graph
                        for (int i = 0; i < this.one_points.Count() && this.atkForm.running; i++)
                        {
                            for (int j = 0; j < this.one_points[i].Count() - 1 && this.atkForm.running; j++)
                            {
                                Point a = this.one_points[i].ElementAt(j);
                                Point b = this.one_points[i].ElementAt(j + 1);

                                // Applying zoom effect
                                a.X = (int)(a.X * this.atkForm.r1zoom * this.scaling);
                                a.Y = (int)(a.Y * this.atkForm.r1zoom * this.scaling);
                                b.X = (int)(b.X * this.atkForm.r1zoom * this.scaling);
                                b.Y = (int)(b.Y * this.atkForm.r1zoom * this.scaling);

                                // Moving points into rectangle
                                a.X += this.atkForm.rect1.Left; a.Y += this.atkForm.rect1.Top;
                                b.X += this.atkForm.rect1.Left; b.Y += this.atkForm.rect1.Top;

                                // Starting from the middle (vertically) of the rectangle, and a bit to the right
                                a.Y += (int)this.atkForm.rect1.Height / 2;
                                b.Y += (int)this.atkForm.rect1.Height / 2;
                                a.X += (int)this.atkForm.rect1.Width / 2;
                                b.X += (int)this.atkForm.rect1.Width / 2;

                                // Moving based on the mov items
                                a.X += this.atkForm.r1move.X;
                                a.Y += this.atkForm.r1move.Y;
                                b.X += this.atkForm.r1move.X;
                                b.Y += this.atkForm.r1move.Y;

                                // In case the line is out of the rectangle, I can skip the draw
                                if (b.X > this.atkForm.rect1.Right || b.X < this.atkForm.rect1.Left || b.Y > this.atkForm.rect1.Bottom || b.Y < this.atkForm.rect1.Top) continue;
                                if (a.X > this.atkForm.rect1.Right || a.X < this.atkForm.rect1.Left || a.Y > this.atkForm.rect1.Bottom || a.Y < this.atkForm.rect1.Top) continue;

                                this.atkForm.grph.DrawLine(this.pens[i % this.pens.Count()], a, b);
                            }
                        }

                        for (int i = 0; i < this.divided.Count() && this.atkForm.running; i++)
                        {
                            for (int j = 0; j < this.divided[i].Count() - 1 && this.atkForm.running; j++)
                            {
                                Point a = this.divided[i].ElementAt(j);
                                Point b = this.divided[i].ElementAt(j + 1);

                                // Applying zoom effect
                                a.X = (int)(a.X * this.atkForm.r2zoom * this.scaling);
                                a.Y = (int)(a.Y * this.atkForm.r2zoom * this.scaling);
                                b.X = (int)(b.X * this.atkForm.r2zoom * this.scaling);
                                b.Y = (int)(b.Y * this.atkForm.r2zoom * this.scaling);

                                // Moving points into rectangle
                                a.X += this.atkForm.rect2.Left; a.Y += this.atkForm.rect2.Top;
                                b.X += this.atkForm.rect2.Left; b.Y += this.atkForm.rect2.Top;

                                // Starting from the middle of the rectangle
                                a.X += (int)this.atkForm.rect2.Width / 2;
                                a.Y += (int)this.atkForm.rect2.Height / 2;
                                b.X += (int)this.atkForm.rect2.Width / 2;
                                b.Y += (int)this.atkForm.rect2.Height / 2;

                                // Moving based on the mov items
                                a.X += this.atkForm.r2move.X;
                                a.Y += this.atkForm.r2move.Y;
                                b.X += this.atkForm.r2move.X;
                                b.Y += this.atkForm.r2move.Y;

                                // In case it's out of the rectangle, I can skip the draw
                                if (b.X > this.atkForm.rect2.Right || b.X < this.atkForm.rect2.Left || b.Y > this.atkForm.rect2.Bottom || b.Y < this.atkForm.rect2.Top) continue;
                                if (a.X > this.atkForm.rect2.Right || a.X < this.atkForm.rect2.Left || a.Y > this.atkForm.rect2.Bottom || a.Y < this.atkForm.rect2.Top) continue;

                                this.atkForm.grph.DrawLine(this.pens[i % this.pens.Count()], a, b);
                            }
                        }

                        for (int i = 0; i < this.zero_points.Count() && this.atkForm.running; i++)
                        {
                            for (int j = 0; j < this.zero_points[i].Count() - 1 && this.atkForm.running; j++)
                            {
                                Point a = this.zero_points[i].ElementAt(j);
                                Point b = this.zero_points[i].ElementAt(j + 1);

                                // Applying zoom effect
                                a.X = (int)(a.X * this.atkForm.r3zoom * this.scaling);
                                a.Y = (int)(a.Y * this.atkForm.r3zoom * this.scaling);
                                b.X = (int)(b.X * this.atkForm.r3zoom * this.scaling);
                                b.Y = (int)(b.Y * this.atkForm.r3zoom * this.scaling);

                                // Moving points into rectangle
                                a.X += this.atkForm.rect3.Left; a.Y += this.atkForm.rect3.Top;
                                b.X += this.atkForm.rect3.Left; b.Y += this.atkForm.rect3.Top;

                                // Starting from the middle of the rectangle
                                a.X += (int)this.atkForm.rect3.Width / 2;
                                a.Y += (int)this.atkForm.rect3.Height / 2;
                                b.X += (int)this.atkForm.rect3.Width / 2;
                                b.Y += (int)this.atkForm.rect3.Height / 2;

                                // Moving based on the mov items
                                a.X += this.atkForm.r3move.X;
                                a.Y += this.atkForm.r3move.Y;
                                b.X += this.atkForm.r3move.X;
                                b.Y += this.atkForm.r3move.Y;

                                // In case it's out of the rectangle, I can skip the draw
                                if (b.X > this.atkForm.rect3.Right || b.X < this.atkForm.rect3.Left || b.Y > this.atkForm.rect3.Bottom || b.Y < this.atkForm.rect3.Top) continue;
                                if (a.X > this.atkForm.rect3.Right || a.X < this.atkForm.rect3.Left || a.Y > this.atkForm.rect3.Bottom || a.Y < this.atkForm.rect3.Top) continue;

                                this.atkForm.grph.DrawLine(this.pens[i % this.pens.Count()], a, b);
                            }
                        }

                        for (int i = 0; i < this.normalized.Count() && this.atkForm.running; i++)
                        {
                            for (int j = 0; j < this.normalized[i].Count() - 1 && this.atkForm.running; j++)
                            {
                                Point a = this.normalized[i].ElementAt(j);
                                Point b = this.normalized[i].ElementAt(j + 1);

                                // Applying zoom effect
                                a.X = (int)(a.X * this.atkForm.r4zoom * this.scaling);
                                a.Y = (int)(a.Y * this.atkForm.r4zoom * this.scaling);
                                b.X = (int)(b.X * this.atkForm.r4zoom * this.scaling);
                                b.Y = (int)(b.Y * this.atkForm.r4zoom * this.scaling);

                                // Moving points into rectangle
                                a.X += this.atkForm.rect4.Left; a.Y += this.atkForm.rect4.Top;
                                b.X += this.atkForm.rect4.Left; b.Y += this.atkForm.rect4.Top;

                                // Starting from the middle of the rectangle
                                a.X += (int)this.atkForm.rect4.Width / 2;
                                a.Y += (int)this.atkForm.rect4.Height / 2;
                                b.X += (int)this.atkForm.rect4.Width / 2;
                                b.Y += (int)this.atkForm.rect4.Height / 2;

                                // Moving based on the mov items
                                a.X += this.atkForm.r4move.X;
                                a.Y += this.atkForm.r4move.Y;
                                b.X += this.atkForm.r4move.X;
                                b.Y += this.atkForm.r4move.Y;

                                // In case it's out of the rectangle, I can skip the draw
                                if (b.X > this.atkForm.rect4.Right || b.X < this.atkForm.rect4.Left || b.Y > this.atkForm.rect4.Bottom || b.Y < this.atkForm.rect4.Top) continue;
                                if (a.X > this.atkForm.rect4.Right || a.X < this.atkForm.rect4.Left || a.Y > this.atkForm.rect4.Bottom || a.Y < this.atkForm.rect4.Top) continue;

                                this.atkForm.grph.DrawLine(this.pens[i % this.pens.Count()], a, b);
                            }
                        }

                        if(this.nAttacks != 0)
                        {
                            factorXHistogram2 = this.atkForm.rect2.Width / (2 * this.nAttacks);
                            factorXHistogram3 = this.atkForm.rect3.Width / (2 * this.nAttacks);
                            factorXHistogram4 = this.atkForm.rect4.Width / (2 * this.nAttacks);

                            foreach (var item in countZero_points)
                            {
                                x = this.aHistogram;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r3zoom * this.scaling);
                                y = (int)(y * this.atkForm.r3zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect3.Left; y += this.atkForm.rect3.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect3.Width / 2;
                                y += (int)this.atkForm.rect3.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r3move.X;
                                y += this.atkForm.r3move.Y;


                                this.zero_histo.X = x;
                                this.zero_histo.Y = y;
                                this.zero_histo.Width = (int)(item.Value * factorXHistogram3);
                                this.zero_histo.Height = (int)(this.atkForm.rect3.Height / (4 * this.nAttacks));

                                if ((x + this.zero_histo.Width) > this.atkForm.rect3.Right || x < this.atkForm.rect3.Left || y > this.atkForm.rect3.Bottom || y < this.atkForm.rect3.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.zero_histo);
                            }

                            foreach (var item in countDivided)
                            {
                                x = this.aHistogram;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r2zoom * this.scaling);
                                y = (int)(y * this.atkForm.r2zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect2.Left; y += this.atkForm.rect2.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect2.Width / 2;
                                y += (int)this.atkForm.rect2.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r2move.X;
                                y += this.atkForm.r2move.Y;


                                this.divided_histo.X = x;
                                this.divided_histo.Y = y;
                                this.divided_histo.Width = (int)(item.Value * factorXHistogram2);
                                this.divided_histo.Height = (int)(this.atkForm.rect2.Height / (6 * this.nAttacks));

                                if ((x + divided_histo.Width) > this.atkForm.rect2.Right || x < this.atkForm.rect2.Left || y > this.atkForm.rect2.Bottom || y < this.atkForm.rect2.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.divided_histo);
                            }

                            foreach (var item in countNormalized)
                            {
                                x = this.aHistogram;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r4zoom * this.scaling);
                                y = (int)(y * this.atkForm.r4zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect4.Left; y += this.atkForm.rect4.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect4.Width / 2;
                                y += (int)this.atkForm.rect4.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r4move.X;
                                y += this.atkForm.r4move.Y;

                                this.normalized_histo.X = x;
                                this.normalized_histo.Y = y;
                                this.normalized_histo.Width = (int)(item.Value * factorXHistogram4);
                                this.normalized_histo.Height = (int)(this.atkForm.rect4.Height / (6 * this.nAttacks));

                                if ((x + normalized_histo.Width) > this.atkForm.rect4.Right || x < this.atkForm.rect4.Left || y > this.atkForm.rect4.Bottom || y < this.atkForm.rect4.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.normalized_histo);
                            }

                            foreach (var item in countZero_points_end)
                            {
                                x = this.nAttacks;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r3zoom * this.scaling);
                                y = (int)(y * this.atkForm.r3zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect3.Left; y += this.atkForm.rect3.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect3.Width / 2;
                                y += (int)this.atkForm.rect3.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r3move.X;
                                y += this.atkForm.r3move.Y;

                                this.zero_histo_end.X = x;
                                this.zero_histo_end.Y = y;
                                this.zero_histo_end.Width = (int)(item.Value * factorXHistogram3);
                                this.zero_histo_end.Height = (int)(this.atkForm.rect3.Height / (4 * this.nAttacks));

                                if ((x + zero_histo_end.Width) > this.atkForm.rect3.Right || x < this.atkForm.rect3.Left || y > this.atkForm.rect3.Bottom || y < this.atkForm.rect3.Top) continue;


                                this.atkForm.grph.FillRectangle(this.histo_pen, this.zero_histo_end);
                            }

                            foreach (var item in countDivided_end)
                            {
                                x = this.nAttacks;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r2zoom * this.scaling);
                                y = (int)(y * this.atkForm.r2zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect2.Left; y += this.atkForm.rect2.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect2.Width / 2;
                                y += (int)this.atkForm.rect2.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r2move.X;
                                y += this.atkForm.r2move.Y;

                                this.divided_histo_end.X = x;
                                this.divided_histo_end.Y = y;
                                this.divided_histo_end.Width = (int)(item.Value * factorXHistogram2);
                                this.divided_histo_end.Height = (int)(this.atkForm.rect2.Height / (6 * this.nAttacks));

                                if ((x + divided_histo_end.Width) > this.atkForm.rect2.Right || x < this.atkForm.rect2.Left || y > this.atkForm.rect2.Bottom || y < this.atkForm.rect2.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.divided_histo_end);
                            }

                            foreach (var item in countNormalized_end)
                            {
                                x = this.nAttacks;
                                y = -(int)(item.Key * 0.5);

                                x = (int)(x * this.atkForm.r4zoom * this.scaling);
                                y = (int)(y * this.atkForm.r4zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect4.Left; y += this.atkForm.rect4.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect4.Width / 2;
                                y += (int)this.atkForm.rect4.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r4move.X;
                                y += this.atkForm.r4move.Y;

                                this.normalized_histo_end.X = x;
                                this.normalized_histo_end.Y = y;
                                this.normalized_histo_end.Width = (int)(item.Value * factorXHistogram4);
                                this.normalized_histo_end.Height = (int)(this.atkForm.rect4.Height / (6 * this.nAttacks));

                                if ((x + normalized_histo_end.Width) > this.atkForm.rect4.Right || x < this.atkForm.rect4.Left || y > this.atkForm.rect4.Bottom || y < this.atkForm.rect4.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.normalized_histo_end);
                            }
                        }

                        this.atkForm.picBox.Refresh();
                    }));
                }
                catch (ObjectDisposedException ex) { break; }

                sw.Stop();
                // Max ~30fps (delta time calculation)
                double sleeptime = 33.34 - sw.ElapsedMilliseconds;
                if (sleeptime > 0) Thread.Sleep((int)sleeptime);
                sw.Reset();
            }

            for (int i = 0; i < this.pens.Count(); i++) this.pens[i].Dispose();
            this.axis_pen.Dispose();
            this.company_pen.Dispose();
            this.rect1_pen.Dispose();
            this.rect2_pen.Dispose();
            this.rect3_pen.Dispose();
            this.rect4_pen.Dispose();
            this.histo_pen.Dispose();
            return;
        }
    }
}
