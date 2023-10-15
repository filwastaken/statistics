using System.Diagnostics;
using System;
using System.Drawing;

namespace nUniform
{
    public partial class nUniform : Form
    {

        private RandomPicker rdpick;

        public Bitmap bitmap;
        public Graphics grph;

        public nUniform()
        {
            InitializeComponent();
            this.bitmap = new Bitmap(this.bmContainer.Width, this.bmContainer.Height);
            this.grph = Graphics.FromImage(this.bitmap);
            this.bmContainer.Image = this.bitmap;

            this.rdpick = new RandomPicker(this.grph, this);

            this.stopbt.Enabled = false;
        }

        private void stopbt_Click(object sender, EventArgs e)
        {
            this.rdpick.allowed = false;
        }

        private void startbt_Click(object sender, EventArgs e)
        {
            this.rdpick.StartPick((int)this.nchooser.Value, (int)this.kchooser.Value);
        }
    }
}