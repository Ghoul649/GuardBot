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

namespace CSRobot
{
    public partial class Form1 : Form
    {
        public SerialPort RobotPort;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];
            else
                comboBox1.SelectedItem = null;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];
            else
                comboBox1.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox1.Enabled = false;
            button2.Text = "Connecting...";
            SerialPort port = new SerialPort((string)comboBox1.SelectedItem);
            try
            {
                port.Open();
                RobotPort = port;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            catch (Exception ex)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                comboBox1.Enabled = true;
                button2.Text = "Connect";
                button2.ForeColor = Color.Red;
                port.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ManualController c = new ManualController(RobotPort);
            c.ShowDialog();
            c.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IRScanner c = new IRScanner(RobotPort);
            c.ShowDialog();
            c.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SerialConsole s = new SerialConsole(RobotPort);
            s.ShowDialog();
        }
    }
}
