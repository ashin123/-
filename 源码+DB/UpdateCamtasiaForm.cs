using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamtasiaStudio;
using System.Data.SqlClient;
namespace CamtasiaStudio
{
    public partial class UpdateCamtasiaForm : Form
    {
        public UpdateCamtasiaForm()
        {
            InitializeComponent();
        }
        public static string txtno;
        public static string txtname;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateCamtasiaForm_Load(object sender, EventArgs e)
        {
            //加载事件
            this.txtNO.Text = txtno;
            this.txtName.Text = txtname;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //确定按钮
            string txtno = this.txtNO.Text;
            string txtname = this.txtName.Text;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlstr = string.Format("update Storage set StorageName='{0}' where  StorageNum={1}", txtname, txtno);
                //MessageBox.Show("" + sqlstr);
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("修改数据成功!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改数据失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改仓库信息出现异常！\n原因：" + ex.Message);
            }
        }
    }
}
