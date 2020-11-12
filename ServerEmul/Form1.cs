using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerEmul
{
    public partial class Form1 : Form
    {
        delegate void AddTextCallback(string str);

        public void AddText(string str)
        {
            if (tbServerPort.InvokeRequired)
            // tbServerPort를 Invoke화 한다.
            {
                AddTextCallback f = new AddTextCallback(AddText);
                object[] oArr = { str };
                Invoke(f, oArr);
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
        TcpListener _listen;
        Thread ServerThread;
        // server쓰레드를 따로 지정해둠으로써 프로그램이 먹통이 되는걸 방지.
        // + invoke or delegate 지정해줘야됨.
        byte[] Buffer = new byte[10000];

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_listen == null)
            {
                _listen = new TcpListener(int.Parse(tbServerPort.Text));
            }
            _listen.Start();    // client의 socket을 받아들일 준비 시작

            ServerThread = new Thread(ServerProcess);
            ServerThread.Start();
        }

        SqlConnection sConn = new SqlConnection();
        SqlCommand sCmd = new SqlCommand();
        string sConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JongWooPark\source\repos\MyTable.mdf;Integrated Security=True;Connect Timeout=30";
        private void ServerProcess()
        {
            while (true)
            {
                // Packet data : [STX]100001, 1200, 3000, 0200[ETX]

                TcpClient _client = _listen.AcceptTcpClient();
                // _listen.AcceptTcpClient()로 받은 socket을 
                // TcpClient _client로 세션에 저장할수 있도록 선언.

                NetworkStream ns = _client.GetStream();
                // 파일스트림처럼 클라이언트와 서버를 연결시킬 통로 객체 ns 선언.
                int n = ns.Read(Buffer, 0, 10000);
                // 담을 그릇 Buffer, 최소크기 0, 최대크기 10000

                string str = Encoding.Default.GetString(Buffer);
                // 클라이언트에서 string 데이터를 Stream(통로)에 전송하기위해
                // byte 데이터로 인코딩했다면 서버에서는 GetStream을 통해
                // Read()로 받은 byte데이터를 다시 string으로 인코딩해서
                // str문자객체에 담은 후 tbCommand창에 str을 뿌려준다. 
                AddText(str + "\r\n");
                // ★★★
                // 원래 tbCommand창에 받은 str을 바로 뿌려주려 했지만
                // 이렇게 되면 쓰레드처리에서 먹통이 되므로
                // AddText 함수를 따로 선언하고 이 함수를 invoke화시켜
                // delegate를 이용하여 뿌린 str값들의 주소를 가리키는
                // 포인터 역할의 AddText를 완성시킨다.

                string ss1 = str.Substring(0, n).Replace('\u0002', ' '); 
                // STX ==> ' '  , 문자열 뒤의 '\0' 제거
                string ss2 = ss1.Replace('\u0003', ' ');   // ETX ==> ' '

                string[] sArr = str.Split(',');
                string s1 = sArr[0].Trim();
                string s4 = sArr[3].Trim();
                string st = DateTime.Now.ToString();
                string sql = $"insert into fstatus " +
                    $"values ('{s1}','{sArr[1]}','{sArr[2]}','{s4}','{st}')";
                sCmd.CommandText = sql;
                sCmd.ExecuteNonQuery();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServerThread.Suspend();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            ServerThread.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sConn.ConnectionString = sConStr;
            sConn.Open();
            sCmd.Connection = sConn;
        }
    }
}