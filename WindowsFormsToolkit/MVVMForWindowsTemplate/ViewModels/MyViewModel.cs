using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using $safeprojectname$.Models;
using WindowsFormsToolkit.MVVM;


namespace $safeprojectname$.ViewModels
{
    public class MyViewModel : ViewModelBase
    {
        public MyViewModel() {
            this.Model = new MyModel();
            this.Model.MyProperty = "$safeprojectname$";
        }

        public string MyProperty {
            get { return this.Model.MyProperty; }
            set {
                if (this.Model.MyProperty != value) {
                    this.Model.MyProperty = value;
                    this.OnPropertyChanged(() => this.MyProperty);
                }
            }
        }
    }
}
