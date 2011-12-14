using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    public class CustomValidator : BaseValidator
    {
        public event EventHandler<ValidatingEventArgs> Validating;

        private Control controlToValidate;
        [Description("Obtient ou défini le contrôle à valider")]
        public virtual Control ControlToValidate
        {
            get { return this.controlToValidate; }
            set { this.controlToValidate = value; }

        }

        protected virtual bool OnValidating(string value)
        {
            if (this.Validating != null)
            {
                bool valid = true;
                ValidatingEventArgs e = new ValidatingEventArgs(value, valid);
                this.Validating(this, e);
                return e.IsValid;
            }
            return true;
        }

        protected override bool EvaluateIsValid()
        {
            if (this.ControlToValidate == null ||
                string.IsNullOrEmpty(this.ControlToValidate.Text))
            {
                return true;
            }
            return this.OnValidating(this.ControlToValidate.Text);

        }
    }

    //public delegate void ValidatingEventHandler(object sender, ValidatingEventArgs e);

    public class ValidatingEventArgs : EventArgs
    {
        private readonly string value;
        private bool isValid;

        public ValidatingEventArgs(string value, bool isValid)
        {
            this.value = value;
            this.isValid = isValid;
        }

        public string Value
        {
            get { return value; }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
    }
}
