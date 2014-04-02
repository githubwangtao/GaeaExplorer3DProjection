namespace GaeaPlayVideo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.DrawLineButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DrawLineButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(872, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // DrawLineButton
            // 
            this.DrawLineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DrawLineButton.Image = ((System.Drawing.Image)(resources.GetObject("DrawLineButton.Image")));
            this.DrawLineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DrawLineButton.Name = "DrawLineButton";
            this.DrawLineButton.Size = new System.Drawing.Size(23, 22);
            this.DrawLineButton.Text = "DrawLineButton";
            this.DrawLineButton.ToolTipText = "画线";
            this.DrawLineButton.Click += new System.EventHandler(this.DrawLineButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 460);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "播放视频";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       // private Gaea.DockControls.Controls.TabPage tabPageWorldControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton DrawLineButton;
    }
}

