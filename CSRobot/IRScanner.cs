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
    public partial class IRScanner : Form
    {
        public SerialPort RobotPort;
        public IRScanner(SerialPort port)
        {
            InitializeComponent();
            RobotPort = port;
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Value -= trackBar1.Value % trackBar5.Value;
            label1.Text = "Start X: " + trackBar1.Value;
            calculate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value < trackBar1.Value)
                trackBar2.Value += trackBar1.Value - trackBar2.Value;
            trackBar2.Value -= trackBar2.Value % trackBar5.Value;
            label2.Text = "End X: " + trackBar2.Value;
            calculate();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar3.Value -= trackBar3.Value % trackBar5.Value;
            label3.Text = "Start Y: " + trackBar3.Value;
            calculate();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar4.Value < trackBar3.Value)
                trackBar4.Value += trackBar3.Value - trackBar4.Value;
            trackBar4.Value -= trackBar4.Value % trackBar5.Value;
            label4.Text = "End Y: " + trackBar4.Value;
            calculate();
        }
        private void calculate()
        {
            int xs = (trackBar2.Value - trackBar1.Value) / trackBar5.Value;
            int ys = (trackBar4.Value - trackBar3.Value) / trackBar5.Value;
            int addel = 0;
            int k = 0;
            for (int i = trackBar7.Value; i > 0; i -= trackBar8.Value)
            {
                if (i > 0)
                    addel += i;
                k++;
                if (k > xs)
                    break;
            }
            k = (xs * ys * trackBar6.Value + addel * ys) / 1000;
            addel = k / 60;
            k = k % 60;
            label9.Text = "Time : " + addel + " : " + k;



        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label5.Text = "Step: " + trackBar5.Value;
            trackBar1.TickFrequency = trackBar5.Value;
            trackBar2.TickFrequency = trackBar5.Value;
            trackBar3.TickFrequency = trackBar5.Value;
            trackBar4.TickFrequency = trackBar5.Value;

            trackBar1.Value -= trackBar1.Value % trackBar5.Value;
            label1.Text = "Start X: " + trackBar1.Value;
            trackBar2.Value -= trackBar2.Value % trackBar5.Value;
            label2.Text = "End X: " + trackBar2.Value;
            trackBar3.Value -= trackBar3.Value % trackBar5.Value;
            label3.Text = "Start Y: " + trackBar3.Value;
            trackBar4.Value -= trackBar4.Value % trackBar5.Value;
            label4.Text = "End Y: " + trackBar4.Value;
            calculate();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            label6.Text = "Delay: " + trackBar6.Value;
            calculate();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            if (trackBar8.Value != 0)
                trackBar7.Value -= trackBar7.Value % trackBar8.Value;
            label7.Text = "Addition delay: " + trackBar7.Value;
            calculate();
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            label8.Text = "Acceleration: " + trackBar8.Value;
            trackBar7.TickFrequency = trackBar8.Value;
            if (trackBar8.Value != 0)
                trackBar7.Value -= trackBar7.Value % trackBar8.Value;
            label7.Text = "Addition delay: " + trackBar7.Value;
            calculate();
        }

        Bitmap image;
        int I = 0;
        int J = 0;
        int IM = 0;
        int JM = 0;
        public void Draw()
        {
            int xs = (trackBar2.Value - trackBar1.Value) / trackBar5.Value;
            int ys = (trackBar4.Value - trackBar3.Value) / trackBar5.Value;
            if (xs > 0 && ys > 0 && image != null)
            {
                Graphics g = panel1.CreateGraphics();
                if (xs > ys)
                {
                    float a = ys / (float)xs;
                    xs = 360;
                    ys = (int)(360 * a);
                }
                else
                {
                    float a = xs / (float)ys;
                    xs = (int)(360 * a);
                    ys = 360;
                }
                g.DrawImage(image,10, 10, xs, ys);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int xs = (trackBar2.Value - trackBar1.Value) / trackBar5.Value;
            int ys = (trackBar4.Value - trackBar3.Value) / trackBar5.Value;
            image = new Bitmap(xs, ys);
            Draw();
            I = 0;
            J = 0;
            JM = xs;
            IM = ys;
            timer1.Enabled = true;
            matrix = new float[xs, ys];
            RobotPort.DiscardInBuffer();
            RobotPort.Write(new byte[9] { 1, (byte)trackBar1.Value, (byte)trackBar2.Value, (byte)trackBar3.Value, (byte)trackBar4.Value, (byte)trackBar5.Value, (byte)trackBar6.Value, (byte)trackBar7.Value, (byte)trackBar8.Value }, 0, 9);
            RobotPort.ReadByte();
            dir = false;
        }
        float tempMin = 19;
        float tempMax = 35;
        bool dir = true;
        bool updated = false;
        Color colorFunc(float val)
        {
            val = (val - tempMin) / (tempMax - tempMin);
            if (val < 0)
                val = 0;
            if (val > 1)
                val = 1;
            if (val < 0.333f)
            {
                val *= 3;
                return Color.FromArgb(0, 0, 100 + (int)(val * 155));
            }
            else if (val < 0.666f)
            {
                val -= 0.333f;
                val *= 3;
                return Color.FromArgb(0, (int)(val * 255), 255 - (int)(val * 255));
            }
            else
            {
                val -= 0.666f;
                val *= 3;
                return Color.FromArgb((int)(val * 255), 255 - (int)(val * 255), 0);
            }
        }
        float[,] matrix;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RobotPort.BytesToRead > 1)
            {
                if (I >= IM)
                {
                    timer1.Enabled = false;
                    return;
                }
                if (!dir && J >= JM)
                {
                    J = JM - 1;
                    dir = !dir;
                    I++;
                    return;
                }
                else if (dir && J < 0)
                {
                    dir = !dir;
                    J = 0;
                    I++;
                    return;
                }
                float val = 0;
                val = (RobotPort.ReadByte() * 256 + RobotPort.ReadByte()) * 0.02f - 273.16f;
                matrix[JM - J - 1, I] = val;
                image.SetPixel(JM - J - 1, I, colorFunc(val));


                updated = true;
                if (!dir)
                    J++;
                else
                    J--;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                if (updated)
                    Draw();
        }
        void redraw() 
        {
            image = new Bitmap(JM, IM);
            
            for (int i = 0; i < IM; i++) 
            {
                for (int j = 0; j < JM; j++) 
                {
                    image.SetPixel(j, i, colorFunc(matrix[j, i]));
                }
            }
            Draw();
        }
        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            if (trackBar9.Value > trackBar10.Value)
                trackBar9.Value = trackBar10.Value;
            label10.Text = "Min: " + trackBar9.Value/5f;
            tempMin = trackBar9.Value/5f;
            if (matrix != null && JM > 0 && IM > 0) 
            {
                redraw();
            }
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            label11.Text = "Max: " + trackBar10.Value/5f;
            tempMax = trackBar10.Value/5f;
            if (matrix != null && JM > 0 && IM > 0)
            {
                redraw();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RobotPort.Write(new byte[1] { 200 }, 0, 1);
            timer1.Enabled = false;
            RobotPort.DiscardInBuffer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (image != null) 
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
                image.Save(saveFileDialog1.FileName);
        }
    }
}
