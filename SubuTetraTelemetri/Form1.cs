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
using System.Globalization;

namespace SubuTetraTelemetri
{
    public partial class Form1 : Form
    {
        SerialPort port = new SerialPort();
        string okunan;
        private void PortlariListele()
        {
            string[] ports = SerialPort.GetPortNames();
            portListCombo.Items.AddRange(ports);
            baudRateTextBox.Text = "115200";
        }
        private void PortKonf()
        {
            port.PortName = portListCombo.SelectedItem.ToString();
            port.BaudRate = Convert.ToInt32(baudRateTextBox.Text);
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.Open();
        }
        private void VeriOku(byte[] okunanHex)
        {
            okunan = BitConverter.ToString(okunanHex);
            
            string[] atanacakVeriler = okunan.Split("-");
            if(atanacakVeriler.Length == 25)
            {
                
            }
            
        }
        public Form1()
        {
            InitializeComponent();
        }
        

        private void portBaglanButton_Click(object sender, EventArgs e)
        {
            PortKonf();
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PortlariListele();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int length = port.BytesToRead;
            byte[] buf = new byte[length];
            port.Read(buf, 0, length);
            VeriOku(buf);
        }

        private void portDisconnectButton_Click(object sender, EventArgs e)
        {
            port.Close();
            timer1.Enabled = false;
        }

        private void dataDisplayTextbox_TextChanged(object sender, EventArgs e)
        {
            dataDisplayTextbox.SelectionStart = dataDisplayTextbox.Text.Length;
            dataDisplayTextbox.ScrollToCaret();
        }
    }
}
