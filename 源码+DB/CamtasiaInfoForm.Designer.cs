namespace CamtasiaStudio
{
    partial class CamtasiaInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamtasiaInfoForm));
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvCamtasiaInfo = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StorageNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StorageName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDeleteInfo = new System.Windows.Forms.Label();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.lblAddInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.Location = new System.Drawing.Point(1179, 34);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 17);
            this.lblUser.TabIndex = 7;
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
            this.label1.TabIndex = 6;
            this.label1.Text = "仓库条码管理系统";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(1090, 663);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(18, 17);
            this.lblTime.TabIndex = 20;
            this.lblTime.Text = "--";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.lvCamtasiaInfo);
            this.panel1.Controls.Add(this.lblDeleteInfo);
            this.panel1.Controls.Add(this.lblUpdateInfo);
            this.panel1.Controls.Add(this.lblAddInfo);
            this.panel1.Location = new System.Drawing.Point(-1, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1303, 580);
            this.panel1.TabIndex = 22;
            // 
            // lvCamtasiaInfo
            // 
            this.lvCamtasiaInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.StorageNum,
            this.StorageName});
            this.lvCamtasiaInfo.FullRowSelect = true;
            this.lvCamtasiaInfo.GridLines = true;
            this.lvCamtasiaInfo.Location = new System.Drawing.Point(0, 34);
            this.lvCamtasiaInfo.Name = "lvCamtasiaInfo";
            this.lvCamtasiaInfo.Size = new System.Drawing.Size(1302, 545);
            this.lvCamtasiaInfo.TabIndex = 3;
            this.lvCamtasiaInfo.UseCompatibleStateImageBehavior = false;
            this.lvCamtasiaInfo.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "编号";
            this.ID.Width = 345;
            // 
            // StorageNum
            // 
            this.StorageNum.Text = "仓库编号";
            this.StorageNum.Width = 428;
            // 
            // StorageName
            // 
            this.StorageName.Text = "仓库名称";
            this.StorageName.Width = 525;
            // 
            // lblDeleteInfo
            // 
            this.lblDeleteInfo.AutoSize = true;
            this.lblDeleteInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeleteInfo.Location = new System.Drawing.Point(205, 11);
            this.lblDeleteInfo.Name = "lblDeleteInfo";
            this.lblDeleteInfo.Size = new System.Drawing.Size(91, 14);
            this.lblDeleteInfo.TabIndex = 22;
            this.lblDeleteInfo.Text = "删除仓库信息";
            this.lblDeleteInfo.Click += new System.EventHandler(this.lblDeleteInfo_Click);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(109, 11);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(91, 14);
            this.lblUpdateInfo.TabIndex = 22;
            this.lblUpdateInfo.Text = "修改仓库信息";
            this.lblUpdateInfo.Click += new System.EventHandler(this.lblUpdateInfo_Click);
            // 
            // lblAddInfo
            // 
            this.lblAddInfo.AutoSize = true;
            this.lblAddInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddInfo.Location = new System.Drawing.Point(13, 11);
            this.lblAddInfo.Name = "lblAddInfo";
            this.lblAddInfo.Size = new System.Drawing.Size(91, 14);
            this.lblAddInfo.TabIndex = 22;
            this.lblAddInfo.Text = "添加仓库信息";
            this.lblAddInfo.Click += new System.EventHandler(this.lblAddInfo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CamtasiaStudio.Properties.Resources.left;
            this.pictureBox1.Location = new System.Drawing.Point(23, 657);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(195, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "-[仓库信息管理]";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CamtasiaInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CamtasiaInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CamtasiaInfoForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CamtasiaInfoForm_Paint);
            this.ParentChanged += new System.EventHandler(this.CamtasiaInfoForm_ParentChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvCamtasiaInfo;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader StorageNum;
        private System.Windows.Forms.ColumnHeader StorageName;
        private System.Windows.Forms.Label lblDeleteInfo;
        private System.Windows.Forms.Label lblUpdateInfo;
        private System.Windows.Forms.Label lblAddInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
    }
}