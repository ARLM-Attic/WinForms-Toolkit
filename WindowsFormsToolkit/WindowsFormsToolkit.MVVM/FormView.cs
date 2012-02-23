using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsToolkit.CommandManager;
using WindowsFormsToolkit.EventAggregator;

namespace WindowsFormsToolkit.MVVM
{
    public class FormView<TViewModel> : Form
        where TViewModel : ViewModel
    {
        private CommandManager.CommandManager commandManager;
        private EventAggregator.EventAggregator eventAggregator;
        private ErrorProvider errorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormView&lt;TViewModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public FormView(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.commandManager = new CommandManager.CommandManager();
            this.errorProvider = new ErrorProvider(this);
        }

        /// <summary>
        /// Gets the command manager.
        /// </summary>
        protected CommandManager.CommandManager CommandManager { get { return this.commandManager; } }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.OnInitializeBinding();
            this.OnInitializeCommands();
            this.OnInitializeEventMessage();

            this.ViewModel.Validated += new EventHandler(ViewModel_Validated);
            base.OnLoad(e);

            if (this.ViewModel is IEventPublisher && !DesignMode)
            {
                (this.ViewModel as IEventPublisher).EventAggregator = this.eventAggregator;
            }

        }

        /// <summary>
        /// Handles the Validated event of the ViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ViewModel_Validated(object sender, EventArgs e)
        {
            this.ViewModel.AttachedControls.ToList().ForEach(c => this.errorProvider.SetError(c.Value as Control, ""));
            if (!string.IsNullOrEmpty(this.ViewModel.Error))
            {
                this.ViewModel.Messages.ToList().ForEach(message =>
                {
                    this.errorProvider.SetError(this.ViewModel.AttachedControls[message.Key] as Control, message.Value);
                });
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this.ViewModel.Validated -= ViewModel_Validated;
            this.errorProvider.Dispose();
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public TViewModel ViewModel { get; private set; }

        /// <summary>
        /// Called when [initialize binding].
        /// </summary>
        protected virtual void OnInitializeBinding() { }

        /// <summary>
        /// Called when [initialize commands].
        /// </summary>
        protected virtual void OnInitializeCommands() { }

        /// <summary>
        /// Called when [initialize event message].
        /// </summary>
        protected virtual void OnInitializeEventMessage()
        {
            if (!DesignMode)
            {
                this.eventAggregator = new EventAggregator.EventAggregator();
                this.eventAggregator.Subscribe(this);
            }
        }
    }
}
