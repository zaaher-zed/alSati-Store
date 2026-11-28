
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace alSati_store
{
    public class HiddenScrollFlowLayoutPanel : FlowLayoutPanel
    {
        private const int SB_BOTH = 3;

        private const int WM_SIZE = 0x0005;
        private const int WM_SHOWWINDOW = 0x0018;
        private const int WM_VSCROLL = 0x0115;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_MOUSEWHEEL = 0x020A;

        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(
            IntPtr hWnd,
            int wBar,
            bool bShow);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (IsHandleCreated &&
                (m.Msg == WM_SIZE ||
                 m.Msg == WM_SHOWWINDOW ||
                 m.Msg == WM_VSCROLL ||
                 m.Msg == WM_HSCROLL ||
                 m.Msg == WM_MOUSEWHEEL))
            {
                HideScrollBars(Handle);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            HideScrollBars(Handle);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            HideScrollBars(Handle);
        }

        public static void HideScrollBars(Control control)
        {
            if (control == null)
                return;

            bool preserveAutoScroll =
                control is UserControl userControl &&
                userControl.AutoScroll;

            if (control.IsHandleCreated && !preserveAutoScroll)
                HideScrollBars(control.Handle);

            foreach (Control child in control.Controls)
                HideScrollBars(child);
        }

        private static void HideScrollBars(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return;

            ShowScrollBar(handle, SB_BOTH, false);
        }
    }
}
