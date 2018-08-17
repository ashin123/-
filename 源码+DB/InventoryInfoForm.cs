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
    public partial class InventoryInfoForm : Form
    {
        public InventoryInfoForm()
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

        private void InventoryInfoForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
            
            //cbType下拉框默认为“--请选择--”
            this.cbType.SelectedIndex = 0;
            //加载所有数据
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                //已入库 马上变为待出库
                string sqlStr = "select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,(g.GoodsNum-ig.GoodsNum) 待入库,(g.GoodsNum-og.GoodsNum) 出库数量,og.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from Goods g,InGoodsInfo ig,InNumber i,OutGoodsInfo og,OutNumber o where ig.InNum=i.InNum and ig.GoodsID=g.GoodsID and og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and i.State='已入库'";
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
                    item.SubItems.Add(rd[8].ToString());
                    item.SubItems.Add(rd[9].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                rd.Close();
                //待出库操作后变为 待入库数据
                string sqlStr2 = "select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,ig.GoodsNum 待入库,g.GoodsNum-ig.GoodsNum 出库数量,g.GoodsNum-ig.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from  Goods g,InGoodsInfo ig,InNumber i where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum and i.State='待入库'";
                SqlCommand cmd2 = new SqlCommand(sqlStr2, conn);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                while (rd2.Read())
                {
                    ListViewItem item = new ListViewItem(rd2[0].ToString());
                    item.SubItems.Add(rd2[1].ToString());
                    item.SubItems.Add(rd2[2].ToString());
                    item.SubItems.Add(rd2[3].ToString());
                    item.SubItems.Add(rd2[4].ToString());
                    item.SubItems.Add(rd2[5].ToString());
                    item.SubItems.Add(rd2[6].ToString());
                    item.SubItems.Add(rd2[7].ToString());
                    item.SubItems.Add(rd2[8].ToString());
                    item.SubItems.Add(rd2[9].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                rd2.Close();
                ////已出库
                string sqlStr3 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsNum-og.GoodsNum,og.GoodsNum,g.GoodsNum-og.GoodsNum,g.GoodsNum,g.PnoName from Goods g,OutGoodsInfo og,OutNumber o where og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and o.State='已出库'");
                SqlCommand cmd3 = new SqlCommand(sqlStr3, conn);
                SqlDataReader rd3 = cmd3.ExecuteReader();
                while (rd3.Read())
                {
                    ListViewItem item = new ListViewItem(rd3[0].ToString());
                    item.SubItems.Add(rd3[1].ToString());
                    item.SubItems.Add(rd3[2].ToString());
                    item.SubItems.Add(rd3[3].ToString());
                    item.SubItems.Add(rd3[4].ToString());
                    item.SubItems.Add(rd3[5].ToString());
                    item.SubItems.Add(rd3[6].ToString());
                    item.SubItems.Add(rd3[7].ToString());
                    item.SubItems.Add(rd3[8].ToString());
                    item.SubItems.Add(rd3[9].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                rd2.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因：" + ex.Message.ToString(),"提示");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

     
        //查询
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //获取查询信息
            string type = this.cbType.Text;
            string name = this.txtName.Text.Trim();
            //非空验证
            if (type == "--请选择查询条件--")
            {
                MessageBox.Show("您还未选择查询条件！");
                return;
            }
            if (name == "")
            {
                MessageBox.Show("请输入查询的详细条件！");
                this.txtName.Focus();
                return;
            }
            #region 按分类进行查询
            try
            {
                //根据商品条码查询
                if (type == "商品条码")
                {
                    Int64 id;
                    if (!Int64.TryParse(name, out id))
                    {
                        MessageBox.Show("商品条形码必须为数字\n应输入数字进行查询！","提示");
                        this.txtName.Clear();
                        this.txtName.Focus();
                        return;
                    }
                    this.lvCamtasiaInfo.Items.Clear();//清空数据
                    //模糊查询
                    SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                    conn.Open();
                    //已入库 马上变为待出库
                    string sqlStr1 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,(g.GoodsNum-ig.GoodsNum) 待入库,(g.GoodsNum-og.GoodsNum) 出库数量,og.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from Goods g,InGoodsInfo ig,InNumber i,OutGoodsInfo og,OutNumber o where ig.InNum=i.InNum and ig.GoodsID=g.GoodsID and og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and i.State='已入库' and g.ProducesM like '{0}%'", name);
                    SqlCommand cmd = new SqlCommand(sqlStr1, conn);
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
                        item.SubItems.Add(rd[8].ToString());
                        item.SubItems.Add(rd[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd.Close();
                    //待出库操作后变为 待入库数据
                    string sqlStr2 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,ig.GoodsNum 待入库,g.GoodsNum-ig.GoodsNum 出库数量,g.GoodsNum-ig.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from  Goods g,InGoodsInfo ig,InNumber i where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum and i.State='待入库'  and g.ProducesM like '{0}%'", name);
                    SqlCommand cmd2 = new SqlCommand(sqlStr2, conn);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    while (rd2.Read())
                    {
                        ListViewItem item = new ListViewItem(rd2[0].ToString());
                        item.SubItems.Add(rd2[1].ToString());
                        item.SubItems.Add(rd2[2].ToString());
                        item.SubItems.Add(rd2[3].ToString());
                        item.SubItems.Add(rd2[4].ToString());
                        item.SubItems.Add(rd2[5].ToString());
                        item.SubItems.Add(rd2[6].ToString());
                        item.SubItems.Add(rd2[7].ToString());
                        item.SubItems.Add(rd2[8].ToString());
                        item.SubItems.Add(rd2[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd2.Close();
                    ////已出库
                    string sqlStr3 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsNum-og.GoodsNum,og.GoodsNum,g.GoodsNum-og.GoodsNum,g.GoodsNum,g.PnoName from Goods g,OutGoodsInfo og,OutNumber o where og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and o.State='已出库' and g.ProducesM like '{0}%'", name);
                    SqlCommand cmd3 = new SqlCommand(sqlStr3, conn);
                    SqlDataReader rd3 = cmd3.ExecuteReader();
                    while (rd3.Read())
                    {
                        ListViewItem item = new ListViewItem(rd3[0].ToString());
                        item.SubItems.Add(rd3[1].ToString());
                        item.SubItems.Add(rd3[2].ToString());
                        item.SubItems.Add(rd3[3].ToString());
                        item.SubItems.Add(rd3[4].ToString());
                        item.SubItems.Add(rd3[5].ToString());
                        item.SubItems.Add(rd3[6].ToString());
                        item.SubItems.Add(rd3[7].ToString());
                        item.SubItems.Add(rd3[8].ToString());
                        item.SubItems.Add(rd3[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd2.Close();
                    conn.Close();
                    //没有对应的数据时，提示没要您要查询的信息
                    if (lvCamtasiaInfo.Items.Count == 0)
                    {
                        MessageBox.Show("没要您要查询的信息！", "提示");
                        return;
                    }
                }
                //根据商品名称查询
                if (type == "商品名称")
                {
                    this.lvCamtasiaInfo.Items.Clear();//清空数据
                    //模糊查询
                    SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                    conn.Open();
                    //已入库 马上变为待出库
                    string sqlStr1 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,(g.GoodsNum-ig.GoodsNum) 待入库,(g.GoodsNum-og.GoodsNum) 出库数量,og.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from Goods g,InGoodsInfo ig,InNumber i,OutGoodsInfo og,OutNumber o where ig.InNum=i.InNum and ig.GoodsID=g.GoodsID and og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and i.State='已入库' and GoodsName like '%{0}%'", name);
                    SqlCommand cmd = new SqlCommand(sqlStr1, conn);
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
                        item.SubItems.Add(rd[8].ToString());
                        item.SubItems.Add(rd[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd.Close();
                    //待出库操作后变为 待入库数据
                    string sqlStr2 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum 入库数量,ig.GoodsNum 待入库,g.GoodsNum-ig.GoodsNum 出库数量,g.GoodsNum-ig.GoodsNum 待出库,g.GoodsNum 总数量,g.PnoName from  Goods g,InGoodsInfo ig,InNumber i where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum and i.State='待入库' and GoodsName like '%{0}%'", name);
                    SqlCommand cmd2 = new SqlCommand(sqlStr2, conn);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    while (rd2.Read())
                    {
                        ListViewItem item = new ListViewItem(rd2[0].ToString());
                        item.SubItems.Add(rd2[1].ToString());
                        item.SubItems.Add(rd2[2].ToString());
                        item.SubItems.Add(rd2[3].ToString());
                        item.SubItems.Add(rd2[4].ToString());
                        item.SubItems.Add(rd2[5].ToString());
                        item.SubItems.Add(rd2[6].ToString());
                        item.SubItems.Add(rd2[7].ToString());
                        item.SubItems.Add(rd2[8].ToString());
                        item.SubItems.Add(rd2[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd2.Close();
                    ////已出库
                    string sqlStr3 = string.Format("select g.GoodsID,g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsNum-og.GoodsNum,og.GoodsNum,g.GoodsNum-og.GoodsNum,g.GoodsNum,g.PnoName from Goods g,OutGoodsInfo og,OutNumber o where og.OutNum=o.OutNum and og.GoodsID=g.GoodsID and o.State='已出库' and GoodsName like '%{0}%'", name);
                    SqlCommand cmd3 = new SqlCommand(sqlStr3, conn);
                    SqlDataReader rd3 = cmd3.ExecuteReader();
                    while (rd3.Read())
                    {
                        ListViewItem item = new ListViewItem(rd3[0].ToString());
                        item.SubItems.Add(rd3[1].ToString());
                        item.SubItems.Add(rd3[2].ToString());
                        item.SubItems.Add(rd3[3].ToString());
                        item.SubItems.Add(rd3[4].ToString());
                        item.SubItems.Add(rd3[5].ToString());
                        item.SubItems.Add(rd3[6].ToString());
                        item.SubItems.Add(rd3[7].ToString());
                        item.SubItems.Add(rd3[8].ToString());
                        item.SubItems.Add(rd3[9].ToString());
                        this.lvCamtasiaInfo.Items.Add(item);
                    }
                    rd2.Close();
                    conn.Close();
                    //没有对应的数据时，提示没要您要查询的信息
                    if (lvCamtasiaInfo.Items.Count == 0)
                    {
                        MessageBox.Show("没要您要查询的信息！","提示");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因：" + ex.Message.ToString(),"提示");
            }
            #endregion
        }

        //重置
        private void btnReset_Click(object sender, EventArgs e)
        {
            //恢复下拉列表默认项
            this.cbType.SelectedIndex = 0;
            //清空文本框中信息
            this.txtName.Clear();
            this.lvCamtasiaInfo.Items.Clear();//清空数据
            LoadData();//重加载数据
        }

        private void InventoryInfoForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
