using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DEV_FORM
{
    public partial class FM_ColumnChart : DEV_FORM.BaseMDIChildForm
    {
        private DataTable dtTempNon = new DataTable();
        public FM_ColumnChart()
        {
            InitializeComponent();
        }

        private void FM_ColumnChart_Load(object sender, EventArgs e)  //창이 쨘하고 나올때 생기는 이벤트
        {
            //콤보 박스 데이터 생성
            DBHelper helper = new DBHelper(false);
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("SP_CHART_KEH_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("ITEMCODE", ""));
                if (dtTemp.Rows.Count == 0)  //데이터가 없으면
                {
                    return;
                }
                else
                {
                    comboBox1.DataSource = dtTemp;
                    comboBox1.DisplayMember = "ITEMNAME";
                    comboBox1.ValueMember = "ITEMCODE";
                }
            }
            catch (Exception ex) { }
            finally
            {
                helper.Close();
            }

            // 깡통 테이블 생성(데이터 그리드 뷰 셋팅)
            dtTempNon.Columns.Add("PRODDATE");
            dtTempNon.Columns.Add("ITEMCODE");
            dtTempNon.Columns.Add("ITEMNAME");
            dtTempNon.Columns.Add("PRODCOUNT");
            dataGridView1.DataSource = dtTempNon;    //화면이 열릴때 그리드 뷰에 비어있는 데이터 테이블을 미리 등록한다.

            // 그리드 뷰의 헤더 명칭 선언
            dataGridView1.Columns["PRODDATE"].HeaderText = "생산일자";
            dataGridView1.Columns["ITEMCODE"].HeaderText = "품목코드";
            dataGridView1.Columns["ITEMNAME"].HeaderText = "품목명";
            dataGridView1.Columns["PRODCOUNT"].HeaderText = "생산수량";

            // 그리드 뷰의 폭 지정
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;

            // 컬럼의 수정 여부 지정한다.
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
        }
        public override void Inquire()
        {
            base.Inquire();
            DBHelper helper = new DBHelper(false);
            try
            {
                string sItemCode = comboBox1.SelectedValue.ToString();
                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("SP_CHART_KEH_S2", CommandType.StoredProcedure
                   , helper.CreateParameter("ITEMCODE", sItemCode));
                if(dtTemp.Rows.Count == 0)
                {
                    dataGridView1.DataSource = dtTempNon;
                    MessageBox.Show("조회할 데이터가 없습니다.");
                }
                   else
                {
                    // 그리드뷰에 데이터 삽입
                    dataGridView1.DataSource = dtTemp;

                    // 컬럼차트 생성
                    MakeColumChart(helper, sItemCode);
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
        private void MakeColumChart(DBHelper helper, string sItemCode)
        {
            DataTable dtTemp = new DataTable();
            chart1.Series.Clear();
            dtTemp = helper.FillTable("SP_CHART_KEH_S3", CommandType.StoredProcedure
                    , helper.CreateParameter("ITEMCODE", sItemCode));
            if (dtTemp.Rows.Count == 0)
            {
                return;
            }
            // 데이터 테이블 차트에 바인딩 한다.
            chart1.DataBindTable(dtTemp.DefaultView, "PRODDATE");
            // 바인딩 된 시리즈의 이름을 지정한다.
            chart1.Series[0].Name = dtTemp.Rows[0]["ITEMNAME"].ToString();
            chart1.Series[0].Color = Color.Green;                 // 차트에 색표시
            chart1.Series[0].IsValueShownAsLabel = true;          // 차트에 값표시
        }
    }
}