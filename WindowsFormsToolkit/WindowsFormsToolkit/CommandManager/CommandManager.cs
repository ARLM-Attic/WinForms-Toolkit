using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.CommandManager
{
    public class CommandManager : Component
    {
        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public IList<ICommand> Commands { get; set; }

        /// <summary>
        /// Gets or sets the command binders.
        /// </summary>
        /// <value>
        /// The command binders.
        /// </value>
        public IList<ICommandBinder> Binders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager() {
            this.Commands = new List<ICommand>();
            this.Binders = new List<ICommandBinder>() { 
                new ControlBinder(),
                new MenuItemCommandBinder()
            };

            Application.Idle += UpdateCommandState;
        }

        /// <summary>
        /// Updates the state of the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UpdateCommandState(object sender, EventArgs e) {
            this.Commands.Do(c => c.Enabled);
        }

        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public CommandManager Bind(ICommand command, IComponent component) {
            if (!this.Commands.Contains(command)) {
                this.Commands.Add(command);
            }

            FindBinder(component).Bind(command, component);
            return this;
        }

        /// <summary>
        /// Finds the binder.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        protected ICommandBinder FindBinder(IComponent component) {
            var binder = GetBinderFor(component);
            if (binder == null) {
                throw new Exception(string.Format("No binding found for compoent of type {0}", component.GetType()));
            }

            return binder;
        }

        /// <summary>
        /// Gets the binder for.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        protected ICommandBinder GetBinderFor(IComponent component) {
            var type = component.GetType();
            while (type != null) {
                var binder = Binders.FirstOrDefault(x => x.SourceType == type);
                if (binder != null) {
                    return binder;
                }

                type = type.BaseType;
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                Application.Idle -= UpdateCommandState;
            }
            base.Dispose(disposing);
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Does the specified @this.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="lamba">The lamba.</param>
        public static void Do<T>(this IEnumerable<T> @this, Func<T, object> lamba)
        {
            @this.ToList().ForEach(i => lamba(i));
        }
    }

}
