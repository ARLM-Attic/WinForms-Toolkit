using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

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

		public const int WM_COMMAND = 0x111;
		public const int WM_PAINT = 0xF;
		public const int WM_NC_PAINT = 0x85;
		public const int WM_SETFOCUS = 0x7;
		public const int WM_KILLFOCUS = 0x8;
		public const int WM_MOUSEACTIVATE = 0x21;
		public const int WM_MOUSEMOVE = 0x200;
		public const int WM_ERASEBKGND = 0x14;
		public const int WM_NC_HITTEST = 0x84;
		public const int WM_PRINTCLIENT = 0x318;

		public const int CBN_DROPDOWN = 7;
		public const int CBN_CLOSEUP = 8;
		public const int CB_GETDROPPEDSTATE = 0x157;

		public const int GWL_EXSTYLE = (-20);
		public const int WS_EX_RIGHT = 0x1000;
		public const int WS_EX_LEFTSCROLLBAR = 0x4000;

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

		[DllImport("user32")]
		public static extern int GetWindowRect(
			IntPtr hWnd,
			ref RECT lpRect);
		[DllImport("user32")]
		public static extern int GetClientRect(
			IntPtr hWnd,
			ref RECT lpRect);

		[DllImport("user32")]
		public static extern IntPtr GetDC(
			IntPtr hWnd);
		[DllImport("user32")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32")]
		public static extern int ReleaseDC(
			IntPtr hWnd,
			IntPtr hdc);

		[DllImport("user32")]
		public extern static IntPtr GetFocus();

		[DllImport("user32", CharSet = CharSet.Auto)]
		public extern static int SendMessage(
			IntPtr hWnd,
			int wMsg,
			IntPtr wParam,
			IntPtr lParam);

		[DllImport("user32")]
		public static extern int IsWindowEnabled(
			IntPtr hWnd);

		[DllImport("user32", CharSet = CharSet.Auto)]
		public static extern int GetWindowLong(
			IntPtr hWnd,
			int nIndex);

		[DllImport("user32", CharSet = CharSet.Unicode)]
		public static extern int SetWindowTheme(
			IntPtr hWnd,
			[MarshalAs(UnmanagedType.LPWStr)]
			String pszSubAppName,
			[MarshalAs(UnmanagedType.LPWStr)]
			String pszSubIdList);

		[DllImport("user32")]
		public static extern IntPtr GetParent(
			IntPtr hWnd);


		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
			public Rectangle ToRectangle()
			{
				//				return new Rectangle(left, top, right - left, bottom - top);
				return new Rectangle(0, 0, right - left, bottom - top);
			}
		}


		#endregion
	}
}
