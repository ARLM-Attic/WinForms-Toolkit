using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsToolkit.MVVM;
using WindowsFormsToolkit.EventAggregator;
//using $safeprojectname$.ViewModels;

namespace $rootnamespace$
{
	public partial class $safeitemrootname$ : Base$safeitemrootname$
	{
        public $safeitemrootname$() : this(new MyViewModel()) 
        {
        }

        public $safeitemrootname$(MyViewModel viewModel) : base(viewModel) 
        {
            InitializeComponent();
        }

        protected override void OnInitializeBinding()
        {
            // Initialize your bindings here
            // this.ViewModel.Bind(this.myTextBox, t => t.Text, vm => vm.MyProperty);
        }
	}

    //HACK: Hack for Windows Forms designer
    // Replace MyViewModel with yours
    public class Base$safeitemrootname$ : BaseFormView<MyViewModel> {
        public Base$safeitemrootname$() : this(new MyViewModel()) { }

        public Base$safeitemrootname$(MyViewModel viewModel) : base(viewModel) { }
    }
}
