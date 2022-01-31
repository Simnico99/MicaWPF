namespace MicaWPF.Helpers;

public class PInvokeHelper
{
    public class ParameterTypes
    {

        [Flags]
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        };
    }

    public static class Methods
    {
        [DllImport("DwmApi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref ParameterTypes.MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, ParameterTypes.DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

        public static int ExtendFrame(IntPtr hwnd, ParameterTypes.MARGINS margins)
            => DwmExtendFrameIntoClientArea(hwnd, ref margins);

        public static int SetWindowAttribute(IntPtr hwnd, ParameterTypes.DWMWINDOWATTRIBUTE attribute, int parameter)
            => DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }
}