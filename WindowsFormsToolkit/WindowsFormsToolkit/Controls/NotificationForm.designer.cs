namespace WindowsFormsToolkit.Controls
{
    partial class NotificationForm
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
                this.gradientBrush.Dispose();
                this.backgroundBrush.Dispose();
                this.hoverCloseButton.Dispose();
                this.closeButton.Dispose();
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
            this.pctClose = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pctClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pctClose
            // 
            this.pctClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctClose.BackColor = System.Drawing.Color.Transparent;
            this.pctClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pctClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctClose.Location = new System.Drawing.Point(282, 2);
            this.pctClose.Name = "pctClose";
            this.pctClose.Size = new System.Drawing.Size(16, 16);
            this.pctClose.TabIndex = 0;
            this.pctClose.TabStop = false;
            this.pctClose.MouseLeave += new System.EventHandler(this.pctClose_MouseLeave);
            this.pctClose.Click += new System.EventHandler(this.pctClose_Click);
            this.pctClose.MouseEnter += new System.EventHandler(this.pctClose_MouseEnter);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Controls.Add(this.pctClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationForm";
            this.ShowInTaskbar = false;
            this.Text = "NotificationForm";
            this.TopMost = true;
            this.MouseLeave += new System.EventHandler(this.NotificationForm_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NotificationForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pctClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctClose;
        private System.Windows.Forms.Timer timer1;
    }
}