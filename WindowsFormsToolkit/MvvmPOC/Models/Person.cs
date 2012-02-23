using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsToolkit.MVVM;
using System.ComponentModel.DataAnnotations;

namespace MvvmPOC.Models
{
    public class Person : Model
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "Firstname is required")]
        [Notify]
        [Notify("FullName")]
        public string FirstName
        {
            get { return base.GetValue<string>("FirstName"); }
            set {
                base.SetValue("FirstName", value);
                //if (this.firstname != value) {
                //    this.firstname = value;
                //    base.OnPropertyChanged(() => this.FirstName);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Lastname is required")]
        [Notify]
        [Notify("FullName")]
        public string LastName
        {
            get { return base.GetValue<string>("LastName"); }
            set {
                base.SetValue("LastName", value);
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Wrong Email address")]
        [DataType(DataType.EmailAddress)]
        [Notify]
        public string Email
        {
            get { return base.GetValue<string>("Email"); }
            set {
                base.SetValue("Email", value);
            }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName {
            get { return this.FirstName + " " + this.LastName; }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Required]
        [DataType("BirthDate", ErrorMessage = "Birthdate must be in past")]
        [Notify]
        public DateTime BirthDate {
            get { return this.GetValue<DateTime>("BirthDate"); }
            set {
                this.SetValue("BirthDate", value);
            }
        }

        /// <summary>
        /// Validates the birthdate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static ValidationResult ValidateBirthdate(object value, ValidationContext context)
        {
            
            if (value is DateTime) {
                var date = (DateTime)value;
                if (date <= DateTime.Now) {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Birth date must be in future !");
            }

            return new ValidationResult("value must be DateTime");
        }
    }
}
