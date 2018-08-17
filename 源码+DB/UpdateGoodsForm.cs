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
    public partial class UpdateGoodsForm : Form
    {
        public UpdateGoodsForm()
        {
            InitializeComponent();
        }

        //商品编号
        public static string goodsId;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //根据商品编号绑定对应的数据信息
        private void UpdateGoodsForm_Load(object sender, EventArgs e)
        {
            try
            {
                string sqlStr = string.Format("select ProducesM,GoodsName,GoodsPrice,GoodsJLDw,PnoName,GoodsDate,GoodsBZQ from Goods where GoodsID='{0}'", goodsId);
                DataTable dt = DBHelper.GetDataTable(sqlStr);
                //填充数据
                this.txtNum.Text = dt.Rows[0]["ProducesM"].ToString();
                this.txtName.Text = dt.Rows[0]["GoodsName"].ToString();
                this.txtPrice.Text = dt.Rows[0]["GoodsPrice"].ToString();
                this.txtUnit.Text = dt.Rows[0]["GoodsJLDw"].ToString();
                this.txtAddress.Text = dt.Rows[0]["PnoName"].ToString();
                this.dtpDateTime.Text = dt.Rows[0]["GoodsDate"].ToString();
                this.txtTime.Text = dt.Rows[0]["GoodsBZQ"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }

        //根据商品编号提交向数据库修改数据（商品条码不允许修改）
        private void btnAccept_Click(object sender, EventArgs e)
        {
            string time = dtpDateTime.Value.ToString("yyyy-MM-dd");
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlStr = string.Format("update  Goods set GoodsName='{0}',GoodsPrice='{1}',GoodsJLDw='{2}',PnoName='{3}',GoodsDate='{4}',GoodsBZQ='{5}' where GoodsID='{6}'", txtName.Text, txtPrice.Text, txtUnit.Text, txtAddress.Text, time, txtTime.Text, goodsId);
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                int Row = cmd.ExecuteNonQuery();
                conn.Close();
                if (Row > 0)
                {
                    MessageBox.Show("修改成功！");
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("修改失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提示：" + ex.Message.ToString());
            }
        }
    }
}
