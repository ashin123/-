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
using CamtasiaStudio;
namespace CamtasiaStudio
{
    public partial class AddCamtasiaForm : Form
    {
        public AddCamtasiaForm()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //获取界面上输入
            string txtNO = this.txtNO.Text;//此处为系统自动赋值，无须用户输入
            string txtName = this.txtName.Text;
               if (txtName=="")
            {
                MessageBox.Show("仓库名称不能为空！");
                this.txtName.Focus();
                return;
            }
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(DBHelper.ConnString);
                conn.Open();
                string sqlstr = string.Format("insert into Storage(StorageName) values ('{0}')", txtName);
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("添加成功");
                    this.Close();
                    //刷新
                   CamtasiaInfoForm cg = new CamtasiaInfoForm();
                     //cg.lvCamtasiaInfoLoad();
                    //CamtasiaInfoForm.ActiveForm
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加仓库出现异常！\n原因："+ex.Message);
            }
            conn.Close();
          //  this.Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //加载仓库编号
        private void AddCamtasiaForm_Load(object sender, EventArgs e)
        {
            //查询出最大的编号再+1
            SqlConnection conn = null;
            try
            {
           conn = new SqlConnection(DBHelper.ConnString);
            conn.Open();
            string sqlstr = string.Format("select MAX(StorageNum)+1 from Storage");
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            string num =cmd.ExecuteScalar().ToString();
            this.txtNO.Text = num;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载仓库编号出现异常！\n原因："+ex.Message);
            }
            conn.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
