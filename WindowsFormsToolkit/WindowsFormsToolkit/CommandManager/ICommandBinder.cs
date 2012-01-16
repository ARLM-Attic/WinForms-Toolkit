using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsToolkit.CommandManager
{
    public interface ICommandBinder
    {
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        Type SourceType { get; }

        /// <summary>
        /// Binds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="source">The source.</param>
        void Bind(ICommand command, object source);
    }
}
