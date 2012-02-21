using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using WindowsFormsToolkit.MVVM;
using System.ComponentModel.DataAnnotations;


namespace $rootnamespace$
{
	public class $safeitemrootname$ : ModelBase
	{
		private string myProperty;

		[Required(ErrorMessage = "MyProperty is required")]
		public string MyProperty {
			get { return this.myProperty; }
			set {
				if (this.myProperty != value) {
					this.myProperty = value;
					base.OnPropertyChanged(() => this.MyProperty);
				}
			}
		}
	}
}
