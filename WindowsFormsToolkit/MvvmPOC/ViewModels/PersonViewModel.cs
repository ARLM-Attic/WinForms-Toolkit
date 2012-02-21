using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsToolkit.MVVM;
using WindowsFormsToolkit.CommandManager;
using WindowsFormsToolkit.EventAggregator;
using MvvmPOC.Models;

namespace MvvmPOC.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private Person person;
        public PersonViewModel() {
            person = new Person();
            this.person.BirthDate = DateTime.Now;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public override dynamic Model
        {
            get
            {
                return this.person;
            }
            protected set
            {
                this.person = value;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName {
            get {
                return this.Model.FirstName;
            }
            set {
                if (value != this.Model.FirstName) {
                    this.Model.FirstName = value;
                    OnPropertyChanged(() => this.FirstName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string LastName {
            get {
                return this.Model.LastName;
            }
            set {
                if (value != this.Model.LastName) {
                    this.Model.LastName = value;
                    OnPropertyChanged(() => this.LastName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email {
            get {
                return this.Model.Email;
            }
            set {
                if (value != this.Model.Email) {
                    this.Model.Email = value;
                    OnPropertyChanged(() => this.Email);
                }
            }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime BirthDate {
            get {
                return this.Model.BirthDate;
            }
            set {
                if (value != this.Model.BirthDate) {
                    this.Model.BirthDate = value;
                    OnPropertyChanged(() => this.BirthDate);
                }
            }
        }
    }
}
