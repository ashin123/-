namespace CamtasiaStudio
{
    partial class OutboundDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutboundDetailForm));
            this.btnReset = new System.Windows.Forms.Button();
            this.caozuo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvCamtasiaInfo = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OutNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ZdName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ZdTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QrName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CkTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OutGoodsNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.totalMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.dtpOverTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.GoodsDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelect = new System.Windows.Forms.Button();
            this.PnoName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvDetailInfo = new System.Windows.Forms.ListView();
            this.outID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProducesM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.totalPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsJLDw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsBZQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReset.Location = new System.Drawing.Point(905, 17);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 27;
            this.btnReset.Text = "重   置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // caozuo
            // 
            this.caozuo.Text = "操作";
            this.caozuo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.caozuo.Width = 118;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvCamtasiaInfo);
            this.groupBox1.Location = new System.Drawing.Point(4, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1296, 181);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出库单信息(总单)";
            // 
            // lvCamtasiaInfo
            // 
            this.lvCamtasiaInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.OutNum,
            this.ZdName,
            this.ZdTime,
            this.QrName,
            this.CkTime,
            this.OutGoodsNum,
            this.totalMoney,
            this.State});
            this.lvCamtasiaInfo.FullRowSelect = true;
            this.lvCamtasiaInfo.GridLines = true;
            this.lvCamtasiaInfo.Location = new System.Drawing.Point(10, 20);
            this.lvCamtasiaInfo.Name = "lvCamtasiaInfo";
            this.lvCamtasiaInfo.OwnerDraw = true;
            this.lvCamtasiaInfo.Size = new System.Drawing.Size(1276, 149);
            this.lvCamtasiaInfo.TabIndex = 24;
            this.lvCamtasiaInfo.UseCompatibleStateImageBehavior = false;
            this.lvCamtasiaInfo.View = System.Windows.Forms.View.Details;
            this.lvCamtasiaInfo.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvCamtasiaInfo_DrawColumnHeader);
            this.lvCamtasiaInfo.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvCamtasiaInfo_DrawSubItem);
            this.lvCamtasiaInfo.Click += new System.EventHandler(this.lvCamtasiaInfo_Click);
            this.lvCamtasiaInfo.DoubleClick += new System.EventHandler(this.lvCamtasiaInfo_DoubleClick);
            // 
            // ID
            // 
            this.ID.Text = "编号";
            // 
            // OutNum
            // 
            this.OutNum.Text = "出库单号";
            this.OutNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OutNum.Width = 193;
            // 
            // ZdName
            // 
            this.ZdName.Text = "制单人";
            this.ZdName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ZdName.Width = 125;
            // 
            // ZdTime
            // 
            this.ZdTime.Text = "制单时间";
            this.ZdTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ZdTime.Width = 142;
            // 
            // QrName
            // 
            this.QrName.Text = "确认人";
            this.QrName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.QrName.Width = 141;
            // 
            // CkTime
            // 
            this.CkTime.Text = "出库时间";
            this.CkTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CkTime.Width = 164;
            // 
            // OutGoodsNum
            // 
            this.OutGoodsNum.Text = "出库数量";
            this.OutGoodsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OutGoodsNum.Width = 184;
            // 
            // totalMoney
            // 
            this.totalMoney.Text = "总金额";
            this.totalMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalMoney.Width = 129;
            // 
            // State
            // 
            this.State.Text = "出库单状态";
            this.State.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.State.Width = 132;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(576, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "至";
            // 
            // dtpOverTime
            // 
            this.dtpOverTime.Location = new System.Drawing.Point(608, 18);
            this.dtpOverTime.Name = "dtpOverTime";
            this.dtpOverTime.Size = new System.Drawing.Size(123, 21);
            this.dtpOverTime.TabIndex = 28;
            this.dtpOverTime.ValueChanged += new System.EventHandler(this.dtpOverTime_ValueChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(443, 18);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(123, 21);
            this.dtpStartTime.TabIndex = 28;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // GoodsDate
            // 
            this.GoodsDate.Text = "生产日期";
            this.GoodsDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsDate.Width = 165;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(793, 18);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 27;
            this.btnSelect.Text = "查    询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // PnoName
            // 
            this.PnoName.Text = "生产厂家";
            this.PnoName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PnoName.Width = 181;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(1179, 34);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 17);
            this.lblUser.TabIndex = 50;
            this.lblUser.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 28);
            this.label1.TabIndex = 49;
            this.label1.Text = "仓库条码管理系统";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(195, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "-[出库详单管理]";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(1089, 663);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(18, 17);
            this.lblTime.TabIndex = 51;
            this.lblTime.Text = "--";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpOverTime);
            this.panel1.Controls.Add(this.dtpStartTime);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(-1, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1303, 580);
            this.panel1.TabIndex = 52;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvDetailInfo);
            this.groupBox2.Location = new System.Drawing.Point(3, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1296, 334);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出库单信息(详单)";
            // 
            // lvDetailInfo
            // 
            this.lvDetailInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.outID,
            this.ProducesM,
            this.GoodsName,
            this.GoodsPrice,
            this.GoodsNum,
            this.totalPrice,
            this.GoodsJLDw,
            this.GoodsBZQ,
            this.PnoName,
            this.GoodsDate,
            this.caozuo});
            this.lvDetailInfo.FullRowSelect = true;
            this.lvDetailInfo.GridLines = true;
            this.lvDetailInfo.Location = new System.Drawing.Point(10, 20);
            this.lvDetailInfo.Name = "lvDetailInfo";
            this.lvDetailInfo.Size = new System.Drawing.Size(1276, 308);
            this.lvDetailInfo.TabIndex = 24;
            this.lvDetailInfo.UseCompatibleStateImageBehavior = false;
            this.lvDetailInfo.View = System.Windows.Forms.View.Details;
            this.lvDetailInfo.Click += new System.EventHandler(this.lvDetailInfo_Click);
            // 
            // outID
            // 
            this.outID.Text = "编号";
            // 
            // ProducesM
            // 
            this.ProducesM.Text = "商品条码";
            this.ProducesM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ProducesM.Width = 134;
            // 
            // GoodsName
            // 
            this.GoodsName.Text = "商品名称";
            this.GoodsName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsName.Width = 138;
            // 
            // GoodsPrice
            // 
            this.GoodsPrice.Text = "商品单价";
            this.GoodsPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsPrice.Width = 80;
            // 
            // GoodsNum
            // 
            this.GoodsNum.Text = "商品数量";
            this.GoodsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsNum.Width = 95;
            // 
            // totalPrice
            // 
            this.totalPrice.Text = "总金额";
            this.totalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalPrice.Width = 99;
            // 
            // GoodsJLDw
            // 
            this.GoodsJLDw.Text = "计量单位";
            this.GoodsJLDw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsJLDw.Width = 92;
            // 
            // GoodsBZQ
            // 
            this.GoodsBZQ.Text = "保质期";
            this.GoodsBZQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsBZQ.Width = 108;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(275, 20);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "查询条件：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CamtasiaStudio.Properties.Resources.left;
            this.pictureBox1.Location = new System.Drawing.Point(23, 653);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OutboundDetailForm
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OutboundDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出库详单管理";
            this.Load += new System.EventHandler(this.OutboundDetailForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ColumnHeader caozuo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvCamtasiaInfo;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader OutNum;
        private System.Windows.Forms.ColumnHeader ZdName;
        private System.Windows.Forms.ColumnHeader ZdTime;
        private System.Windows.Forms.ColumnHeader QrName;
        private System.Windows.Forms.ColumnHeader CkTime;
        private System.Windows.Forms.ColumnHeader OutGoodsNum;
        private System.Windows.Forms.ColumnHeader totalMoney;
        private System.Windows.Forms.ColumnHeader State;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpOverTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.ColumnHeader GoodsDate;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ColumnHeader PnoName;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvDetailInfo;
        private System.Windows.Forms.ColumnHeader outID;
        private System.Windows.Forms.ColumnHeader ProducesM;
        private System.Windows.Forms.ColumnHeader GoodsName;
        private System.Windows.Forms.ColumnHeader GoodsPrice;
        private System.Windows.Forms.ColumnHeader GoodsNum;
        private System.Windows.Forms.ColumnHeader totalPrice;
        private System.Windows.Forms.ColumnHeader GoodsJLDw;
        private System.Windows.Forms.ColumnHeader GoodsBZQ;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}