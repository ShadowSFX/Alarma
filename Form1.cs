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

        int iSegundos = 0;
        int iMinutos = 0;

        public Form1()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory + "preview.mp3";
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @path;

            timer1.Stop();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPar.Enabled = false;
            txtEstrat.Enabled = false;

            timer1.Stop();
            iSegundos = 0;
            iMinutos = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = Int32.Parse(txtTiempo.Text) * 60;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            iSegundos++;
            progressBar1.Value++;

            if (iSegundos == 60) {
                iSegundos = 0;
                iMinutos++;
            }

            if (iMinutos == Int32.Parse(txtTiempo.Text)) {
                iMinutos = 0;
                progressBar1.Value = 0;
                wplayer.controls.play();
            }                      
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            txtPar.Enabled = true;
            txtEstrat.Enabled = true;
        }
    }
}
