namespace Drawing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 8);

            g.DrawLine(pen, 100, 600, 500, 600);
            g.DrawRectangle(pen, 100, 75, 400, 400);
            g.DrawArc(pen, 600, 125, 450, 450, 0, 360);


            pen.Dispose();
        }
    }
}