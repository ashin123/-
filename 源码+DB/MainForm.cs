using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamtasiaStudio
{
    public partial class MainForm : Form
    {
        public MainForm()
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

        //仓库管理窗体
        private void panel1_Click(object sender, EventArgs e)
        {
            CamtasiaInfoForm cf = new CamtasiaInfoForm();
            CamtasiaInfoForm.UserName = UserName;
            CamtasiaInfoForm.UserPower = UserPower;
            cf.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CamtasiaInfoForm cf = new CamtasiaInfoForm();
            CamtasiaInfoForm.UserName = UserName;
            CamtasiaInfoForm.UserPower = UserPower;
            cf.Show();
        }

        //员工管理窗体
        private void panel2_Click(object sender, EventArgs e)
        {
            EmployeeForm em = new EmployeeForm();
            EmployeeForm.UserName = UserName;
            EmployeeForm.UserPower = UserPower;
            em.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            EmployeeForm em = new EmployeeForm();
            EmployeeForm.UserName = UserName;
            EmployeeForm.UserPower = UserPower;
            em.Show();
        }

        //商品管理窗体
        private void panel3_Click(object sender, EventArgs e)
        {
            GoodsInfoForm gf = new GoodsInfoForm();
            GoodsInfoForm.UserName = UserName;
            GoodsInfoForm.UserPower = UserPower;
            gf.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            GoodsInfoForm gf = new GoodsInfoForm();
            GoodsInfoForm.UserName = UserName;
            GoodsInfoForm.UserPower = UserPower;
            gf.Show();
        }

        //入库查询窗体
        private void panel4_Click(object sender, EventArgs e)
        {
            StorageForm sf = new StorageForm();
            StorageForm.UserName = UserName;
            StorageForm.UserPower = UserPower;
            sf.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            StorageForm sf = new StorageForm();
            StorageForm.UserName = UserName;
            StorageForm.UserPower = UserPower;
            sf.Show();
        }

        //出库查询窗体
        private void panel5_Click(object sender, EventArgs e)
        {
            OutboundForm of = new OutboundForm();
            OutboundForm.UserName = UserName;
            OutboundForm.UserPower = UserPower;
            of.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OutboundForm of = new OutboundForm();
            OutboundForm.UserName = UserName;
            OutboundForm.UserPower = UserPower;
            of.Show();
        }

        //入库管理窗体
        private void panel6_Click(object sender, EventArgs e)
        {
            StorageDetailForm odf = new StorageDetailForm();
            StorageDetailForm.UserName = UserName;
            StorageDetailForm.UserPower = UserPower;
            odf.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            StorageDetailForm odf = new StorageDetailForm();
            StorageDetailForm.UserName = UserName;
            StorageDetailForm.UserPower = UserPower;
            odf.Show();
        }

        //出库管理窗体
        private void panel7_Click(object sender, EventArgs e)
        {
            OutboundDetailForm odf = new OutboundDetailForm();
            OutboundDetailForm.UserName = UserName;
            OutboundDetailForm.UserPower = UserPower;
            odf.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OutboundDetailForm odf = new OutboundDetailForm();
            OutboundDetailForm.UserName = UserName;
            OutboundDetailForm.UserPower = UserPower;
            odf.Show();
        }

        //库存查询窗体
        private void panel8_Click(object sender, EventArgs e)
        {
            InventoryInfoForm iif = new InventoryInfoForm();
            InventoryInfoForm.UserName = UserName;
            InventoryInfoForm.UserPower = UserPower;
            iif.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            InventoryInfoForm iif = new InventoryInfoForm();
            InventoryInfoForm.UserName = UserName;
            InventoryInfoForm.UserPower = UserPower;
            iif.Show();
        }

        //数据报表窗体
        private void panel9_Click(object sender, EventArgs e)
        {
            ReportformsInfoForm rif = new ReportformsInfoForm();
            ReportformsInfoForm.UserName = UserName;
            ReportformsInfoForm.UserPower = UserPower;
            rif.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ReportformsInfoForm rif = new ReportformsInfoForm();
            ReportformsInfoForm.UserName = UserName;
            ReportformsInfoForm.UserPower = UserPower;
            rif.Show();
        }

        //统计信息窗体
        private void panel10_Click(object sender, EventArgs e)
        {
            ReportformsViewForm rvf = new ReportformsViewForm();
            ReportformsViewForm.UserName = UserName;
            ReportformsViewForm.UserPower = UserPower;
            rvf.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            ReportformsViewForm rvf = new ReportformsViewForm();
            ReportformsViewForm.UserName = UserName;
            ReportformsViewForm.UserPower = UserPower;
            rvf.Show();
        }

        //主窗体加载事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //判断权限为“普通用户”时禁用以下项
            if (UserPower == "普通用户")
            {
                this.panel6.Enabled = false;
                this.panel7.Enabled = false;
                this.panel8.Enabled = false;
                this.panel9.Enabled = false;
                this.panel10.Enabled = false;
            }
            //显示当前的系统时间  
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();

        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            new Thread(new ThreadStart(delegate
            {
                Application.Run(new LoginForm());
            })).Start(); 
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
