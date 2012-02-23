using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsToolkit.MVVM
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class NotifyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyAttribute"/> class.
        /// </summary>
        public NotifyAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyAttribute"/> class.
        /// </summary>
        /// <param name="notifyProperty">The notify property.</param>
        public NotifyAttribute(string notifyProperty) {
            this.NotifyProperty = notifyProperty;
        }

        /// <summary>
        /// Gets or sets the notify property.
        /// </summary>
        /// <value>
        /// The notify property.
        /// </value>
        public string NotifyProperty { get; set; }
    }
}
