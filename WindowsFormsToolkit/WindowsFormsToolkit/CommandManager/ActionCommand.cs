using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsToolkit.CommandManager
{
    public class ActionCommand : Command
    {
        private readonly Func<bool> enabled;
        private readonly Action execute;
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="execute">The execute.</param>
        public ActionCommand(string name, Func<bool> enabled, Action execute)
        {
            this.name = name;
            this.enabled = enabled;
            this.execute = execute;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            execute();
        }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute()
        {
            return enabled();
        }
    }
}
