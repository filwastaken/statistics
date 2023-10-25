using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Threading;

namespace pAttacks
{
    public partial class AttackForm : Form
    {
        public Bitmap bitmap;
        public Graphics grph;

        private Attack attck;

        public Rectangle rect1;
        public Rectangle rect2;
        public Rectangle rect3;
        public Rectangle rect4;

        public float r1zoom = 1.0f;
        public float r2zoom = 1.0f;
        public float r3zoom = 1.0f;
        public float r4zoom = 1.0f;

        private int edgeprecision = 12;

        public Point r1move = new(0, 0);
        public Point r2move = new(0, 0);
        public Point r3move = new(0, 0);
        public Point r4move = new(0, 0);

        public bool running;


        public AttackForm()
        {
            InitializeComponent();

            this.bitmap = new Bitmap(this.picBox.Width, this.picBox.Height);
            this.grph = Graphics.FromImage(this.bitmap);
            this.picBox.Image = this.bitmap;

            this.picBox.MouseWheel += new MouseEventHandler(MouseWheelFunc);

            this.cancBT.Enabled = false;
            this.rect1 = new Rectangle(10, 10, 540, 360);
            this.rect2 = new Rectangle(560, 10, 540, 360);
            this.rect3 = new Rectangle(10, 380, 540, 360);
            this.rect4 = new Rectangle(560, 380, 540, 360);

            this.running = true;
            this.attck = new Attack(this);
        }

        private void startBT_Click(object sender, EventArgs e)
        {
            this.attck.startAttacks((int)this.nValue.Value, (double)this.pValue.Value, (int)this.mSystems.Value, (int)this.A.Value);
        }

        private void cancBT_Click(object sender, EventArgs e)
        {
            this.attck.cancelAttack();
        }

        private void closingFunc(object sender, FormClosingEventArgs e)
        {
            this.running = false;
            this.Dispose();
        }

        private void resizePicBox(object sender, EventArgs e)
        {
            this.picBox.Width = this.Width;
            this.picBox.Height = this.Height - this.picBox.Top;
            this.bitmap = new Bitmap(this.bitmap, new Size(this.picBox.Width, this.picBox.Height));
            this.grph = Graphics.FromImage(this.bitmap);
            this.picBox.Image = this.bitmap;
        }

        private bool holding = false;
        private bool sliding = false;
        private bool resizing = false;

        private byte currentRect;
        private int edge;
        private Point previous;


        private void mouseDown(object sender, MouseEventArgs e)
        {
            findRectangle(e.Location);
            if (this.currentRect == 4) return;

            if (e.Button == MouseButtons.Left)
            {
                if (this.resizing) return;

                if (this.currentRect == 0) this.edge = inEdge(this.rect1, e.Location);
                else if (this.currentRect == 1) this.edge = inEdge(this.rect2, e.Location);
                else if (this.currentRect == 2) this.edge = inEdge(this.rect3, e.Location);
                else this.edge = inEdge(this.rect4, e.Location);

                if (this.edge == 8) this.holding = true;
                else this.resizing = true; // In case I'm pressing down on an edge, I resize the rectangle instead of moving it
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.sliding = true;

                switch (this.currentRect)
                {
                    case 0:
                        this.r1move.X += e.Location.X - this.previous.X;
                        this.r1move.Y += e.Location.Y - this.previous.Y;
                        break;
                    case 1:
                        this.r2move.X += e.Location.X - this.previous.X;
                        this.r2move.Y += e.Location.Y - this.previous.Y;
                        break;
                    case 2:
                        this.r3move.X += e.Location.X - this.previous.X;
                        this.r3move.Y += e.Location.Y - this.previous.Y;
                        break;
                    case 3:
                        this.r4move.X += e.Location.X - this.previous.X;
                        this.r4move.Y += e.Location.Y - this.previous.Y;
                        break;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                switch (this.currentRect)
                {
                    case 0:
                        this.r1move.X = 0;
                        this.r1move.Y = 0;
                        this.r1zoom = 1.0f;
                        break;
                    case 1:
                        this.r2move.X = 0;
                        this.r2move.Y = 0;
                        this.r2zoom = 1.0f;
                        break;
                    case 2:
                        this.r3move.X = 0;
                        this.r3move.Y = 0;
                        this.r3zoom = 1.0f;
                        break;
                    case 3:
                        this.r4move.X = 0;
                        this.r4move.Y = 0;
                        this.r4zoom = 1.0f;
                        break;
                }
            }

            this.previous = e.Location;
        }

        private void MouseWheelFunc(object sender, MouseEventArgs e)
        {
            findRectangle(e.Location);
            switch (this.currentRect)
            {
                case 0:
                    if (this.r1zoom == float.MaxValue) break;

                    if (e.Delta == 120) this.r1zoom += 0.1f;
                    else if (this.r1zoom >= 1.0f) this.r1zoom -= 0.1f;
                    break;
                case 1:
                    if (this.r2zoom == float.MaxValue) break;

                    if (e.Delta == 120) this.r2zoom += 0.1f;
                    else if (this.r2zoom >= 1.0f) this.r2zoom -= 0.1f;
                    break;
                case 2:
                    if (this.r3zoom == float.MaxValue) break;

                    if (e.Delta == 120) this.r3zoom += 0.1f;
                    else if (this.r3zoom >= 1.0f) this.r3zoom -= 0.1f;
                    break;
                case 3:
                    if (this.r4zoom == float.MaxValue) break;

                    if (e.Delta == 120) this.r4zoom += 0.1f;
                    else if (this.r4zoom >= 1.0f) this.r4zoom -= 0.1f;
                    break;
            }
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (inEdge(this.rect1, e.Location) != 8 || inEdge(this.rect2, e.Location) != 8 || inEdge(this.rect3, e.Location) != 8 || inEdge(this.rect4, e.Location) != 8) Cursor.Current = Cursors.Arrow;

            int moveX = (int)(e.X - previous.X);
            int moveY = (int)(e.Y - previous.Y);

            if (this.sliding)
            {
                switch (this.currentRect)
                {
                    case 0:
                        this.r1move.X += moveX;
                        this.r1move.Y += moveY;
                        break;
                    case 1:
                        this.r2move.X += moveX;
                        this.r2move.Y += moveY;
                        break;
                    case 2:
                        this.r3move.X += moveX;
                        this.r3move.Y += moveY;
                        break;
                    case 3:
                        this.r4move.X += moveX;
                        this.r4move.Y += moveY;
                        break;
                }
            }
            else if (this.holding)
            {
                switch (this.currentRect)
                {
                    case 0:
                        this.rect1.X += moveX;
                        this.rect1.Y += moveY;
                        break;
                    case 1:
                        this.rect2.X += moveX;
                        this.rect2.Y += moveY;
                        break;
                    case 2:
                        this.rect3.X += moveX;
                        this.rect3.Y += moveY;
                        break;
                    case 3:
                        this.rect4.X += moveX;
                        this.rect4.Y += moveY;
                        break;
                }
            }
            else if (this.resizing)
            {
                switch (this.edge)
                {
                    case 0:
                        if (this.currentRect == 0) { this.rect1.X += moveX; this.rect1.Width -= moveX; this.rect1.Y += moveY; this.rect1.Height -= moveY; }
                        else if (this.currentRect == 1) { this.rect2.X += moveX; this.rect2.Width -= moveX; this.rect2.Y += moveY; this.rect2.Height -= moveY; }
                        else if (this.currentRect == 2) { this.rect3.X += moveX; this.rect3.Width -= moveX; this.rect3.Y += moveY; this.rect3.Height -= moveY; }
                        else if (this.currentRect == 3) { this.rect4.X += moveX; this.rect4.Width -= moveX; this.rect4.Y += moveY; this.rect4.Height -= moveY; }
                        break;
                    case 1:
                        if (this.currentRect == 0) { this.rect1.Width += moveX; this.rect1.Y += moveY; this.rect1.Height -= moveY; }
                        else if (this.currentRect == 1) { this.rect2.Width += moveX; this.rect2.Y += moveY; this.rect2.Height -= moveY; }
                        else if (this.currentRect == 2) { this.rect3.Width += moveX; this.rect3.Y += moveY; this.rect3.Height -= moveY; }
                        else if (this.currentRect == 3) { this.rect4.Width += moveX; this.rect4.Y += moveY; this.rect4.Height -= moveY; }
                        break;
                    case 2:
                        if (this.currentRect == 0) { this.rect1.Width += moveX; this.rect1.Height += moveY; }
                        else if (this.currentRect == 1) { this.rect2.Width += moveX; this.rect2.Height += moveY; }
                        else if (this.currentRect == 2) { this.rect3.Width += moveX; this.rect3.Height += moveY; }
                        else if (this.currentRect == 3) { this.rect4.Width += moveX; this.rect4.Height += moveY; }
                        break;
                    case 3:
                        if (this.currentRect == 0) { this.rect1.X += moveX; this.rect1.Width -= moveX; this.rect1.Height += moveY; }
                        else if (this.currentRect == 1) { this.rect2.X += moveX; this.rect2.Width -= moveX; this.rect2.Height += moveY; }
                        else if (this.currentRect == 2) { this.rect3.X += moveX; this.rect3.Width -= moveX; this.rect3.Height += moveY; }
                        else if (this.currentRect == 3) { this.rect4.X += moveX; this.rect4.Width -= moveX; this.rect4.Height += moveY; }
                        break;
                    case 4:
                        if (this.currentRect == 0) { this.rect1.Y += moveY; this.rect1.Height -= moveY; }
                        else if (this.currentRect == 1) { this.rect2.Y += moveY; this.rect2.Height -= moveY; }
                        else if (this.currentRect == 2) { this.rect3.Y += moveY; this.rect3.Height -= moveY; }
                        else if (this.currentRect == 3) { this.rect4.Y += moveY; this.rect4.Height -= moveY; }
                        break;
                    case 5:
                        if (this.currentRect == 0) { this.rect1.Width += moveX; }
                        else if (this.currentRect == 1) { this.rect2.Width += moveX; }
                        else if (this.currentRect == 2) { this.rect3.Width += moveX; }
                        else if (this.currentRect == 3) { this.rect4.Width += moveX; }
                        break;
                    case 6:
                        if (this.currentRect == 0) { this.rect1.Height += moveY; }
                        else if (this.currentRect == 1) { this.rect2.Height += moveY; }
                        else if (this.currentRect == 2) { this.rect3.Height += moveY; }
                        else if (this.currentRect == 3) { this.rect4.Height += moveY; }
                        break;
                    case 7:
                        if (this.currentRect == 0) { this.rect1.X += moveX; this.rect1.Width -= moveX; }
                        else if (this.currentRect == 1) { this.rect2.X += moveX; this.rect2.Width -= moveX; }
                        else if (this.currentRect == 2) { this.rect3.X += moveX; this.rect3.Width -= moveX; }
                        else if (this.currentRect == 3) { this.rect4.X += moveX; this.rect4.Width -= moveX; }
                        break;
                }
            }

            this.previous = e.Location;
        }

        private void mouseUp(object sender, MouseEventArgs e) { this.holding = false; this.sliding = false; this.resizing = false; }

        private void findRectangle(Point p)
        {
            if (this.rect1.Contains(p)) this.currentRect = 0;
            else if (this.rect2.Contains(p)) this.currentRect = 1;
            else if (this.rect3.Contains(p)) this.currentRect = 2;
            else if (this.rect4.Contains(p)) this.currentRect = 3;
            else this.currentRect = 4;
        }

        private byte inEdge(Rectangle rect, Point p)
        {
            // In a corner
            if (Math.Pow(rect.Left - p.X, 2) + Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 0;
            else if (Math.Pow(rect.Right - p.X, 2) + Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 1;
            else if (Math.Pow(rect.Right - p.X, 2) + Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 2;
            else if (Math.Pow(rect.Left - p.X, 2) + Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 3;

            // In an edge
            if (Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 4;
            else if (Math.Pow(rect.Right - p.X, 2) <= Math.Pow(this.edgeprecision, 2)) return 5;
            else if (Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 6;
            else if (Math.Pow(rect.Left - p.X, 2) <= Math.Pow(this.edgeprecision, 2)) return 7;
            return 8;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}