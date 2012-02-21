using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsToolkit.MVVM
{
    public partial class BaseControlView<TViewModel> : UserControl
        where TViewModel : ViewModelBase
    {
        private ErrorProvider errorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseControlView&lt;TViewModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public BaseControlView(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.errorProvider = new ErrorProvider(this);
        }

        /// <summary>
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            this.ViewModel.Validating -= new EventHandler<CancelEventArgs>(ViewModel_Validating);
            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.OnInitializeBinding();
            this.OnInitializeCommands();
            this.ViewModel.Validating += new EventHandler<CancelEventArgs>(ViewModel_Validating);
            base.OnLoad(e);
        }

        /// <summary>
        /// Handles the Validating event of the ViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        void ViewModel_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ViewModel.Error))
            {
                this.ViewModel.Messages.ToList().ForEach(m =>
                {
                    this.errorProvider.SetError(this.ViewModel.AttachedControls[m.Key] as Control, m.Value);
                });
            }
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public TViewModel ViewModel
        {
            get;
            private set;
        }

        /// <summary>
        /// Called when [initialize binding].
        /// </summary>
        protected virtual void OnInitializeBinding() { }

        /// <summary>
        /// Called when [initialize commands].
        /// </summary>
        protected virtual void OnInitializeCommands() { }
    }
}
