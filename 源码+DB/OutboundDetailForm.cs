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
    public partial class OutboundDetailForm : Form
    {
        public OutboundDetailForm()
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

        private void OutboundDetailForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
          
            //将数据绑定到下拉列表框中
            string sql = string.Format("select distinct State from OutNumber");
            SqlDataReader dr = DBHelper.GetDataReader(sql);
            this.cbType.Items.Add("全部");
            while (dr.Read())
            {
                this.cbType.Items.Add(dr["State"].ToString());
            }
            dr.Close();
            this.cbType.SelectedIndex = 0;

            //出库详单的listview不可见
            this.lvDetailInfo.Visible = false;

            //将数据绑定到listview控件中
            load();
            this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
        }

        private void load()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sql1 = "select o.ID,o.OutNum,ZdName,ZdTime,QrName,CkTime,sum(g.GoodsNum) OutGoodsNum ,sum(g.GoodsNum*g.GoodsPrice) totalMoney,State  from OutNumber o,OutGoodsInfo og,Goods g where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum  group by o.OutNum ,o.ID,ZdName,ZdTime,QrName,CkTime,State";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ListViewItem item = new ListViewItem(rd["ID"].ToString());
                    item.SubItems.Add(rd["OutNum"].ToString());
                    item.SubItems.Add(rd["ZdName"].ToString());
                    item.SubItems.Add(rd["ZdTime"].ToString());
                    item.SubItems.Add(rd["QrName"].ToString());
                    item.SubItems.Add(rd["CkTime"].ToString());
                    item.SubItems.Add(rd["OutGoodsNum"].ToString());
                    item.SubItems.Add(rd["totalMoney"].ToString());
                    item.SubItems.Add(rd["State"].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                rd.Close();
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
                string state = this.cbType.Text;
                DateTime startTime = this.dtpStartTime.Value;
                DateTime overTime = this.dtpOverTime.Value;
                if (cbType.Text == "全部")
                {
                    sql = string.Format("select o.ID,o.OutNum,ZdName,ZdTime,QrName,CkTime,sum(g.GoodsNum) outGoodsNum,sum(g.GoodsNum*g.GoodsPrice) totalMoney,State  from OutNumber o,OutGoodsInfo og,Goods g where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum  group by o.OutNum ,o.ID,ZdName,ZdTime,QrName,CkTime,State  ");
                }
                else
                {
                    sql = string.Format("select o.ID,o.OutNum,ZdName,ZdTime,QrName,CkTime,sum(g.GoodsNum) outGoodsNum ,sum(g.GoodsNum*g.GoodsPrice) totalMoney,State  from OutNumber o,OutGoodsInfo og,Goods g where g.GoodsID=og.GoodsID and og.OutNum=o.OutNum and State='{0}' and ZdTime between '{1}' and '{2}' group by o.OutNum ,o.ID,ZdName,ZdTime,QrName,CkTime,State", state, startTime, overTime);
                }
                SqlDataReader dr = DBHelper.GetDataReader(sql);
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["OutNum"].ToString());
                    item.SubItems.Add(dr["ZdName"].ToString());
                    item.SubItems.Add(dr["ZdTime"].ToString());
                    item.SubItems.Add(dr["QrName"].ToString());
                    item.SubItems.Add(dr["CkTime"].ToString());
                    item.SubItems.Add(dr["OutGoodsNum"].ToString());
                    item.SubItems.Add(dr["totalMoney"].ToString());
                    item.SubItems.Add(dr["State"].ToString());
                    this.lvCamtasiaInfo.Items.Add(item);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因是：" + ex.Message.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cbType.SelectedIndex =0;
            this.lvCamtasiaInfo.Items.Clear();
            load();
            this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
            this.dtpOverTime.Text = DateTime.Now.ToString();
            this.lvDetailInfo.Visible = false;
        }

        private void lvCamtasiaInfo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string state = this.lvCamtasiaInfo.SelectedItems[0].SubItems[8].Text.ToString();
                string outnum = this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text.ToString();
                this.lvDetailInfo.Visible = true;
                this.lvDetailInfo.Items.Clear();
                if (state == "已出库")
                {
                    this.lvDetailInfo.Columns[10].Width = 0;
                }
                if (state == "待出库")
                {
                    this.lvDetailInfo.Columns[10].Width = 100;
                }
                sql = string.Format("select og.OutNum,outID,ProducesM,GoodsName,GoodsPrice,g.GoodsNum,GoodsPrice*g.GoodsNum totalPrice,GoodsJLDw,GoodsBZQ,PnoName,GoodsDate from OutNumber o,OutGoodsInfo og,Goods g where  g.GoodsID=og.GoodsID and o.OutNum=og.OutNum and   State='{0}' and og.OutNum='{1}'  group by og.OutNum,outID,ProducesM,GoodsName,GoodsPrice,g.GoodsNum,GoodsJLDw,GoodsBZQ,PnoName,GoodsDate", state, outnum);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["outID"].ToString());
                    item.SubItems.Add(dr["ProducesM"].ToString());
                    item.SubItems.Add(dr["GoodsName"].ToString());
                    item.SubItems.Add(dr["GoodsPrice"].ToString());
                    item.SubItems.Add(dr["GoodsNum"].ToString());
                    item.SubItems.Add(dr["totalPrice"].ToString());
                    item.SubItems.Add(dr["GoodsJLDw"].ToString());
                    item.SubItems.Add(dr["GoodsBZQ"].ToString());
                    item.SubItems.Add(dr["PnoName"].ToString());
                    item.SubItems.Add(dr["GoodsDate"].ToString());
                    item.SubItems.Add("删除");
                    this.lvDetailInfo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误原因是：" + ex.Message.ToString());
            }
        }

        private void lvCamtasiaInfo_DoubleClick(object sender, EventArgs e)
        {
            string state = this.lvCamtasiaInfo.SelectedItems[0].SubItems[8].Text.ToString();
            if (state == "待出库")
            {
                DialogResult dr = MessageBox.Show("确定此项操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        string qrname = UserName;
                        string outnum = this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text.ToString();
                        SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                        conn.Open();
                        string sql = string.Format("update OutNumber set State='已出库' ,  QrName='{0}',CkTime='{1}'  where OutNum='{2}'", qrname, DateTime.Now.ToString(), outnum);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int num = cmd.ExecuteNonQuery();
                        if (num > 0)
                        {
                            MessageBox.Show("出库成功！");
                        }
                        conn.Close();
                        this.lvCamtasiaInfo.Items.Clear();
                        this.lvDetailInfo.Items.Clear();
                        load();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误原因是：" + ex.Message.ToString());
                    }
                }
            }
        }

        private void lvCamtasiaInfo_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
            e.DrawFocusRectangle(e.Bounds);
            for (int i = 0; i < lvCamtasiaInfo.Items.Count; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (lvCamtasiaInfo.Items[i].SubItems[8].Text == "待出库")
                    {
                        lvCamtasiaInfo.Items[i].SubItems[j].BackColor = Color.DarkGray;
                        lvCamtasiaInfo.Items[i].SubItems[j].ForeColor = Color.Red;
                    }
                }
            }
        }

        private void lvCamtasiaInfo_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        //详单的删除事件
        private void lvDetailInfo_Click(object sender, EventArgs e)
        {
            if (lvDetailInfo.Columns[10].Width != 0)
            {
                DialogResult dr = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    foreach (int i in lvDetailInfo.SelectedIndices)
                    {
                        int unm = (int.Parse)(this.lvDetailInfo.Items[i].SubItems[0].Text);
                        this.lvDetailInfo.SelectedItems[0].Remove();
                        SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                        conn.Open();
                        string sql = string.Format("delete from OutGoodsInfo where outID ='{0}'", unm);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int num = cmd.ExecuteNonQuery();
                        if (num > 0)
                        {
                            //此时要删除入库详单里的数据(InGoodsInfo)
                            //根据编号查询出商品ID(OutGoodsInfo)


                            MessageBox.Show("删除成功！");
                        }
                        conn.Close();
                        this.lvCamtasiaInfo.Items.Clear();
                        load();
                    }
                }
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = this.dtpStartTime.Value;
            DateTime overTime = this.dtpOverTime.Value;
            DateTime nowTime = DateTime.Now;
            if (startTime >= overTime)
            {
                MessageBox.Show("第一个时间不能大于或等于第二时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
                return;
            }
        }

        private void dtpOverTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = this.dtpStartTime.Value;
            DateTime overTime = this.dtpOverTime.Value;
            DateTime nowTime = DateTime.Now;
            //对当前时间进行判断
            if (overTime >= nowTime)
            {
                MessageBox.Show("第二个时间不能大于或等于当前时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dtpOverTime.Text = DateTime.Now.ToString();
                return;
            }
            //对第一个时间进行判断
            if (overTime <= startTime)
            {
                MessageBox.Show("第二时间不能小于或等于第一时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dtpOverTime.Text = DateTime.Now.ToString();
                return;
            }
        }
    }
}
