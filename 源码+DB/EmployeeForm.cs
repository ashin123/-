using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;//API
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamtasiaStudio
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        [DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        public const Int32 AW_CENTER = 0x00000010;//从中间向四周出现

        //定义获取当前登录的用户的姓名
        public static string UserName;
        //定义获取当前登录的用户的所属权限
        public static string UserPower;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
           
            if (UserPower == "普通用户")
            {
                this.lblAddInfo.Enabled = false;
                this.lblUpdateInfo.Enabled = false;
                this.lblDeleteInfo.Enabled = false;
            }
            LoadInfo();
        }

        private void LoadInfo()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlStr = "select * from [User]";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ListViewItem item = new ListViewItem(rd[0].ToString());
                    item.SubItems.Add(rd[1].ToString());
                    item.SubItems.Add(rd[2].ToString());
                    item.SubItems.Add(rd[3].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                rd.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

        //添加员工信息窗体
        private void lblAddInfo_Click(object sender, EventArgs e)
        {
            AddEmployeeForm aef = new AddEmployeeForm();
            aef.ShowDialog();
            //清空信息后刷新信息显示
            lvCamtasiaInfo.Items.Clear();
            LoadInfo();
        }

        //修改员工信息
        private void lblUpdateInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中要修改的员工信息！");
                return;
            }
            else
            {
                UpdateEmployeeForm uef = new UpdateEmployeeForm();
                UpdateEmployeeForm.employeeID = employeeID;//将该员工的编号传递到修改员工信息窗体
                uef.ShowDialog();
                //清除数据再次刷新显示
                lvCamtasiaInfo.Items.Clear();
                LoadInfo();
            }
        }

        //删除员工信息
        private void lblDeleteInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中要删除的员工信息！");
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否确认删除该员工的信息！", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //删除该员工的信息
                    SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                    conn.Open();
                    string sqlStr = string.Format("delete  from [User] where UserID='{0}'", employeeID);
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    int Row = cmd.ExecuteNonQuery();
                    if (Row > 0)
                    {
                        MessageBox.Show("删除成功！");
                        //删除成功后刷新列表信息
                        lvCamtasiaInfo.Items.Clear();
                        LoadInfo();
                    }
                }
            }
        }

        //选择时记录员工的编号
        string employeeID;
        private void lvCamtasiaInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in this.lvCamtasiaInfo.SelectedIndices)
            {
                employeeID = this.lvCamtasiaInfo.Items[i].SubItems[0].Text.ToString();
                //MessageBox.Show(employeeID);
            }
        }

        private void EmployeeForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
