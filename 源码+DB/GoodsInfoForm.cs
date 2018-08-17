using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamtasiaStudio
{
    public partial class GoodsInfoForm : Form
    {
        public GoodsInfoForm()
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

        private void GoodsInfoForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
           
            //加载所有商品信息
            LoadInfo();
        }

        private void LoadInfo()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlStr = "select GoodsID,ProducesM,GoodsName,GoodsPrice,GoodsJLDw,PnoName,GoodsDate,GoodsBZQ from Goods";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ListViewItem item = new ListViewItem(rd[0].ToString());
                    item.SubItems.Add(rd[1].ToString());
                    item.SubItems.Add(rd[2].ToString());
                    item.SubItems.Add(rd[3].ToString());
                    item.SubItems.Add(rd[4].ToString());
                    item.SubItems.Add(rd[5].ToString());
                    item.SubItems.Add(rd[6].ToString());
                    item.SubItems.Add(rd[7].ToString());
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

        //添加商品信息
        private void lblAddInfo_Click(object sender, EventArgs e)
        {
            AddGoodsForm agf = new AddGoodsForm();
            AddGoodsForm.UserName = UserName;
            agf.ShowDialog();
            //清空后加载数据
            this.lvCamtasiaInfo.Items.Clear();
            LoadInfo();
        }

        //修改商品信息
        private void lblUpdateInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中要修改的数据行！");
                return;
            }
            else
            {
                UpdateGoodsForm ugf = new UpdateGoodsForm();
                UpdateGoodsForm.goodsId = goodsId;
                ugf.ShowDialog();
                //清空后加载数据
                this.lvCamtasiaInfo.Items.Clear();
                LoadInfo();
            }
        }

        //删除商品
        private void lblDeleteInfo_Click(object sender, EventArgs e)
        {
            if (this.lvCamtasiaInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("还未选中要删除的数据行！");
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否确认删除该商品的信息？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //根据选中行的商品编号删除商品信息数据
                    try
                    {
                        SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                        conn.Open();
                        string sqlStr = string.Format("delete from Goods where GoodsID='{0}'", goodsId);
                        SqlCommand cmd = new SqlCommand(sqlStr,conn);
                        int Row = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (Row > 0)
                        {
                            MessageBox.Show("删除成功！");
                            //重新加载数据
                            this.lvCamtasiaInfo.Items.Clear();
                            LoadInfo();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误提示：" + ex.Message.ToString());
                    }
                }
            }
        }


        private void GoodsInfoForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }

        //记录商品编号
        string goodsId;
        private void lvCamtasiaInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in this.lvCamtasiaInfo.SelectedIndices)
            {
                goodsId = this.lvCamtasiaInfo.Items[i].SubItems[0].Text.ToString();
            }
        }
    }
}
