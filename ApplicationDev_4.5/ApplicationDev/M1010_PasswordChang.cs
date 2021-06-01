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
    public partial class M1010_PasswordChang : Form
    {
        public M1010_PasswordChang()
        {
            InitializeComponent();
        }

        private SqlConnection  Connect = null;          //  데이터 베이스 접속 정보 
        private SqlTransaction transaction;             //  데이터 베이스 관리 권한 부여
        private SqlCommand     cmd = new SqlCommand();  //  데이터 베이스 명령 전달 

        private void btnChangPw_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                // 비밀번호 변경 버튼 클릭.

                // 2. 비밀 번호 변경 프로시져 호출
                // 3. 마무리.

                // 11. 데이터 베이스 접속 경로 설정
                //string strConn = "Data Source=(local);Initial Catalog=AppDev;Integrated Security=SSPI;";  //DESKTOP-HNMKD38\SQLEXPRESS
                string strConn = "Data Source=222.235.141.8; Initial Catalog=AppDev;User ID=kfqs1;Password=1234";  //윈도우 계정 연결 false : SQL 계정 연결
                Connect = new SqlConnection(strConn);


                // 22. 데이터 베이스 연결 상태 확인.dcc
                Connect.Open();
                if (Connect.State != System.Data.ConnectionState.Open) MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");

                string sLogInId     = txtUserId.Text;
                string sPerPassWord = txtPerPW.Text;
                string sNewPassWord = txtNewPw.Text;
                 
                // 기존의 비밀 번호 찾기.
                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW FROM TB_USER_DSH WHERE USERID = '" + sLogInId + "'" , Connect);
                DataTable DtTemp = new DataTable();

                Adapter.Fill(DtTemp);

                // ID 존재 여부 확인.
                if (DtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("로그인 ID 가 잘못 되었습니다.");
                    txtUserId.Text = "";
                    txtPerPW.Text  = "";
                    txtNewPw.Text  = "";
                    txtUserId.Focus();
                    return;
                }

                // 기존 비밀 번호 비교 
                else if (sPerPassWord != DtTemp.Rows[0]["PW"].ToString())
                {
                    MessageBox.Show("비밀번호 가 잘못 되었습니다.");
                    txtPerPW.Text = "";
                    txtNewPw.Text = "";
                    txtPerPW.Focus();
                    return;
                }
                else // ID 와 비밀번호가 동일할 경우 비밀번호 변경
                {   
                    if (MessageBox.Show("해당 비밀번호로 변경을 진행 하시겠습니까?","비밀번호 변경",MessageBoxButtons.YesNo) == DialogResult.No) return;

                    // Start a local transaction.
                    transaction = Connect.BeginTransaction("SampleTransaction"); // 트랜잭션 선언

                    cmd.Transaction = transaction;      // 트랜잭션 설정 
                    cmd.Connection = Connect;
                    cmd.CommandText = "UPDATE TB_USER_DSH SET PW = '" + sNewPassWord + "' WHERE USERID = '" + sLogInId + "'";
                    cmd.ExecuteNonQuery();

                    transaction.Commit(); // 트랜잭션 등록 완료

                    MessageBox.Show("정상적으로 변경 하였습니다.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("비밀번호 변경 중 오류가 발생 하였습니다. \r\n" + ex.ToString());
                transaction.Rollback(); 
            }
            finally
            {   
                Connect.Close();
            }
            
        }

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangPw_Click(null, null);
            }
            
        }
    }
}
