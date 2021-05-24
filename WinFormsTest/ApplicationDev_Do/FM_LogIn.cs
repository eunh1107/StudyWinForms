using System; 
using System.Data; 
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ApplicationDev_Do
{
    public partial class FM_LogIn : Form
    {

        private SqlConnection Connect = null;


        public FM_LogIn()
        {
            InitializeComponent();
            this.Tag = "FAIL";
        }

        private void btnPWChang_Click(object sender, EventArgs e)
        {
            // 비밀번호 변경 화면 팝업을 호출한다.
            this.Visible = false; // 로그인 화면을 보이지 않게 한다. 
            FM_PassWord FmPassWord = new FM_PassWord();
            FmPassWord.ShowDialog();
            this.Visible = true;
        }
        private int PwFailCount = 0;
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {

                // 11. 데이터 베이스 접속 경로 설정
                string strConn = "Data Source=61.105.9.203; Initial Catalog=AppDev;User ID=kfqs1;Password=1234";  //DESKTOP-HNMKD38\SQLEXPRESS 
                //"Server=localhost; Uid=test;Pwd =test; database=test; "
                //string strConn = "Server=localhost; Uid=test;Pwd =test; database=test; ";  //DESKTOP-HNMKD38\SQLEXPRESS 
                Connect = new SqlConnection(strConn);


                // 22. 데이터 베이스 연결 상태 확인.
                Connect.Open();
                if (Connect.State != System.Data.ConnectionState.Open) MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");

                string sLogInId = txtUserID.Text;
                string sPassWord = txtPassword.Text;

                // 기존의 비밀 번호 찾기.
                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW,USERNAME FROM TB_USER_KEH WHERE USERID = '" + sLogInId + "'", Connect);
                DataTable DtTemp = new DataTable();

                Adapter.Fill(DtTemp);

                // ID 존재 여부 확인.
                if (DtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("로그인 ID가 잘못 되었습니다.");
                    //txtUserID.Text = "";
                    txtPassword.Focus();
                    return;
                }

                // 기존 비밀 번호 비교 
                else if (sPassWord != DtTemp.Rows[0]["PW"].ToString())
                {

                    txtPassword.Text = "";
                    txtPassword.Focus();
                    PwFailCount += 1;
                    MessageBox.Show("비밀번호가 잘못 되었습니다. 누적 횟수 : " + PwFailCount.ToString());
                    if (PwFailCount == 3)
                    {
                        MessageBox.Show("3회 이상 비밀번호를 잘못입력하여 프로그램을 종료합니다.");
                        this.Close();
                    }
                    return;
                }
                else
                {
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogIn_Click(null, null);
            }
        }
    }
}
