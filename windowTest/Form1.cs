using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string sConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JongWooPark\source\repos\MyTable.mdf;Integrated Security=True;Connect Timeout=30";
        // 해당 데이터파일.mdf의 속성에서 연결문자열 링크임.
        // 더블코테이션 앞 @는 모든 이스케이프 시퀀스를 무시한다는 뜻
        //  (\를 두번 \\써야하는데 \를 한번쓰도록 해줌) 
        SqlConnection sConn = new SqlConnection();    // Database file에 연결객체 : msSql을 씀
        SqlCommand sCmd = new SqlCommand();        // sql 명령문 처리객체

        private void btnAddCol_Click(object sender, EventArgs e)
        {
            string str = tbTest1.Text;
            if (str != "")
            {
                dataGrideView1.Columns.Add(str, str);
                tbTest1.Text = "";
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dataGrideView1.Rows.Add();
        }

        private void mnuAddCol_Click(object sender, EventArgs e)
        {
            frmInput dlg = new frmInput();
            dlg.ShowDialog();

            string str = dlg.sRet;
            if (str != "")
            {
                dataGrideView1.Columns.Add(str, str);
                tbTest1.Text = "";
            }
        }

        private void mnuAddRow_Click(object sender, EventArgs e)
        {
            frmInput dlg = new frmInput();
            dlg.ShowDialog();

            string str = dlg.sRet;
            if (str != "")
            {
                dataGrideView1.Rows.Add(str, str);
                tbTest1.Text = "";
            }
        }

        private void mnuDBopen_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ValidateNames = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                // openFileDialog1 도구상자를 view할수있도록 함.
                {
                    string[] sArr = sConString.Split(';');
                    // sArr에는 sConString문자열(파일경로)을 ';'마다 구분하여
                    // 4개의 필드로 저장되있다. 4개의 필드 중 2번째 필드(index:1) 수정 필요.

                    string sConnStr = string.Format("{0};AttachDbFilename={1};{2};{3}",
                                                     sArr[0], openFileDialog1.FileName,
                                                     sArr[2], sArr[3]);
                    // 위 sConnStr과 sConnStr1은 같은 문구다. 아래 sConnStr1이 후 버전 문구
                    string sConnStr1 = $"{sArr[0]};AttachDbFilename={openFileDialog1.FileName};{sArr[2]};{sArr[3]}";

                    sConn.ConnectionString = sConnStr1;
                    sConn.Open();
                    sCmd.Connection = sConn;

                    StatusLabel1.Text = "Database Opened.";
                    StatusLabel1.BackColor = Color.Green;

                    DataTable dt = sConn.GetSchema("Tables");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string str = dt.Rows[i].ItemArray[2].ToString();
                        // 2번째 배열요소가 Table 이름

                        tbSql.Text += str + "\r\n";
                        stComboBox1.Items.Add(str);
                    }    
                }
            }
            catch (Exception e1)
            // dataBase 파일 오픈 에러 처리
            {
                MessageBox.Show(e1.Message);
                StatusLabel1.Text = "Database Failled!!";
                StatusLabel1.BackColor = Color.Red;
            }
        }

        private void mnuDBclose_Click(object sender, EventArgs e)
        {
            sConn.Close();
            StatusLabel1.Text = "Database Closed.";
            StatusLabel1.BackColor = Color.Gray;
        }

        public string GetToken(int index, string str, string sdel)
        {         // str 문자열을 'sDel' 구분자로 분할하여 그 중 index 번째의 문자열을 반환
                  // GetToken(3, "1|2|3|4|5", "|") ==> "4" 반환
            string[] sArr = str.Split(sdel[0]);
            return sArr[index];
        }

        /* 함수 일반화 #n
           함수명 : public void RunSqlNone(string sql)
           인수 : string sql ==> 조회값이 없는 SQL 명령어 (Insert, Update, Delete)
                                 Select 문을 제외한 나머지 모두
                                      -> SELECT * FROM [table_name]
           리턴 : void                                                           */
        public void RunSql(string sql)
        {
            try
            {   // 첫번째 단어를 분리하고 소문자로 변환
                string s1 = GetToken(0, sql, " ").ToLower();
                sCmd.CommandText = sql;

                if (s1 != "select")
                {
                    sCmd.ExecuteNonQuery();
                }
                else
                {
                    SqlDataReader sr = sCmd.ExecuteReader();  // record 단위로 명령처리
                    for (int i = 0; i < sr.FieldCount; i++)
                    {
                        dataGrideView1.Columns.Add(sr.GetName(i), sr.GetName(i));
                    }
                    for (int i = 0; sr.Read(); i++)   // 읽을 record가 있으면 true
                    {                               // 없으면 false
                        dataGrideView1.Rows.Add();
                        for (int ii = 0; ii < sr.FieldCount; ii++)
                        {
                            dataGrideView1.Rows[i].Cells[ii].Value = sr.GetValue(ii);
                        }
                    }
                }

                StatusLabel2.Text = "Sucessfully Apply.";
                StatusLabel2.BackColor = Color.CadetBlue;
            }
            catch (Exception e1)
            {
                StatusLabel2.Text = e1.Message;
                StatusLabel2.BackColor = Color.IndianRed;
            }
        }

        private void mnuTestCmd1_Click(object sender, EventArgs e)
        {
            // For SQL coding test
            //String sTime = GetToken(0, DateTime.Now.Date.ToString(), " ");
            string sTime = $"{DateTime.Now:s}";  // 시간 규격을 국제 표준에 맞춰줌
            string str = $"insert into fStatus " +
                    $"values ('10001', '10.50', '50.00', '02.00', '{sTime}')";
            // SQL insert 문
            RunSql(str);
        }

        private void mnuTestCmd2_Click(object sender, EventArgs e)
        {
            // For SQL coding test
            // string sTime = $"{DateTime.Now:s}";  // 시간 규격을 국제 표준에 맞춰줌
            string str = $"insert into fStatus (FCODE, TEMP, HUM, WIND) " +
                    $"values ('10001', '10.50', '50.00', '02.00')";
            // SQL insert 문
            RunSql(str);
        }

        private void mnuTestCmd3_Click(object sender, EventArgs e)
        {
            RunSql("Select * from facility");
            StatusLabel3.Text = "facility";
        }

        int x, y;
        string sHeader;
        private void dataGrideView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            x = e.ColumnIndex;
            y = e.RowIndex;
            dataGrideView1.Rows[y].Cells[x].ToolTipText = ".";

            sHeader = dataGrideView1.Columns[x].HeaderText;
            // cell에 위치한곳의 header정보 즉, Field명
        }

        private void mnuDBUpdate_Click(object sender, EventArgs e)
        // UPDATE [Table_name] SET [Field_name]='[Cell_Value]' 
        //                    where [ID] = [ID_VALUE]
        {
            for(int i=0; i<dataGrideView1.RowCount; i++)
            // ROW 쭉 검사
            {
                for(int j=0; j<dataGrideView1.ColumnCount; j++)
                // COL 쭉 검사
                {
                    if(dataGrideView1.Rows[i].Cells[j].ToolTipText == ".")
                    // 현재 수정한 해당 CELL
                    {
                        string tn = StatusLabel3.Text;  // 테이블명
                        string fn = dataGrideView1.Columns[j].HeaderText;   // 해당 col명
                        string cv = dataGrideView1.Rows[i].Cells[j].Value.ToString(); // 수정한 해당 cell값
                        string iv = dataGrideView1.Columns[0].HeaderText;   // 해당 id명
                        string a = dataGrideView1.Rows[i].Cells[0].Value.ToString();    // 수정한 해당 id값
                        string sql = $"UPDATE {tn} SET {fn} = '{cv}' WHERE {iv} = '{a}'";
                        RunSql(sql);
                        dataGrideView1.Rows[i].Cells[j].ToolTipText = "";
                        //RunSql("UPDATE " + tn + " SET = '" + sHeader + "'"
                        //    + "WHERE ID = " + y);
                    }
                }
            }
            
            
        }
    }
}

