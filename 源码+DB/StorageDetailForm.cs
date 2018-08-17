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
    public partial class StorageDetailForm : Form
    {
        public StorageDetailForm()
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

        private void StorageDetailForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
           
            //将数据绑定到下拉列表框中
            string sql = string.Format("select distinct State from InNumber");
            SqlDataReader dr = DBHelper.GetDataReader(sql);
            this.cbType.Items.Add("全部");
            while (dr.Read())
            {
                this.cbType.Items.Add(dr["State"].ToString());
            }
            dr.Close();
            this.cbType.SelectedIndex = 0;

            //入库详单的listview不可见
            this.lvDetailInfo.Visible = false;

            //调用方法
            loadInfo();
            this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
        }

        //将数据绑定到listview控件中
        private void loadInfo()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sql1 = "select i.ID,i.InNum,ZdName,ZdTime,QrName,RkTime,sum(ig.GoodsNum) InGoodsNum ,sum(g.GoodsNum*g.GoodsPrice) totalPrice,State  from InNumber i,InGoodsInfo ig,Goods g where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum  group by i.InNum ,i.ID,ZdName,ZdTime,QrName,RkTime,State";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ListViewItem item = new ListViewItem(rd["ID"].ToString());
                    item.SubItems.Add(rd["InNum"].ToString());
                    item.SubItems.Add(rd["ZdName"].ToString());
                    item.SubItems.Add(rd["ZdTime"].ToString());
                    item.SubItems.Add(rd["QrName"].ToString());
                    item.SubItems.Add(rd["RkTime"].ToString());
                    item.SubItems.Add(rd["InGoodsNum"].ToString());
                    item.SubItems.Add(rd["totalPrice"].ToString());
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

        //重置按钮的单击事件，清空相应的数据
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cbType.SelectedIndex =0;
            this.lvCamtasiaInfo.Items.Clear();
            loadInfo();
            this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
            this.dtpOverTime.Text = DateTime.Now.ToString();
            this.lvDetailInfo.Visible = false;    //入库详单的listview不可见
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
                    sql = string.Format("select i.ID,i.InNum,ZdName,ZdTime,QrName,RkTime,sum(ig.GoodsNum) InGoodsNum,sum(ig.GoodsNum*g.GoodsPrice) totalPrice,State  from InNumber i,InGoodsInfo ig,Goods g where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum  group by i.InNum ,i.ID,ZdName,ZdTime,QrName,RkTime,State  ");
                }
                else
                {
                    sql = string.Format("select i.ID,i.InNum,ZdName,ZdTime,QrName,RkTime,sum(ig.GoodsNum) InGoodsNum ,sum(ig.GoodsNum*g.GoodsPrice) totalPrice,State  from InNumber i,InGoodsInfo ig,Goods g where g.GoodsID=ig.GoodsID and ig.InNum=i.InNum and State='{0}' and ZdTime between '{1}' and '{2}' group by i.InNum ,i.ID,ZdName,ZdTime,QrName,RkTime,State", state, startTime, overTime);
                }
                SqlDataReader dr = DBHelper.GetDataReader(sql);
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["InNum"].ToString());
                    item.SubItems.Add(dr["ZdName"].ToString());
                    item.SubItems.Add(dr["ZdTime"].ToString());
                    item.SubItems.Add(dr["QrName"].ToString());
                    item.SubItems.Add(dr["RkTime"].ToString());
                    item.SubItems.Add(dr["InGoodsNum"].ToString());
                    item.SubItems.Add(dr["totalPrice"].ToString());
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

        private void lvCamtasiaInfo_Click(object sender, EventArgs e)
        {
            //for (int j = 0; j < 9; j++)
            //{
            //    if (lvCamtasiaInfo.SelectedItems[0].SubItems[8].Text == "已入库")
            //    {
            //        //lvCamtasiaInfo.SelectedItems[0].SubItems[j].BackColor = Color.DodgerBlue;
            //        lvCamtasiaInfo.SelectedItems[0].SubItems[j].ForeColor = Color.DodgerBlue;
            //    }
            //}

            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string state = this.lvCamtasiaInfo.SelectedItems[0].SubItems[8].Text.ToString();
                string innum = this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text.ToString();
                this.lvDetailInfo.Visible = true;
                this.lvDetailInfo.Items.Clear();
                if (state == "已入库")
                {
                    this.lvDetailInfo.Columns[10].Width = 0;
                }
                if (state == "待入库")
                {
                    this.lvDetailInfo.Columns[10].Width = 100;
                }
                sql = string.Format("select ig.InNum,InfoID,ProducesM,GoodsName,GoodsPrice,ig.GoodsNum,GoodsPrice*ig.GoodsNum totalMoney,GoodsJLDw,GoodsBZQ,PnoName,GoodsDate from InGoodsInfo ig ,Goods g ,InNumber i where  g.GoodsID=ig.GoodsID and i.InNum=ig.InNum and   State='{0}' and ig.InNum='{1}'  group by ig.InNum,InfoID,ProducesM,GoodsName,GoodsPrice,ig.GoodsNum,GoodsJLDw,GoodsBZQ,PnoName,GoodsDate", state, innum);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["InfoID"].ToString());
                    item.SubItems.Add(dr["ProducesM"].ToString());
                    item.SubItems.Add(dr["GoodsName"].ToString());
                    item.SubItems.Add(dr["GoodsPrice"].ToString());
                    item.SubItems.Add(dr["GoodsNum"].ToString());
                    item.SubItems.Add(dr["totalMoney"].ToString());
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
            if (state == "待入库")
            {
                DialogResult dr = MessageBox.Show("确定此项操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        //入库单号
                        string qrname = UserName;
                        string innum = this.lvCamtasiaInfo.SelectedItems[0].SubItems[1].Text.ToString();
                        SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                        conn.Open();
                        string sql = string.Format("update InNumber set QrName='{0}',RkTime='{1}',State='已入库'  where InNum='{2}'", qrname, DateTime.Now.ToString(), innum);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int num = cmd.ExecuteNonQuery();
                        if (num > 0)
                        {
                            //添加到出库详单
                            string cknum = "CK" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            //获取制单人（是入库单里的确认人）qrname
                            //获取数量
                            string state1 = "待出库";
                            string sql2 = string.Format("insert into OutNumber(OutNum,ZdName,ZdTime,State) values ('{0}','{1}','{2}','{3}')", cknum, qrname, DateTime.Now.ToString(), state1);
                            SqlCommand cm2 = new SqlCommand(sql2, conn);
                            int nu2 = cm2.ExecuteNonQuery();
                            if (nu2 > 0)
                            {
                                //先查询出每个的商品编号，商品数量
                                string sql3 = string.Format("select GoodsID,GoodsNum from InGoodsInfo ig,InNumber im where ig.InNum=im.InNum and im.inNum='{0}'", innum);
                                DataTable dt = DBHelper.GetDataTable(sql3);
                                int Row = dt.Rows.Count;
                                int[] goodsId = new int[Row];
                                int[] goodsNum = new int[Row];

                                for (int i = 0; i < Row; i++)
                                {
                                    goodsId[i] = int.Parse(dt.Rows[i]["GoodsID"].ToString());
                                    goodsNum[i] = int.Parse(dt.Rows[i]["GoodsNum"].ToString());
                                }
                                for (int i = 0; i < Row; i++)
                                {
                                    //出库单号
                                    string sql4 = string.Format("insert into OutGoodsInfo(OutNum,GoodsID,GoodsNum) values('{0}','{1}','{2}')", cknum, goodsId[i], goodsNum[i]);
                                    SqlCommand cmd3 = new SqlCommand(sql4, conn);
                                    int ck = cmd3.ExecuteNonQuery();
                                    this.lvDetailInfo.Items.Clear();
                                }
                            }
                        }
                        MessageBox.Show("入库成功！");
                        conn.Close();
                        this.lvCamtasiaInfo.Items.Clear();
                        loadInfo();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误原因是：" + ex.Message.ToString());
                    }
                }
            }
        }

        private void lvCamtasiaInfo_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
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
                    if (lvCamtasiaInfo.Items[i].SubItems[8].Text == "待入库")
                    {
                        lvCamtasiaInfo.Items[i].SubItems[j].BackColor = Color.DarkGray;
                        lvCamtasiaInfo.Items[i].SubItems[j].ForeColor = Color.Red;
                    }
                }
            }
        }

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
                        string sql = string.Format("delete from InGoodsInfo where InfoID ='{0}'", unm);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int num = cmd.ExecuteNonQuery();
                        if (num > 0)
                        {
                            MessageBox.Show("删除成功！");
                        }
                        conn.Close();
                        this.lvCamtasiaInfo.Items.Clear();
                        loadInfo();
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

