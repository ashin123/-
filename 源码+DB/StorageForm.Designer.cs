namespace CamtasiaStudio
{
    partial class StorageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageForm));
            this.ZdName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PnoName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsJLDw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvCamtasiaInfo = new System.Windows.Forms.ListView();
            this.InNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProducesM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StorageName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GoodsBZQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QrName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ZdName
            // 
            this.ZdName.DisplayIndex = 7;
            this.ZdName.Text = "制单人";
            this.ZdName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ZdName.Width = 90;
            // 
            // PnoName
            // 
            this.PnoName.DisplayIndex = 5;
            this.PnoName.Text = "生产厂家";
            this.PnoName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PnoName.Width = 147;
            // 
            // GoodsJLDw
            // 
            this.GoodsJLDw.DisplayIndex = 4;
            this.GoodsJLDw.Text = "计量单位";
            this.GoodsJLDw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsJLDw.Width = 66;
            // 
            // GoodsPrice
            // 
            this.GoodsPrice.DisplayIndex = 3;
            this.GoodsPrice.Text = "商品单价";
            this.GoodsPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsPrice.Width = 81;
            // 
            // GoodsDate
            // 
            this.GoodsDate.DisplayIndex = 6;
            this.GoodsDate.Text = "生产日期";
            this.GoodsDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsDate.Width = 132;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lvCamtasiaInfo);
            this.panel1.Location = new System.Drawing.Point(-1, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1303, 580);
            this.panel1.TabIndex = 34;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(793, 49);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "查    询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(541, 48);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(162, 21);
            this.txtName.TabIndex = 1;
            // 
            // cbType
            // 
            this.cbType.BackColor = System.Drawing.SystemColors.Window;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(275, 49);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 0;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "查询条件：";
            // 
            // lvCamtasiaInfo
            // 
            this.lvCamtasiaInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InNum,
            this.ProducesM,
            this.StorageName,
            this.GoodsName,
            this.GoodsPrice,
            this.GoodsNum,
            this.GoodsJLDw,
            this.GoodsBZQ,
            this.GoodsDate,
            this.PnoName,
            this.ZdName,
            this.QrName});
            this.lvCamtasiaInfo.FullRowSelect = true;
            this.lvCamtasiaInfo.GridLines = true;
            this.lvCamtasiaInfo.Location = new System.Drawing.Point(-1, 116);
            this.lvCamtasiaInfo.Name = "lvCamtasiaInfo";
            this.lvCamtasiaInfo.Size = new System.Drawing.Size(1303, 464);
            this.lvCamtasiaInfo.TabIndex = 23;
            this.lvCamtasiaInfo.UseCompatibleStateImageBehavior = false;
            this.lvCamtasiaInfo.View = System.Windows.Forms.View.Details;
            // 
            // InNum
            // 
            this.InNum.Text = "入库单号";
            this.InNum.Width = 99;
            // 
            // ProducesM
            // 
            this.ProducesM.Text = "商品条形码";
            this.ProducesM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ProducesM.Width = 131;
            // 
            // StorageName
            // 
            this.StorageName.DisplayIndex = 8;
            this.StorageName.Text = "所属仓库";
            this.StorageName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StorageName.Width = 92;
            // 
            // GoodsName
            // 
            this.GoodsName.DisplayIndex = 2;
            this.GoodsName.Text = "商品名称";
            this.GoodsName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsName.Width = 143;
            // 
            // GoodsNum
            // 
            this.GoodsNum.DisplayIndex = 9;
            this.GoodsNum.Text = "商品数量";
            this.GoodsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsNum.Width = 82;
            // 
            // GoodsBZQ
            // 
            this.GoodsBZQ.DisplayIndex = 10;
            this.GoodsBZQ.Text = "保质日期";
            this.GoodsBZQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GoodsBZQ.Width = 116;
            // 
            // QrName
            // 
            this.QrName.Text = "确认人";
            this.QrName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.QrName.Width = 120;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(1179, 34);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 17);
            this.lblUser.TabIndex = 32;
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
            this.label1.TabIndex = 31;
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
            this.label2.TabIndex = 36;
            this.label2.Text = "-[入库单据查询]";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(1089, 663);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(18, 17);
            this.lblTime.TabIndex = 33;
            this.lblTime.Text = "--";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CamtasiaStudio.Properties.Resources.left;
            this.pictureBox1.Location = new System.Drawing.Point(23, 653);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StorageForm
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StorageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "入库查询";
            this.Load += new System.EventHandler(this.StorageForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StorageForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ZdName;
        private System.Windows.Forms.ColumnHeader PnoName;
        private System.Windows.Forms.ColumnHeader GoodsJLDw;
        private System.Windows.Forms.ColumnHeader GoodsPrice;
        private System.Windows.Forms.ColumnHeader GoodsDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvCamtasiaInfo;
        private System.Windows.Forms.ColumnHeader InNum;
        private System.Windows.Forms.ColumnHeader ProducesM;
        private System.Windows.Forms.ColumnHeader GoodsName;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader StorageName;
        private System.Windows.Forms.ColumnHeader GoodsNum;
        private System.Windows.Forms.ColumnHeader GoodsBZQ;
        private System.Windows.Forms.ColumnHeader QrName;
        private System.Windows.Forms.Timer timer1;
    }
}