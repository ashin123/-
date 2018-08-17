using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Collections;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace CamtasiaStudio
{
    public partial class ReportformsInfoForm : Form
    {

        public ReportformsInfoForm()
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

        //导出
        private void btnExport_Click(object sender, EventArgs e)
        {
            //声明保存对话框
            SaveFileDialog sfd = new SaveFileDialog();
            //文件后缀列表   //只有1个过滤字符串索引
            sfd.Filter = "All files (*.*)|*.*|文本文档(*.txt) |*.txt|Excel 97-2003 工作薄|*.xls|Word 文档|*.doc";
            //默然路径是系统当前路径
          // dlg.InitialDirectory = Directory.GetCurrentDirectory();
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //定义表格内数据的行数和列数
            int rowscount = dgv.Rows.Count;
            int colscount = dgv.Columns.Count;
            //行数必须大于0
            if (rowscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数必须大于0
            if (colscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //行数不可以大于65536
            if (rowscount > 65536)
            {
                MessageBox.Show("数据记录数太多(最多不能超过65536条)，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数不可以大于255
            if (colscount > 255)
            {
                MessageBox.Show("数据记录行数太多，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //打开用户选定具有读西文权限的文件
                Stream myStream = sfd.OpenFile();
                //创建写入器，字符编码（解决导出数据的乱码问题）
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));
                string columnTitle = "";
                try
                {
                    //写入列标题
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;
                    }
                    //把表头写入Excel表中
                    sw.WriteLine(columnTitle);

                    //写入列内容
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string  columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                        }
                        //把数据内容写入Excel表中
                        sw.WriteLine(columnValue);

                    }
                    //关闭写入器
                    sw.Close();
                    //关闭文件流
                    myStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误提示："+ex.Message.ToString());
                }
                MessageBox.Show("导出完毕! ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //默认绑定下拉列表框的值
        private void LoadType()
        {
            cbType.Items.Clear();
           cbType.Items.Add("入库");
            cbType.Items.Add("出库");//0
             
            cbType.SelectedIndex = 0; //默认选中入库
        }
        //窗体加载事件
        private void ReportformsInfoForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_CENTER);//开始动画  目标窗口、持续时间、动画类型

            try
            {
                //显示当前登录的用户名、权限
                this.lblUser.Text = UserName + "[" + UserPower + "]";
                //显示当前的系统时间  
                this.timer1.Start();
               
                //窗体加载时，查询出入库的列表框里应该添加：
                LoadType();
                //对第一个时间控件自定义时间
                this.dtpStartTime.Text = DateTime.Now.ToString("2014年1月1日");
                //获取开始时间和结束时间
                DateTime startTime = this.dtpStartTime.Value;
                DateTime overTime = this.dtpOverTime.Value;
                //查询所有出库数据，填充DataGridView
                string sqlStr = string.Format(" select g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsPrice*g.GoodsNum Price,g.GoodsJLDw from Goods g,InGoodsInfo ig,InNumber im where ig.InNum=im.InNum and ig.GoodsID=g.GoodsID and [State]='已入库'  and RKTime>'{0}' and RKTime<'{1}'", startTime, overTime);
                System.Data.DataTable dt = DBHelper.GetDataTable(sqlStr);
                //绑定数据源
                this.dgv.AutoGenerateColumns = false;
                this.dgv.DataSource = dt;
                //清除默认选择项
                this.dgv.ClearSelection();
            }
            catch (Exception ex)
            {

                MessageBox.Show("错误原因：" + ex.Message.ToString());
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }
        //打印按钮
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                 if (InitializePrinting())
                            {
                                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();//打印对话框窗体实例化
                                printPreviewDialog.Document = printDocument1;//预览的文档
                                printPreviewDialog.ShowDialog();//将窗体显示为模式对话框
                            }
            }
            catch 
            {
                
                 MessageBox.Show("打印错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        GridPrinter gridPrinter;//打印类
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = gridPrinter.DrawDataGridView(e.Graphics);//绘制打印页
            if (more == true)
                e.HasMorePages = true;//打印附加页
        }
        //自定义函数
        private bool InitializePrinting()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowCurrentPage = true;//显示“当前页”选项按钮
            printDialog.AllowPrintToFile = true;//启用“打印到文件”复选框
            printDialog.AllowSelection = true;//启用“选择”选项按钮
            printDialog.AllowSomePages = true;//启用“页”选项按钮
            printDialog.PrintToFile = true;//是否选中“打印到文件”复选框
            printDialog.ShowHelp = true;//显示帮助按钮
            printDialog.ShowNetwork = true;//显示网络按钮
            //打印文件对话框
            if (printDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDocument1.DocumentName = "出、入库信息表";//打印时要显示的文档名
            printDocument1.PrinterSettings = printDialog.PrinterSettings;//对话框修改的打印机设置
            printDocument1.DefaultPageSettings = printDialog.PrinterSettings.DefaultPageSettings;//打印机的默认页设置
            printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);//打印页的边距分别是40

            gridPrinter = new GridPrinter(dgv, printDocument1, true, true, "出、入库信息表", new System.Drawing.Font("黑体", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            return true;
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime startTime = this.dtpStartTime.Value;
                DateTime overTime = this.dtpOverTime.Value;
                string sql = "";
                string type = cbType.SelectedItem.ToString();
                if (cbType.SelectedItem.ToString() == "入库")
                {
                    sql = string.Format("  select g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsPrice*g.GoodsNum Price,g.GoodsJLDw from Goods g,InGoodsInfo ig,InNumber im where ig.InNum=im.InNum and ig.GoodsID=g.GoodsID and [State]='已入库'  and RKTime>'{0}' and RKTime<'{1}'", startTime, overTime);
                }

                else
                {
                    
                    sql = string.Format("select g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsPrice*g.GoodsNum Price,g.GoodsJLDw from Goods g,OutGoodsInfo og,OutNumber om where og.OutNum=om.OutNum and og.GoodsID=g.GoodsID and State='已出库' and CkTime>'{0}' and CkTime<'{1}'", startTime, overTime);
                }
                //利用之前拼接好的SQL，进行查询，绑定
                System.Data.DataTable dt = DBHelper.GetDataTable(sql);
                dgv.DataSource = dt;
                dgv.AutoGenerateColumns = false;
                //清除默认选择项
                this.dgv.ClearSelection();
            }
            catch (Exception ex)
            {

                MessageBox.Show("错误原因：" + ex.Message.ToString());
            }
        }
        //定义绑定数据函数
        public void LoadDY()
        {
			try
			{
				DateTime startTime = this.dtpStartTime.Value;
				DateTime overTime = this.dtpOverTime.Value;
				string sql = "";
				string type = cbType.SelectedItem.ToString();
				if(cbType.SelectedItem.ToString() == "入库")
				{
					sql = string.Format("  select g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsPrice*g.GoodsNum Price,g.GoodsJLDw from Goods g,InGoodsInfo ig,InNumber im where ig.InNum=im.InNum and ig.GoodsID=g.GoodsID and [State]='已入库'  and RKTime>'{0}' and RKTime<'{1}'", startTime, overTime);
				}

				else
				{

					sql = string.Format("select g.ProducesM,g.GoodsName,g.GoodsPrice,g.GoodsNum,g.GoodsPrice*g.GoodsNum Price,g.GoodsJLDw from Goods g,OutGoodsInfo og,OutNumber om where og.OutNum=om.OutNum and og.GoodsID=g.GoodsID and State='已出库' and CkTime>'{0}' and CkTime<'{1}'", startTime, overTime);
				}
				//利用之前拼接好的SQL，进行查询，绑定
				System.Data.DataTable dt = DBHelper.GetDataTable(sql);
				dgv.DataSource = dt;
				dgv.AutoGenerateColumns = false;
				//清除默认选择项
				this.dgv.ClearSelection();
			}
			catch(Exception ex)
			{

				MessageBox.Show("错误原因：" + ex.Message.ToString());
			}
        }
        //当第二个时间发生改变时进行判断
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
           //当第二个时间发生改变时，按当前条件进行重新加载数据
            LoadDY();
        }
        //当第一时间发生变化时进行判断
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
            //当第一个时间时间发生改变时，按当前条件进行重新加载数据
            LoadDY();
        }

        private void ReportformsInfoForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}