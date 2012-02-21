using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace WindowsFormsToolkit.MVVM
{
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Binds the specified view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="viewModel">The view model.</param>
        /// <param name="control">The control.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="dataMember">The data member.</param>
        /// <returns></returns>
        public static Binding Bind<TViewModel, TControl, T1, T2>(
            this TViewModel viewModel,
            TControl control,
            Expression<Func<TControl, T1>> propertyName,
            Expression<Func<TViewModel, T2>> dataMember,
            bool formattingEnabled = false,
            DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged,
            bool autoValidate = true)
            where TViewModel : ViewModelBase
            where TControl : IBindableComponent
        {
            viewModel.AttachedControls.Add(GetPropertyName(dataMember), control);

            if (autoValidate)
            {
                (control as Control).Validating += (s, e) => { viewModel.Validate(); };
            }

            return control.DataBindings.Add(
                propertyName.GetPropertyName(),
                viewModel,
                dataMember.GetPropertyName(),
                formattingEnabled,
                updateMode);
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private static string GetPropertyName<T1, T2>(this Expression<Func<T1, T2>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }
    }
}
