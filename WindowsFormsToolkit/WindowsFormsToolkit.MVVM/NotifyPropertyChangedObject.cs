using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace WindowsFormsToolkit.MVVM
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        private Dictionary<string, dynamic> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPropertyChangedObject"/> class.
        /// </summary>
        public NotifyPropertyChangedObject()
        {
            values = new Dictionary<string, dynamic>();
        }

        #region NotifyAttribute
        private static Dictionary<string, IEnumerable<string>> properties;
        private static bool isAlreadyInitialize = false;

        /// <summary>
        /// Initializes the notify attributes.
        /// </summary>
        private void InitializeNotifyAttributes()
        {
            if (isAlreadyInitialize)
            {
                return;
            }

            // récupération des propriétés et des notifications
            var prop = from p in this.GetType().GetProperties()
                       where p.GetCustomAttributes(typeof(NotifyAttribute), true).Any()
                       select new { 
                           p.Name, 
                           Attributes = p.GetCustomAttributes(typeof(NotifyAttribute), true)
                                            .Cast<NotifyAttribute>()
                                            .Select(a=>string.IsNullOrEmpty(a.NotifyProperty) ? p.Name : a.NotifyProperty)
                       };

            properties = new Dictionary<string, IEnumerable<string>>();
            // création d'un dictionnaire
            prop.ToList().ForEach(p => properties.Add(p.Name, p.Attributes.ToList()));

            isAlreadyInitialize = true;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        public void SetValue<TObject>(Expression<Func<TObject>> expression, dynamic value) {
            InitializeNotifyAttributes();
            var key = GetPropertyName(expression);
            SetValue(key, value);
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetValue<T>(string key, T defaultValue = default(T)) {
            InitializeNotifyAttributes();
            if (values.ContainsKey(key)) {
                return (T)values[key];
            }
            return defaultValue;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, dynamic value) {
            InitializeNotifyAttributes();
            // si la valeur est différente de l'ancienne
            // on l'enregistre et on déclenche les notifications

            if (!values.ContainsKey(key))
            {
                values.Add(key, value);
                properties[key].ToList().ForEach(p => this.OnPropertyChanged(p));
            }
            else
            {
                if (values[key] != value) {
                    values[key] = value;
                    properties[key].ToList().ForEach(p => this.OnPropertyChanged(p));
                }
            }
        }
        #endregion

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
