using System;
using System.Windows.Forms;
using System.Reflection; 
namespace ApplicationDev
{   
    public partial class M0000_Main : Form
    {
        public M0000_Main()
        {
            InitializeComponent();

            M1000_LogIn FrmLogIn = new M1000_LogIn();
            FrmLogIn.ShowDialog();
            if (FrmLogIn.Tag.ToString() == "FAIL")
            {
                // 로그인 실패 시 스레드 종료 및 어플리케이션 죵료
                Application.ExitThread();
                Environment.Exit(0);
            }
            stslbUserName.Text          = FrmLogIn.Tag.ToString();
            menuStrip1.Text    = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            stslbNowTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }

        /// <summary>
        /// 조회 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ChildCommands("SEARCH");
        } 

        /// <summary>
        ///  추가 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ChildCommands("NEW");
        }
        /// <summary>
        ///  삭제 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ChildCommands("DELETE");
        }
        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ChildCommands("SAVE");
        }
        /// <summary>
        /// 닫기 버튼 클릭.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // 페이지 오픈 여부 확인
            if (mdiControl1.TabPages.Count == 0) return;
            // 현재 활성화 되어있는 페이지 닫기
            mdiControl1.SelectedTab.Dispose();

        }
        /// <summary>
        /// 종료 버튼 클릭.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                // 확인 후 프로그램 종료.
                if (MessageBox.Show("프로그램을 종료 하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Application.ExitThread();
                Environment.Exit(0);
            }
            catch
            {
            }
        }

       

        private void ChildCommands(string Command)
        {
            if (this.mdiControl1.TabPages.Count == 0) return;
            var Child = mdiControl1.SelectedTab.Controls[0] as DEV_FORM.IChildCommands;
            if (Child == null) return;

            Cursor.Current = Cursors.WaitCursor;

            switch (Command)
            {
                case "NEW":    Child.DoNew();   break;
                case "SAVE":   Child.Save();    break;
                case "SEARCH": Child.Inquire(); break;
                case "DELETE": Child.Delete();  break;
            }
            

            ////// 활성화 페이지 닫은 후 실행된 창 띄우기. 
            //mdiControl1.SelectedTab.Dispose();
            //mdiControl1.Add(MDIActiveForm);

            Cursor.Current = Cursors.Default;
        }

        private void M_System_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            //// 해당 화면 오픈.
            //try
            //{
            //    // 1. CS 파일을 직접 호출.
            //    //DEV_FORM.MDI_TEST Test = new DEV_FORM.MDI_TEST();

            // 2.프로그램을 호출
            //매뉴 구성을 DB 에서 설정 할때 사용 가능
            Assembly assembly = Assembly.LoadFrom(Application.StartupPath + @"\" + "DEV_FORM.DLL");
            Type typeForm = assembly.GetType("DEV_FORM." + e.ClickedItem.Name.ToString(), true);
            Form Test = (Form)Activator.CreateInstance(typeForm);

            //    //Test.MdiParent = this;
            //    //Test.WindowState = FormWindowState.Maximized;  // 화면 가득 채우기
            //    //Test.Show();


            // 3-1 중복 페이지 설정 시 리턴
            for (int i = 0; i < mdiControl1.TabPages.Count; i++)
            {
                if (mdiControl1.TabPages[i].Name == e.ClickedItem.Name.ToString()) return;
            }
            // 3. TAB PAGE 에 등록
            //DEV_FORM.MDI_TEST Test = new DEV_FORM.MDI_TEST();
            mdiControl1.Add(Test);

            //}
            //catch (Exception ex)
            //{

            //}
        }
    } 


    public partial class MDIControl : TabControl
    {
        public MDIControl()
        {

        }
        public void Add(Form sb)
        {
            if (sb == null) return;
            sb.TopLevel = false;   //  자식 창으로 사용하기 위하여  최상위 창 핸들링 방지
            MDIForm Page = new MDIForm();
            Page.Controls.Clear();
            Page.Controls.Add(sb);

            Page.Text = sb.Text;
            Page.Name = sb.Name;
            
            base.TabPages.Add(Page);
            sb.Show();
            base.SelectedTab = Page;
            return;
        }
    }
    public partial class MDIForm : TabPage
    {
        public MDIForm()
        {

        } 
    }
}
