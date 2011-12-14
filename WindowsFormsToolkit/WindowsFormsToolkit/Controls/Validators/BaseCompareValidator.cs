using System;
using System.ComponentModel;

namespace WindowsFormsToolkit.Controls.Validators
{
    public abstract class BaseCompareValidator : BaseValidator
    {
        private ValidationDataType type;

        [DefaultValue(ValidationDataType.String)]
        [Description("Obtient ou défini le type des données à comparer")]
        public ValidationDataType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public static bool CanConvert(string text, ValidationDataType type)
        {
            object obj = null;

            return Convert(text, type, out obj);
        }

        protected static bool Convert(string text, ValidationDataType type, out object outValue)
        {
            outValue = null;

            try
            {
                switch (type)
                {
                    case ValidationDataType.String:
                        outValue = text;
                        break;
                    case ValidationDataType.Integer:
                        int retInt;
                        if (Int32.TryParse(text, out retInt))
                        {
                            outValue = retInt;
                        }
                        break;
                    case ValidationDataType.Double:
                        double retDouble;
                        if (Double.TryParse(text, out retDouble))
                        {
                            outValue = retDouble;
                        }
                        break;
                    case ValidationDataType.Date:
                        DateTime retDate;
                        if (DateTime.TryParse(text, out retDate))
                        {
                            outValue = retDate;
                        }
                        break;
                    case ValidationDataType.Currency:
                        decimal retDecimal;
                        if (Decimal.TryParse(text, out retDecimal))
                        {
                            outValue = retDecimal;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch // (Exception ex)
            {
                outValue = null;
            }

            return outValue != null;
        }
    }

    public enum ValidationCompareOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanEqual,
        LessThan,
        LessThanEqual,
        DataTypeCheck
    }

    public enum ValidationDataType
    {
        String,
        Integer,
        Double,
        Date,
        Currency
    }
}