using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsFormsToolkit.Internal;

namespace WindowsFormsToolkit.Controls
{
    [ToolboxItemFilter("System.Windows.Forms")]
    [ProvideProperty("CueText", typeof(Control))]
    public class CueTextExtender : Component, IExtenderProvider
    {
        private Dictionary<Control, string> cueTexts = new Dictionary<Control, string>();
        private System.ComponentModel.Container components = null;

        public CueTextExtender(IContainer container) : this() {
            container.Add(this);
        }

        public CueTextExtender() : base() {
            this.components = new Container();
        }

        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="extendee">The <see cref="T:System.Object"/> to receive the extender properties.</param>
        /// <returns>
        /// true if this object can provide extender properties to the specified object; otherwise, false.
        /// </returns>
        public bool CanExtend(object extendee)
        {
            return extendee is TextBoxBase ||
                extendee is ComboBox;
        }

        /// <summary>
        /// Gets the cue text.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public string GetCueText(Control control) {
            if (cueTexts.ContainsKey(control)) {
                return cueTexts[control];
            }
            return string.Empty;
        }

        /// <summary>
        /// Sets the cue text.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="cueText">The cue text.</param>
        public void SetCueText(Control control, string cueText) {
            IntPtr handle = control.Handle;
            if (!cueTexts.ContainsKey(control)) {
                cueTexts.Add(control, string.Empty);
            }

            cueTexts[control] = cueText;

            if (control is ComboBox)
            {
                handle = PInvoke.GetWindow(control.Handle, PInvoke.GW_CHILD);
            }
            PInvoke.SendMessage(handle, PInvoke.EM_SETCUEBANNER, IntPtr.Zero, cueText);
        }
    }
}
