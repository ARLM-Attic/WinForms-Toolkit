using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.CommandManager
{
    public abstract class CommandBinder<T> : ICommandBinder
        where T : IComponent
    {
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        public Type SourceType {
            get { return typeof(T); }
        }

        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="source">The source.</param>
        public void Bind(ICommand command, object source) {
            Bind(command, (T)source);
        }

        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="source">The source.</param>
        protected abstract void Bind(ICommand command, T source);
    }

    public class ControlBinder : CommandBinder<Control> {
        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="source">The source.</param>
        protected override void Bind(ICommand command, Control source)
        {
            source.DataBindings.Add("Enabled", command, "Enabled");
            source.DataBindings.Add("Text", command, "Name");
            source.Click += (s, e) => command.Execute();

        }
    }

    public class MenuItemCommandBinder : CommandBinder<ToolStripItem> {
        
        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="source">The source.</param>
        protected override void Bind(ICommand command, ToolStripItem source)
        {
            source.Text = command.Name;
            source.Enabled = command.Enabled;
            source.Click += (s, e) => command.Execute();
            command.PropertyChanged += (s, e) => source.Enabled = command.Enabled;
        }
    }
}
