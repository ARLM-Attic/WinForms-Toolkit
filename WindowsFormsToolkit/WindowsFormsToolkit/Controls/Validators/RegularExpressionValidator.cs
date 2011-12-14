using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// Vérifie que la valeur saisie dans le contrôle correspondant bien au pattern fournit
    /// </summary>
    [Description("Vérifie que la valeur saisie dans le contrôle correspondant bien au pattern fournit")]
    public class RegularExpressionValidator : BaseValidator
    {
        private string pattern;
        private Control controlToValidate;
        [Description("Obtient ou défini le contrôle à valider")]
        public virtual Control ControlToValidate
        {
            get { return this.controlToValidate; }
            set { this.controlToValidate = value; }

        }

        [Description("Obtient ou défini l'expression qui servira à évaluer le contenu du contrôle")]
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
