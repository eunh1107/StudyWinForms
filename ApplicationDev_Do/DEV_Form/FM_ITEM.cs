using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace DEV_Form
{
    public partial class FM_ITEM : Form , ChildInterFace
    {
        private SqlConnection Connect = null; // 접속 정보 객체 명명
        // 접속 주소 
        private string strConn = "Data Source=61.105.9.203; Initial Catalog=AppDev;User ID=kfqs1;Password=1234";

        public FM_ITEM()
        {
            InitializeComponent();
        }

        public void Inquire()
        {
            btnSearch_Click(null,null);
        }
        public void DoNew()
        {
        }
        public void Delete()
        {
        }
        public void Save()
        {
        }

        private void FM_ITEM_Load(object sender, EventArgs e)
        {
            try
            {
                // 콤보박스 품목 상세 데이터 조회 및 추가
                // 접속 정보 커넥선 에 등록 및 객체 선언
                Connect = new SqlConnection(strConn);
                Connect.Open();

                if (Connect.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");
                    return;
                }

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT ITEMDESC FROM TB_TESTITEM_KEH ", Connect);
                DataTable dtTemp = new DataTable();
                adapter.Fill(dtTemp);

                cboItemDesc.DataSource = dtTemp;
                cboItemDesc.DisplayMember = "ITEMDESC"; // 눈으로 보여줄 항목
                cboItemDesc.ValueMember = "ITEMDESC"; // 실제 데이터를 처리할 코드 항목 
                cboItemDesc.Text = "";

                // 원하는 날짜 픽스
                dtpStart.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvGrid.DataSource = null; // 그리드의 데이터 소스를 초기화 한다.
                Connect = new SqlConnection(strConn);
                Connect.Open();

                if (Connect.State != System.Data.ConnectionState.Open)
                {   MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");
                    return; }

                // 조회를 위한 파라매터 설정
                string sItemCode  = txtItemCode.Text;  // 품목 코드
                string sItemName  = txtItemName.Text;  // 품목 명
                string sStartDate = dtpStart.Text;     // 출시 시작 일자 
                string sEndDate   = dtpEnd.Text;       // 출시 종료 일자
                string sItemdesc  = cboItemDesc.Text;  // 품목 상세

                string sEndFlag = "N";
                if (rdoEnd.Checked == true)      sEndFlag = "Y";  // 단종여부
                if (chkNameOnly.Checked == true) sItemCode = "";  // 이름으로만 검색

                string Sql = "SELECT ITEMCODE,  " +
                             "       ITEMNAME,  " +
                             "       ITEMDESC,  " +
                             "       ITEMDESC2, " +
                             "       CASE WHEN ENDFLAG = 'Y' THEN '단종' " +
                             "       WHEN ENDfLAG = 'N' THEN '생산' END AS  ENDFLAG,   " +
                             "       PRODDATE,  " +
                             "       MAKEDATE,  " +
                             "       MAKER,     " +
                             "       EDITDATE,  " +
                             "       EDITOR     " +
                             "  FROM TB_TESTITEM_KEH WITH(NOLOCK) " +
                             " WHERE ITEMCODE LIKE '%" + sItemCode + "%' " +
                             "   AND ITEMNAME LIKE '%" + sItemName + "%' " +
                             "   AND ITEMDESC LIKE '%" + sItemdesc + "%' " +
                             "   AND ENDFLAG  = '" + sEndFlag + "'" +
                             "   AND PRODDATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "'";
                
                SqlDataAdapter Adapter = new SqlDataAdapter(Sql , Connect);

                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0)
                {
                    dgvGrid.DataSource = null;
                    return;
                }
                dgvGrid.DataSource = dtTemp; // 데이터 그리드 뷰에 데이터 테이블 등록

                // 그리드뷰의 헤더 명칭 선언
                dgvGrid.Columns["ITEMCODE"].HeaderText  = "품목 코드";
                dgvGrid.Columns["ITEMNAME"].HeaderText  = "품목 명";
                dgvGrid.Columns["ITEMDESC"].HeaderText  = "품목 상세";
                dgvGrid.Columns["ITEMDESC2"].HeaderText = "품목 상세2";
                dgvGrid.Columns["ENDFLAG"].HeaderText   = "단종 여부";
                dgvGrid.Columns["PRODDATE"].HeaderText  = "출시 일자";
                dgvGrid.Columns["MAKEDATE"].HeaderText  = "등록 일시";
                dgvGrid.Columns["MAKER"].HeaderText     = "등록자";
                dgvGrid.Columns["EDITDATE"].HeaderText  = "수정일시";
                dgvGrid.Columns["EDITOR"].HeaderText    = "수정자";

                // 그리드 뷰의 폭 지정
                dgvGrid.Columns[0].Width = 100;
                dgvGrid.Columns[1].Width = 200;
                dgvGrid.Columns[2].Width = 200;
                dgvGrid.Columns[3].Width = 200;
                dgvGrid.Columns[4].Width = 100;

                // 컬럼의 수정 여부를 지정 한다
                dgvGrid.Columns["ITEMCODE"].ReadOnly = true;
                dgvGrid.Columns["MAKER"].ReadOnly    = true;
                dgvGrid.Columns["MAKEDATE"].ReadOnly = true;
                dgvGrid.Columns["EDITOR"].ReadOnly   = true;
                dgvGrid.Columns["EDITDATE"].ReadOnly = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 데이터 그리드 뷰 에 신규 행 추가
            DataRow dr = ((DataTable)dgvGrid.DataSource).NewRow();
            ((DataTable)dgvGrid.DataSource).Rows.Add(dr);
            dgvGrid.Columns["ITEMCODE"].ReadOnly = false;   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 선택된 행을 삭제 한다.
            if (this.dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 삭제 하시겠습니까", "데이터삭제", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            SqlCommand cmd = new SqlCommand();
            SqlTransaction tran;

            Connect = new SqlConnection(strConn);
            Connect.Open();

            // 트랜잭션 관리를 위한 권한 위임 
            tran = Connect.BeginTransaction("TranStart");
            cmd.Transaction = tran;
            cmd.Connection = Connect;

            try
            {
                string Itemcode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "DELETE TB_TESTITEM_KEH WHERE ITEMCODE = '" + Itemcode + "'";

                cmd.ExecuteNonQuery();

                // 성공 시 DB Commit
                tran.Commit();
                MessageBox.Show("정상적으로 삭제 하였습니다.");
                btnSearch_Click(null, null); // 데이터 재조회
            }
            catch
            {
                tran.Rollback();
                MessageBox.Show("데이터 삭제에 실패 하였습니다.");
            }
            finally
            {
                Connect.Close();
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 선택된 행 데이터 저장 
            if (dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 등록 하시겠습니까?", "데이터등록", 
                                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sItemCode    = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
            string sItemName    = dgvGrid.CurrentRow.Cells["ITEMNAME"].Value.ToString();
            string sItemDesc    = dgvGrid.CurrentRow.Cells["ITEMDESC"].Value.ToString();
            string sItemDesc2   = dgvGrid.CurrentRow.Cells["ITEMDESC2"].Value.ToString();
            string sItemEndFlag = dgvGrid.CurrentRow.Cells["ENDFLAG"].Value.ToString();
            string sProdDate    = dgvGrid.CurrentRow.Cells["PRODDATE"].Value.ToString();

            SqlCommand cmd = new SqlCommand();
            SqlTransaction Tran;

            Connect = new SqlConnection(strConn);
            Connect.Open();

            // 데이터 조회 후 해당 데이터가 있는지 확인 후 UPDATE , INSERT 구문 분기
            //string sSql = "SELECT ITEMCODE FROM TB_TESTITEM_KEH WHERE ITEMCODE = '" + sItemCode + "'";
            //SqlDataAdapter adapter = new SqlDataAdapter(sSql, Connect);
            //DataTable dtTemp = new DataTable();
            //adapter.Fill(dtTemp);


            // 트랜잭션 설정
            Tran = Connect.BeginTransaction("TestTran");
            cmd.Transaction = Tran;
            cmd.Connection = Connect;

            cmd.CommandText = "UPDATE TB_TESTITEM_KEH                         "  +
                              "    SET ITEMNAME  = '"  + sItemName     + "',   " +
                              "        ITEMDESC  = '"  + sItemDesc     + "',   " +
                              "        ITEMDESC2 = '"  + sItemDesc2    + "',   " +
                              "        ENDFLAG   = '"  + "N"           + "',   " +
                              "        PRODDATE  = '"  + sProdDate     + "',   " +
                              "        EDITOR = '"    + Common.LogInId + "',  "  +
                              "        EDITDATE = GETDATE()          " +
                              "  WHERE ITEMCODE = '" + sItemCode + "'" +
                              " IF (@@ROWCOUNT =0) " +
                              "INSERT INTO TB_TESTITEM_KEH(ITEMCODE,           ITEMNAME,            ITEMDESC,           ITEMDESC2,          ENDFLAG,           PRODDATE,      MAKEDATE,     MAKER) " +
                              "VALUES('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','" + "N" + "','" + sProdDate + "',GETDATE(),'" + Common.LogInId + "')";


            // 데이터가 있는경우 UPDATE, 없는경우 INSERT
            //if (dtTemp.Rows.Count == 0)
            //{
            //    // 데이터가 없으니 INSERT 해라
            //    cmd.CommandText = "INSERT INTO TB_TESTITEM_KEH  (ITEMCODE,       ITEMNAME,                 ITEMDESC,        ITEMDESC2,                 ENDFLAG,                 PRODDATE,      MAKEDATE,   MAKER)" +
            //                      "                      VALUES ('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','" + "N" + "','" + sProdDate + "',GETDATE(),'" + "" + "')";
            //}
            //else
            //{
            //    // 데이터가 있으니 update 해라
            //    cmd.CommandText = "UPDATE TB_TESTITEM_KEH                        " +
            //                      "    SET ITEMNAME = '"   + sItemName    + "',  " +
            //                      "        ITEMDESC = '"   + sItemDesc    + "',  " +
            //                      "        ITEMDESC2 = '"  + sItemDesc2   + "',  " +
            //                      "        ENDFLAG = '"    + "N"          + "',  " +
            //                      "        PRODDATE = '"   + sProdDate    + "',  " +
            //                      "        EDITOR = '',  " +
            //                    //"        EDITOR = '"     + Commoncs.LoginUserID + "',  " +
            //                      "        EDITDATE = GETDATE()     "                    +
            //                      "  WHERE ITEMCODE = '"   + sItemCode                   + "'";
            //}
            cmd.ExecuteNonQuery();

            // 성공 시 DB COMMIT
            Tran.Commit();
            MessageBox.Show("정상적으로 등록 하였습니다.");
            Connect.Close();
        }

        private void btnLoadPic_Click(object sender, EventArgs e)
        {
            try
            {
                string sImageFile = string.Empty;
                // 이미지 불러오기 및 저장. 파일 탐색기 호출. 
                if (dgvGrid.Rows.Count == 0) return;

                OpenFileDialog Dialog = new OpenFileDialog();
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    sImageFile = Dialog.FileName;
                    picImage.Tag = Dialog.FileName;
                }
                else return;
                picImage.Image = Bitmap.FromFile(sImageFile);  // 지정된 파일에서 System.Drawing.Image를 만든다.
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPicSave_Click(object sender, EventArgs e)
        {
            // 픽처박스의 이미지 저장. 
            if (dgvGrid.Rows.Count == 0) return;
            if (picImage.Image == null) return;
            if (picImage.Tag.ToString() == "") return;

            if (MessageBox.Show("선택된 이미지로 등록 하시겠습니까?", "이미지 등록", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Byte[] bImage = null;
            Connect = new SqlConnection(strConn);

            try
            {
                //바이너리 코드는 컴퓨터가 인식할 수 있는 0과 1로 구성된 이진코드를 의미한다.

                //바이트 코드는  CPU가 아닌 가상 머신에서 이해할 수 있는 코드를 위한 이진 표현법이다. 즉, 가상 머신이 이해할 수 있는0과 1로 구성된 이진코드를 의미.
                // 고급언어로 작성된 소스코드를 가상 머신이 이해할 수 있는 중간 코드로 컴파일한 것을 말한다.

                // 파일을 불러오기 위한 파일 스트림 선언 및 이미지 파일 바이너리 파일로 저장.
                // 현재 FileStream 개체가 파일의 경로, 파일을 열거나 만드는 방법을 결정 , 읽기 권한으로 접근     
                FileStream stream = new FileStream(picImage.Tag.ToString(), FileMode.Open, FileAccess.Read);  // 읽어들일 파일을 바이너리 화 할 대상(FileStream) 으로 변경한다.
                BinaryReader reader = new BinaryReader(stream);                                                   // 읽어들인 파일을 바이너리 코드로 객체를 생성 한다.
                bImage = reader.ReadBytes(Convert.ToInt32(stream.Length));                                        // 만들어진 바이너리 코드의 이미지를 Byte 화 하여 데이터 형식으로 저장 한다.
                reader.Close();                                                                                   // 바이너리 리더를 종료한다.
                stream.Close();                                                                                   // 스트림 리더를 종료한다.

                SqlCommand cmd = new SqlCommand();                              // 커멘드 설정

                cmd.Connection = Connect;
                Connect.Open();

                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "UPDATE  TB_TESTITEM_KEH SET ITEMIMG = @Image WHERE ITEMCODE = @ItemCode";
                cmd.Parameters.AddWithValue("@Image", bImage);
                cmd.Parameters.AddWithValue("@ItemCode", sItemCode);

                cmd.ExecuteNonQuery();
                MessageBox.Show("정상적으로 등록 하였습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("이미지 저장 중 오류가 발생하였습니다.");
            }
            finally
            {
                Connect.Close();
            }
        }

        private void dgvGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 선택 시 해당품목 이미지 가져오기.
            string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
            Connect = new SqlConnection(strConn);
            Connect.Open();
            try
            {
                // 이미지 초기화
                picImage.Image = null;
                string sSql = "SELECT ITEMIMG FROM TB_TESTITEM_KEH WHERE ITEMCODE = '" + sItemCode 
                              + "' AND ITEMIMG IS NOT NULL";
                SqlDataAdapter Adapter = new SqlDataAdapter(sSql, Connect);
                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0) return;

                byte[] bImage = null;
                bImage = (byte[])dtTemp.Rows[0]["ITEMIMG"]; // 이미지를 byte 화 한다.
                if (bImage != null)
                {
                    picImage.Image = new Bitmap(new MemoryStream(bImage)); // 메모리 스트림을 이용하여 파일을 그림 파일로 만든다.
                    picImage.BringToFront();
                }
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Connect.Close();
            }

        }

        private void btnPicDelete_Click(object sender, EventArgs e)
        {
            // 품목에 저장된 이미지 삭제
            if (dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택한 이미지를 삭제하시겠습니까?", 
                "이미지삭제", MessageBoxButtons.YesNo) == DialogResult.No) return;

            SqlCommand cmd = new SqlCommand();
            Connect = new SqlConnection(strConn);
            Connect.Open();

            try
            {
                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "UPDATE TB_TESTITEM_KEH SET ITEMIMG = null WHERE ITEMCODE = '"  
                                  + sItemCode + "'";
                cmd.Connection = Connect;
                cmd.ExecuteNonQuery();
                picImage.Image = null;
                MessageBox.Show("정상적으로 삭제 하였습니다.");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connect.Close();
            }
        }
    }
}
