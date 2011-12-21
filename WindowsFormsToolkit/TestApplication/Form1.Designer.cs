namespace TestApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            WindowsFormsToolkit.Controls.Validators.CompareValidator compareValidator5 = new WindowsFormsToolkit.Controls.Validators.CompareValidator();
            WindowsFormsToolkit.Controls.Validators.CompareToControlValidator compareToControlValidator3 = new WindowsFormsToolkit.Controls.Validators.CompareToControlValidator();
            WindowsFormsToolkit.Controls.Validators.RegularExpressionValidator regularExpressionValidator3 = new WindowsFormsToolkit.Controls.Validators.RegularExpressionValidator();
            WindowsFormsToolkit.Controls.Validators.CompareValidator compareValidator6 = new WindowsFormsToolkit.Controls.Validators.CompareValidator();
            WindowsFormsToolkit.Controls.Validators.RequiredFieldValidator requiredFieldValidator3 = new WindowsFormsToolkit.Controls.Validators.RequiredFieldValidator();
            WindowsFormsToolkit.Controls.Validators.RangeValidator rangeValidator3 = new WindowsFormsToolkit.Controls.Validators.RangeValidator();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.firstChoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondChoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new WindowsFormsToolkit.Controls.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.colorPalette1 = new WindowsFormsToolkit.Controls.ColorPalette();
            this.splitButton1 = new WindowsFormsToolkit.Controls.SplitButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.balloonTipExtender1 = new WindowsFormsToolkit.Controls.BalloonTipExtender();
            this.cueTextExtender1 = new WindowsFormsToolkit.Controls.CueTextExtender(this.components);
            this.validatorExtender1 = new WindowsFormsToolkit.Controls.Validators.ValidatorExtender(this.components);
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.validatorExtender1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox6
            // 
            this.cueTextExtender1.SetCueText(this.textBox6, "this !");
            this.textBox6.Location = new System.Drawing.Point(320, 161);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(82, 20);
            this.textBox6.TabIndex = 5;
            this.balloonTipExtender1.SetText(this.textBox6, null);
            this.balloonTipExtender1.SetTitle(this.textBox6, null);
            compareValidator5.ControlToCompare = this.textBox6;
            compareValidator5.ErrorMessage = "";
            compareValidator5.ValueToCompare = null;
            this.validatorExtender1.SetValidator(this.textBox6, compareValidator5);
            this.validatorExtender1.SetValidatorType(this.textBox6, WindowsFormsToolkit.Controls.Validators.ValidatorType.CompareValidator);
            // 
            // textBox5
            // 
            this.cueTextExtender1.SetCueText(this.textBox5, "This value ");
            this.textBox5.Location = new System.Drawing.Point(98, 161);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(105, 20);
            this.textBox5.TabIndex = 3;
            this.balloonTipExtender1.SetText(this.textBox5, null);
            this.balloonTipExtender1.SetTitle(this.textBox5, null);
            compareToControlValidator3.ControlToCompare = this.textBox6;
            compareToControlValidator3.ControlToValidate = this.textBox5;
            compareToControlValidator3.ErrorMessage = "This value must be greater than the other value !";
            compareToControlValidator3.Type = WindowsFormsToolkit.Controls.Validators.ValidationDataType.Integer;
            compareToControlValidator3.ValidationCompareOperator = WindowsFormsToolkit.Controls.Validators.ValidationCompareOperator.GreaterThan;
            this.validatorExtender1.SetValidator(this.textBox5, compareToControlValidator3);
            this.validatorExtender1.SetValidatorType(this.textBox5, WindowsFormsToolkit.Controls.Validators.ValidatorType.CompareToControlValidator);
            // 
            // textBox2
            // 
            this.cueTextExtender1.SetCueText(this.textBox2, "This TextBox can contain numbers !");
            this.textBox2.Location = new System.Drawing.Point(98, 109);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(304, 20);
            this.textBox2.TabIndex = 1;
            this.balloonTipExtender1.SetText(this.textBox2, null);
            this.balloonTipExtender1.SetTitle(this.textBox2, null);
            regularExpressionValidator3.ControlToValidate = this.textBox2;
            regularExpressionValidator3.ErrorMessage = "Please enter numbers !";
            regularExpressionValidator3.Pattern = "^\\d*$";
            this.validatorExtender1.SetValidator(this.textBox2, regularExpressionValidator3);
            this.validatorExtender1.SetValidatorType(this.textBox2, WindowsFormsToolkit.Controls.Validators.ValidatorType.RegularExpressionValidator);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // textBox4
            // 
            this.cueTextExtender1.SetCueText(this.textBox4, "Value must be less than 100 !");
            this.textBox4.Location = new System.Drawing.Point(98, 135);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(304, 20);
            this.textBox4.TabIndex = 2;
            this.balloonTipExtender1.SetText(this.textBox4, null);
            this.balloonTipExtender1.SetTitle(this.textBox4, null);
            compareValidator6.ControlToCompare = this.textBox4;
            compareValidator6.ErrorMessage = "Value must be less than 100 !";
            compareValidator6.Type = WindowsFormsToolkit.Controls.Validators.ValidationDataType.Integer;
            compareValidator6.ValidationCompareOperator = WindowsFormsToolkit.Controls.Validators.ValidationCompareOperator.LessThan;
            compareValidator6.ValueToCompare = "100";
            this.validatorExtender1.SetValidator(this.textBox4, compareValidator6);
            this.validatorExtender1.SetValidatorType(this.textBox4, WindowsFormsToolkit.Controls.Validators.ValidatorType.CompareValidator);
            // 
            // textBox3
            // 
            this.cueTextExtender1.SetCueText(this.textBox3, "You must enter some text in this textbox !");
            this.textBox3.Location = new System.Drawing.Point(98, 83);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(304, 20);
            this.textBox3.TabIndex = 0;
            this.balloonTipExtender1.SetText(this.textBox3, null);
            this.balloonTipExtender1.SetTitle(this.textBox3, null);
            requiredFieldValidator3.ControlToValidate = this.textBox3;
            requiredFieldValidator3.ErrorMessage = "This field is required !";
            this.validatorExtender1.SetValidator(this.textBox3, requiredFieldValidator3);
            this.validatorExtender1.SetValidatorType(this.textBox3, WindowsFormsToolkit.Controls.Validators.ValidatorType.RequiredFieldValidator);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstChoiceToolStripMenuItem,
            this.secondChoiceToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 48);
            // 
            // firstChoiceToolStripMenuItem
            // 
            this.firstChoiceToolStripMenuItem.Name = "firstChoiceToolStripMenuItem";
            this.firstChoiceToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.firstChoiceToolStripMenuItem.Text = "First Choice";
            // 
            // secondChoiceToolStripMenuItem
            // 
            this.secondChoiceToolStripMenuItem.Name = "secondChoiceToolStripMenuItem";
            this.secondChoiceToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.secondChoiceToolStripMenuItem.Text = "Second Choice ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 392);
            this.tabControl1.TabContextMenu = null;
            this.tabControl1.TabContextMenuStrip = null;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.colorPalette1);
            this.tabPage1.Controls.Add(this.splitButton1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Base controls";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // colorPalette1
            // 
            this.colorPalette1.AutoSize = false;
            this.colorPalette1.ColorButtonSize = new System.Drawing.Size(16, 16);
            this.colorPalette1.Colors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.White,
            System.Drawing.Color.Transparent,
            System.Drawing.Color.Snow,
            System.Drawing.Color.GhostWhite,
            System.Drawing.Color.MintCream,
            System.Drawing.Color.Ivory,
            System.Drawing.Color.FloralWhite,
            System.Drawing.Color.Honeydew,
            System.Drawing.Color.LavenderBlush,
            System.Drawing.Color.Azure,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.SeaShell,
            System.Drawing.Color.WhiteSmoke,
            System.Drawing.Color.OldLace,
            System.Drawing.Color.Linen,
            System.Drawing.Color.MistyRose,
            System.Drawing.Color.Lavender,
            System.Drawing.Color.LightYellow,
            System.Drawing.Color.LightCyan,
            System.Drawing.Color.Cornsilk,
            System.Drawing.Color.PapayaWhip,
            System.Drawing.Color.Beige,
            System.Drawing.Color.AntiqueWhite,
            System.Drawing.Color.BlanchedAlmond,
            System.Drawing.Color.LemonChiffon,
            System.Drawing.Color.LightGoldenrodYellow,
            System.Drawing.Color.Bisque,
            System.Drawing.Color.Pink,
            System.Drawing.Color.Gainsboro,
            System.Drawing.Color.PeachPuff,
            System.Drawing.Color.LightPink,
            System.Drawing.Color.Moccasin,
            System.Drawing.Color.NavajoWhite,
            System.Drawing.Color.Wheat,
            System.Drawing.Color.LightGray,
            System.Drawing.Color.PaleTurquoise,
            System.Drawing.Color.PaleGoldenrod,
            System.Drawing.Color.Thistle,
            System.Drawing.Color.PowderBlue,
            System.Drawing.Color.LightBlue,
            System.Drawing.Color.PaleGreen,
            System.Drawing.Color.LightSteelBlue,
            System.Drawing.Color.LightSkyBlue,
            System.Drawing.Color.Silver,
            System.Drawing.Color.LightGreen,
            System.Drawing.Color.Aquamarine,
            System.Drawing.Color.Plum,
            System.Drawing.Color.Khaki,
            System.Drawing.Color.LightSalmon,
            System.Drawing.Color.SkyBlue,
            System.Drawing.Color.LightCoral,
            System.Drawing.Color.Violet,
            System.Drawing.Color.Salmon,
            System.Drawing.Color.HotPink,
            System.Drawing.Color.BurlyWood,
            System.Drawing.Color.DarkSalmon,
            System.Drawing.Color.Tan,
            System.Drawing.Color.MediumSlateBlue,
            System.Drawing.Color.SandyBrown,
            System.Drawing.Color.DarkGray,
            System.Drawing.Color.CornflowerBlue,
            System.Drawing.Color.Coral,
            System.Drawing.Color.PaleVioletRed,
            System.Drawing.Color.RosyBrown,
            System.Drawing.Color.MediumPurple,
            System.Drawing.Color.Orchid,
            System.Drawing.Color.DarkSeaGreen,
            System.Drawing.Color.Tomato,
            System.Drawing.Color.MediumAquamarine,
            System.Drawing.Color.GreenYellow,
            System.Drawing.Color.IndianRed,
            System.Drawing.Color.DarkKhaki,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.SlateBlue,
            System.Drawing.Color.RoyalBlue,
            System.Drawing.Color.Turquoise,
            System.Drawing.Color.DodgerBlue,
            System.Drawing.Color.MediumTurquoise,
            System.Drawing.Color.DeepPink,
            System.Drawing.Color.LightSlateGray,
            System.Drawing.Color.BlueViolet,
            System.Drawing.Color.Peru,
            System.Drawing.Color.SlateGray,
            System.Drawing.Color.Gray,
            System.Drawing.Color.Chartreuse,
            System.Drawing.Color.CadetBlue,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Red,
            System.Drawing.Color.SpringGreen,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Orange,
            System.Drawing.Color.OrangeRed,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.Gold,
            System.Drawing.Color.Magenta,
            System.Drawing.Color.YellowGreen,
            System.Drawing.Color.LimeGreen,
            System.Drawing.Color.Lime,
            System.Drawing.Color.DeepSkyBlue,
            System.Drawing.Color.Fuchsia,
            System.Drawing.Color.Cyan,
            System.Drawing.Color.DarkOrange,
            System.Drawing.Color.DarkOrchid,
            System.Drawing.Color.LawnGreen,
            System.Drawing.Color.SteelBlue,
            System.Drawing.Color.Goldenrod,
            System.Drawing.Color.MediumSpringGreen,
            System.Drawing.Color.Crimson,
            System.Drawing.Color.Chocolate,
            System.Drawing.Color.MediumSeaGreen,
            System.Drawing.Color.MediumVioletRed,
            System.Drawing.Color.Firebrick,
            System.Drawing.Color.DarkViolet,
            System.Drawing.Color.LightSeaGreen,
            System.Drawing.Color.DimGray,
            System.Drawing.Color.DarkTurquoise,
            System.Drawing.Color.Brown,
            System.Drawing.Color.MediumBlue,
            System.Drawing.Color.Sienna,
            System.Drawing.Color.DarkSlateBlue,
            System.Drawing.Color.DarkGoldenrod,
            System.Drawing.Color.SeaGreen,
            System.Drawing.Color.OliveDrab,
            System.Drawing.Color.ForestGreen,
            System.Drawing.Color.SaddleBrown,
            System.Drawing.Color.DarkOliveGreen,
            System.Drawing.Color.DarkRed,
            System.Drawing.Color.DarkBlue,
            System.Drawing.Color.DarkMagenta,
            System.Drawing.Color.DarkCyan,
            System.Drawing.Color.MidnightBlue,
            System.Drawing.Color.Indigo,
            System.Drawing.Color.Maroon,
            System.Drawing.Color.Purple,
            System.Drawing.Color.Green,
            System.Drawing.Color.Teal,
            System.Drawing.Color.Navy,
            System.Drawing.Color.Olive,
            System.Drawing.Color.DarkSlateGray,
            System.Drawing.Color.DarkGreen,
            System.Drawing.Color.Black});
            this.colorPalette1.ColorSortOrder = WindowsFormsToolkit.Controls.ColorSortOrder.Brightness;
            this.colorPalette1.Location = new System.Drawing.Point(35, 133);
            this.colorPalette1.Name = "colorPalette1";
            this.colorPalette1.SelectedColor = System.Drawing.Color.Transparent;
            this.colorPalette1.Size = new System.Drawing.Size(354, 202);
            this.colorPalette1.TabIndex = 3;
            // 
            // splitButton1
            // 
            this.splitButton1.AutoSize = true;
            this.splitButton1.ContextMenuStrip = this.contextMenuStrip1;
            this.splitButton1.Location = new System.Drawing.Point(35, 81);
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.Size = new System.Drawing.Size(148, 46);
            this.splitButton1.TabIndex = 2;
            this.splitButton1.Text = "I\'m a SplitButton";
            this.splitButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.cueTextExtender1.SetCueText(this.comboBox1, "I\'m a ComboBox with Cue Text !");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(35, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(354, 21);
            this.comboBox1.TabIndex = 1;
            this.validatorExtender1.SetValidator(this.comboBox1, null);
            // 
            // textBox1
            // 
            this.balloonTipExtender1.SetAutoshowOnFocus(this.textBox1, true);
            this.cueTextExtender1.SetCueText(this.textBox1, "Click me to open balloon tip !");
            this.balloonTipExtender1.SetIcon(this.textBox1, WindowsFormsToolkit.Controls.BalloonTip.BalloonTipIcon.Info);
            this.textBox1.Location = new System.Drawing.Point(35, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(354, 20);
            this.textBox1.TabIndex = 0;
            this.balloonTipExtender1.SetText(this.textBox1, "Wow ! I\'m a balloon tip !");
            this.balloonTipExtender1.SetTitle(this.textBox1, "Windows Forms Toolkit");
            this.validatorExtender1.SetValidator(this.textBox1, null);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBox6);
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(596, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Validators";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "must be greater than";
            // 
            // textBox7
            // 
            this.cueTextExtender1.SetCueText(this.textBox7, "Value must be between 10 and 20");
            this.textBox7.Location = new System.Drawing.Point(98, 188);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(304, 20);
            this.textBox7.TabIndex = 6;
            this.balloonTipExtender1.SetText(this.textBox7, null);
            this.balloonTipExtender1.SetTitle(this.textBox7, null);
            rangeValidator3.ControlToCompare = this.textBox7;
            rangeValidator3.ErrorMessage = "Value must be between 10 and 20 !";
            rangeValidator3.MaximumValue = "10";
            rangeValidator3.MinimumValue = "20";
            rangeValidator3.Type = WindowsFormsToolkit.Controls.Validators.ValidationDataType.Integer;
            this.validatorExtender1.SetValidator(this.textBox7, rangeValidator3);
            this.validatorExtender1.SetValidatorType(this.textBox7, WindowsFormsToolkit.Controls.Validators.ValidatorType.RangeValidator);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 46);
            this.button1.TabIndex = 4;
            this.button1.Text = "Click me to show a notification form !";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 392);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.validatorExtender1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsFormsToolkit.Controls.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private WindowsFormsToolkit.Controls.BalloonTipExtender balloonTipExtender1;
        private WindowsFormsToolkit.Controls.CueTextExtender cueTextExtender1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBox1;
        private WindowsFormsToolkit.Controls.SplitButton splitButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem firstChoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondChoiceToolStripMenuItem;
        private WindowsFormsToolkit.Controls.ColorPalette colorPalette1;
        private WindowsFormsToolkit.Controls.Validators.ValidatorExtender validatorExtender1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button1;






    }
}

