using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsToolkit.CommandManager;
using System.Windows.Forms;

namespace TestApplication
{
    public class MyViewModel
    {
        /// <summary>
        /// Gets the ShowNotification command.
        /// </summary>
        public ICommand ShowNotification {
            get {
                return new ActionCommand("Show notification form !", () => true, () => {
                    var notificationForm = new MyNotification();
                    notificationForm.AutoHide = true;
                    notificationForm.AutoHideTime = 5;
                    notificationForm.Left = Screen.PrimaryScreen.WorkingArea.Width - notificationForm.Width - 15;
                    notificationForm.Top = Screen.PrimaryScreen.WorkingArea.Height - notificationForm.Height - 15;

                    notificationForm.Show();
                });
            }
        }
    }
}
