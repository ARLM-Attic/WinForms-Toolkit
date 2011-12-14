using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// V�rifie que la valeur saisie dans le contr�le correspondant bien au pattern fournit
    /// </summary>
    [Description("V�rifie que la valeur saisie dans le contr�le correspondant bien au pattern fournit")]
    public class RegularExpressionValidator : BaseValidator
    {
        private string pattern;
        private Control controlToValidate;
        [Description("Obtient ou d�fini le contr�le � valider")]
        public virtual Control ControlToValidate
        {
            get { return this.controlToValidate; }
            set { this.controlToValidate = value; }

        }

        [Description("Obtient ou d�fini l'expression qui servira � �valuer le contenu du contr�le")]
        public string Pattern
        {
            get { return this.pattern; }
            set { this.pattern = value; }
        }

        protected override bool EvaluateIsValid()
        {
            return Regex.IsMatch(this.ControlToValidate.Text, this.pattern, RegexOptions.Compiled);
        }
    }
}
