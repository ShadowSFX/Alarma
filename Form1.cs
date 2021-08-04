using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarma
{
    public partial class Form1 : Form
    {
        string path;
        WMPLib.WindowsMediaPlayer wplayer;

        public Form1()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory + "preview.mp3";
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @path;

            timer1.Stop();
            timer2.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = Int32.Parse(txtTiempo.Text) * 60000;
            timer1.Start();

            timer2.Stop();
            timer2.Interval = 1000;
            progressBar1.Value = 0;
            progressBar1.Maximum = Int32.Parse(txtTiempo.Text) * 60;
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            wplayer.controls.play();            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {            
            if (progressBar1.Value < progressBar1.Maximum)
                progressBar1.Value++;
        }
    }
}
