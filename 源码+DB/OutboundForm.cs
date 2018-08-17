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
    public partial class OutboundForm : Form
    {
        public OutboundForm()
        {
            InitializeComponent();
        }

        [DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        public const Int32 AW_CENTER = 0x00000010;//从中间向四周出现

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //定义获取当前登录的用户的姓名
        public static string UserName;
        //定义获取当前登录的用户的所属权限
        public static string UserPower;

        private void OutboundForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
           
            //绑定下拉框的值
            this.cbType.Items.Add("全部选项");
            this.cbType.Items.Add("按制单人");
            this.cbType.Items.Add("按确认人");
            this.cbType.Items.Add("按所属仓库");
            this.cbType.Items.Add("按商品名称");
            this.cbType.Items.Add("按生产厂家");
            this.cbType.Items.Add("按生产日期");
            this.cbType.SelectedIndex = 0;

            //将数据绑定到listview控件中
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sql = "select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og ,Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["OutNum"].ToString());
                    item.SubItems.Add(dr["ProducesM"].ToString());
                    item.SubItems.Add(dr["StorageName"].ToString());
                    item.SubItems.Add(dr["GoodsName"].ToString());
                    item.SubItems.Add(dr["GoodsPrice"].ToString());
                    item.SubItems.Add(dr["GoodsNum"].ToString());
                    item.SubItems.Add(dr["GoodsJLDw"].ToString());
                    item.SubItems.Add(dr["GoodsBZQ"].ToString());
                    item.SubItems.Add(dr["GoodsDate"].ToString());
                    item.SubItems.Add(dr["PnoName"].ToString());
                    item.SubItems.Add(dr["ZdName"].ToString());
                    item.SubItems.Add(dr["QrName"].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因是：" + ex.Message.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

        string sql;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.lvCamtasiaInfo.Items.Clear();
                string values = this.txtName.Text;
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                if (cbType.Text != "全部选项" && txtName.Text == "")
                {
                    MessageBox.Show("请输入查询条件");
                    return;
                }
                if (cbType.Text == "全部选项")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum ");
                }
                if (cbType.Text == "按制单人")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s  where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and ZdName like '%{0}%'", values);
                }
                if (cbType.Text == "按确认人")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s  where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and QrName like '%{0}%'", values);
                }
                if (cbType.Text == "按所属仓库")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and   StorageName like '%{0}%'", values);
                }
                if (cbType.Text == "按商品名称")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and  GoodsName  like '%{0}%'", values);
                }
                if (cbType.Text == "按生产厂家")
                {
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and  PnoName like '%{0}%'", values);
                }
                if (cbType.Text == "按生产日期")
                {
                    DateTime date = DateTime.Parse(this.txtName.Text);
                    sql = string.Format("select o.OutNum,ProducesM,StorageName,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,GoodsDate,PnoName,ZdName,QrName from Goods g,OutNumber o,OutGoodsInfo og, Storage s where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and s.StorageNum=g.StorageNum  and  GoodsDate = '{0}'", date);
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["OutNum"].ToString());
                    item.SubItems.Add(dr["ProducesM"].ToString());
                    item.SubItems.Add(dr["StorageName"].ToString());
                    item.SubItems.Add(dr["GoodsName"].ToString());
                    item.SubItems.Add(dr["GoodsPrice"].ToString());
                    item.SubItems.Add(dr["GoodsNum"].ToString());
                    item.SubItems.Add(dr["GoodsJLDw"].ToString());
                    item.SubItems.Add(dr["GoodsBZQ"].ToString());
                    item.SubItems.Add(dr["GoodsDate"].ToString());
                    item.SubItems.Add(dr["PnoName"].ToString());
                    item.SubItems.Add(dr["ZdName"].ToString());
                    item.SubItems.Add(dr["QrName"].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因是：" + ex.Message.ToString());
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.Text == "全部选项")
            {
                this.txtName.Enabled = false;
                this.txtName.Clear();
            }
            else
            {
                this.txtName.Enabled = true;
            }
        }

        private void OutboundForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
