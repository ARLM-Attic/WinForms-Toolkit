using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// Vérifie qu'un champ obligatoire est bien saisie
    /// </summary>
    [Description("Vérifie qu'un champ obligatoire est bien saisie")]
    public class RequiredFieldValidator : BaseValidator
    {
        private Control controlToValidate;
        [Description("Obtient ou défini le contrôle à valider")]
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
