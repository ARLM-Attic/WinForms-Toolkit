using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsToolkit.Data;
using WindowsFormsToolkit.Threading;
using WindowsFormsToolkit.Extensions;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        private HotKeysManager hkManager;
        public Form1()
        {
            InitializeComponent();

            hkManager = new HotKeysManager(this);
            hkManager.HotKeyPress += new EventHandler<HotKeysEventArgs>(hkManager_HotKeyPress);
            hkManager.AddHotKey(Keys.F, HotKeyModifiers.Windows | HotKeyModifiers.Alt);
        }

        protected void hkManager_HotKeyPress(object sender, HotKeysEventArgs e)
        {
            MessageBox.Show(string.Format("Key: {0}\r\nModifiers: {1}", e.Key, e.Modifiers));
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var notificationForm = new MyNotification();
            notificationForm.AutoHide = true;
            notificationForm.AutoHideTime = 5;
            notificationForm.Left = Screen.PrimaryScreen.WorkingArea.Width - notificationForm.Width - 15;
            notificationForm.Top = Screen.PrimaryScreen.WorkingArea.Height - notificationForm.Height - 15;

            

            notificationForm.Show();
        }
    }
}
