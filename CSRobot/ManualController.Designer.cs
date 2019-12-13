namespace CSRobot
{
    partial class ManualController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (RobotPort != null)
                RobotPort.Write(new byte[1] { 0 }, 0, 1);
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trackBarR3 = new System.Windows.Forms.TrackBar();
            this.trackBarR2 = new System.Windows.Forms.TrackBar();
            this.trackBarR1 = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBarL3 = new System.Windows.Forms.TrackBar();
            this.trackBarL2 = new System.Windows.Forms.TrackBar();
            this.trackBarL1 = new System.Windows.Forms.TrackBar();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.DimGray;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox1.Location = new System.Drawing.Point(6, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "InvX";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox2.Location = new System.Drawing.Point(6, 40);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "InvY";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tower";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBar6);
            this.groupBox2.Controls.Add(this.trackBar5);
            this.groupBox2.Controls.Add(this.trackBar4);
            this.groupBox2.Controls.Add(this.trackBar3);
            this.groupBox2.Controls.Add(this.trackBar2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(19, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 323);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wheels";
            // 
            // trackBar6
            // 
            this.trackBar6.Location = new System.Drawing.Point(9, 265);
            this.trackBar6.Maximum = 255;
            this.trackBar6.Minimum = 16;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Size = new System.Drawing.Size(116, 45);
            this.trackBar6.TabIndex = 8;
            this.trackBar6.TickFrequency = 16;
            this.trackBar6.Value = 255;
            // 
            // trackBar5
            // 
            this.trackBar5.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar5.Location = new System.Drawing.Point(96, 49);
            this.trackBar5.Maximum = 100;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar5.Size = new System.Drawing.Size(45, 210);
            this.trackBar5.TabIndex = 7;
            this.trackBar5.TickFrequency = 5;
            this.trackBar5.Value = 100;
            // 
            // trackBar4
            // 
            this.trackBar4.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar4.Location = new System.Drawing.Point(66, 49);
            this.trackBar4.Maximum = 100;
            this.trackBar4.Minimum = 10;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar4.Size = new System.Drawing.Size(45, 210);
            this.trackBar4.TabIndex = 6;
            this.trackBar4.TickFrequency = 5;
            this.trackBar4.Value = 50;
            // 
            // trackBar3
            // 
            this.trackBar3.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar3.Location = new System.Drawing.Point(36, 49);
            this.trackBar3.Maximum = 100;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 210);
            this.trackBar3.TabIndex = 5;
            this.trackBar3.TickFrequency = 5;
            this.trackBar3.Value = 10;
            // 
            // trackBar2
            // 
            this.trackBar2.BackColor = System.Drawing.Color.DimGray;
            this.trackBar2.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar2.Location = new System.Drawing.Point(6, 49);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 10;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 210);
            this.trackBar2.TabIndex = 4;
            this.trackBar2.TickFrequency = 5;
            this.trackBar2.Value = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "LMotor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "RMotor";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(13, 485);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(131, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Frequency";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(150, 494);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "20";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackBarR3);
            this.groupBox3.Controls.Add(this.trackBarR2);
            this.groupBox3.Controls.Add(this.trackBarR1);
            this.groupBox3.Location = new System.Drawing.Point(518, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 154);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Right hand";
            // 
            // trackBarR3
            // 
            this.trackBarR3.Location = new System.Drawing.Point(7, 107);
            this.trackBarR3.Maximum = 180;
            this.trackBarR3.Minimum = 40;
            this.trackBarR3.Name = "trackBarR3";
            this.trackBarR3.Size = new System.Drawing.Size(187, 45);
            this.trackBarR3.TabIndex = 2;
            this.trackBarR3.TickFrequency = 15;
            this.trackBarR3.Value = 90;
            this.trackBarR3.Scroll += new System.EventHandler(this.trackBarR3_Scroll);
            // 
            // trackBarR2
            // 
            this.trackBarR2.Location = new System.Drawing.Point(7, 58);
            this.trackBarR2.Maximum = 180;
            this.trackBarR2.Name = "trackBarR2";
            this.trackBarR2.Size = new System.Drawing.Size(187, 45);
            this.trackBarR2.TabIndex = 1;
            this.trackBarR2.TickFrequency = 15;
            this.trackBarR2.Value = 90;
            this.trackBarR2.Scroll += new System.EventHandler(this.trackBarR2_Scroll);
            // 
            // trackBarR1
            // 
            this.trackBarR1.Location = new System.Drawing.Point(7, 20);
            this.trackBarR1.Maximum = 110;
            this.trackBarR1.Name = "trackBarR1";
            this.trackBarR1.Size = new System.Drawing.Size(187, 45);
            this.trackBarR1.TabIndex = 0;
            this.trackBarR1.TickFrequency = 15;
            this.trackBarR1.Value = 90;
            this.trackBarR1.Scroll += new System.EventHandler(this.trackBarR1_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trackBarL3);
            this.groupBox4.Controls.Add(this.trackBarL2);
            this.groupBox4.Controls.Add(this.trackBarL1);
            this.groupBox4.Location = new System.Drawing.Point(312, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 154);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Left hand";
            // 
            // trackBarL3
            // 
            this.trackBarL3.Location = new System.Drawing.Point(7, 107);
            this.trackBarL3.Maximum = 180;
            this.trackBarL3.Minimum = 40;
            this.trackBarL3.Name = "trackBarL3";
            this.trackBarL3.Size = new System.Drawing.Size(187, 45);
            this.trackBarL3.TabIndex = 2;
            this.trackBarL3.TickFrequency = 15;
            this.trackBarL3.Value = 90;
            this.trackBarL3.Scroll += new System.EventHandler(this.trackBarL3_Scroll);
            // 
            // trackBarL2
            // 
            this.trackBarL2.Location = new System.Drawing.Point(7, 58);
            this.trackBarL2.Maximum = 180;
            this.trackBarL2.Name = "trackBarL2";
            this.trackBarL2.Size = new System.Drawing.Size(187, 45);
            this.trackBarL2.TabIndex = 1;
            this.trackBarL2.TickFrequency = 15;
            this.trackBarL2.Value = 90;
            this.trackBarL2.Scroll += new System.EventHandler(this.trackBarL2_Scroll);
            // 
            // trackBarL1
            // 
            this.trackBarL1.Location = new System.Drawing.Point(7, 20);
            this.trackBarL1.Maximum = 180;
            this.trackBarL1.Minimum = 70;
            this.trackBarL1.Name = "trackBarL1";
            this.trackBarL1.Size = new System.Drawing.Size(187, 45);
            this.trackBarL1.TabIndex = 0;
            this.trackBarL1.TickFrequency = 15;
            this.trackBarL1.Value = 90;
            this.trackBarL1.Scroll += new System.EventHandler(this.trackBarL1_Scroll);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(175, 13);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 17);
            this.checkBox3.TabIndex = 15;
            this.checkBox3.Text = "Detects fire";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Get sensors info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 78);
            this.label9.TabIndex = 17;
            this.label9.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6";
            // 
            // ManualController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(730, 543);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.KeyPreview = true;
            this.Name = "ManualController";
            this.Text = "ManualController";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManualController_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ManualController_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ManualController_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ManualController_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ManualController_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBarR3;
        private System.Windows.Forms.TrackBar trackBarR2;
        private System.Windows.Forms.TrackBar trackBarR1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBarL3;
        private System.Windows.Forms.TrackBar trackBarL2;
        private System.Windows.Forms.TrackBar trackBarL1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
    }
}