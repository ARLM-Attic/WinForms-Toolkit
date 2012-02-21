using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using System.ComponentModel.DataAnnotations;
using WindowsFormsToolkit.MVVM;
//using $safeprojectname$.Models;


namespace $rootnamespace$
{
	public class $safeitemrootname$ : ViewModelBase
	{
        public $safeitemrootname$() {
            // Initialize your model here
            // this.Model = new MyModel();
        }

        public string MyProperty {
            get { return this.Model.MyProperty; }
            set {
                if (this.Model.MyProperty != value) {
                    this.Model.MyProperty = value;
                    base.OnPropertyChanged(() => this.MyProperty);
                }
            }
        }
	}
}
