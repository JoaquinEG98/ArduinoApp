using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using ArduinoApp.Models;
using System.Configuration;

namespace ArduinoApp
{
    public partial class Form1 : Form
    {
        SerialPort ArduinoPort;
        bool IsClosed = false;
        List<HumedadDTO> humedades;
        Thread hilo;
        System.Timers.Timer timerEncendido;

        public Form1()
        {
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
            this.btnEncender.Click += btnEncender_Click;
            this.btnApagar.Click += btnApagar_Click;

            humedades = new List<HumedadDTO>();

            ConfigurarTimer();
            ConectarArduino();
        }

        private void ConectarArduino()
        {
            ArduinoPort = new SerialPort();
            ArduinoPort.PortName = ConfigurationManager.AppSettings["PuertoArduino"];
            ArduinoPort.BaudRate = int.Parse(ConfigurationManager.AppSettings["BaudRate"]);
            ArduinoPort.DtrEnable = true;
            ArduinoPort.ReadTimeout = int.Parse(ConfigurationManager.AppSettings["IntervaloTimeout"]);

            try
            {
                ArduinoPort.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ConfigurarTimer()
        {
            timerEncendido = new System.Timers.Timer();
            timerEncendido.Interval = int.Parse(ConfigurationManager.AppSettings["IntervaloHumedad"]);
            timerEncendido.Enabled = true;
            timerEncendido.Elapsed += TimerEncendido_Elapsed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                hilo = new Thread(GetHumedad);
                hilo.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            try
            {
                IsClosed = false;

                if (hilo.ThreadState == ThreadState.Stopped)
                {
                    hilo = new Thread(GetHumedad);
                    hilo.Start();
                }

                timerEncendido.Start();
                CambiarLabelEstado(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void TimerEncendido_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ArduinoPort.Write("a");
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                IsClosed = true;

                if (hilo.ThreadState == ThreadState.Running) hilo.Abort();

                timerEncendido.Stop();

                humedades = new List<HumedadDTO>();
                LimpiarDataGrid();
                CambiarLabelEstado(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerEncendido.Stop();
            IsClosed = true;
            if (ArduinoPort.IsOpen) ArduinoPort.Close();
        }

        private void GetHumedad()
        {
            while (!IsClosed)
            {
                try
                {
                    string cadena = ArduinoPort.ReadLine();

                    if (cadena != null)
                    {
                        btnEncender.Invoke(new MethodInvoker(delegate
                        {
                            humedades.Add(HumedadDTO.FillObject(cadena));
                            CargarGrid();
                        }));
                    }
                }
                catch { }
            }
        }

        private void CargarGrid()
        {
            LimpiarDataGrid();
            dataGridView1.DataSource = humedades;
        }

        private void LimpiarDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void CambiarLabelEstado(bool estado)
        {
            switch (estado)
            {
                case true:
                    lblEstado.Text = "ENCENDIDO";
                    lblEstado.ForeColor = Color.Green;
                    break;
                case false:
                    lblEstado.Text = "APAGADO";
                    lblEstado.ForeColor = Color.Red;
                    break;
            }
        }
    }
}
