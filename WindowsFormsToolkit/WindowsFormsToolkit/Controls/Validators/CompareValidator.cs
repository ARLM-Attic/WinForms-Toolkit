using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls.Validators
{
    /// <summary>
    /// Compare la valeur du contr�le � une valeur pr�d�finie
    /// </summary>
    [Description("Compare la valeur du contr�le � une valeur pr�d�finie")]
    public class CompareValidator : BaseCompareValidator
    {
        private Control controlToCompare;
        private ValidationCompareOperator validationCompareOperator;
        private string valueToCompare;

        [Description("Obtient ou d�fini le contr�le � valider")]
        public Control ControlToCompare
        {
            get { return controlToCompare; }
            set { controlToCompare = value; }
        }

        [Description("Obtient ou d�fini la valeur � comparer avec le contenu du contr�le")]
        public string ValueToCompare
        {
            get { return valueToCompare; }
            set { valueToCompare = value; }
        }

        [DefaultValue(Validators.ValidationCompareOperator.Equal)]
        [Description("Obtient ou d�fini le type d'op�rateur de comparaison entre les valeurs")]
        public ValidationCompareOperator ValidationCompareOperator
        {
            get { return validationCompareOperator; }
            set { validationCompareOperator = value; }
        }

        protected override bool EvaluateIsValid()
        {
            object obj1;

            if (!Convert(this.ControlToCompare.Text, base.Type, out obj1))
            {
                return false;
            }

            if (this.ValidationCompareOperator != Validators.ValidationCompareOperator.DataTypeCheck)
            {
                object obj2;
                if (!Convert(this.ValueToCompare, base.Type, out obj2))
                {
                    return true;
                }

                int num;
                switch(base.Type)
                {
                    case ValidationDataType.String:
                        num = string.Compare((string) obj1, (string) obj2);
                        break;
                    case ValidationDataType.Integer:
                        num = ((int) obj1).CompareTo((int) obj2);
                        break;
                    case ValidationDataType.Double:
                        num = ((double) obj1).CompareTo((double) obj2);
                        break;
                    case ValidationDataType.Date:
                        num = ((DateTime) obj1).CompareTo((DateTime) obj2);
                        break;
                    case ValidationDataType.Currency:
                        num = ((Decimal) obj1).CompareTo((decimal) obj2);
                        break;
                    default:
                        return true;
                }

                switch (this.ValidationCompareOperator)
                {
                    case Validators.ValidationCompareOperator.Equal:
                        return num == 0;
                    case Validators.ValidationCompareOperator.GreaterThan:
                        return num > 0;
                    case Validators.ValidationCompareOperator.GreaterThanEqual:
                        return num >= 0;
                    case Validators.ValidationCompareOperator.NotEqual:
                        return num != 0;
                    case Validators.ValidationCompareOperator.LessThanEqual:
                        return num <= 0;
                    case Validators.ValidationCompareOperator.LessThan:
                        return num < 0;
                }
            }
            return true;
        }
    }
}