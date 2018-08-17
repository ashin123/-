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
    public partial class UpdateEmployeeForm : Form
    {
        public UpdateEmployeeForm()
        {
            InitializeComponent();
        }
        //用于接收员工信息窗体传递过来的员工编号
        public static string employeeID;

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //加载要修改的信息
        private void UpdateEmployeeForm_Load(object sender, EventArgs e)
        {
            try
            {
                //根据员工的编号查询出该员工的所有信息（此处对员工的编号不允许修改）
                this.txtNum.Text = employeeID;
                string sqlStr = string.Format("select UserName,UserPwd,UserType from [User] where UserID='{0}'", employeeID);
                DataTable dt = DBHelper.GetDataTable(sqlStr);
                this.txtName.Text = dt.Rows[0]["UserName"].ToString();
                this.txtPwd.Text = dt.Rows[0]["UserPwd"].ToString();
                this.cbPower.Text = dt.Rows[0]["UserType"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示："+ex.Message.ToString());
            }
        }

        //提交，根据该员工编号向数据库修改该员工的信息
        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                //此处要先判断用户是否将控件内的信息清除
                string name = this.txtName.Text.Trim();
                string pwd = this.txtPwd.Text.Trim();
                string type = this.cbPower.Text.Trim();
                if (name.Length == 0)
                {
                    MessageBox.Show("员工姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtName.Focus();
                    return;
                }
                if (pwd.Length == 0)
                {
                    MessageBox.Show("员工密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPwd.Focus();
                    return;
                }
                if (type.Length == 0)
                {
                    MessageBox.Show("员工权限不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.cbPower.Focus();
                    return;
                }
                //验证员工权限只能为“管理员”，“普通员工”
                if (type != "管理员" && type != "普通用户")
                {
                    MessageBox.Show("员工权限只能为“管理员”或“普通用户”！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.cbPower.Focus();
                    return;
                }
                //验证结果都通过后，即向数据库提交数据
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlStr = string.Format("update [User] set UserName='{0}',UserPwd='{1}',UserType='{2}' where UserID='{3}'", name, pwd, type, employeeID);
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                int Row = cmd.ExecuteNonQuery();
                conn.Close();
                if (Row > 0)
                {
                    MessageBox.Show("该员工信息修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
