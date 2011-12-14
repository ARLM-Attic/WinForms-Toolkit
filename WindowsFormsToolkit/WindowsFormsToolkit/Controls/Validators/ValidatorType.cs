using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsToolkit.Controls.Validators
{
    public enum ValidatorType
    {
        None,
        RequiredFieldValidator,
        RegularExpressionValidator,
        CompareValidator,
        CompareToControlValidator,
        CustomValidator,
        RangeValidator
    }
}
