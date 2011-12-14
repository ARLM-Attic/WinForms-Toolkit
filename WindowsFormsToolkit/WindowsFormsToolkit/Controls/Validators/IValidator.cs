using System.ComponentModel;

namespace WindowsFormsToolkit.Controls.Validators
{
    public interface IValidator
    {
        /// <summary>
        /// Validate this.
        /// </summary>
        void Validate();

        /// <summary>
        /// Get or Set the message that show are an error occurs.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Get a value that indicates if validator is good
        /// </summary>
        bool IsValid { get; }
    }
}
