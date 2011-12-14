using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// V�rifie que la valeur saisie est born�e
    /// </summary>
    [Description("V�rifie que la valeur saisie est born�e")]
    public class RangeValidator : BaseCompareValidator
    {
        private Control controlToCompare;
        private string maximumValue;
        private string minimumValue;

        protected override bool EvaluateIsValid()
        {
            // v�rification des valeurs
            object maxObj;
            object minObj;
            object valueObj;

            if (!Convert(this.MaximumValue, base.Type, out maxObj) ||
               !Convert(this.MinimumValue, base.Type, out minObj) ||
               !Convert(this.controlToCompare.Text, base.Type, out valueObj))
            {
                return false;
            }

            switch (base.Type)
            {
                case ValidationDataType.String:
                    return ((string) valueObj).CompareTo((string) minObj) >= 0 &&
                           ((string) valueObj).CompareTo((string) maxObj) <= 0;
                case ValidationDataType.Integer:
                    return ((int)valueObj).CompareTo((int)minObj) >= 0 &&
                           ((int)valueObj).CompareTo((int)maxObj) <= 0;
                case ValidationDataType.Double:
                    return ((double)valueObj).CompareTo((double)minObj) >= 0 &&
                           ((double)valueObj).CompareTo((double)maxObj) <= 0;
                case ValidationDataType.Date:
                    return ((DateTime)valueObj).CompareTo((DateTime)minObj) >= 0 &&
                           ((DateTime)valueObj).CompareTo((DateTime)maxObj) <= 0;
                case ValidationDataType.Currency:
                    return ((decimal)valueObj).CompareTo((decimal)minObj) >= 0 &&
                           ((decimal)valueObj).CompareTo((decimal)maxObj) <= 0;
                default:
                    return false;
            }
        }

        [Description("Obtient ou d�fini le contr�le � valider")]
        public Control ControlToCompare
        {
            get { return this.controlToCompare; }
            set { this.controlToCompare = value; }
        }

        [Description("Obtient ou d�fini la valeur maximale")]
        public string MaximumValue
        {
            get { return maximumValue; }
            set { maximumValue = value; }
        }

        [Description("Obtient ou d�fini la valeur minimale")]
        public string MinimumValue
        {
            get { return minimumValue; }
            set { minimumValue = value; }
        }
    }
}
