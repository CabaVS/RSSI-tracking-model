using System;
using System.Drawing;
using System.Windows.Forms;

namespace ViewNS
{
    public partial class Form1 : Form
    {
        public event EventHandler ButtonStartClick;
        public event EventHandler ButtonPauseClick;
        public event EventHandler ButtonShowGridClick;

        public Form1()
        {
            InitializeComponent();

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void  buttonStart_Click(object sender, EventArgs e)
        {
            ButtonStartClick(sender, e);
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            ButtonPauseClick(sender, e);
        }

        private void buttonShowGrid_Click(object sender, EventArgs e)
        {
            ButtonShowGridClick(sender, e);
        }
    }
}
