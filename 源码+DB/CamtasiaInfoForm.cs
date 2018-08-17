using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamtasiaStudio;
using System.Runtime.InteropServices;

namespace CamtasiaStudio
{
    public partial class CamtasiaInfoForm : Form
    {
        public CamtasiaInfoForm()
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

        private void CamtasiaInfoForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
           
            //加载lvCamtasiaInfo控件数据
            lvCamtasiaInfoLoad();
        }

        public void lvCamtasiaInfoLoad()//加载事件
        {
            SqlConnection conn = null;
            SqlDataReader rd = null;
            try
            {
                conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlstr = "select * from Storage order by StorageNum";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                rd = cmd.ExecuteReader();
                int i = 1;
                while (rd.Read())//循环读取结果
                {
                    ListViewItem item = new ListViewItem("" + i);
                    item.SubItems.Add(rd[0].ToString());
                    item.SubItems.Add(rd[1].ToString());
                    lvCamtasiaInfo.Items.Add(item);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("仓库列表加载时出现异常,请联系管理员！\n原因：" + ex.Message);
            }
            finally
            {
                if (rd != null)
                {
                    rd.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

        //添加仓库窗体
        private void lblAddInfo_Click(object sender, EventArgs e)
        {
            AddCamtasiaForm af = new AddCamtasiaForm();
            af.ShowDialog();
            lvCamtasiaInfo.Items.Clear();//清空Listview
            lvCamtasiaInfoLoad();           //加载Listview
        }

        //修改仓库信息
        private void lblUpdateInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中需要修改的数据行！");
                return;
            }
            else
            {
                //根据选中行的编号查询出数据，传递到修改窗体显示在相对应的文本控件内
                string txtNOs = this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text.ToString();
                string txtNames = this.lvCamtasiaInfo.SelectedItems[0].SubItems[2].Text;
                UpdateCamtasiaForm ucf = new UpdateCamtasiaForm();
                UpdateCamtasiaForm.txtno = txtNOs;
                UpdateCamtasiaForm.txtname = txtNames;
                ucf.ShowDialog();
                lvCamtasiaInfo.Items.Clear();
                lvCamtasiaInfoLoad();
            }
        }

        //删除仓库信息
        private void lblDeleteInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中需要删除的数据行！");
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否确认删除该仓库信息！", "删除提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SqlConnection conn = null;
                    //根据选中行的编号进行删除数据
                  
                    conn = new SqlConnection(DBHelper.ConnString);
                    conn.Open();
                    int a = int.Parse(this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text);//获取选中行的ID
                    MessageBox.Show("" + a);
                    string sqlstr = string.Format("delete from Storage where StorageNum={0}", a);
                    SqlCommand cmd = new SqlCommand(sqlstr, conn);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("删除成功");
                        this.lvCamtasiaInfo.Clear();//清空ListView
                        lvCamtasiaInfo.Columns.Add("编号");//给ListView添加列
                        lvCamtasiaInfo.Columns.Add("仓库编号");
                        lvCamtasiaInfo.Columns.Add("仓库名称");
                        lvCamtasiaInfo.Columns[0].Width = 345;//设置ListView每列的宽度
                        lvCamtasiaInfo.Columns[1].Width = 428;
                        lvCamtasiaInfo.Columns[2].Width = 525;
                        lvCamtasiaInfoLoad();
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                 
                    conn.Close();
                  
                }
            }
        }

       

        private void CamtasiaInfoForm_Paint(object sender, PaintEventArgs e)
        {
           
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void CamtasiaInfoForm_ParentChanged(object sender, EventArgs e)
        {
            this.lvCamtasiaInfo.Clear();//清空ListView
            lvCamtasiaInfo.Columns.Add("编号");//给ListView添加列
            lvCamtasiaInfo.Columns.Add("仓库编号");
            lvCamtasiaInfo.Columns.Add("仓库名称");
            lvCamtasiaInfo.Columns[0].Width = 345;//设置ListView每列的宽度
            lvCamtasiaInfo.Columns[1].Width = 428;
            lvCamtasiaInfo.Columns[2].Width = 525;
            lvCamtasiaInfoLoad();
        }
    }
}
