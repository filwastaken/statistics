namespace Drawing
{
    public partial class Form1 : Form
    {

        public Bitmap b;
        public Graphics g;

        public Form1()
        {
            InitializeComponent();
            this.b = new Bitmap(this.picBox.Width, this.picBox.Height);
            this.g = Graphics.FromImage(this.b);
            this.picBox.Image = this.b;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Black, 8);

            g.DrawLine(pen, 100, 600, 500, 600);
            g.DrawRectangle(pen, 100, 75, 400, 400);
            g.DrawArc(pen, 600, 125, 450, 450, 0, 360);
            pen.Dispose();
        }
    }
}