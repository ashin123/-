using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamtasiaStudio
{
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //添加员工信息
        private void btnAccept_Click(object sender, EventArgs e)
        {
            //先获取控件中的信息
            string name = this.txtName.Text.Trim();
            string pwd = this.txtPwd.Text.Trim();
            string type = this.cbPower.Text;
            //验证判断
            if (name.Length == 0)
            {
                MessageBox.Show("用户名不能为空！");
                this.txtName.Focus();
                return;
            }
            if (pwd.Length == 0)
            {
                MessageBox.Show("密码不能为空！");
                this.txtPwd.Focus();
                return;
            }
            if (type == "--请选择用户权限--")
            {
                MessageBox.Show("请选择该用户的权限！");
                return;
            }
            //条件都通过后向数据库提交信息
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlStr = string.Format("insert into [User](UserName,UserPwd,UserType) values('{0}','{1}','{2}')", name, pwd, type);
                SqlCommand cmd = new SqlCommand(sqlStr,conn);
                int Row = cmd.ExecuteNonQuery();
                conn.Close();
                if (Row > 0)
                {
                    MessageBox.Show("添加员工信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }

        //下拉列表的默认项
        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            this.cbPower.SelectedIndex = 0;
        }

    }
}
