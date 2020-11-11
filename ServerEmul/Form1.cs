using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerEmul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Client에서 보낸 socket을 받아들일 리스너 선언
        // Client tcp open : server ip & server port 필요
        // server open : server port만 필요
        TcpListener _listen;

        private void btnStart_Click(object sender, EventArgs e)
        {
            _listen = new TcpListener(int.Parse(tbServerPort.Text));
            _listen.Start();
        }
    }
}
