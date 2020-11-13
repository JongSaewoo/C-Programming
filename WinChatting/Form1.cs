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

namespace WinChatting
{
    public partial class Form1 : Form   // Ethernet Chatting
    {
        delegate void AddTextCallback(string str, int n);

        public void AddText(string str, int n)
        {
            // 쓰레드를 구성하기 위해선 -> delegate선언 -> Invoke화
            if (tbServer.InvokeRequired)
            {
                AddTextCallback cb = new AddTextCallback(AddText);
                object[] ob = { str, n };
                Invoke(cb, ob);
            }
            else
            {
                if (n == 0) // 서버에 텍스트 보낼때 n = 0
                {
                    tbServer.Text += str;
                }
                else if (n == 1) // 클라이언트에 텍스트 보낼때 n = 1
                {
                    tbClient.Text += str;
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        TcpListener _listen;    // 소켓(데이터)을 받을 리스너(서버)
        Thread _Sessionthread;  // 쓰레드 1 : 세션쓰레드
        Thread _ReadThread;     // 쓰레드 2 : 리드쓰레드
        TcpClient _socket;      // 리스너에게 보낼 소켓(데이터)
        byte[] bArr = new byte[10000];
        // 소켓(데이터)의 크기 지정
        // 가변적 형태의 변수를 선언하거나 객체의 컴포넌트를 선언할때
        // new, 그 외에는 new를 쓰지 않는다.
        int ServerPort = 9000;  // (포트)어딘가에서 수정할 수 있음.
        private void mnuStart_Click(object sender, EventArgs e)
        {
            if(_listen == null)
                _listen = new TcpListener(ServerPort);

            AddText($"Chatting Server Started.[{ServerPort}]\r\n", 0);
            _Sessionthread = new Thread(ServerProcess); // 쓰레드 준비
            _Sessionthread.Start();
            // 쓰레드 시작 : 준비단계에서 백그라운드로 실행된 ServerProcess를 시작
            timer1.Enabled = true;
            // 쓰레드를 감지할 주기 설정(타이머 설정)
        }

        public void ServerProcess()
        // 모든 쓰레드의 메소드 구성 : 접속 요청 수락시까지만 ...
        {
            _listen.Start();

            while(true)
            // while, if, break를 통해 프로세스 정상적으로 ON,OFF
            // 없으면 앞서 실행했던 프로세스가 정상적으로 종료가 안되서 계속 굴러감.
            // 클라이언트와 달리 서버 소켓에서는 TimeOut을 지정할 수 없기 때문에
            // 따로 메소드를 구현하였다.
            {
                if(_listen.Pending())
                {
                    // 소켓에게서 접속 요청을 받을 리스너 start
                    _socket = _listen.AcceptTcpClient();
                    // 소켓의 접속 요청
                    AddText($"Connected to Remote Client..[{_socket.Client.RemoteEndPoint.ToString()}]\r\n", 0);
                    // 요청상태[요청한 포트번호]에 대해 출력

                    break;
                }
            }

        }

        public void ReadProcess()
        {
            try
            {
                while (true)
                {
                    NetworkStream ns = _socket.GetStream();
                    // 파일스트림처럼 클라이언트와 서버를 연결시킬 통로 객체 ns 선언.
                    int n = ns.Read(bArr, 0, 10000);
                    // 담을 그릇 Buffer bArr, 최소크기 0, 최대크기 10000

                    string str = Encoding.Default.GetString(bArr, 0, n);
                    // 클라이언트에서 string 데이터를 Stream(통로)에 전송하기위해
                    // byte 데이터로 인코딩했다면 서버에서는 GetStream을 통해
                    // Read()로 받은 byte데이터를 다시 string으로 인코딩해서
                    // str문자객체에 담은 후 tbCommand창에 str을 뿌려준다. 
                    AddText(str + "\r\n", 0);
                }
            }
            catch(Exception e)
            // 오류 처리
            {
                string s1 = $"오류:{e.Message}\r\n";
                AddText(s1, 0);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_socket != null)
            {
                _ReadThread = new Thread(ServerProcess);
                _ReadThread.Start();
                timer1.Enabled = false;
            }
        }

        public void SendString(string str)
        {
            if (_sock != null)
            {
                byte[] bArr1 = Encoding.Default.GetBytes(str);
                _sock.Send(bArr1);
            }
        }

        private void mnuSend1_Click(object sender, EventArgs e)
        {
            SendString(tbClient.SelectedText);
        }

        private void tbServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string str = tbClient.Text.Trim().Split('\r').Last();
                SendString(str);
            }
        }

        private void mnuServerStop_Click(object sender, EventArgs e)
        {

        }

        private void mnuSetup_Click(object sender, EventArgs e)
        {

        }

        // 프로세스 종료
        private void Form1_Formclosed(object sender, FormClosedEventArgs e)
        {
            if(_Sessionthread != null)
            {
                _Sessionthread.Interrupt();
                _Sessionthread.Abort();
            }
            if(_ReadThread != null)
            {
                _ReadThread.Interrupt();
                _ReadThread.Abort();
            }
            if(_ClientThread != null)
            {
                _ClientThread.Interrupt();
                _ClientThread.Abort();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Sessionthread != null)
            {
                _Sessionthread.Interrupt();
                _Sessionthread.Abort();
            }
            if (_ReadThread != null)
            {
                _ReadThread.Interrupt();
                _ReadThread.Abort();
            }
            if (_ClientThread != null)
            {
                _ClientThread.Interrupt();
                _ClientThread.Abort();
            }
        }


        ///////////////////   Client Mode methods   ///////////////////////
        // Target(server) IP/Port에 대하여 접속 요청
        // -Socket
        // -Connect 사용
        // Client method ReadThread
        string RemoteIP = "192.168.0.148";  // 나의 IP
        int RemotePort = 9000;
        Thread _ClientThread;
        Socket _sock;   // 'TcpClient _socket;' 과 다름

        private void ClientProcess()    // clientEmul에 ReadProcess와 같음
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(20);
                    int n = _sock.Receive(bArr); // Low level socket method
                    string str = Encoding.Default.GetString(bArr, 0, n) + "\r\n";
                    AddText(str, 1);
                }
            }
            catch (Exception e)
            {
                string s1 = $"오류 : {e.Message}\r\n";
                AddText(s1, 1);
            }
        }

        private void mnuClientStart_Click(object sender, EventArgs e)
        {
            _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _sock.Connect(RemoteIP, RemotePort);
            if (_sock.Connected)
            {
                _ClientThread = new Thread(ClientProcess);
                _sock.ReceiveTimeout = 1000;    
                // 1초의 딜레이를 주며 타임아웃을 구성
                // ClientProcess에서 정상적인 프로세스 종료 메커니즘을 위해 선언.                                 
                _ClientThread.Start();
            }
        }

        private void mnuClientStop_Click(object sender, EventArgs e)
        {

        }

    }
    // mnuStart -> 서버or클라이언트 요청 : 세션 및 리드 프로세스 구동
    //              -> start -> 세션에서 데이터 받을준비 ->socket ->(timer)
    //                  -> 데이터 스트림 열기(GetStream)
    //                  -> 바이트 인코딩 : Encoding.Default.GetBytes
    //                  -> char로 인코딩(디코딩) : Encoding.Default.GetString
    //                  -> 디코딩 된 데이터를 listen으로 받음
    //              -> 리드 프로세스에서 데이터 Read
    //          -> 서버or클라이언트 응답
}
