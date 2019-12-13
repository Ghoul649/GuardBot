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
    public partial class ManualController : Form
    {
        public SerialPort RobotPort;
        
        public bool LMB = false;
        public bool RMB = false;
        public bool KW = false;
        public bool KS = false;
        public bool KA = false;
        public bool KD = false;
        public bool KE = false;
        public bool KQ = false;
        public bool KR = false;
        public bool KShift = false;

        public ManualController(SerialPort s)
        {
            InitializeComponent();
            buffer[0] = 2;
            RobotPort = s;
            RobotPort.DiscardInBuffer();
            RobotPort.Write(buffer, 0, 1);
        }

        private void ManualController_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ManualController_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                KW = true;
            else if (e.KeyCode == Keys.S)
                KS = true;
            else if (e.KeyCode == Keys.A)
                KA = true;
            else if (e.KeyCode == Keys.D)
                KD = true;
            else if (e.KeyCode == Keys.E)
                KE = true;
            else if (e.KeyCode == Keys.Q)
                KQ = true;
            else if (e.KeyCode == Keys.R)
                KR = true;
            else if (e.KeyCode == Keys.ShiftKey)
                KShift = true;
            else if (e.KeyCode == Keys.Space)
            {
                UI = !UI;
                if (UI)
                {
                    button1.Enabled = false;
                    checkBox3.Enabled = false;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = true;
                    trackBar1.Enabled = true;
                }
                else 
                {
                    button1.Enabled = false;
                    checkBox3.Enabled = false;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;
                    trackBar1.Enabled = false;
                }
            }
        }

        private void ManualController_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        Point winStart;
        Point winCenter;

        byte TXMax = 180;
        byte TXMin = 0;
        byte TYMax = 180;
        byte TYMin = 0;
        byte TX = 90;
        byte TY = 90;
        byte TLX = 90;
        byte TLY = 90;
        void towerUpdate(bool pressed) 
        {
            if (pressed) 
            {
                Cursor.Position = new Point(TX * Width / (TXMax - TXMin) + winStart.X, TY * Height / (TYMax - TYMin) + winStart.Y);
            }
            if (!RMB)
                return;
            TLX = TX;
            TLY = TY;
            TX = (byte)((Cursor.Position.X - winStart.X) * (TXMax - TXMin) / Width);
            TY = (byte)((Cursor.Position.Y - winStart.Y) * (TYMax - TYMin) / Height);
            if (TX > 180)
                TX = 180;
            if (TY > 180)
                TY = 180;
            if (TLX != TX) 
            {
                label1.Text = "X " + TX;
                buffer[0] = 11;
                if (!checkBox1.Checked)
                    buffer[1] = (byte)(TXMax - TX);
                else
                    buffer[1] = TX;
                RobotPort.Write(buffer, 0, 2);
            }
            if (TLY != TY) 
            {
                label2.Text = "Y " + TY;
                buffer[0] = 12;
                if (checkBox2.Checked)
                    buffer[1] = (byte)(TYMax - TY);
                else
                    buffer[1] = TY;
                RobotPort.Write(buffer, 0, 2);
            }

        }
        int RW = 0;
        int LW = 0;
        int LRW = 0;
        int LLW = 0;
        float T = 0;
        float F = 0;
        float x = 0;
        float y = 0;
        bool lKShift = false;
        void wheelsUpdate()
        {
            if (!KShift)
            {
                if (KW)
                {
                    if (F < trackBar6.Value)
                        F += trackBar2.Value / 2;
                }
                else if (KS)
                {
                    if (F > -trackBar6.Value)
                        F -= trackBar2.Value / 2;
                }
                else
                {
                    if (Math.Abs(F) < trackBar3.Value / 2)
                        F = 0;
                    else if (F > 0)
                        F -= trackBar3.Value / 2;
                    else if (F < 0)
                        F += trackBar3.Value / 2;
                }
                if (F > trackBar6.Value)
                    F = trackBar6.Value;
                else if (F < -trackBar6.Value)
                    F = -trackBar6.Value;
                if (KD)
                {
                    if (T < trackBar6.Value)
                        T += trackBar4.Value / 2;
                }
                else if (KA)
                {
                    if (T > -trackBar6.Value)
                        T -= trackBar4.Value / 2;
                }
                else
                {
                    if (Math.Abs(T) < trackBar5.Value / 2)
                        T = 0;
                    else if (T > 0)
                        T -= trackBar5.Value / 2;
                    else if (T < 0)
                        T += trackBar5.Value / 2;
                }
                if (T > trackBar6.Value)
                    T = trackBar6.Value;
                else if (T < -trackBar6.Value)
                    T = -trackBar6.Value;
                lKShift = false;
            }
            else
            {
                if (!lKShift)
                {
                    Cursor.Position = new Point(winCenter.X + (int)(0.5f * Width * T / trackBar6.Value), winCenter.Y - (int)(0.5f * Height * F / trackBar6.Value));
                    lKShift = true;
                }
                else
                {
                    T = Cursor.Position.X - winCenter.X;
                    T = 510 * T / Width;
                    if (T > trackBar6.Value)
                    {
                        T = trackBar6.Value;
                        x = trackBar6.Value;
                    }
                    else if (T < -trackBar6.Value)
                    {
                        T = -trackBar6.Value;
                        x = -trackBar6.Value;
                    }
                    else 
                    {
                        x = T;
                    }
                    F = Cursor.Position.Y - winCenter.Y;
                    F = -510 * F / Height;
                    if (F > trackBar6.Value)
                    {
                        F = trackBar6.Value;
                        y = trackBar6.Value;
                    }
                    else if (F < -trackBar6.Value)
                    {
                        F = -trackBar6.Value;
                        y = -trackBar6.Value;
                    }
                    else
                        y = F;

                    if (x > trackBar5.Value)
                        x -= trackBar5.Value;
                    else if (x < -trackBar5.Value)
                        x += trackBar5.Value;
                    else
                        x = 0;

                    if (y > trackBar3.Value)
                        y -= trackBar3.Value;
                    else if (y < -trackBar3.Value)
                        y += trackBar3.Value;
                    else
                        y = 0;
                    Cursor.Position = new Point(winCenter.X + (int)(0.5f * Width * x / trackBar6.Value), winCenter.Y - (int)(0.5f * Height * y / trackBar6.Value));
                }
            }

            LRW = RW;
            LLW = LW;
            RW = (int)(F + T);
            LW = (int)(F - T);
            if (RW > trackBar6.Value)
                RW = trackBar6.Value;
            else if (RW < -trackBar6.Value)
                RW = -trackBar6.Value;
            if (LW > trackBar6.Value)
                LW = trackBar6.Value;
            else if (LW < -trackBar6.Value)
                LW = -trackBar6.Value;

            if (RW != LRW) 
            {
                if (RW > 0)
                {
                    buffer[0] = 15;
                    buffer[1] = 1;
                    buffer[2] = 13;
                    buffer[3] = (byte)RW;
                }
                else if (RW < 0)
                {
                    buffer[0] = 15;
                    buffer[1] = 2;
                    buffer[2] = 13;
                    buffer[3] = (byte)(-RW);
                }
                else 
                {
                    buffer[0] = 15;
                    buffer[1] = 3;
                    buffer[2] = 13;
                    buffer[3] = 0;
                }
                label6.Text = "" + RW;
                RobotPort.Write(buffer, 0, 4);
                
            }
            if (LW != LLW)
            {
                if (LW > 0)
                {
                    buffer[0] = 16;
                    buffer[1] = 1;
                    buffer[2] = 14;
                    buffer[3] = (byte)LW;
                }
                else if (LW < 0)
                {
                    buffer[0] = 16;
                    buffer[1] = 2;
                    buffer[2] = 14;
                    buffer[3] = (byte)(-LW);
                }
                else
                {
                    buffer[0] = 16;
                    buffer[1] = 3;
                    buffer[2] = 14;
                    buffer[3] = 0;
                }
                label5.Text = "" + LW;
                RobotPort.Write(buffer, 0, 4);
            }
        }
        byte[] buffer = new byte[12];

        bool KEl = false;
        bool KQl = false;
        bool KRl = false;
        void RightHandHandle() 
        {
            if (!KEl)
            {
                Cursor.Position = new Point(RH2 * Width / (trackBarR2.Maximum - trackBarR2.Minimum) + winStart.X, RH1 * Height / (trackBarR1.Maximum - trackBarR1.Minimum) + winStart.Y);
            }
            else 
            {
                R2((byte)((Cursor.Position.X - winStart.X) * (trackBarR2.Maximum - trackBarR2.Minimum) / Width));
                R1((byte)((Cursor.Position.Y - winStart.Y) * (trackBarR1.Maximum - trackBarR1.Minimum) / Height));
                if (RMB)
                {
                    if (RH3 + 5 <= trackBarR3.Maximum)
                        R3((byte)(RH3 + 5));
                    else
                        R3((byte)trackBarR3.Maximum);
                }
                else if (LMB)
                {
                    if (RH3 - 5 >= trackBarR3.Minimum)
                        R3((byte)(RH3 - 5));
                    else
                        R3((byte)trackBarR3.Minimum);
                }
            }
        }
        void LeftHandHandle() 
        {
            if (!KQl)
            {
                Cursor.Position = new Point(LH2 * Width / (trackBarL2.Maximum - trackBarL2.Minimum) + winStart.X, LH1 * Height / (trackBarL1.Maximum - trackBarL1.Minimum) + winStart.Y);
            }
            else
            {
                L2((byte)((Cursor.Position.X - winStart.X) * (trackBarL2.Maximum - trackBarL2.Minimum) / Width));
                L1((byte)((Cursor.Position.Y - winStart.Y) * (trackBarL1.Maximum - trackBarL1.Minimum) / Height));
                if (LMB)
                {
                    if (LH3 + 5 <= trackBarL3.Maximum)
                        L3((byte)(LH3 + 5));
                    else
                        L3((byte)trackBarL3.Maximum);
                }
                else if (RMB)
                {
                    if (LH3 - 5 >= trackBarL3.Minimum)
                        L3((byte)(LH3 - 5));
                    else
                        L3((byte)trackBarL3.Minimum);
                }
            }
        }

        void HandsHandle() 
        {
            R2((byte)((Cursor.Position.X - winStart.X) * (trackBarR2.Maximum - trackBarR2.Minimum) / Width));
            R1((byte)((Cursor.Position.Y - winStart.Y) * (trackBarR1.Maximum - trackBarR1.Minimum) / Height));
            L2((byte)(trackBarL2.Maximum - RH2 + trackBarL2.Minimum));
            L1(RH1);
            if (RMB)
            {
                if (RH3 + 5 <= trackBarR3.Maximum)
                    R3((byte)(RH3 + 5));
                else
                    R3((byte)trackBarR3.Maximum);
                if (LH3 - 5 >= trackBarL3.Minimum)
                    L3((byte)(LH3 - 5));
                else
                    L3((byte)trackBarL3.Minimum);
            }
            else if (LMB)
            {
                if (RH3 - 5 >= trackBarR3.Minimum)
                    R3((byte)(RH3 - 5));
                else
                    R3((byte)trackBarR3.Minimum);
                if (LH3 + 5 <= trackBarL3.Maximum)
                    L3((byte)(LH3 + 5));
                else
                    L3((byte)trackBarL3.Maximum);
            }
        }

        void globalUpdate() 
        {
            if (RH1l) 
            {
                RH1l = false;
                buffer[0] = 17;
                buffer[1] = (byte)(trackBarR1.Maximum - RH1 + trackBarR1.Minimum);
                RobotPort.Write(buffer, 0, 2);
            }
            if (RH2l)
            {
                RH2l = false;
                buffer[0] = 18;
                buffer[1] = (byte)(trackBarR2.Maximum - RH2 + trackBarR2.Minimum);
                RobotPort.Write(buffer, 0, 2);
            }
            if (RH3l)
            {
                RH3l = false;
                buffer[0] = 22;
                buffer[1] = RH3;
                RobotPort.Write(buffer, 0, 2);
            }
            if (LH1l)
            {
                LH1l = false;
                buffer[0] = 20;
                buffer[1] = (byte)(trackBarL1.Maximum - LH1 + trackBarL1.Minimum);
                RobotPort.Write(buffer, 0, 2);
            }
            if (LH2l)
            {
                LH2l = false;
                buffer[0] = 21;
                buffer[1] = (byte)(trackBarL2.Maximum - LH2 + trackBarL2.Minimum);
                RobotPort.Write(buffer, 0, 2);
            }
            if (LH3l)
            {
                LH3l = false;
                buffer[0] = 19;
                buffer[1] = LH3;
                RobotPort.Write(buffer, 0, 2);
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox3.Checked) 
            {
                buffer[0] = 106;
                RobotPort.Write(buffer, 0, 1);
                RobotPort.Read(buffer, 0, 1);
                if (buffer[0] == 1) {
                    this.BackColor = Color.Red;
                }
            }
            winStart = Location;
            winCenter = new Point(Location.X + Width / 2, Location.Y + Height / 2);
            globalUpdate();
            wheelsUpdate();
            if (KShift)
                return;
            if (KE)
            {
                RightHandHandle();
            }
            else if (KQ)
            {
                LeftHandHandle();
            }
            else if (KR)
            {
                HandsHandle();
            }
            else 
            {
                towerUpdate(false);
            }
            KEl = KE;
            KQl = KQ;
            KRl = KR;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ManualController_MouseDown(object sender, MouseEventArgs e)
        {
            if (Location.X < Cursor.Position.X && Location.Y < Cursor.Position.Y)
                if (Cursor.Position.X < Location.X + Width && Cursor.Position.Y < Location.Y + Height) 
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        LMB = true;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        RMB = true;
                        if (!(KShift || KE || KQ || KR))
                            towerUpdate(true);
                    }
                }
                     
        }

        private void ManualController_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                LMB = false;
            else if (e.Button == MouseButtons.Right)
                RMB = false;
        }
        bool UI = true;
        private void ManualController_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                KW = false;
            else if (e.KeyCode == Keys.S)
                KS = false;
            else if (e.KeyCode == Keys.A)
                KA = false;
            else if (e.KeyCode == Keys.D)
                KD = false;
            else if (e.KeyCode == Keys.E)
                KE = false;
            else if (e.KeyCode == Keys.Q)
                KQ = false;
            else if (e.KeyCode == Keys.R)
                KR = false;
            else if (e.KeyCode == Keys.ShiftKey)
                KShift = false;
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            label8.Text = "" + Math.Round((float)1000 / trackBar1.Value, 2);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TX = (byte)(TXMax - TX + TXMin);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TY = (byte)(TYMax - TY + TYMin);
        }
        byte RH1 = 90;
        byte RH2 = 90;
        byte RH3 = 90;
        byte LH1 = 90;
        byte LH2 = 90;
        byte LH3 = 90;
        bool RH1l = false;
        bool RH2l = false;
        bool RH3l = false;
        bool LH1l = false;
        bool LH2l = false;
        bool LH3l = false;

        void R1(byte val) 
        {
            if (val > trackBarR1.Maximum)
                val = (byte)trackBarR1.Maximum;
            else if (val < trackBarR1.Minimum)
                val = (byte)trackBarR1.Minimum;
            trackBarR1.Value = val;
            if (RH1 != val) 
            {
                RH1 = val;
                RH1l = true;
            }
        }
        void R2(byte val)
        {
            if (val > trackBarR2.Maximum)
                val = (byte)trackBarR2.Maximum;
            else if (val < trackBarR2.Minimum)
                val = (byte)trackBarR2.Minimum;
            trackBarR2.Value = val;
            if (RH2 != val)
            {
                RH2 = val;
                RH2l = true;
            }
        }
        void R3(byte val)
        {
            if (val > trackBarR3.Maximum)
                val = (byte)trackBarR3.Maximum;
            else if (val < trackBarR3.Minimum)
                val = (byte)trackBarR3.Minimum;
            trackBarR3.Value = val;
            if (RH3 != val)
            {
                RH3 = val;
                RH3l = true;
            }
        }
        void L1(byte val)
        {
            val = (byte)(180 - val);
            if (val > trackBarL1.Maximum)
                val = (byte)trackBarL1.Maximum;
            else if (val < trackBarL1.Minimum)
                val = (byte)trackBarL1.Minimum;
            trackBarL1.Value = val;
            if (LH1 != val)
            {
                LH1 = val;
                LH1l = true;
            }
        }
        void L2(byte val)
        {
            if (val > trackBarL2.Maximum)
                val = (byte)trackBarL2.Maximum;
            else if (val < trackBarL2.Minimum)
                val = (byte)trackBarL2.Minimum;
            trackBarL2.Value = val;
            if (LH2 != val)
            {
                LH2 = val;
                LH2l = true;
            }
        }
        void L3(byte val)
        {
            if (val > trackBarL3.Maximum)
                val = (byte)trackBarL3.Maximum;
            else if (val < trackBarL3.Minimum)
                val = (byte)trackBarL3.Minimum;
            trackBarL3.Value = val;
            if (LH3 != val)
            {
                LH3 = val;
                LH3l = true;
            }
        }
        private void trackBarR1_Scroll(object sender, EventArgs e)
        {
            R1((byte)trackBarR1.Value);
        }

        private void trackBarR2_Scroll(object sender, EventArgs e)
        {
            R2((byte)trackBarR2.Value);
        }

        private void trackBarR3_Scroll(object sender, EventArgs e)
        {
            R3((byte)trackBarR3.Value);
        }

        private void trackBarL1_Scroll(object sender, EventArgs e)
        {
            L1((byte)trackBarL1.Value);
        }

        private void trackBarL2_Scroll(object sender, EventArgs e)
        {
            L2((byte)trackBarL2.Value);
        }

        private void trackBarL3_Scroll(object sender, EventArgs e)
        {
            L3((byte)trackBarL3.Value);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked) 
            {
                this.BackColor = Color.DimGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "";
            buffer[0] = 107;

            RobotPort.Write(buffer,0,1);

            RobotPort.Read(buffer, 0, 12);

            msg += "temp: "+((buffer[0] * 256 + buffer[1])*175.72f/65536 - 46.85f);
            msg += "\nhumidity: " + ((buffer[2] * 256 + buffer[3]) * 125f / 65536 - 6);
            msg += "\nalcho: " + ((buffer[4] * 256 + buffer[5]) / 360f);
            msg += "\nlumin: " + ((1024 - buffer[6] * 256 + buffer[7]) / 1024f);
            msg += "\ndust: " + ((buffer[8] * 256 + buffer[9])/800f);
            msg += "\nFire: " + (buffer[10] * 256 + buffer[11]);

            label9.Text = msg;
        }
    }
}
