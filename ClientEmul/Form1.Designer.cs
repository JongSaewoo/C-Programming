namespace ClientEmul
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.tbRetry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbVal3 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tbSep = new System.Windows.Forms.TextBox();
            this.ckbTimer = new System.Windows.Forms.CheckBox();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSend1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCommand
            // 
            this.tbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommand.ContextMenuStrip = this.PopupMenu;
            this.tbCommand.Location = new System.Drawing.Point(3, 3);
            this.tbCommand.Multiline = true;
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(443, 281);
            this.tbCommand.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server IP";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(76, 288);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(107, 25);
            this.tbServerIP.TabIndex = 2;
            this.tbServerIP.Text = "192.168.0.148";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(239, 288);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(51, 25);
            this.tbServerPort.TabIndex = 4;
            this.tbServerPort.Text = "9000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Interval";
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(364, 288);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(58, 25);
            this.tbInterval.TabIndex = 6;
            this.tbInterval.Text = "2000";
            // 
            // tbRetry
            // 
            this.tbRetry.Location = new System.Drawing.Point(364, 320);
            this.tbRetry.Name = "tbRetry";
            this.tbRetry.Size = new System.Drawing.Size(58, 25);
            this.tbRetry.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Code";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(78, 354);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(100, 25);
            this.tbCode.TabIndex = 9;
            this.tbCode.Text = "100001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Temp";
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(78, 385);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(100, 25);
            this.tbVal1.TabIndex = 9;
            this.tbVal1.Text = "0012";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 420);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Hum.";
            // 
            // tbVal2
            // 
            this.tbVal2.Location = new System.Drawing.Point(78, 416);
            this.tbVal2.Name = "tbVal2";
            this.tbVal2.Size = new System.Drawing.Size(100, 25);
            this.tbVal2.TabIndex = 9;
            this.tbVal2.Text = "5000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 451);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Wind";
            // 
            // tbVal3
            // 
            this.tbVal3.Location = new System.Drawing.Point(78, 447);
            this.tbVal3.Name = "tbVal3";
            this.tbVal3.Size = new System.Drawing.Size(100, 25);
            this.tbVal3.TabIndex = 9;
            this.tbVal3.Text = "0200";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(221, 354);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 118);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(347, 354);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 118);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // tbSep
            // 
            this.tbSep.Location = new System.Drawing.Point(185, 447);
            this.tbSep.Name = "tbSep";
            this.tbSep.Size = new System.Drawing.Size(16, 25);
            this.tbSep.TabIndex = 12;
            this.tbSep.Text = ",";
            // 
            // ckbTimer
            // 
            this.ckbTimer.AutoSize = true;
            this.ckbTimer.Location = new System.Drawing.Point(428, 292);
            this.ckbTimer.Name = "ckbTimer";
            this.ckbTimer.Size = new System.Drawing.Size(18, 17);
            this.ckbTimer.TabIndex = 13;
            this.ckbTimer.UseVisualStyleBackColor = true;
            // 
            // PopupMenu
            // 
            this.PopupMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSend1});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(209, 28);
            // 
            // mnuSend1
            // 
            this.mnuSend1.Name = "mnuSend1";
            this.mnuSend1.Size = new System.Drawing.Size(210, 24);
            this.mnuSend1.Text = "선택된 문자열 전송";
            this.mnuSend1.Click += new System.EventHandler(this.mnuSend1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 487);
            this.Controls.Add(this.ckbTimer);
            this.Controls.Add(this.tbSep);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbVal3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbVal2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbVal1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRetry);
            this.Controls.Add(this.tbInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCommand);
            this.Name = "Form1";
            this.Text = "Form1";
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInterval;
        private System.Windows.Forms.TextBox tbRetry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbVal1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbVal2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbVal3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox tbSep;
        private System.Windows.Forms.CheckBox ckbTimer;
        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSend1;
    }
}

