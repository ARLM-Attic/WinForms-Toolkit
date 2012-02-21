using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq.Expressions;
using WindowsFormsToolkit.Validation;

namespace WindowsFormsToolkit.MVVM
{
    public class ModelBase : NotifyPropertyChangedBaseObject, IValidatableObject
    {
        private static Dictionary<string, Delegate> propertyGetters;
        private static Dictionary<string, ValidationAttribute[]> validators;
        private bool alreadyLoaded = false;

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            this.EnsureDataErrorInfoInitialize();
            var result = new List<ValidationResult>();
            propertyGetters.ToList().ForEach(
                pg =>
                {
                    validationContext.MemberName = pg.Key;
                    var value = pg.Value.DynamicInvoke(this);
                    for (int i = 0; i < validators[pg.Key].Length; i++)
                    {
                        var r = validators[pg.Key][i].GetValidationResult(value, validationContext);
                        if (r != null)
                        {
                            result.Add(r);
                        }
                    }
                    //var value = pg.Value.DynamicInvoke(this);
                    //var errors = validators[pg.Key].Where(v => !validationManager.IsValid(v, value))
                    //    .Select(v => v.ErrorMessage).ToList();
                    //messages.Remove(pg.Key);
                    //messages.Add(pg.Key, string.Join(Environment.NewLine, errors));
                });
            return result;
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate() {
            return this.Validate(new ValidationContext(this, null, null));
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
        /// Ensures the data error info is initialized.
        /// </summary>
        private void EnsureDataErrorInfoInitialize()
        {
            if (!this.alreadyLoaded)
            {
                InitializeDataErrorInfo();
            }
        }

        /// <summary>
        /// Initializes the data error info.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        private void InitializeDataErrorInfo()
        {
            var modelType = this.GetType();
            propertyGetters = modelType.GetProperties()
                                .Where(p => GetValidations(p).Length != 0)
                                .ToDictionary(p => p.Name, p => GetValueGetter(modelType, p));

            validators = modelType.GetProperties()
            .Where(p => GetValidations(p).Length != 0)
            .ToDictionary(p => p.Name, p => GetValidations(p));

            this.alreadyLoaded = true;
        }
    }
}
