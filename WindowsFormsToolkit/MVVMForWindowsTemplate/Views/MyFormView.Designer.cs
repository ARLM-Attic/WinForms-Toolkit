using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsToolkit.MVVM;
using WindowsFormsToolkit.EventAggregator;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Views
{
    partial class MyFormView
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
            this.myPropertyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // myPropertyTextBox
            // 
            this.myPropertyTextBox.Location = new System.Drawing.Point(55, 56);
            this.myPropertyTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.myPropertyTextBox.Name = "myPropertyTextBox";
            this.myPropertyTextBox.Size = new System.Drawing.Size(308, 25);
            this.myPropertyTextBox.TabIndex = 1;
            // 
            // MyFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 247);
            this.Controls.Add(this.myPropertyTextBox);
            this.Font = new System.Drawing.Font("Segoe WP", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MyFormView";
            this.Text = "MVVMForWindows9";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox myPropertyTextBox;
    }
}
