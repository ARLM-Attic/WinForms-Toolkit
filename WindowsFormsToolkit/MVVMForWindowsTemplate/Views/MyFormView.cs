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
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Views
{
    public partial class MyFormView : BaseMyFormView
    {
        public MyFormView() : this(new MyViewModel()) {
        }

        public MyFormView(MyViewModel viewModel) : base(viewModel) {
            InitializeComponent();
        }

        protected override void OnInitializeBinding() {
            this.ViewModel.Bind(this.myPropertyTextBox, t => t.Text, vm => vm.MyProperty);
        }
    }

    // HACK: for Windows Forms designer
    public class BaseMyFormView : BaseFormView<MyViewModel> {
        public BaseMyFormView() : this(new MyViewModel()) { }

        public BaseMyFormView(MyViewModel viewModel) : base(viewModel) { }
    }
}
