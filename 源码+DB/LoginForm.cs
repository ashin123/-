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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //获取用户名、权限、密码
            string name = this.txtName.Text.Trim();
            string power = this.cbType.Text;
            string pwd = this.txtPwd.Text.Trim();
            //非空验证验证
            if (name == "")
            {
                MessageBox.Show("用户名不能为空", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtName.Focus();
                return;
            }
            if (pwd == "")
            {
                MessageBox.Show("密码不能为空", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                this.txtPwd.Focus();
                return;
            }
            //根据用户名查询出用户的信息
            string sqlStr = string.Format("select * from [User] where UserName='{0}'", name);
            DataTable dt = DBHelper.GetDataTable(sqlStr);
            //当返回数据为零行时显示不存在该用户
            if (dt.Rows.Count==0)
            {
                MessageBox.Show("该用户不存在", "操作提示", MessageBoxButtons.OK,MessageBoxIcon.Hand);
                txtName.Clear();
                txtPwd.Clear();
                txtName.Focus();
                return;
            }
            string userName = dt.Rows[0]["UserName"].ToString();
            string userType = dt.Rows[0]["UserType"].ToString();
            string userPwd = dt.Rows[0]["UserPwd"].ToString();
            //若存在该用户时则先验证用户的类型是否正确
            if (name == userName && power == userType)
            {
                //若用户的类型正确进一步验证密码是否正确
                if (pwd == userPwd)
                {
                    MainForm mf = new MainForm();
                    MainForm.UserName = name;
                    MainForm.UserPower = power;
                    mf.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码错误", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtPwd.Clear();
                    txtPwd.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("该用户类型不正确", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        //用户类型
        private void LoadCB()
        {
            try
            {
                string str = "select distinct UserType from [User]";
                DataTable dt = DBHelper.GetDataTable(str);
                //CombBox相当于DataGridView
                //1.绑定数据源 DataSource属性
                cbType.DataSource = dt;
                //DispilayMember显示值绑定字段  
                //2.CombBox显示值绑定字段
                //直接绑定字段
                cbType.DisplayMember = "UserType";
                //3.CombBox项的实际值绑定字段 不显示的
                cbType.ValueMember = "UserType";
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常错误：" + ex.Message);
            }
        }

        //退出
      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
        //窗体加载事件
        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoadCB();
            this.AcceptButton = btnLogin;//登录按钮
            this.CancelButton = button1;//取消按钮
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Application.Exit();

        }

        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    Application.Exit();
        //}
    }
}
