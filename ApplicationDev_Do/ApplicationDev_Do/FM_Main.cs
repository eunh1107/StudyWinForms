using System;
using System.Reflection;
using System.Windows.Forms;

namespace ApplicationDev_Do
{
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            // 로그인 폼 호출
            FM_LogIn Login = new FM_LogIn();
            Login.ShowDialog();

            tssUserName.Text = Login.Tag.ToString();
            if (Login.Tag.ToString() == "FAIL")
            {
                System.Environment.Exit(0);
            }


            // 버튼 종료이벤 트 추가
            this.stbExit.Click += new System.EventHandler(this.stbExit_Click);

            // 버튼 닫기이벤트 추가
            this.stbclose.Click += new System.EventHandler(this.stbClose_Click);

            // 매뉴 클릭 이벤트 추가
            this.M_SYSTEM.DropDownItemClicked += 
                new System.Windows.Forms.ToolStripItemClickedEventHandler(this.M_SYSTEM_DropDownItemClicked);

            // 조회 버튼 이벤트 추가
            this.stbSearch.Click += new System.EventHandler(this.stbSearch_Click);
            // 추가 버튼 이벤트 추가
            this.stbInsert.Click += new System.EventHandler(this.stbInsert_Click);
            // 삭제 버튼 이벤트 추가
            this.stbDelete.Click += new System.EventHandler(this.stbDelete_Click);
            // 저장 버튼 이벤트 추가
            this.stbSave.Click   += new System.EventHandler(this.stbSave_Click);
        }
        private void stbSearch_Click(object sender, EventArgs e)
        {
            ChildCommand("SEARCH");
        }
        private void stbInsert_Click(object sender, EventArgs e)
        {
            ChildCommand("NEW");
        }
        private void stbDelete_Click(object sender, EventArgs e)
        {
            ChildCommand("DELETE");
        }
        private void stbSave_Click(object sender, EventArgs e)
        {
            ChildCommand("SAVE");
        }
        private void stbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stbClose_Click(object sender, EventArgs e)
        {
            // 열려있는 화면이 있는지 확인
            if (myTabControl1.TabPages.Count == 0) return;

            // 선택된 탭페이지를 닫는다.
            myTabControl1.SelectedTab.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssNowDate.Text = DateTime.Now.ToString();
        }

        private void M_SYSTEM_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 1. 단순히 폼을 호출하는 경우.
            //DEV_Form.MDI_TEST Form = new DEV_Form.MDI_TEST();
            //Form.MdiParent = this;
            //Form.Show();

            //2. 프로그램을 호출
            Assembly assemb = Assembly.LoadFrom(Application.StartupPath + @"\" + "DEV_Form.DLL");
            Type typeForm = assemb.GetType("DEV_Form." + e.ClickedItem.Name.ToString(), true);
            Form ShowForm = (Form)Activator.CreateInstance(typeForm);

            // 해당되는 폼이 이미 오픈되어 있는지 확인 후 활성화 또는 신규 오픈 한다.
            for (int i = 0; i < myTabControl1.TabPages.Count; i++)
            {
                if (myTabControl1.TabPages[i].Name == e.ClickedItem.Name.ToString())
                {
                    myTabControl1.SelectedTab =  myTabControl1.TabPages[i];
                    return;
                }
            }
            //ShowForm.MdiParent = this;
            //ShowForm.Show();
            myTabControl1.AddForm(ShowForm); // 탭페이지에 폼을 추가하여 오픈한다.
        }
        private void ChildCommand(string Command)
        {
            if (this.myTabControl1.TabPages.Count == 0) return;
            var Child = myTabControl1.SelectedTab.Controls[0] as DEV_Form.ChildInterFace;
            if (Child == null) return;
            switch (Command)
            {
                case "NEW"   : Child.DoNew();   break;
                case "SAVE"  : Child.Save();    break;
                case "SEARCH": Child.Inquire(); break;
                case "DELETE": Child.Delete();  break;
            }
        }



    }


    public partial class MDIForm : TabPage
    {}
    public partial class MyTabControl : TabControl
    {
        public void AddForm(Form NewForm)
        {
            if (NewForm == null) return;  // 인자로 받은 폼이 없을경우 실행 중지
            NewForm.TopLevel = false;     // 인자로 받은 폼이 최상위 개체가 아님을 선언 
            MDIForm page = new MDIForm(); // 탭 페이지 객체 생성 
            page.Controls.Clear();        // 페이지 초기화
            page.Controls.Add(NewForm);   // 페이지에 폼 추가
            page.Text = NewForm.Text;     // 폼에서 지정한 명칭으로 탭 페이지 설정
            page.Name = NewForm.Name;     // 폼에서 설정한 이름으로 탭 페이지 설정
            base.TabPages.Add(page);      // 탭 컨트롤에 페이지를 추가한다.
            NewForm.Show();               // 인자로 받은 폼을 보여준다
            base.SelectedTab = page;      // 현재 선택된 페이지를 호출한 폼의 페이지로 설정한다.
        }
    }
}
