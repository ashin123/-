using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace CamtasiaStudio
{
    public partial class ReportformsViewForm : Form
    {
        public ReportformsViewForm()
        {
            InitializeComponent();
        }

        //定义获取当前登录的用户的姓名
        public static string UserName;
        //定义获取当前登录的用户的所属权限
        public static string UserPower;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReportformsViewForm_Load(object sender, EventArgs e)
        {
            //显示当前登录的用户名、权限
            this.lblUser.Text = UserName + "[" + UserPower + "]";
            //显示当前的系统时间  
            this.timer1.Start();
          
            //轨道滑块默认为小
            this.trackBar1.Value = 1;
            //隐藏Chart
            this.chart2.Visible = false;
            //隐藏标题
            this.lblTitle.Visible = false;
            this.cbIn.SelectedIndex = 0;
            this.cbOut.SelectedIndex = 0;
            //样式
            this.cbStyle.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前系统时间
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
        }

        #region 将视图导出成图片
        //将视图导出成图片
        //声明一个API函数
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]

        private static extern bool BitBlt(
                        IntPtr hdcDest,   //目标DC的句柄  
                        int nXDest,
                        int nYDest,
                        int nWidth,
                        int nHeight,
                        IntPtr hdcSrc,     //源DC的句柄  
                        int nXSrc,
                        int nYSrc,
                        System.Int32 dwRop     //光栅的处理数值  
                          );
        private void btnKeepImage_Click(object sender, EventArgs e)
        {
            if (chart2.Visible == false)
            {
                MessageBox.Show("您还未绘制视图，不能保存！");
                return;
            }
            ////获得当前屏幕的大小
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            //创建一个以当前屏幕为模板的图象  
            Graphics g1 = this.CreateGraphics();
            //创建以屏幕大小为标准的位图    
            Image MyImage = new Bitmap(rect.Width * 57 / 80, rect.Height * 69 / 80, g1);
            Graphics g2 = Graphics.FromImage(MyImage);
            //得到屏幕的DC  
            IntPtr dc1 = g1.GetHdc();
            //得到Bitmap的DC    
            IntPtr dc2 = g2.GetHdc();
            //调用API函数，实现屏幕捕获  
            BitBlt(dc2, 0, 0, rect.Width, rect.Height, dc1, 0, 0, 13369376);
            //释放掉屏幕的DC  
            g1.ReleaseHdc(dc1);
            //释放掉Bitmap的DC    
            g2.ReleaseHdc(dc2);
            //声明文件格式  
            saveFileDialog1.Filter = "BMP|*.bmp|JPEG|*.jpeg|GIF|*.gif|PNG|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //文件路径名称
                string picPath = saveFileDialog1.FileName;
                //文件类型
                string picType = picPath.Substring(picPath.LastIndexOf(".") + 1, (picPath.Length - picPath.LastIndexOf(".") - 1));
                //根据选择的不同类型来保存不同的图片 
                switch (picType)
                {
                    case "bmp":
                        MyImage.Save(picPath, ImageFormat.Bmp);
                        break;
                    case "jpeg":
                        MyImage.Save(picPath, ImageFormat.Jpeg);
                        break;
                    case "gif":
                        MyImage.Save(picPath, ImageFormat.Gif);
                        break;
                    case "png":
                        MyImage.Save(picPath, ImageFormat.Png);
                        break;
                }
            }
        }

        #endregion

        //打开调色板选择颜色
        private void plBgColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                plBgColor.BackColor = colorDialog1.Color;
            }
        }

        //绘图
        private void btnDraw_Click(object sender, EventArgs e)
        {

            {
                //清空chart视图的所有数据
                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }
                //显示Chart
                this.chart2.Visible = true;
                //显示标题
                this.lblTitle.Visible = true;
                //Chart控件的背景色
                this.chart2.BackColor = plBgColor.BackColor;
                //标题
                this.lblTitle.Text = this.txtTitle.Text;
                this.lblTitle.BackColor = this.plBgColor.BackColor;
                //视图大小   //2为正常    //0为最小
                if (this.trackBar1.Value == 2)
                {
                    this.chart2.Width = 923;
                    this.chart2.Height = 480;
                }
                if (this.trackBar1.Value == 1)
                {
                    this.chart2.Width = 616;
                    this.chart2.Height = 320;
                }
                if (this.trackBar1.Value == 0)
                {
                    this.chart2.Width = 511;
                    this.chart2.Height = 266;
                }
                //视图
                try
                {
                    //视图Y轴
                    //通过数据库查询赋值
                    //年出库
                    string sqlStr1 = "select SUM(GoodsNum) allNum from OutNumber om,OutGoodsInfo og where om.OutNum=og.OutNum and State='已出库'";
                    DataTable dt = DBHelper.GetDataTable(sqlStr1);
                    int year = (int.Parse)(dt.Rows[0]["allNum"].ToString());
                    if (dt.Rows.Count != 0)
                    {
                        year = (int.Parse)(dt.Rows[0]["allNum"].ToString());
                    }
                    else
                    {
                        year = 0;
                    }
                    //月出库
                    DateTime firstDayInMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//获取当月的第一天
                    DateTime lastDayInMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddMilliseconds(-3);//获取当月的最后第一天
                    string sqlStr2 = string.Format("select SUM(GoodsNum) allNum from OutNumber om,OutGoodsInfo og where om.OutNum=og.OutNum and CkTime>'{0}' and CKTime<'{1}' and State='已出库'", firstDayInMonth, lastDayInMonth);
                    DataTable dt2 = DBHelper.GetDataTable(sqlStr2);
                    int month = (int.Parse)(dt2.Rows[0]["allNum"].ToString());
                    if (dt2.Rows.Count != 0)
                    {
                        month = (int.Parse)(dt2.Rows[0]["allNum"].ToString());
                    }
                    else
                    {
                        month = 0;
                    }
                    //周出库
                    //DateTime firstdayWeek = DateTime.Parse(DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek) + 1).ToString("yyyy-MM-dd"));//本周第一天
                    //DateTime lastdayWeek = DateTime.Parse(DateTime.Now.Date.AddDays(7 - (int)(DateTime.Now.DayOfWeek)).ToString("yyyy-MM-dd"));//本周最后一天
                    //MessageBox.Show("" + firstdayWeek);
                    //MessageBox.Show("" + lastdayWeek);
                    //string sqlStr3 = string.Format("select SUM(GoodsNum) allNum from OutNumber om,OutGoodsInfo og where om.OutNum=og.OutNum and CkTime>'{0}' and CKTime<'{1}' and State='已出库'", firstdayWeek, lastdayWeek);
                    //DataTable dt3 = DBHelper.GetDataTable(sqlStr3);
                    //int week = (int.Parse)(dt3.Rows[0][0].ToString());
                    //if (dt3.Rows.Count != 0)
                    //{
                    //    week = (int.Parse)(dt3.Rows[0][0].ToString());
                    //}
                    //else
                    //{
                    //    week = 0;
                    //}

                    //日出库
                    //DateTime dayTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    //string sqlStr4 = string.Format("select SUM(GoodsNum) allNum from OutNumber om,OutGoodsInfo og where om.OutNum=og.OutNum and CkTime='{0}' and State='已出库'", dayTime);
                    //MessageBox.Show(sqlStr4);
                    //DataTable dt4 = DBHelper.GetDataTable(sqlStr4);
                    //int day;
                    //if (dt4.Rows.Count != 0)
                    //{
                    //    day = (int.Parse)(dt4.Rows[0]["allNum"].ToString());
                    //}
                    //else
                    //{
                    //    day = 0;
                    //}
                    ;
                    int[] y1 = new int[4];//定义数组存储出库数据
                    y1[0] = 100;
                    y1[1] = 200;
                    y1[2] = month;
                    y1[3] = year;

                    //通过数据库查询赋值
                    //年入库
                    string sqlStr = "select SUM(GoodsNum) allNum from InNumber im,InGoodsInfo ig where im.InNum=ig.InNum and State='已入库'";
                    DataTable dt5 = DBHelper.GetDataTable(sqlStr);
                    int year2 = (int.Parse)(dt5.Rows[0]["allNum"].ToString());
                    if (dt5.Rows.Count != 0)
                    {
                        year2 = (int.Parse)(dt5.Rows[0]["allNum"].ToString());
                    }
                    else
                    {
                        year2 = 0;
                    }
                    //月入库
                    string sqlStr6 = string.Format("select SUM(GoodsNum) allNum from InNumber im,InGoodsInfo ig where im.InNum=ig.InNum and RKTime>'{0}' and RKTime<'{1}' and State='已入库'", firstDayInMonth, lastDayInMonth);
                    DataTable dt6 = DBHelper.GetDataTable(sqlStr6);
                    int month2 = (int.Parse)(dt6.Rows[0]["allNum"].ToString());
                    if (dt6.Rows.Count != 0)
                    {
                        month2 = (int.Parse)(dt6.Rows[0]["allNum"].ToString());
                    }
                    else
                    {
                        month2 = 0;
                    }
                    //周入库
                    //string sqlStr7 = string.Format("select SUM(GoodsNum) allNum from InNumber im,InGoodsInfo ig where im.InNum=ig.InNum and RKTime>'{0}' and RKTime<'{1}' and State='已入库'", firstdayWeek, lastdayWeek);
                    //DataTable dt7 = DBHelper.GetDataTable(sqlStr7);
                    //int week2 = (int.Parse)(dt7.Rows[0]["allNum"].ToString());
                    //if (dt7.Rows.Count != 0)
                    //{
                    //    week2 = (int.Parse)(dt7.Rows[0]["allNum"].ToString());
                    //}
                    //else
                    //{
                    //    week2 = 0;
                    //}

                    //日入库
                    //string sqlStr8 = string.Format("select SUM(GoodsNum) allNum from InNumber im,InGoodsInfo ig where im.InNum=ig.InNum and RKTime='{0}' and State='已入库'", DateTime.Now.ToString());
                    //DataTable dt8 = DBHelper.GetDataTable(sqlStr8);
                    //int day2 = (int.Parse)(dt8.Rows[0]["allNum"].ToString());
                    //if (dt8.Rows.Count != 0)
                    //{
                    //    day2 = (int.Parse)(dt8.Rows[0]["allNum"].ToString());
                    //}
                    //else
                    //{
                    //    day2 = 0;
                    //}

                    int[] y2 = new int[4];//定义数组存储入库数据
                    y2[0] = 90;
                    y2[1] = 370;
                    y2[2] = month2;
                    y2[3] = year2;
                    //视图X轴
                    string[] x = { "日", "周", "月", "年" };
                    //根据cbStyle下拉列表框中选择的类型调用对应的视图样式
                    if (this.cbStyle.Text == "柱状图")
                    {
                        FillColumnChart(chart2, x, y1, y2);
                    }
                    if (this.cbStyle.Text == "折线图")
                    {
                        FillLineChart(chart2, x, y1, y2);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message.ToString(), "提示");
                    return;
                }
            }

        }

        #region 视图样式
        //柱状图
        public void FillColumnChart(Chart chart2, string[] x, int[] y1, int[] y2)
        {
            chart2.Legends.Clear();
            chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart2.ChartAreas[0].Area3DStyle.Inclination = 30;
            chart2.ChartAreas[0].Area3DStyle.PointDepth = 50;
            chart2.ChartAreas[0].Area3DStyle.IsClustered = true;
            chart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart2.Series[0].ChartType = SeriesChartType.Column;
            chart2.Series[0].Points.DataBindXY(x, y1);
            for (int i = 0; i < 4; i++)
            {
                //出库颜色
                if (cbOut.Text == "红色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Red;
                }
                if (cbOut.Text == "橙色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Orange;
                }
                if (cbOut.Text == "黄色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Yellow;
                }
                if (cbOut.Text == "绿色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Green;
                }
                if (cbOut.Text == "青色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Cyan;
                }
                if (cbOut.Text == "蓝色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Blue;
                }
                if (cbOut.Text == "紫色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Purple;
                }
            }
            Series second = new Series();
            second.ChartType = SeriesChartType.Column;
            second.Points.DataBindXY(x, y2);
            chart2.Series.Add(second);
        }
        //折线图
        public void FillLineChart(Chart chart2, string[] x, int[] y1, int[] y2)
        {
            chart2.Legends.Clear();
            chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart2.ChartAreas[0].Area3DStyle.Inclination = 30;
            chart2.ChartAreas[0].Area3DStyle.PointDepth = 50;
            chart2.ChartAreas[0].Area3DStyle.IsClustered = true;
            chart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].Points.DataBindXY(x, y1);
            for (int i = 0; i < 4; i++)
            {
                //出库颜色
                if (cbOut.Text == "红色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Red;
                }
                if (cbOut.Text == "橙色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Orange;
                }
                if (cbOut.Text == "黄色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Yellow;
                }
                if (cbOut.Text == "绿色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Green;
                }
                if (cbOut.Text == "青色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Cyan;
                }
                if (cbOut.Text == "蓝色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Blue;
                }
                if (cbOut.Text == "紫色")
                {
                    this.chart2.Series[0].Points[i].Color = Color.Purple;
                }
            }
            Series second = new Series();
            second.ChartType = SeriesChartType.Line;
            second.Points.DataBindXY(x, y2);
            chart2.Series.Add(second);
        }

        #endregion

        //重置
        private void btnConfig_Click(object sender, EventArgs e)
        {
            //清除报表视图
            //chart2
            try
            {
                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }
                this.chart2.Visible = false;
                this.lblTitle.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message.ToString());
            }
        }

        //出库下拉选项发生改变时更换对应选择的颜色
        private void cbOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOut.Text == "红色")
            {
                this.cbOut.BackColor = Color.Red;
            }
            if (cbOut.Text == "橙色")
            {
                this.cbOut.BackColor = Color.Orange;
            }
            if (cbOut.Text == "黄色")
            {
                this.cbOut.BackColor = Color.Yellow;
            }
            if (cbOut.Text == "绿色")
            {
                this.cbOut.BackColor = Color.Green;
            }
            if (cbOut.Text == "青色")
            {
                this.cbOut.BackColor = Color.Cyan;
            }
            if (cbOut.Text == "蓝色")
            {
                this.cbOut.BackColor = Color.Blue;
            }
            if (cbOut.Text == "紫色")
            {
                this.cbOut.BackColor = Color.Purple;
            }
        }

        private void ReportformsViewForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
