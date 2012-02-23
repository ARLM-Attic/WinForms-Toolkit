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
    public class PersonViewModel : ViewModel
    {
        //private Person person;
        public PersonViewModel() {
            this.Model = new Person();
            //person = new Person();
            //this.person.BirthDate = DateTime.Now;
            this.Model.BirthDate = DateTime.Now;
        }

        ///// <summary>
        ///// Gets the model.
        ///// </summary>
        //public override dynamic Model
        //{
        //    get
        //    {
        //        return this.person;
        //    }
        //    protected set
        //    {
        //        this.person = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Notify]
        [Notify("FullName")]
        public string FirstName {
            get {
                return this.Model.FirstName;
            }
            set {
                this.Model.FirstName = value;
                //if (value != this.Model.FirstName) {
                //    this.Model.FirstName = value;
                //    OnPropertyChanged(() => this.FirstName);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Notify]
        [Notify("FullName")]
        public string LastName
        {
            get {
                return this.Model.LastName;
            }
            set {
                this.Model.LastName = value;
                //if (value != this.Model.LastName) {
                //    this.Model.LastName = value;
                //    OnPropertyChanged(() => this.LastName);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Notify]
        public string Email {
            get {
                return this.Model.Email;
            }
            set {
                this.Model.Email = value;
                //if (value != this.Model.Email) {
                //    this.Model.Email = value;
                //    OnPropertyChanged(() => this.Email);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Notify]
        public DateTime BirthDate {
            get {
                return this.Model.BirthDate;
            }
            set {
                this.Model.BirthDate = value;
                //if (value != this.Model.BirthDate) {
                //    this.Model.BirthDate = value;
                //    OnPropertyChanged(() => this.BirthDate);
                //}
            }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName {
            get { return this.Model.FullName; }
        }
    }
}
