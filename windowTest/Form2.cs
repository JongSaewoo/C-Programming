using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowTest
{
    public partial class frmInput : Form
    {
        public frmInput()
        {
            InitializeComponent();
        }

        public string sRet = "";

        private void textBox1_keyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')   // '/r'은 엔터를 의미한다.(아스키 숫자:13, 헥사:0d)
            {
                sRet = textBox1.Text;
                Close();
            }
        }
    }
}
