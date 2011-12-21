using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsToolkit.Controls.Validators;
using ComponentModel_TypeConverter=System.ComponentModel.TypeConverter;

namespace WindowsFormsToolkit.Controls.Validators
{
    [ProvideProperty("Validator", typeof(Control))]
    [ProvideProperty("ValidatorType", typeof(Control))]
    [ProvideProperty("LockFocusOnError", typeof(Control))]
    public partial class ValidatorExtender : Component, IExtenderProvider, ISupportInitialize
    {
        private readonly Dictionary<Control, ValidatorControl> validators;

        public ValidatorExtender()
        {
            validators = new Dictionary<Control, ValidatorControl>();
            InitializeComponent();
        }

        public ValidatorExtender(IContainer container)
        {
            validators = new Dictionary<Control, ValidatorControl>();
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// Gets the validator for control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public IValidator GetValidatorForControl(Control control) {
            var validator = from v in this.validators
                            where v.Key == control
                            select v.Value.Validator;

            return validator.FirstOrDefault();
        }

        /// <summary>
        /// Gets the validator comparing to control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public IEnumerable<IValidator> GetValidatorComparingToControl(Control control) {
            var validator = from v in this.validators
                            where v.Value.ValidatorType == ValidatorType.CompareToControlValidator
                                && (v.Value.Validator as CompareToControlValidator).ControlToCompare == control
                            select v.Value.Validator;
            return validator;
        }

        /// <summary>
        /// Gets all controls compare to.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public IEnumerable<Control> GetAllControlsCompareTo(Control control) {
            var controls = from v in this.validators
                            where v.Value.ValidatorType == ValidatorType.CompareToControlValidator
                                && (v.Value.Validator as CompareToControlValidator).ControlToCompare == control
                            select v.Key;
            return controls;
        }

        #region Properties
        #region ValidatorType property
        [DefaultValue(ValidatorType.None)]
        [Category("Validator")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Obtient ou d�fini le type de validator pour le contr�le s�lectionn�")]
        public ValidatorType GetValidatorType(Control c)
        {
            if (validators.ContainsKey(c))
            {
                return validators[c].ValidatorType;
            }
            else
            {
                return ValidatorType.None;
            }
        }

        public void SetValidatorType(Control c, ValidatorType type)
        {
            if (validators.ContainsKey(c) && type == validators[c].ValidatorType)
            {
                return;
            }

            if (!validators.ContainsKey(c))
            {
                ValidatorControl vc = new ValidatorControl();
                validators.Add(c, vc);
            }

            validators[c].ValidatorType = type;
            IValidator v = ValidatorExtender.CreateValidatorFromType(type);
            // si le type du validator est le m�me que celui que j'ajoute
            // on zap... ca permet d'�viter un bug sur le chargement du formulaire
            if (validators[c].Validator == null || 
                v == null || 
                !v.GetType().Equals(validators[c].Validator.GetType()))
            {
                this.SetValidator(c, v);
            }
        }

        #endregion

        #region Validator property
        [Category("Validator")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Affiche les propri�t�s du validateur pour le contr�le s�lectionn�")]
        public IValidator GetValidator(Control c)
        {
            if (validators.ContainsKey(c))
            {
                return validators[c].Validator;
            }
            return null;
        }

        public void SetValidator(Control c, IValidator validator)
        {
            //if (validators.ContainsKey(c))
            //{
            //    validators.Remove(c);
            //}

            //ValidatorControl vc = new ValidatorControl();
            //vc.validator = validator;

            //validators.Add(c, vc);
            if (!validators.ContainsKey(c))
            {
                ValidatorControl vc = new ValidatorControl();
                validators.Add(c, vc);
            }

            //IValidator v = new RequiredFieldValidator();
            if (validator != null)
            {
                Type t = validator.GetType();
                PropertyInfo pi = t.GetProperty("ControlToValidate") ?? t.GetProperty("ControlToCompare");
                if (pi != null)
                {
                    pi.SetValue(validator, c, null);
                }
            }

            validators[c].Validator = validator;

            //c.CausesValidation = true;
            //c.Validating += new CancelEventHandler(c_Validating);

            //ValidatorControl vc2 = new ValidatorControl();
            //vc2.validator = v;
            //validators.Add(c, vc2);

        }

        protected void c_Validating(object sender, CancelEventArgs e)
        {
            ValidatorControl vc = this.validators[(Control) sender];
            IValidator v = this.validators[(Control)sender].Validator;

            if (v == null)
            {
                e.Cancel = false;
                return;
            }

            v.Validate();
            if (v.IsValid)
            {
                this.errorProvider1.SetError((Control) sender, string.Empty);
            }
            else
            {
                this.errorProvider1.SetError((Control) sender, v.ErrorMessage);
            }

            e.Cancel = vc.LockFocusOnError && !v.IsValid;

        }
        #endregion

        #region LockFocusOnError property
        [DefaultValue(false)]
        [Description("Obtient ou d�fini une valeur indiant si en cas de non validation le focus reste bloqu� sur le contr�le courant")]
        [Category("Validator")]
        public bool GetLockFocusOnError(Control c)
        {
            if (validators.ContainsKey(c))
            {
                return validators[c].LockFocusOnError;
            }
            return false;   
        }

        public void SetLockFocusOnError(Control c, bool lockFocusOnError)
        {
            if (validators.ContainsKey(c))
            {
                validators[c].LockFocusOnError = lockFocusOnError;
            }
        }

        #endregion
        #endregion

        #region Private methods
        private static IValidator CreateValidatorFromType(ValidatorType type)
        {
            switch(type)
            {
                default:
                case ValidatorType.None:
                    return null;
                case ValidatorType.RequiredFieldValidator:
                    return new RequiredFieldValidator();
                case ValidatorType.RegularExpressionValidator:
                    return new RegularExpressionValidator();
                case ValidatorType.RangeValidator:
                    return new RangeValidator();
                case ValidatorType.CustomValidator:
                    return new CustomValidator();
                case ValidatorType.CompareValidator:
                    return new CompareValidator();
                case ValidatorType.CompareToControlValidator:
                    return new CompareToControlValidator();
            }
        }

        #endregion

        #region 
        public Dictionary<Control, ValidatorControl> GetValidators()
        {
            return this.validators;
        }

        #endregion

        #region IExtenderProvider Members
        public bool CanExtend(object extendee)
        {
            return extendee is TextBox
                   || extendee is ComboBox;
        }
        #endregion

        #region ISupportInitialize Members

        ///<summary>
        ///Signals the object that initialization is starting.
        ///</summary>
        ///
        public void BeginInit()
        {
            //throw new NotImplementedException();
        }

        ///<summary>
        ///Signals the object that initialization is complete.
        ///</summary>
        public void EndInit()
        {
            //throw new NotImplementedException();

            if (validators.Count > 0)
            {
                foreach(Control c in validators.Keys)
                {
                    c.CausesValidation = true;
                    c.Validating += new CancelEventHandler(c_Validating);

                    if (validators[c].ValidatorType == ValidatorType.CompareToControlValidator) {
                        var controlToCompare = (validators[c].Validator as CompareToControlValidator).ControlToCompare;

                        if (controlToCompare != null)
                        {
                            controlToCompare.CausesValidation = true;
                            controlToCompare.Validating += (s,e) => {
                                var controls = this.GetAllControlsCompareTo(s as Control);
                                if (controls.Any()) {
                                    controls.ToList().ForEach(ctrl => c_Validating(ctrl, new CancelEventArgs()));
                                }
                                //var _validators = this.GetValidatorComparingToControl(s as Control);
                                //if (_validators.Any()) {
                                //    _validators.ToList().ForEach(v => v.Validate());                                    
                                //}
                            };
                        }
                    }
                }
            }
        }

        #endregion

        #region ValidatorControl nested class
        public class ValidatorControl
        {
            private Control control;
            private IValidator validator;
            private bool lockFocusOnError = false;
            private ValidatorType type = ValidatorType.None;

            internal Control Control
            {
                get { return control; }
                set
                {
                    control = value;
                }
            }

            public IValidator Validator
            {
                get { return validator; }
                set
                {
                    validator = value;
                }
            }

            /// <summary>
            /// Obtient ou d�fini une valeur indiant si en cas de non validation 
            /// le focus reste bloqu� sur le contr�le courant
            /// </summary>
            [Description("Obtient ou d�fini une valeur indiant si en cas de non validation le focus reste bloqu� sur le contr�le courant")]
            public bool LockFocusOnError
            {
                get { return lockFocusOnError; }
                set
                {
                    lockFocusOnError = value;
                }
            }

            public ValidatorType ValidatorType
            {
                get { return type; }
                set
                {
                    type = value;
                }
            }
        }
        #endregion
    }
}
