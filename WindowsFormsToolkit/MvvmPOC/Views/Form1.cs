using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsToolkit.MVVM;
using WindowsFormsToolkit.EventAggregator;
using MvvmPOC.ViewModels;

namespace MvvmPOC.Views
{
    public partial class Form1 : BaseForm1
    {
        public Form1() : this(new PersonViewModel()) { 
        }

        public Form1(PersonViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        protected override void OnInitializeBinding()
        {
            this.ViewModel.Bind(this.nameTextBox, t => t.Text, vm => vm.LastName, false, DataSourceUpdateMode.OnPropertyChanged);
            this.ViewModel.Bind(this.firstNameTextBox, t => t.Text, vm => vm.FirstName, false, DataSourceUpdateMode.OnPropertyChanged);
            this.ViewModel.Bind(this.emailTextBox, t => t.Text, vm => vm.Email, false, DataSourceUpdateMode.OnPropertyChanged);
            this.ViewModel.Bind(this.birthdateDateTimePicker, t => t.Value, vm => vm.BirthDate, false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }

    public class BaseForm1 : BaseFormView<PersonViewModel> {
        public BaseForm1() : this(new PersonViewModel()) { }

        public BaseForm1(PersonViewModel viewModel) : base(viewModel) { 
        }
    }
}
