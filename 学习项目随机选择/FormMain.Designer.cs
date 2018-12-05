namespace 学习项目随机选择
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.clbStudy = new System.Windows.Forms.CheckedListBox();
            this.btnStudy = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtShowTime = new System.Windows.Forms.RichTextBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbStudy
            // 
            this.clbStudy.Enabled = false;
            this.clbStudy.FormattingEnabled = true;
            this.clbStudy.Location = new System.Drawing.Point(46, 36);
            this.clbStudy.Name = "clbStudy";
            this.clbStudy.Size = new System.Drawing.Size(454, 409);
            this.clbStudy.TabIndex = 0;
            // 
            // btnStudy
            // 
            this.btnStudy.Location = new System.Drawing.Point(217, 517);
            this.btnStudy.Name = "btnStudy";
            this.btnStudy.Size = new System.Drawing.Size(169, 67);
            this.btnStudy.TabIndex = 1;
            this.btnStudy.Text = "学习开始";
            this.btnStudy.UseVisualStyleBackColor = true;
            this.btnStudy.Click += new System.EventHandler(this.btnStudy_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // txtShowTime
            // 
            this.txtShowTime.Location = new System.Drawing.Point(536, 36);
            this.txtShowTime.Name = "txtShowTime";
            this.txtShowTime.Size = new System.Drawing.Size(454, 409);
            this.txtShowTime.TabIndex = 4;
            this.txtShowTime.Text = "";
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(651, 517);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(169, 67);
            this.btnEnd.TabIndex = 5;
            this.btnEnd.Text = "学习结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1036, 621);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.txtShowTime);
            this.Controls.Add(this.btnStudy);
            this.Controls.Add(this.clbStudy);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "学习项目随机选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbStudy;
        private System.Windows.Forms.Button btnStudy;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.RichTextBox txtShowTime;
        private System.Windows.Forms.Button btnEnd;
    }
}

