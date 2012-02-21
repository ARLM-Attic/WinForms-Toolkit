using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using WindowsFormsToolkit.Validation;

namespace WindowsFormsToolkit.MVVM
{
    public abstract class ViewModelBase : NotifyPropertyChangedBaseObject, IDataErrorInfo, IViewModel
    {
        #region IDataErrorInfo
        private static ValidationManager validationManager = new ValidationManager();
        private static Dictionary<string, ValidationAttribute[]> validators;
        private Dictionary<string, string>  messages = new Dictionary<string, string>();
        private bool isValidating = false;
        private dynamic model;
        private string error;
        private Dictionary<string, IBindableComponent> attachedControls = new Dictionary<string, IBindableComponent>();

        /// <summary>
        /// Gets the model.
        /// </summary>
        public virtual dynamic Model
        {
            get { return model; }
            protected set { this.model = value; }
        }

        /// <summary>
        /// Gets the attached controls.
        /// </summary>
        public Dictionary<string, IBindableComponent> AttachedControls {
            get { return this.attachedControls; }
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        public Dictionary<string, string> Messages {
            get {
                return this.messages;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is validating.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is validating; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidating {
            get { return this.isValidating; }
            set { this.isValidating = value; }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                return error;
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Validate()
        {
            var cancelArgs = new CancelEventArgs(false);
            this.OnValidating(this, cancelArgs);

            if (!cancelArgs.Cancel && !isValidating)
            {
                this.isValidating = true;

                ValidationContext c = new ValidationContext(this.Model, null, null);
                var result = (this.Model as ModelBase).Validate(c);
                messages.Clear();

                if (result != null && result.Any()) {
                    result.ToList().ForEach(r => { 
                        if (!messages.ContainsKey(r.MemberNames.First())) {
                            messages.Add(r.MemberNames.First(), r.ErrorMessage); 
                        } else {
                            messages[r.MemberNames.First()] += Environment.NewLine + r.ErrorMessage;
                        }
                    });
                }
            }
            this.error = string.Join(Environment.NewLine, messages.Select(m=>m.Value));
            if (!cancelArgs.Cancel)
            {
                this.OnValidated(this, EventArgs.Empty);
            }
            return messages;
        }

        /// <summary>
        /// Occurs when [validated].
        /// </summary>
        public event EventHandler Validated;

        /// <summary>
        /// Occurs when [validating].
        /// </summary>
        public event EventHandler<CancelEventArgs> Validating;

        /// <summary>
        /// Called when [validated].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnValidated(object sender, EventArgs e) {
            if (this.Validated != null) {
                this.Validated(sender, e);
            }
            this.isValidating = false;
        }

        /// <summary>
        /// Called when [validating].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        protected virtual void OnValidating(object sender, CancelEventArgs e) {
            if (this.Validating != null) {
                this.Validating(sender, e);
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string columnName]
        {
            get
            {
                if (messages != null && messages.ContainsKey(columnName))
                {
                    return messages[columnName];
                }

                return string.Empty;
            }
        }
        #endregion
    }
}
