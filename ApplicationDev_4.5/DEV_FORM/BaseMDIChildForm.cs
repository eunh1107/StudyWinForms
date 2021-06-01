using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_FORM
{
    public partial class BaseMDIChildForm : Form, IChildCommands
    {
        public BaseMDIChildForm()
        {
            InitializeComponent();
        }
        public virtual void Inquire()
        {
            //MessageBox.Show("조회 버튼 클릭");
        }
        public virtual void DoNew() //  메서드, 속성, 인덱서 또는 이벤트 선언을 수정하고 파생 클래스에서 재정의하도록 허용하는 데 사용
        {

        }
        public virtual void Delete()
        {

        }
        public virtual void Save()
        {

        }

    }
}
