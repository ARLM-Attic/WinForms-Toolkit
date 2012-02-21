using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace WindowsFormsToolkit.MVVM
{
    public class NotifyPropertyChangedBaseObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        public void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        ///// <summary>
        ///// Called when [property changed].
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="action">The action.</param>
        ///// <param name="value">The value.</param>
        //public void OnPropertyChanged<T>(Expression<Func<T>> action, T value, Action doAction) {
        //    var propertyName = GetPropertyName(action);
        //    var property = this.GetType().GetProperty(propertyName);

        //    if (value.Equals(property.GetValue(this, null)))
        //    {
        //        return;
        //    }

        //    doAction();
        //    OnPropertyChanged(propertyName);
        //}

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private static PropertyInfo GetProperty<T>(Expression<Func<T>> action) {
            return typeof(T).GetProperty(GetPropertyName(action));
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
