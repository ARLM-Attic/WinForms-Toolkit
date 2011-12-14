using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    public class RequiredFieldValidor : BaseValidator
    {
        private Control controlToValidate;
        public virtual Control ControlToValidate
        {
            get { return this.controlToValidate; }
            set { this.controlToValidate = value; }

        }

        protected override bool EvaluateIsValid()
        {
            return !string.IsNullOrEmpty(this.ControlToValidate.Text.Trim());
        }
    }
}
