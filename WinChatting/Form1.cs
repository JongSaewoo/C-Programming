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
        delegate void AddTextCallback(string str);

        public void AddText(string str)
        {
            // 쓰레드를 구성하기 위해선 -> delegate선언 -> Invoke화
            if(tbServer.InvokeRequired)
            {
                AddTextCallback cb = new AddTextCallback(AddText);
                object[] ob = { str };
                Invoke(cb, ob);
            }
            else
            {
                tbServer.Text += str;
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

            AddText($"Chatting Server Started.[{ServerPort}]");
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
            // 소켓에게서 접속 요청을 받을 리스너 start
            _socket = _listen.AcceptTcpClient();
            // 소켓의 접속 요청
            AddText($"Connected to Remote Client..[{_socket.Client.RemoteEndPoint.ToString()}]\r\n");
            // 요청상태[요청한 포트번호]에 대해 출력
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
                    AddText(str + "\r\n");
                }
            }
            catch(Exception e)
            // 오류 처리
            {
                string s1 = $"오류:{e.Message}\r\n";
                AddText(s1);
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

        private void mnuSend1_Click(object sender, EventArgs e)
        {
            if(_socket != null)
            {
                NetworkStream ns = _socket.GetStream(); 
                // 세션에 보내기위해 스트림 구동
                string str = tbClient.SelectedText; 
                // 보낼 데이터 구성
                byte[] bArr1 = Encoding.Default.GetBytes(str);
                // 데이터를 스트림에 집어넣기위해서 바이트로 인코딩
                ns.Write(bArr1, 0, bArr1.Length);

            }
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
