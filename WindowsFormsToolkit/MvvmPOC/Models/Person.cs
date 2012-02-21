using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsToolkit.MVVM;
using System.ComponentModel.DataAnnotations;

namespace MvvmPOC.Models
{
    public class Person : ModelBase
    {
        private string firstname;
        private string lastname;
        private string email;
        private DateTime birthdate;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "Name is required")]
        public string FirstName
        {
            get { return this.firstname; }
            set {
                if (this.firstname != value) {
                    this.firstname = value;
                    base.OnPropertyChanged(() => this.FirstName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "FirstName is required")]
        public string LastName
        {
            get { return this.lastname; }
            set {
                if (this.lastname != value) {
                    this.lastname = value;
                    base.OnPropertyChanged(() => this.LastName);
                }
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
        public string Email
        {
            get { return this.email; }
            set {
                if (this.email != value) {
                    this.email = value;
                    base.OnPropertyChanged(()=>this.Email);
                }
            }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Required]
        [DataType("BirthDate", ErrorMessage = "Birthdate must be in past")]
        //[CustomValidation(typeof(Person), "ValidateBirthdate")]
        //[DateRangeValidation(MinDateTime=DateTime.MinValue, MaxDateTime=DateTime.Now)]
        public DateTime BirthDate {
            get { return this.birthdate; }
            set {
                if (this.birthdate != value) {
                    this.birthdate = value;
                    base.OnPropertyChanged(() => this.BirthDate);
                }
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
