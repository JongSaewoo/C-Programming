using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientEmul
{
    public partial class Form1 : Form
    {
        delegate void AddTextCallback(string str);

        public void AddText(string str)
        {
            // 쓰레드를 구성하기 위해선 -> delegate선언 -> Invoke화
            if (tbCommand.InvokeRequired)
            {
                AddTextCallback cb = new AddTextCallback(AddText);
                object[] ob = { str };
                Invoke(cb, ob);
            }
            else
            {
                tbCommand.Text += str;
            }
        }
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
                    char[] cArr = sPack.ToCharArray();
                    byte[] bArr = Encoding.Default.GetBytes(cArr);
                    // .ToCharArray 배열은 byte[] 배열과 다른 배열이다.
                    // .ToCharArray = 글자열(2byte)배열, byte = 문자열(1byte) 배열
                    // -> Encoding을 통해 char배열을 byte배열로 인코딩한다...
                    // byte배열 크기로 인코딩하는 이유???
                    // == 서버쪽에서 GetStream할 때 받은 데이터를 Read()하면
                    // Read할때 byte형태로 밖에 못받아들이기 때문에 인코딩함.

                    _sock.Send(bArr);
                    RetryCount = 0; // 연결이 됬으므로 재시도 횟수 0 으로 초기화.
                }
            }
            catch (Exception e)  // 연결되지 않았을 때
            {
                // start버튼이 눌리면 timer가 접속 시도 -> 접속이 안된다면
                tbRetry.Text = $"{RetryCount++}";   // 접속 시도 횟수 +1 씩
            }

        }

        Socket _sock;
        Thread _thread;
        byte[] bArr = new byte[10000];

        // start 버튼(timer의 주기를 설정)
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            
            if(ckbTimer.Checked == true)
            {
                timer1.Interval = int.Parse(tbInterval.Text);
                timer1.Enabled = true;
            }
            else
            {
                // 서버에 데이터를 socket에 담아 보냄
                _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _sock.Connect(tbServerIP.Text, int.Parse(tbServerPort.Text));

                if (_sock.Connected)
                {
                    _thread = new Thread(ReadProcess);
                    _thread.Start();
                }  
            }
        }

        public void ReadProcess()
        {
            try
            {
                while (true)
                {
                    int n = _sock.Receive(bArr); // Low level Socket method
                    string str = Encoding.Default.GetString(bArr) + "\r\n";
                    AddText(str);

                    Thread.Sleep(20); 
                    // request, response가 한 싸이클 돌때마다 쓰레드에 sleep구간을 주어
                    // 다른 프로그램(마우스이동, 키보드입력, 카톡, 등등..)의 
                    // 지연을 방지(좋은 프로그램을 위해 넣음)
                }
            }
            catch (Exception e)
            // 오류 처리
            {
                string s1 = $"오류 : {e.Message}\r\n";
                AddText(s1);
            }
        }

        // timer(start버튼을 통해 timer가 발생하고 
        //       timer에서는 어떤 데이터를 보낼지 설정)
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            string s1 = tbCode.Text;    // "[STX:02]어쩌고저쩌고[ETX:03]
            string s2 = tbVal1.Text;
            string s3 = tbVal2.Text;
            string s4 = tbVal3.Text;
            string s = tbSep.Text;  // printf("%c",2); [STX:02]
            //char[] c1 = new char[2]; 
            //char c1 = Convert.ToChar(02);//"02" : STX
            //char c2 = Convert.ToChar(03);//"03" : ETX

            SendPacket($"\u0002{s1}{s}{s2}{s}{s3}{s}{s4}\u0003");
            SendPacket($"{Convert.ToChar(02)}{s1}{s}{s2}{s}{s3}{s}{s4}{Convert.ToChar(03)}");
        }

        // stop버튼(timer의 주기 stop)
        private void btnStop_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void mnuSend1_Click(object sender, EventArgs e)
        {
            if(_sock != null)
            {
                string str = tbCommand.SelectedText;
                bArr = Encoding.Default.GetBytes(str);
                _sock.Send(bArr);
            }
        }
    }
}