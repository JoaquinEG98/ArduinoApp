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
using ArduinoApp.Services;
using ArduinoApp.Models.DTOs;

namespace ArduinoApp
{
    public partial class Form1 : Form
    {
        #region Variables
        SerialPort ArduinoPort;
        bool IsClosed = true;
        List<HumedadDTO> humedades;
        Thread hilo;
        System.Timers.Timer timerEncendido;
        System.Timers.Timer timerCloud;
        private readonly LocalService _localService;

        #endregion

        public Form1(LocalService localService)
        {
            InitializeComponent();

            _localService = localService;

            this.FormClosing += Form1_FormClosing;
            this.btnEncender.Click += btnEncender_Click;
            this.btnApagar.Click += btnApagar_Click;

            humedades = new List<HumedadDTO>();

            ConfigurarTimer();
            ConectarArduino();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGridDatosNoSubidos();

                hilo = new Thread(GetHumedad);
                hilo.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Métodos
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
                            HumedadDTO humedad = HumedadDTO.FillObject(cadena);
                            humedades.Add(humedad);
                            CargarGridDatosCapturados();
                            _localService.InsertarLocal(humedad);
                            CargarGridDatosNoSubidos();
                        }));
                    }
                }
                catch { }
            }
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

        private void ApagarArduino()
        {
            timerEncendido.Stop();
            timerEncendido.Dispose();
            timerCloud.Stop();
            timerCloud.Dispose();

            if (hilo.ThreadState == ThreadState.Running) hilo.Abort();
            IsClosed = true;
            if (ArduinoPort.IsOpen)
            {
                ArduinoPort.Write("b");
                ArduinoPort.Close();
            }
        }
        #endregion

        #region Botones
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


        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoPort.Write("b");
                IsClosed = true;
                if (hilo.ThreadState == ThreadState.Running) hilo.Abort();
                timerEncendido.Stop();

                humedades = new List<HumedadDTO>();
                LimpiarGridDatosCapturados();
                CambiarLabelEstado(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Eventos
        private void TimerEncendido_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ArduinoPort.Write("a");
        }

        private void TimerCloud_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<Humedad> humedades = _localService.ObtenerNoSubidos();

            if (humedades.Count > 0)
            {
                foreach (Humedad humedad in humedades)
                {
                    _localService.MarcarSubido(humedad);
                }
            }

            CargarGridDatosNoSubidos();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApagarArduino();
            System.Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApagarArduino();
        }
        #endregion

        #region Tools
        private void ConfigurarTimer()
        {
            timerEncendido = new System.Timers.Timer();
            timerEncendido.Interval = int.Parse(ConfigurationManager.AppSettings["IntervaloHumedad"]);
            timerEncendido.Enabled = false;
            timerEncendido.Elapsed += TimerEncendido_Elapsed;

            timerCloud = new System.Timers.Timer();
            timerCloud.Interval = int.Parse(ConfigurationManager.AppSettings["IntervaloCloud"]);
            timerCloud.Enabled = false;
            timerCloud.Start();
            timerCloud.Elapsed += TimerCloud_Elapsed;
        }

        private void CargarGridDatosCapturados()
        {
            LimpiarGridDatosCapturados();
            dataGridDatosCapturados.DataSource = humedades;
        }

        private void LimpiarGridDatosCapturados()
        {
            dataGridDatosCapturados.DataSource = null;
            dataGridDatosCapturados.Rows.Clear();
        }

        private void CargarGridDatosNoSubidos()
        {
            dataGridDatosNoSubidos.Invoke(new MethodInvoker(delegate
            {
                LimpiarGridDatosNoSubidos();
                List<Humedad> NoSubidos = _localService.ObtenerNoSubidos();

                if (NoSubidos.Count > 0)
                {
                    dataGridDatosNoSubidos.DataSource = DatosNoSubidosDTO.FillListDTO(NoSubidos);
                    dataGridDatosNoSubidos.Columns["Id"].Visible = false;
                }
            }));
        }

        private void LimpiarGridDatosNoSubidos()
        {
            dataGridDatosNoSubidos.DataSource = null;
            dataGridDatosNoSubidos.Rows.Clear();
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
        #endregion
    }
}
