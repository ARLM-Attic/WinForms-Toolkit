using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls
{
    public class TabControl : System.Windows.Forms.TabControl
    {
        private const int WM_MOUSEDOWN = 0x201;

        #region Déclaration des évènements
        [Category("Mouse")]
        public event MouseEventHandler TabMouseUp;
        [Category("Mouse")]
        public event MouseEventHandler TabMouseDown;
        [Category("Mouse")]
        public event MouseEventHandler TabClick;
        [Category("Mouse")]
        public event MouseEventHandler TabDoubleClick;
        [Category("Mouse")]
        public event EventHandler TabMouseEnter;
        [Category("Mouse")]
        public event EventHandler TabMouseLeave;
        [Category("Mouse")]
        public event EventHandler TabMouseHover;
        [Category("Mouse")]
        public event MouseEventHandler TabMouseMove;

        [Category("Key")]
        public event KeyEventHandler CtrlTabPress;

        [Category("Behavior")]
        public event TabControlCancelEventHandler SelectedIndexChanging;
        #endregion

        public TabControl() : base() { }

        #region Propriétés
        private bool disableNavigationByKeys;
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Active ou désactive l'action des touches permettant de naviger entre les onglets.")]
        public bool DisableNavigationByKeys
        {
            get { return this.disableNavigationByKeys; }
            set { this.disableNavigationByKeys = value; }
        }

        private ContextMenuStrip tabContextMenuStrip;
        [Category("Behavior")]
        [Description("Menu contextuel s'affichant lorsque l'utilisateur clic que le bouton droit de la souris au dessus de l'onglet")]
        public ContextMenuStrip TabContextMenuStrip
        {
            get { return this.tabContextMenuStrip; }
            set { this.tabContextMenuStrip = value; }
        }

        private ContextMenu tabContextMenu;
        [Category("Behavior")]
        [Description("Menu contextuel s'affichant lorsque l'utilisateur clic que le bouton droit de la souris au dessus de l'onglet")]
        [Browsable(false)]
        public ContextMenu TabContextMenu
        {
            get { return this.tabContextMenu; }
            set { this.tabContextMenu = value; }
        }
        #endregion

        #region Gestion des évènements souris
        /// <summary>
        /// Traitement de l'évènement MouseUp
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (GetTabIndexFromPoint(e.Location) != -1)
            {
                if (this.TabMouseUp != null)
                {
                    this.TabMouseUp(this, e);
                }

                if (e.Button == MouseButtons.Right && this.tabContextMenuStrip != null)
                {
                    this.tabContextMenuStrip.Show(this, e.Location);
                }

                if (e.Button == MouseButtons.Right && this.tabContextMenu != null && this.tabContextMenuStrip == null)
                {
                    this.tabContextMenu.Show(this, e.Location);
                }
            }
            else base.OnMouseUp(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseDown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (GetTabIndexFromPoint(e.Location) != -1 && this.TabMouseDown != null)
            {
                this.TabMouseDown(this, e);
            }
            else base.OnMouseDown(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (GetTabIndexFromPoint(e.Location) != -1 && this.TabClick != null)
            {
                this.TabClick(this, e);
            }
            else base.OnMouseClick(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseDoubleClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (GetTabIndexFromPoint(e.Location) != -1 && this.TabDoubleClick != null)
            {
                this.TabDoubleClick(this, e);
            }
            else base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseEnter
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (GetTabIndexFromPoint(MousePosition) != -1 && this.TabMouseEnter != null)
            {
                this.TabMouseEnter(this, e);
            }
            else base.OnMouseEnter(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseLeave
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (GetTabIndexFromPoint(MousePosition) != -1 && this.TabMouseLeave != null)
            {
                this.TabMouseLeave(this, e);
            }
            else base.OnMouseLeave(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseHover
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            if (GetTabIndexFromPoint(MousePosition) != -1 && this.TabMouseHover != null)
            {
                this.TabMouseHover(this, e);
            }
            else base.OnMouseHover(e);
        }

        /// <summary>
        /// Traitement de l'évènement MouseMove
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (GetTabIndexFromPoint(e.Location) != -1 && this.TabMouseMove != null)
            {
                this.TabMouseMove(this, e);
            }
            else base.OnMouseMove(e);
        }
        #endregion

        /// <summary>
        /// Déclenche l'évènement SelectedIndexChanging
        /// </summary>
        /// <param name="e">Paramètre de type TabControlCancelEventArgs</param>
        protected virtual void OnSelectedIndexChanging(TabControlCancelEventArgs e)
        {
            if (this.SelectedIndexChanging != null)
            {
                this.SelectedIndexChanging(this, e);
            }

            // si l'évènement est annulé, on remet l'onglet précédent
            if (!e.Cancel)
            {
                this.SelectedTab = e.TabPage;
            }
        }

        /// <summary>
        /// Déclenche l'évènement CtrlTabPressed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCtrlTabPressed(KeyEventArgs e)
        {
            if (this.CtrlTabPress != null)
            {
                this.CtrlTabPress(this, e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (disableNavigationByKeys && Convert.ToBoolean(keyData & Keys.Tab | Keys.Control))
            {
                // si disableNavigationByKeys est vrai,
                // dans ce cas, on sort de la méthode tout de suite
                // pour courtcircuiter l'action des touches
                return true;
            }
            else
            {
                if (keyData == (Keys.Tab | Keys.Control) || keyData == (Keys.Tab | Keys.Shift | Keys.Control))
                {
                    // si l'une des combinaisons Ctrl+Tab, Ctrl+Shift+Tab est appuyée,
                    // on génère les évènements : CtrlTabPress et SelectedIndexChanging
                    KeyEventArgs ke = new KeyEventArgs(keyData);
                    OnCtrlTabPressed(ke);
                    if (!ke.Handled)
                    {
                        int index = this.SelectedIndex;
                        if ((keyData & Keys.Shift) == Keys.Shift)
                        {
                            if (index == 0)
                            {
                                index = this.TabCount - 1;
                            }
                            else
                            {
                                index--;
                            }
                        }
                        else
                        {
                            index = (index + 1) % this.TabCount;
                        }
                        TabControlCancelEventArgs ev = new TabControlCancelEventArgs(
                            this.TabPages[index], index, false, TabControlAction.Selecting);
                        OnSelectedIndexChanging(ev);
                    }
                    return true;

                }
                else return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        protected override void WndProc(ref Message m)
        {
            // Intercepte le MouseDown pour prévenir le changement d'onglet avec la souris
            if (m.Msg == WM_MOUSEDOWN)
            {
                int tabIndex = GetTabIndexFromPoint(new Point(m.LParam.ToInt32()));
                if (tabIndex != -1)
                {
                    TabControlCancelEventArgs e = new TabControlCancelEventArgs(this.TabPages[tabIndex], tabIndex, false, TabControlAction.Selecting);
                    OnSelectedIndexChanging(e);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Alt && e.KeyValue >= 37 && e.KeyValue <= 40)
            {
                e.Handled = true;

                int index = this.SelectedIndex;
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                {
                    if (index == 0)
                    {
                        index = this.TabCount - 1;
                    }
                    else
                    {
                        index--;
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
                    {
                        index = (index + 1) % this.TabCount;
                    }
                }

                TabControlCancelEventArgs ev = new TabControlCancelEventArgs(
                    this.TabPages[index], index, false, TabControlAction.Selecting);
                OnSelectedIndexChanging(ev);

            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Récupère l'index de l'onglet correspondant au Point p
        /// </summary>
        /// <param name="p">Point a traiter</param>
        /// <returns>Index de l'onglet trouvé... -1 sinon</returns>
        protected int GetTabIndexFromPoint(Point p)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                if (this.GetTabRect(i).Contains(p))
                    return i;
            }

            return -1;
        }
    }
}
