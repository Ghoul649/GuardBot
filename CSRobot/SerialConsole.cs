using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSRobot
{
    public partial class SerialConsole : Form
    {
        SerialPort RobotPort;
        public SerialConsole(SerialPort port)
        {
            InitializeComponent();
            RobotPort = port;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] sdata = textBox2.Text.Split(' ');
                textBox2.Text = "";
                byte[] data = new byte[sdata.Length];
                for (int i = 0; i < sdata.Length; i++)
                {
                    data[i] = byte.Parse(sdata[i]);
                }
                RobotPort.Write(data, 0, data.Length);
                textBox2.ForeColor = Color.Black;
            }
            catch
            {
                textBox2.ForeColor = Color.Red;
            }
        }
        List<string> strings = new List<string>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RobotPort.BytesToRead > 0)
            {
                strings.Add("" + RobotPort.ReadByte());
                if (strings.Count > 50)
                {
                    strings.RemoveAt(0);
                }
                string s = "";
                foreach (string st in strings)
                {
                    s += st + " \n";
                }
                textBox1.Text = s;
            }
        }



        private void SerialConsole_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string[] sdata = textBox2.Text.Split(' ');
                    textBox2.Text = "";
                    byte[] data = new byte[sdata.Length];
                    for (int i = 0; i < sdata.Length; i++)
                    {
                        data[i] = byte.Parse(sdata[i]);
                    }
                    RobotPort.Write(data, 0, data.Length);
                    textBox2.ForeColor = Color.Black;
                }
                catch 
                {
                    textBox2.ForeColor = Color.Red;
                }
            }
        }
    }
}
