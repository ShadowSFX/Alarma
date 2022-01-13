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
        string path2; 
        WMPLib.WindowsMediaPlayer wplayer;
        bool bTimeAdjusted = false;

        bool bEmpiezanMartingalas = false;
        int iSegundosMartingalas = 0;
        int iMinutosMartingalas = 0;

        int iMinVela = 0;
        int iSegundos = 0;
        int iMinutos = 0;

        public Form1()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory + "preview.mp3";
            path2 = AppDomain.CurrentDomain.BaseDirectory + "preview2.mp3";
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @path;
            wplayer.controls.stop();

            timer1.Stop();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTiempo.Enabled = false;
            txtPar.Enabled = false;
            txtEstrat.Enabled = false;
            button1.Enabled = false;
            btnDetener.Enabled = true;

            timer1.Stop();
            iSegundos = 0;
            iMinutos = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = Int32.Parse(txtTiempo.Text) * 60;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bTimeAdjusted)
            {
                ProcesarSegundos();
            }
            else {
                var date = DateTime.Now;                
                if (date.AddSeconds(45).Minute % 5 == 0) {
                    if (!checkBox1.Checked)
                        wplayer.controls.play();
                    
                    bTimeAdjusted = true;
                }
            }
        }

        private void ProcesarSegundos() {
            iSegundos++;
            progressBar1.Value++;

            if (bEmpiezanMartingalas) {
                iSegundosMartingalas++;

                if (iSegundosMartingalas == 60) {
                    iMinutosMartingalas++;
                    iSegundosMartingalas = 0;

                    if (radioButton2.Checked && iMinutosMartingalas == 1)
                    {
                        NotificacionExtra();
                        iMinutosMartingalas = 0;
                        iSegundosMartingalas = 0;
                        bEmpiezanMartingalas = false;
                    }

                    if (radioButton3.Checked && iMinutosMartingalas == 2)
                    {
                        NotificacionExtra();
                        iMinutosMartingalas = 0;
                        iSegundosMartingalas = 0;
                        bEmpiezanMartingalas = false;
                    }
                }
            }

            // notificación extra 15 segundos antes del minuto 6
            if (iSegundos == 30 && iMinutos == 0) 
                NotificacionExtra();

            if (iSegundos == 60)
            {
                iSegundos = 0;
                iMinutos++;
            }

            // primera notificación al minuto 5
            if (iMinutos == Int32.Parse(txtTiempo.Text))
            {
                bEmpiezanMartingalas = true;
                iMinutos = 0;
                progressBar1.Value = 0;
                if (!checkBox1.Checked)
                    wplayer.controls.play();
            }
        }

        private void NotificacionExtra() {
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = path2;
            wplayer.controls.play();

            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = path;
            wplayer.controls.stop();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            txtTiempo.Enabled = true;
            txtPar.Enabled = true;
            txtEstrat.Enabled = true;
            button1.Enabled = true;
            btnDetener.Enabled = false;

            timer1.Stop();            
        }
    }
}
