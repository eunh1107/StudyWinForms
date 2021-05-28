using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_Form
{
    public partial class FM_USER : BaseMDIChildForm
    {
        public FM_USER()
        {
            InitializeComponent();
        }
        public override void Inquire()

        {
            base.Inquire();

            // DB HELPER 선언
            DBHelper helper = new DBHelper(false);
            try
            {
                string sUserId = txtUserID.Text;
                string sUserName = txtUserName.Text;
                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("SP_USER_KEH_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("USERID", sUserId)
                                          , helper.CreateParameter("USERNAME", sUserName));

                if (dtTemp.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("조회할 데이터가 없습니다.");
                }
                else
                {
                    // 그리드 뷰에 데이터 삽입
                    dataGridView1.DataSource = dtTemp;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                helper.Close();
            }
        }
        public override void DoNew()
        {
            base.DoNew();
            DataRow dr = ((DataTable)dataGridView1.DataSource).NewRow();
            ((DataTable)dataGridView1.DataSource).Rows.Add(dr);
        }
        public override void Delete()
        {
            base.Delete();
            if (dataGridView1.Rows.Count == 0) return;
            //int iSelectIndex = dataGridView1.CurrentCell.RowIndex;
            //DataTable dtTemp = (DataTable)dataGridView1.DataSource;
            //dtTemp.Rows[iSelectIndex].Delete();

            string sUserId = dataGridView1.CurrentRow.Cells["USERID"].Value.ToString();
            DataTable dtTemp = (DataTable)dataGridView1.DataSource;
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (dtTemp.Rows[i].RowState == DataRowState.Deleted) continue;
                if (dtTemp.Rows[i][0].ToString() == sUserId)
                {
                    dtTemp.Rows[i].Delete();
                    break;
                }
            }
        }

        public override void Save()
        {
            base.Save();
            string UserId = string.Empty;
            string sUserName = string.Empty;
            string sPassWord = string.Empty;

            DataTable dtTemp = ((DataTable)dataGridView1.DataSource).GetChanges();
            if (dtTemp == null) return;

            if (MessageBox.Show("데이터를 등록 하시겠습니까?", "데이터 저장",
                MessageBoxButtons.YesNo) == DialogResult.No) return;
            DBHelper helper = new DBHelper(true);
            try
            {
                // 트랜잭션 설정
                // 데이터 테이블의 상태 체크
                foreach (DataRow drRow in dtTemp.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            drRow.RejectChanges();
                            UserId = drRow["USERID"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_KEH_D1",
                                                    CommandType.StoredProcedure
                                                    , helper.CreateParameter("USERID", UserId));
                            break;
                        case DataRowState.Added:
                            #region 추가 
                            UserId = drRow["USERID"].ToString();
                            sUserName = drRow["USERNAME"].ToString();
                            sPassWord = drRow["PW"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_KEH_I1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("USERID", UserId)
                                                    , helper.CreateParameter("USERNAME", sUserName)
                                                    , helper.CreateParameter("PASSWORD", sPassWord)
                                                    , helper.CreateParameter("MAKER", Common.LogInId)
                                                    );
                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정 
                            UserId = drRow["USERID"].ToString();
                            sUserName = drRow["USERNAME"].ToString();
                            sPassWord = drRow["PW"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_KEH_U1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("USERID", UserId)
                                                    , helper.CreateParameter("USERNAME", sUserName)
                                                    , helper.CreateParameter("PASSWORD", sPassWord)
                                                    , helper.CreateParameter("EDITOR", Common.LogInId));

                            #endregion
                            break;
                    }
                }

                // 성공 시 DB COMMIT
                helper.Commit();
                // 메세지
                MessageBox.Show("정상적으로 등록 하였습니다.");

                // 재조회
                Inquire();
            }
            catch (Exception ex)
            {
                // 트랜잭션 롤백
                helper.Rollback();
                // 메세지
                MessageBox.Show("데이터 등록에 실패 하였습니다.");
            }
            finally
            {
                // DB CLOSE
                helper.Close();
            }
        }
    }
}
        