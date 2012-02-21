using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using WindowsFormsToolkit.MVVM;

namespace $safeprojectname$.Models
{
    public class MyModel : ModelBase
    {
        private string myProperty;

        public string MyProperty {
            get { return this.myProperty; }
            set {
                if (this.myProperty != value) {
                    this.myProperty = value;
                    this.OnPropertyChanged(() => this.MyProperty);
                }
            }
        }
    }
}
