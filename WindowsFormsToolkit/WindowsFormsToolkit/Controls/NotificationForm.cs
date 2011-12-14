using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsToolkit.Controls
{
    public partial class NotificationForm : Form
    {
        private const int CS_DROPSHADOW = 0x20000;

        private Brush backgroundBrush;
        private Brush gradientBrush;
        private RectangleF bottomRectangle;
        private float gradientHeight;
        private double opacity = 0.6;
        private bool autoHide;
        private int autoHideTime=10;
        private Bitmap closeButton;
        private Bitmap hoverCloseButton;

        public NotificationForm()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.StartPosition = FormStartPosition.Manual;

            base.Opacity = this.opacity;

            //topRectangle = new Rectangle(0, 0, this.Width, (2 * this.Height) / 3);
            this.gradientHeight = SystemInformation.CaptionHeight;
            bottomRectangle = new RectangleF(0, this.Height - gradientHeight, this.Width, gradientHeight);

            backgroundBrush = new SolidBrush(SystemColors.ControlLightLight);
            gradientBrush = new LinearGradientBrush(
                bottomRectangle,
                SystemColors.ControlLightLight,
                SystemColors.ControlDark,
                LinearGradientMode.Vertical);

            InitCloseButtonImages();
            this.pctClose.Image = closeButton;

        }

        [Category("Window Style")]
        [Description("Obtient ou défini une valeur indiquant si la croix sera affichée pour fermer la fenêtre.")]
        [DefaultValue(true)]
        public bool ShowCloseButton {
            get { return this.pctClose.Visible; }
            set { this.pctClose.Visible = value; }
        }

        [Description("Obtient ou défini une valeur indiquant l'opacité de la fenêtre.")]
        [DefaultValue(0.6)]
        public new double Opacity {
            get { return this.opacity; }
            set { this.opacity = value; }
        }

        [Category("Behavior")]
        [Description("Obtient ou défini une valeur indiquant si la fenêtre se ferme automatiquement.")]
        [DefaultValue(true)]
        public bool AutoHide {
            get { return this.autoHide; }
            set { this.autoHide = value; }
        }

        [Category("Behavior")]
        [Description("Obtient ou défini une valeur précisant la durée d'affichage de la fenêtre.")]
        [DefaultValue(10)]
        public int AutoHideTime {
            get { return this.autoHideTime; }
            set { this.autoHideTime = value; }
        }

        private void InitCloseButtonImages() {
            closeButton = new Bitmap(16, 16);
            Pen pen = new Pen(SystemColors.ControlDark, 2);
            using (Graphics g = Graphics.FromImage(closeButton)) {
                g.DrawLine(pen, new Point(3, 3), new Point(12, 12));
                g.DrawLine(pen, new Point(3, 12), new Point(12, 3));
            }

            pen.Width = 1;
            hoverCloseButton = new Bitmap(closeButton);
            using (Graphics g = Graphics.FromImage(hoverCloseButton)) {
                g.DrawRectangle(pen, new Rectangle(0, 0, 15, 15));
            }
            
            pen.Dispose();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (OSFeature.Feature.IsPresent(OSFeature.Themes)) // alors OS >= WinXP
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);

        //    e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);
        //    e.Graphics.FillRectangle(gradientBrush, bottomRectangle);
        //    e.Graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(0,0,this.Width-1,this.Height-1));
        //}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.gradientHeight == 0)
                return;
            bottomRectangle = new RectangleF(0, this.Height - gradientHeight, this.Width, gradientHeight);

            gradientBrush = new LinearGradientBrush(
                bottomRectangle,
                SystemColors.ControlLightLight,
                SystemColors.ControlDark,
                LinearGradientMode.Vertical);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            //topRectangle = new Rectangle(0, 0, this.Width, (2 * this.Height) / 3);
            this.Invalidate(true);
        }

        private void pctClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pctClose_MouseEnter(object sender, EventArgs e)
        {
            this.pctClose.Image = this.hoverCloseButton;
        }

        private void pctClose_MouseLeave(object sender, EventArgs e)
        {
            this.pctClose.Image = this.closeButton;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.MouseMove += new MouseEventHandler(NotificationForm_MouseMove);
            e.Control.MouseLeave += new EventHandler(NotificationForm_MouseLeave);
        }

        private void NotificationForm_MouseMove(object sender, MouseEventArgs e)
        {
            base.Opacity = 1;

            if (this.timer1.Enabled) {
                this.timer1.Enabled = false;
            }
        }

        private void NotificationForm_MouseLeave(object sender, EventArgs e)
        {
            base.Opacity = this.opacity;
            if (this.autoHide)
            {
                timer1.Interval = this.autoHideTime * 1000;
                timer1.Tag = "Shown";
                timer1.Enabled = true;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!this.DesignMode)
            {
                if (!this.timer1.Enabled && (this.autoHide && this.autoHideTime > 0))
                {
                    timer1.Interval = this.autoHideTime * 1000;
                    timer1.Tag = "Shown";
                    timer1.Enabled = true;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.timer1.Enabled = false;
            base.OnClosing(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch ((string)timer1.Tag) { 
                case "Shown":
                    timer1.Enabled = false;
                    timer1.Interval = 100;
                    timer1.Tag = "Hiding";
                    timer1.Enabled = true;
                    break;
                case "Hiding":
                    if (base.Opacity > 0.01)
                    {
                        base.Opacity -= 0.01;
                    }
                    else {
                        this.timer1.Enabled = false;
                        this.Close();
                    }
                    break;
            }
        }
    }
}