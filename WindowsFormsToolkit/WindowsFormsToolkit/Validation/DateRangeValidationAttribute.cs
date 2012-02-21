using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsToolkit.Validation
{
    public class DateRangeValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRangeValidationAttribute"/> class.
        /// </summary>
        public DateRangeValidationAttribute() {
            this.MinDateTime = DateTime.MinValue;
            this.MaxDateTime = DateTime.MaxValue;
        }

        /// <summary>
        /// Gets or sets the min date time.
        /// </summary>
        /// <value>
        /// The min date time.
        /// </value>
        public DateTime MinDateTime { get; set; }

        /// <summary>
        /// Gets or sets the max date time.
        /// </summary>
        /// <value>
        /// The max date time.
        /// </value>
        public DateTime MaxDateTime { get; set; }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (!(value is DateTime)) {
                return false;
            }

            var dateTime = (DateTime)value;
            if (dateTime < this.MinDateTime || dateTime > this.MaxDateTime) {
                return false;
            }
            return true;
            //return base.IsValid(value);
        }
    }
}
