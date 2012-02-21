using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsToolkit.Validation
{
    public class ValidationManager
    {
        /// <summary>
        /// Determines whether the specified validation attribute is valid.
        /// </summary>
        /// <param name="validationAttribute">The validation attribute.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified validation attribute is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(ValidationAttribute validationAttribute, object value) {
            if (!(validationAttribute is DataTypeAttribute)) {
                return validationAttribute.IsValid(value);
            }

            return false;
        }
    }
}
