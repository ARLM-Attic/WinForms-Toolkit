using System.ComponentModel;

namespace WindowsFormsToolkit.Controls.Validators
{
    public abstract class BaseValidator : IValidator
    {
        private string errorMessage;
        private bool isValid;
        private bool enabled = true;

        protected BaseValidator()
        {
            this.isValid = true;
            this.errorMessage = string.Empty;
        }

        [DefaultValue(true)]
        [Description("Obtient ou défini une valeur indiquant si le validateur est activé")]
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }


        #region IValidator Members

        /// <summary>
        /// Validate this.
        /// </summary>
        public void Validate()
        {
            if (!this.enabled)
            {
                this.isValid = true;
            } else
            {
                this.isValid = this.EvaluateIsValid();
            }
        }

        /// <summary>
        /// Get or Set the message that show are an error occurs.
        /// </summary>
        [Description("Obtient ou défini le message à afficher en cas d'erreur")]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        /// <summary>
        /// Get a value that indicates if validator is good
        /// </summary>
        [Description("Obtient une valeur indiquant si le contenu du contrôle est valide")]
        public bool IsValid
        {
            get { return isValid; }
        }

        #endregion

        protected abstract bool EvaluateIsValid();

    }
}
