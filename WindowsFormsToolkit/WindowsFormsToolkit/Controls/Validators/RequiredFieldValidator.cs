using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// V�rifie qu'un champ obligatoire est bien saisie
    /// </summary>
    [Description("V�rifie qu'un champ obligatoire est bien saisie")]
    public class RequiredFieldValidator : BaseValidator
    {
        private Control controlToValidate;
        [Description("Obtient ou d�fini le contr�le � valider")]
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
