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
    public partial class AddGoodsForm : Form
    {
        public AddGoodsForm()
        {
            InitializeComponent();
        }

        //定义获取当前登录的用户的姓名  为后面插入数据到入库单号表：（InNumber）表 提供   制单人 姓名
        public static string UserName;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                //获取界面上输入
                string txtNum = this.txtNum.Text.Trim();
                string txtName = this.txtName.Text.Trim();
                string txtNumber = this.txtNumber.Text.Trim();
                string txtPrice = this.txtPrice.Text.Trim();
                string txtUnit = this.txtUnit.Text.Trim();
                string txtAddress = this.txtAddress.Text.Trim();
                DateTime time = this.dtpDateTime.Value;
                string txtBzq = this.txtTime.Text.Trim();
                //根据仓库名称查询出仓库编号
                string storagrId = string.Format("select StorageNum from Storage where StorageName='{0}'", cbStorage.Text);
                DataTable dt1 = DBHelper.GetDataTable(storagrId);
                int txtStorage = int.Parse(dt1.Rows[0]["StorageNum"].ToString());
                //非空验证
                if (txtNum.Length==0)
                {
                    MessageBox.Show("商品条形码不能为空！");
                    this.txtNum.Focus();
                    return;
                }
                if (txtName.Length == 0)
                {
                    MessageBox.Show("商品名称不能为空！");
                    this.txtName.Focus();
                    return;
                }
                if (txtNumber.Length == 0)
                {
                    MessageBox.Show("商品数量不能为空！");
                    this.txtNumber.Focus();
                    return;
                }
                if (txtPrice.Length == 0)
                {
                    MessageBox.Show("商品单价不能为空！");
                    this.txtPrice.Focus();
                    return;
                }
                if (txtUnit.Length == 0)
                {
                    MessageBox.Show("商品计量单位不能为空！");
                    this.txtUnit.Focus();
                    return;
                }
                if (txtAddress.Length == 0)
                {
                    MessageBox.Show("商品生产厂家不能为空！");
                    this.txtAddress.Focus();
                    return;
                }
                if (txtBzq.Length == 0)
                {
                    MessageBox.Show("商品保质期不能为空！");
                    this.txtTime.Focus();
                    return;
                }
                //对填写数字的进行验证
                int num; 
                Int64 num2;
                if (!Int64.TryParse(txtNum, out num2))
                {
                    MessageBox.Show("商品条形码必须为数字！", "提示");
                    this.txtNum.Clear();
                    this.txtNum.Focus();
                    return;
                }
                if (!int.TryParse(txtNumber, out num))
                {
                    MessageBox.Show("商品数量必须为数字！", "提示");
                    this.txtNumber.Clear();
                    this.txtNumber.Focus();
                    return;
                }
                if (!int.TryParse(txtBzq, out num))
                {
                    MessageBox.Show("商品保质期必须为数字！", "提示");
                    this.txtTime.Clear();
                    this.txtTime.Focus();
                    return;
                }
               
                if (!int.TryParse(txtPrice,out num))
                {
                    MessageBox.Show("商品价格必须为数字！", "提示");
                    this.txtPrice.Clear();
                    this.txtPrice.Focus();
                    return;
                }
                //添加商品信息
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlstr = string.Format("insert into Goods(ProducesM,GoodsName,GoodsNum,GoodsPrice,GoodsJLDw,PnoName,StorageNum,GoodsDate,GoodsBZQ) " +
                     " values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8})", txtNum, txtName, txtNumber, txtPrice, txtUnit, txtAddress, txtStorage, time, txtBzq);
                //MessageBox.Show(sqlstr);
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    //MessageBox.Show("添加商品表信息成功");
                    //根据条码查询出刚刚添加的商品编号
                    string sqlstr2 = string.Format("select GoodsID from Goods where ProducesM='{0}'", txtNum);
                    DataTable dt = DBHelper.GetDataTable(sqlstr2);
                    string goodsid = dt.Rows[0]["GoodsID"].ToString();
                    //MessageBox.Show("刚刚添加的商品编号是：" + goodsid);


                    //首先要判断入库单号在入库单号表里是否存在   InNum
                    string storage = this.lblRKNum.Text;
                    string sqlStr4 = string.Format("select * from InNumber where InNum='{0}'", storage);
                    DataTable dt2 = DBHelper.GetDataTable(sqlStr4);
                    //当不存在该入库编号信息时进行添加信息
                    if (dt2.Rows.Count == 0)
                    {
                        //并将该批次的数据加入到入库单号表：（InNumber）表    制单人为当前登录的用户
                        string state = "待入库";
                        //MessageBox.Show(UserName);
                        string sqlStr5 = string.Format("insert into InNumber(InNum,ZdName,ZdTime,State) values('{0}','{1}','{2}','{3}')", storage, UserName, DateTime.Now.ToString(), state);
                        //MessageBox.Show(sqlStr5);
                        SqlCommand cmd4 = new SqlCommand(sqlStr5, conn);
                        int ro = cmd4.ExecuteNonQuery();
                        //if (ro > 0)
                        //{
                        //    MessageBox.Show("添加入库单号表成功");
                        //}
                    }

                    //并将该批次的数据加入到入库详单管理InGoodsInfo表（入库单号  商品编号  商品数量）
                    string sqlstr3 = string.Format("insert into InGoodsInfo(InNum,GoodsID,GoodsNum) values('{0}','{1}','{2}')", storage, goodsid, txtNumber);
                    SqlCommand cmd3 = new SqlCommand(sqlstr3, conn);
                    int r = cmd3.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("添加商品成功!");
                        //清空信息，供再次添加
                        this.txtNum.Clear();
                        this.txtName.Clear();
                        this.txtNumber.Clear();
                        this.txtPrice.Clear();
                        this.txtUnit.Clear();
                        this.txtAddress.Clear();
                        this.txtTime.Clear();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }

        //加载入库单号    //并将该批次输入的数据加入到入库详单管理
        private void AddGoodsForm_Load(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.lblRKNum.Text = "RK" + time;
            //加载仓库列表

            string sqlStr = "select * from Storage";
            DataTable table = DBHelper.GetDataTable(sqlStr);
            this.cbStorage.DataSource = table;
            this.cbStorage.DisplayMember = "StorageName";
            this.cbStorage.ValueMember = "StorageNum";
        }
    }
}
