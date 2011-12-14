using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsFormsToolkit.Internal
{
    internal static class PInvoke
    {
        #region P/Invoke
        [StructLayout(LayoutKind.Sequential)]
        public struct EditBalloonTip
        {
            public int cbStruct;
            public IntPtr pszTitle;
            public IntPtr pszText;
            public WindowsFormsToolkit.Controls.BalloonTip.BalloonTipIcon ttiIcon;
        }

        public const int ECM_FIRST = 0x1500;
        public const int EM_SHOWBALLOONTIP = (ECM_FIRST + 3);
        public const int EM_HIDEBALLOONTIP = (ECM_FIRST + 4);

        public const uint EM_SETCUEBANNER = ECM_FIRST + 1;
        public const uint EM_GETCUEBANNER = ECM_FIRST + 2;
        public const uint GW_CHILD = 5;


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
        #endregion
    }
}
