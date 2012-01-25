using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace TestMVVM
{
    public abstract class ViewModelBase : NotifyPropertyChangedBaseObject, IDataErrorInfo, IViewModel
    {
        #region IDataErrorInfo
        private static Dictionary<string, Delegate> propertyGetters;
        private static Dictionary<string, ValidationAttribute[]> validators;
        private Dictionary<string, string>  messages = new Dictionary<string, string>();
        private bool alreadyLoaded = false;
        private bool isValidating = false;
        private dynamic model;
        private string error;
        internal Dictionary<string, IBindableComponent> bindControl = new Dictionary<string, IBindableComponent>();

        /// <summary>
        /// Gets the model.
        /// </summary>
        public virtual dynamic Model
        {
            get { return model; }
            protected set { this.model = value; }
        }

        /// <summary>
        /// Gets the data members.
        /// </summary>
        public List<string> DataMembers {
            get {
                return validators.Select(v => v.Key).ToList();
            }
        }

        /// <summary>
        /// Ensures the data error info is initialized.
        /// </summary>
        private void EnsureDataErrorInfoInitialize()
        {
            if (this.Model != null && !this.alreadyLoaded)
            {
                InitializeDataErrorInfo(this.Model);
            }
        }

        /// <summary>
        /// Initializes the data error info.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        private void InitializeDataErrorInfo<TModel>(TModel model)
        {
            var modelType = typeof(TModel);
            propertyGetters = modelType.GetProperties()
                              .Where(p => GetValidations(p).Length != 0)
                              .ToDictionary(p => p.Name, p => GetValueGetter(modelType, p));

            validators = modelType.GetProperties()
            .Where(p => GetValidations(p).Length != 0)
            .ToDictionary(p => p.Name, p => GetValidations(p));

            this.alreadyLoaded = true;
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
        /// Gets the validations.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property
                .GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        /// <summary>
        /// Gets the value getter.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static Delegate GetValueGetter(Type t, PropertyInfo property)
        {
            var instance = Expression.Parameter(t, "i");
            //var instance = LinqExpression.Expression.Parameter(typeof(object), "i");
            var cast = Expression.TypeAs(
                Expression.Property(instance, property),
                typeof(object));

            var _t = Expression.Lambda(cast, instance).Compile();

            return _t; // as Func<object, object>;
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
                if (this.Model != null)
                {
                    propertyGetters.ToList().ForEach(
                        pg =>
                        {
                            var value = pg.Value.DynamicInvoke(this.Model);
                            var errors = validators[pg.Key].Where(v => !v.IsValid(value))
                                .Select(v => v.ErrorMessage).ToList();
                            messages.Remove(pg.Key);
                            messages.Add(pg.Key, string.Join(Environment.NewLine, errors));
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
                EnsureDataErrorInfoInitialize();
                //messages = Validate();
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
