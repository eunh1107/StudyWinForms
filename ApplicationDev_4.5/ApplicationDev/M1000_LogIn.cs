using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ApplicationDev
{
    public partial class M1000_LogIn : Form
    {
        public M1000_LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = "FALSE";
            this.Visible = false;
            M1010_PasswordChang PWChang = new M1010_PasswordChang();
            PWChang.ShowDialog();
            this.Visible = true;
        }

        private SqlConnection Connect = null;

        private void button2_Click(object sender, EventArgs e)
        { 
            try
            { 

                // 11. 데이터 베이스 접속 경로 설정
                string strConn = "Data Source=222.235.141.8; Initial Catalog=AppDev;User ID=kfqs1;Password=1234";  //DESKTOP-HNMKD38\SQLEXPRESS 
                //"Server=localhost; Uid=test;Pwd =test; database=test; "
                //string strConn = "Server=localhost; Uid=test;Pwd =test; database=test; ";  //DESKTOP-HNMKD38\SQLEXPRESS 
                Connect = new SqlConnection(strConn);


                // 22. 데이터 베이스 연결 상태 확인.
                Connect.Open();
                if (Connect.State != System.Data.ConnectionState.Open) MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");

                string sLogInId  = txtID.Text;
                string sPassWord = txtPW.Text;

                // 기존의 비밀 번호 찾기.
                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW,USERNAME FROM TB_USER_DSH WHERE USERID = '" + sLogInId + "'", Connect);
                DataTable DtTemp = new DataTable();

                Adapter.Fill(DtTemp);

                // ID 존재 여부 확인.
                if (DtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("로그인 ID 가 잘못 되었습니다.");
                    txtID.Text = ""; 
                    txtID.Focus();
                    return;
                }

                // 기존 비밀 번호 비교 
                else if (sPassWord != DtTemp.Rows[0]["PW"].ToString())
                {
                    MessageBox.Show("비밀번호 가 잘못 되었습니다.");
                    txtPW.Text = "";
                    txtPW.Focus();
                    return;
                }
                else  
                {
                    DEV_FORM.Commoncs.LoginUserID   = txtID.Text;
                    DEV_FORM.Commoncs.LoginUserName = DtTemp.Rows[0]["USERNAME"].ToString();
                    this.Tag = DtTemp.Rows[0]["USERNAME"].ToString(); // 유저 명을 메인화면으로 보냄
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("로그인 작업중 오류가 발생하였습니다." + ex.ToString());
            }
            finally
            {
                Connect.Close();
            } 
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
            }
        }
    }
}
