using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsToolkit.Controls
{
    [ToolboxBitmap(typeof(Button))]
    public class SplitButton : Button
    {
        private PushButtonState _state;
        private const int PushButtonWidth = 14;
        private static int BorderSize = SystemInformation.Border3DSize.Width * 2;
        private bool skipNextOpen = false;
        private Rectangle dropDownRectangle = new Rectangle();
        private bool showSplit = true;
        private bool showSplitOnlyOnFocus = false;

        /// <summary>
        /// Constructeur de la classe SplitButton
        /// </summary>
        public SplitButton()
        {
            this.AutoSize = true;
        }

        /// <summary>
        /// Obtient ou défini une valeur indiquant si ce bouton affichera
        /// le split
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowSplit
        {
            get
            {
                return this.showSplit;
            }
            set
            {
                if (value != showSplit)
                {
                    showSplit = value;
                    Invalidate();
                    if (this.Parent != null)
                    {
                        this.Parent.PerformLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Obtient ou défini une valeur indiquant si la 
        /// barre de split ne sera visible que lorsque
        /// le bouton aura le focus
        /// </summary>
        [DefaultValue(false)]
        [Category("Appearance")]
        public bool ShowSplitOnlyFocus
        {
            get { return this.showSplitOnlyOnFocus; }
            set
            {
                if (this.showSplitOnlyOnFocus != value)
                {
                    this.showSplitOnlyOnFocus = value;
                    this.Invalidate();
                }
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //public ToolStripItemCollection ContextItems {
        //    get { return this.contextMenu.Items; }
        //}

        /// <summary>
        /// Obtient ou défini l'état du bouton
        /// </summary>
        private PushButtonState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (!_state.Equals(value))
                {
                    _state = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Calcule la taille idéale pour le bouton
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);
            if (showSplit && !string.IsNullOrEmpty(Text) && TextRenderer.MeasureText(Text, Font).Width + PushButtonWidth > preferredSize.Width)
            {
                return preferredSize + new Size(PushButtonWidth + BorderSize * 2, 0);
            }
            return preferredSize;
        }

        #region Gestion des évènements clavier
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData.Equals(Keys.Down) && showSplit)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnGotFocus(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Default;
            }
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (showSplit)
            {
                if (kevent.KeyCode.Equals(Keys.Down))
                {
                    ShowContextMenuStrip();
                }
                else if (kevent.KeyCode.Equals(Keys.Space) && kevent.Modifiers == Keys.None)
                {
                    State = PushButtonState.Pressed;
                }
            }

            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyCode.Equals(Keys.Space))
            {
                if (Control.MouseButtons == MouseButtons.None)
                {
                    State = PushButtonState.Normal;
                }
            }
            base.OnKeyUp(kevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnLostFocus(e);
                return;
            }
            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Normal;
            }
        }
        #endregion

        #region Gestion des évènements souris
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseDown(e);
                return;
            }

            if (dropDownRectangle.Contains(e.Location))
            {
                ShowContextMenuStrip();
            }
            else
            {
                State = PushButtonState.Pressed;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseEnter(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Hot;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseLeave(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                if (Focused)
                {
                    State = PushButtonState.Default;
                }
                else
                {
                    State = PushButtonState.Normal;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (!showSplit)
            {
                base.OnMouseUp(mevent);
                return;
            }

            if (ContextMenuStrip == null || !ContextMenuStrip.Visible)
            {
                SetButtonDrawState();
                if (Bounds.Contains(Parent.PointToClient(Cursor.Position)) && !dropDownRectangle.Contains(mevent.Location))
                {
                    OnClick(new EventArgs());
                }
            }
        }

        #endregion

        /// <summary>
        /// Dessine le bouton dans le context donné
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (!showSplit)
            {
                return;
            }

            Graphics g = pevent.Graphics;
            Rectangle bounds = this.ClientRectangle;

            // draw the button background as according to the current state.
            if (State != PushButtonState.Pressed && IsDefault && !Application.RenderWithVisualStyles)
            {
                Rectangle backgroundBounds = bounds;
                backgroundBounds.Inflate(-1, -1);
                ButtonRenderer.DrawButton(g, backgroundBounds, State);

                // button renderer doesnt draw the black frame when themes are off =(
                g.DrawRectangle(SystemPens.WindowFrame, 0, 0, bounds.Width - 1, bounds.Height - 1);

            }
            else
            {
                ButtonRenderer.DrawButton(g, bounds, State);
            }
            // calculate the current dropdown rectangle.
            dropDownRectangle = new Rectangle(bounds.Right - PushButtonWidth - 1, BorderSize, PushButtonWidth, bounds.Height - BorderSize * 2);

            int internalBorder = BorderSize;
            Rectangle focusRect =
                new Rectangle(internalBorder,
                              internalBorder,
                              bounds.Width - dropDownRectangle.Width - internalBorder,
                              bounds.Height - (internalBorder * 2));

            bool drawSplitLine = !this.showSplitOnlyOnFocus || (State == PushButtonState.Hot || State == PushButtonState.Pressed || !Application.RenderWithVisualStyles);

            if (RightToLeft == RightToLeft.Yes)
            {
                dropDownRectangle.X = bounds.Left + 1;
                focusRect.X = dropDownRectangle.Right;
                if (drawSplitLine)
                {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Left + PushButtonWidth, BorderSize, bounds.Left + PushButtonWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Left + PushButtonWidth + 1, BorderSize, bounds.Left + PushButtonWidth + 1, bounds.Bottom - BorderSize);
                }
            }
            else
            {
                if (drawSplitLine)
                {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Right - PushButtonWidth, BorderSize, bounds.Right - PushButtonWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Right - PushButtonWidth - 1, BorderSize, bounds.Right - PushButtonWidth - 1, bounds.Bottom - BorderSize);
                }

            }

            // Draw an arrow in the correct location 
            PaintArrow(g, dropDownRectangle);

            // Figure out how to draw the text
            TextFormatFlags formatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            // If we dont' use mnemonic, set formatFlag to NoPrefix as this will show ampersand.
            if (!UseMnemonic)
            {
                formatFlags = formatFlags | TextFormatFlags.NoPrefix;
            }
            else if (!ShowKeyboardCues)
            {
                formatFlags = formatFlags | TextFormatFlags.HidePrefix;
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                TextRenderer.DrawText(g, Text, Font, focusRect, SystemColors.ControlText, formatFlags);
            }

            // draw the focus rectangle.

            if (State != PushButtonState.Pressed && Focused && !Application.RenderWithVisualStyles)
            {
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }

            // Draw image
            if (this.Image != null)
            {
                ((Bitmap)this.Image).MakeTransparent();
                pevent.Graphics.DrawImage(this.Image, ComputeImageRectangle(focusRect));
            }
        }

        /// <summary>
        /// Dessine la flèche dans le rectangle défini
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dropDownRect"></param>
        private void PaintArrow(Graphics g, Rectangle dropDownRect)
        {
            Point middle = new Point(Convert.ToInt32(dropDownRect.Left + dropDownRect.Width / 2), Convert.ToInt32(dropDownRect.Top + dropDownRect.Height / 2));

            //if the width is odd - favor pushing it over one pixel right.
            middle.X += (dropDownRect.Width % 2);

            Point[] arrow = new Point[] { new Point(middle.X - 2, middle.Y - 1), new Point(middle.X + 3, middle.Y - 1), new Point(middle.X, middle.Y + 2) };

            g.FillPolygon(SystemBrushes.ControlText, arrow);
        }

        /// <summary>
        /// Affiche le menu contextuel
        /// </summary>
        private void ShowContextMenuStrip()
        {
            if (skipNextOpen)
            {
                // we were called because we're closing the context menu strip
                // when clicking the dropdown button.
                skipNextOpen = false;
                return;
            }
            State = PushButtonState.Pressed;

            if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Closing += new ToolStripDropDownClosingEventHandler(ContextMenuStrip_Closing);
                ContextMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
            //else {
            //    if (this.ContextItems.Count > 0) {
            //        this.contextMenu.Closing += new ToolStripDropDownClosingEventHandler(ContextMenuStrip_Closing);
            //        this.contextMenu.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            //    }
            //}
        }

        /// <summary>
        /// Délégué attaché à la fermeture du ContextMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            ContextMenuStrip cms = sender as ContextMenuStrip;
            if (cms != null)
            {
                cms.Closing -= new ToolStripDropDownClosingEventHandler(ContextMenuStrip_Closing);
            }

            SetButtonDrawState();

            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                skipNextOpen = (dropDownRectangle.Contains(this.PointToClient(Cursor.Position)));
            }
        }

        /// <summary>
        /// Défini l'état du bouton pour l'affichage
        /// </summary>
        private void SetButtonDrawState()
        {
            if (Bounds.Contains(Parent.PointToClient(Cursor.Position)))
            {
                State = PushButtonState.Hot;
            }
            else if (Focused)
            {
                State = PushButtonState.Default;
            }
            else
            {
                State = PushButtonState.Normal;
            }
        }

        /// <summary>
        /// Calcule le rectangle qui contiendra l'image du bouton
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Rectangle ComputeImageRectangle(Rectangle rect)
        {
            Rectangle retRect;
            int X = 0, Y = 0;
            switch (this.ImageAlign)
            {
                case System.Drawing.ContentAlignment.BottomCenter:
                    Y = rect.Bottom - this.Image.Height;
                    X = rect.Left + (rect.Width - this.Image.Width) / 2;
                    break;
                case System.Drawing.ContentAlignment.BottomLeft:
                    Y = rect.Bottom - this.Image.Height;
                    X = rect.X;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    Y = rect.Bottom - this.Image.Height;
                    X = rect.Right - this.Image.Width;
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    Y = rect.Top + (rect.Height - this.Image.Height) / 2;
                    X = rect.Left + (rect.Width - this.Image.Width) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                    Y = rect.Top + (rect.Height - this.Image.Height) / 2;
                    X = rect.Left;
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    Y = rect.Top + (rect.Height - this.Image.Height) / 2;
                    X = rect.Right - this.Image.Width;
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    Y = rect.Top;
                    X = rect.Left + (rect.Width - this.Image.Width) / 2;
                    break;
                case System.Drawing.ContentAlignment.TopLeft:
                    Y = rect.Top;
                    X = rect.Left;
                    break;
                case System.Drawing.ContentAlignment.TopRight:
                    Y = rect.Top;
                    X = rect.Right - this.Image.Width;
                    break;
            }

            retRect = new Rectangle(X, Y, this.Image.Width, this.Image.Height);

            return retRect;

        }
    }
}
