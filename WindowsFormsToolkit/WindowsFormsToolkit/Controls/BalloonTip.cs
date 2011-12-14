using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsToolkit.Internal;

namespace WindowsFormsToolkit.Controls
{
    public class BalloonTip
    {
        public enum BalloonTipIcon : int
        {
            None = 0,
            Info = 1,
            Warning = 2,
            Error = 3
        }

        /// <summary>
        /// Evènement déclenché à l'ouverture de la bulle
        /// </summary>
        public event EventHandler Shown;
        /// <summary>
        /// Evènement déclenché lorsque la méthode Hide() est 
        /// appelée pour fermer la bulle
        /// </summary>
        public event EventHandler Closed;

        #region Propriétés
        private Control parentControl;
        /// <summary>
        /// Gets or sets the parent control.
        /// </summary>
        /// <value>
        /// The parent control.
        /// </value>
        public Control ParentControl
        {
            get { return this.parentControl; }
            set { this.parentControl = value; }
        }

        private string title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        private string text;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private BalloonTipIcon icon = BalloonTipIcon.None;
        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public BalloonTipIcon Icon
        {
            get { return this.icon; }
            set { this.icon = value; }
        }

        private bool autoshowOnFocus = false;
        /// <summary>
        /// Gets or sets a value indicating whether [autoshow on focus].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [autoshow on focus]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoshowOnFocus
        {
            get { return this.autoshowOnFocus; }
            set
            {
                if (value != this.autoshowOnFocus)
                {
                    this.autoshowOnFocus = value;
                    if (value)
                    {
                        this.parentControl.Enter += new EventHandler(parentControl_Enter);
                        this.parentControl.Leave += new EventHandler(parentControl_Leave);
                    }
                    else
                    {
                        this.parentControl.Enter -= new EventHandler(parentControl_Enter);
                        this.parentControl.Leave -= new EventHandler(parentControl_Leave);
                    }
                }
            }
        }

        public bool showOnMouseHover = false;
        /// <summary>
        /// Gets or sets a value indicating whether [show on mouse hover].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show on mouse hover]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMouseHover
        {
            get { return this.showOnMouseHover; }
            set
            {
                if (this.showOnMouseHover != value)
                {
                    this.showOnMouseHover = value;
                    if (value)
                    {
                        this.parentControl.MouseHover += new EventHandler(parentControl_Enter);
                        this.parentControl.MouseLeave += new EventHandler(parentControl_Leave);
                    }
                    else
                    {
                        this.parentControl.MouseHover -= new EventHandler(parentControl_Enter);
                        this.parentControl.MouseLeave -= new EventHandler(parentControl_Leave);
                    }
                }
            }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show()
        {
            int ret;
            PInvoke.EditBalloonTip ebt = new PInvoke.EditBalloonTip();
            ebt.cbStruct = Marshal.SizeOf(ebt);
            ebt.pszText = Marshal.StringToBSTR(this.text);
            ebt.pszTitle = Marshal.StringToBSTR(this.title);
            ebt.ttiIcon = this.icon;

            IntPtr structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ebt));

            try
            {
                Marshal.StructureToPtr(ebt, structPtr, false);

                ret = PInvoke.SendMessage(
                    this.parentControl.Handle,
                    PInvoke.EM_SHOWBALLOONTIP,
                    IntPtr.Zero,
                    structPtr);
            }
            finally
            {
                Marshal.FreeHGlobal(structPtr);
            }

            OnShown(this, EventArgs.Empty);
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Hide()
        {
            int ret;

            ret = PInvoke.SendMessage(this.parentControl.Handle, PInvoke.EM_HIDEBALLOONTIP, IntPtr.Zero, IntPtr.Zero);
            OnClosed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [shown].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnShown(object sender, EventArgs e)
        {
            if (this.Shown != null)
            {
                this.Shown(sender, e);
            }
        }

        /// <summary>
        /// Called when [closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnClosed(object sender, EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(sender, e);
            }
        }


        #endregion

        #region Evènements
        /// <summary>
        /// Handles the Leave event of the parentControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void parentControl_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles the Enter event of the parentControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void parentControl_Enter(object sender, EventArgs e)
        {
            this.Show();
        }
        #endregion

    }
}
