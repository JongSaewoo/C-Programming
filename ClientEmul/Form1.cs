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

namespace ClientEmul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 1 Packet Process function
        // 함수명 : SendPacket(string sPack
        // 인수   : string spack - 전송할 패킷 문자열
        // 리턴값 : None
        // 기능   : 입력된 문자열을 byte 배열로 변환하여 서버로 전송
        //          한글 엔코딩 기능 부여
        int RetryCount = 0; // 접속 재시도 횟수
        public void SendPacket(string sPack)
        {   // 1 Packet Process
            try
            {
                // 서버에 데이터를 socket에 담아 보냄
                Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _sock.Connect(tbServerIP.Text, int.Parse(tbServerPort.Text));

                if (_sock.Connected)
                {
                    _sock.Send(Encoding.Default.GetBytes(
                        (tbCode.Text + tbVal1.Text + tbVal2.Text + tbVal3.Text).ToCharArray()));
                    // .ToCharArray 배열은 byte[] 배열과 다른 배열이다.
                    // .ToCharArray = 글자열(2byte)배열, byte = 문자열(1byte) 배열
                    // -> Encoding을 통해 char배열을 byte배열로 인코딩한다...
                    // byte배열 크기로 인코딩하는 이유???
                    // == 서버쪽에서 GetStream할 때 받은 데이터를 Read()하면
                    // Read할때 byte형태로 밖에 못받아들이기 때문에 인코딩함.

                    RetryCount = 0; // 연결이 됬으므로 재시도 횟수 0 으로 초기화.
                }
            }
            catch (Exception e)  // 연결되지 않았을 때
            {
                // start버튼이 눌리면 timer가 접속 시도 -> 접속이 안된다면
                tbRetry.Text = $"{RetryCount++}";   // 접속 시도 횟수 +1 씩
            }

        }

        // start 버튼(timer의 주기를 설정)
        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(tbInterval.Text);
            timer1.Enabled = true;
        }

        // timer(start버튼을 통해 timer가 발생하고 
        //       timer에서는 어떤 데이터를 보낼지 설정)
        private void timer1_Tick(object sender, EventArgs e)
        {
            string s1 = tbCode.Text;    // "[STX:02]어쩌고저쩌고[ETX:03]
            string s2 = tbVal1.Text;
            string s3 = tbVal2.Text;
            string s4 = tbVal3.Text;
            string s = tbSep.Text;  // 구분자박스 -> WIND옆 옆 textBox
            char[] c1 = new char[2];

            c1[0] = Convert.ToChar(02); // "02" : STX
            string ss1 = c1.ToString();

            c1[0] = Convert.ToChar(03); // "03" : ETX
            string ss2 = c1.ToString();

            SendPacket($"{ss1}{s1}{s}{s2}{s}{s3}{s}{s4}{ss2}");
        }

        // stop버튼(timer의 주기 stop)
        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}