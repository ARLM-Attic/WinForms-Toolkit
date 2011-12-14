using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls
{
    /// <summary>
    /// Fourni aux textbox la possibilité d'afficher une infobulle
    /// </summary>
    [ProvideProperty("Text", typeof(TextBox))]
    [ProvideProperty("Icon", typeof(TextBox))]
    [ProvideProperty("Title", typeof(TextBox))]
    [ProvideProperty("AutoshowOnFocus", typeof(TextBox))]
    [ProvideProperty("ShowOnMouseHover", typeof(TextBox))]
    [Description("Fourni aux textbox la possibilité d'afficher une infobulle")]
    public class BalloonTipExtender : System.ComponentModel.Component, IExtenderProvider
    {
        private System.Collections.Generic.Dictionary<TextBox, BalloonTip> controls = new Dictionary<TextBox, BalloonTip>();
        #region IExtenderProvider Members
        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="extendee">The <see cref="T:System.Object"/> to receive the extender properties.</param>
        /// <returns>
        /// true if this object can provide extender properties to the specified object; otherwise, false.
        /// </returns>
        public bool CanExtend(object extendee)
        {
            return extendee is TextBox;
        }
        #endregion

        #region Propriété Title
        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="title">The title.</param>
        [Category("BalloonTip")]
        public void SetTitle(TextBox control, string title)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            GetBalloonTip(control).Title = title;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        [Category("BalloonTip")]
        [Description("Texte affiché dans le titre de la bulle")]
        public string GetTitle(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            return GetBalloonTip(control).Title;
        }
        #endregion

        #region Text
        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="text">The text.</param>
        [Category("BalloonTip")]
        public void SetText(TextBox control, string text)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            GetBalloonTip(control).Text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        [Category("BalloonTip")]
        [Description("Texte qui sera affiché dans la bulle")]
        public string GetText(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            return GetBalloonTip(control).Text;
        }
        #endregion

        #region Icone
        /// <summary>
        /// Sets the icon.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="icon">The icon.</param>
        [Category("BalloonTip")]
        public void SetIcon(TextBox control, BalloonTip.BalloonTipIcon icon)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            GetBalloonTip(control).Icon = icon;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        [Category("BalloonTip")]
        [Description("Sélectionne l'icône qui sera affichée en haut à gauche de la bulle")]
        [DefaultValue(BalloonTip.BalloonTipIcon.None)]
        public BalloonTip.BalloonTipIcon GetIcon(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            return GetBalloonTip(control).Icon;
        }
        #endregion

        #region AutoshowOnFocus
        /// <summary>
        /// Sets the autoshow on focus.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="autoshowOnFocus">if set to <c>true</c> [autoshow on focus].</param>
        [Category("BalloonTip")]
        public void SetAutoshowOnFocus(TextBox control, bool autoshowOnFocus)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            GetBalloonTip(control).AutoshowOnFocus = autoshowOnFocus;
        }

        /// <summary>
        /// Gets the autoshow on focus.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        [Category("BalloonTip")]
        [DefaultValue(false)]
        [Description("Détermine si la bulle apparaît automatiquement lorsque la textbox prend le focus")]
        public bool GetAutoshowOnFocus(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            return GetBalloonTip(control).AutoshowOnFocus;
        }
        #endregion

        #region ShowOnMouseHover
        /// <summary>
        /// Sets the show on mouse hover.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="showOnMouseHover">if set to <c>true</c> [show on mouse hover].</param>
        [Category("BalloonTip")]
        public void SetShowOnMouseHover(TextBox control, bool showOnMouseHover)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            GetBalloonTip(control).ShowOnMouseHover = showOnMouseHover;
        }

        /// <summary>
        /// Gets the show on mouse hover.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        [Category("BalloonTip")]
        [DefaultValue(false)]
        [Description("Détermine si la bulle apparaît automatiquement lorsque la souris reste au dessus du textbox")]
        public bool GetShowOnMouseHover(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            return GetBalloonTip(control).ShowOnMouseHover;
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Shows the balloon.
        /// </summary>
        /// <param name="control">The control.</param>
        public void ShowBalloon(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (this.controls.ContainsKey(control))
            {
                GetBalloonTip(control).Show();
            }
        }

        /// <summary>
        /// Hides the balloon.
        /// </summary>
        /// <param name="control">The control.</param>
        public void HideBalloon(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (this.controls.ContainsKey(control))
            {
                GetBalloonTip(control).Hide();
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Gets the balloon tip.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private BalloonTip GetBalloonTip(Control control)
        {
            TextBox tb = control as TextBox;
            if (tb == null)
                return null;

            if (this.controls.ContainsKey(tb))
            {
                return this.controls[tb];
            }
            BalloonTip bt = new BalloonTip();
            bt.ParentControl = tb;
            this.controls.Add(tb, bt);
            return bt;
        }
        #endregion
    }
}
