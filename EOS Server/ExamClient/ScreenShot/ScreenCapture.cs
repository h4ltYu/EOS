using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ScreenShot
{
    public class ScreenCapture
    {
        public Image CaptureScreen()
        {
            return this.CaptureWindow(ScreenCapture.User32.GetDesktopWindow());
        }

        public Image CaptureWindow(IntPtr handle)
        {
            IntPtr windowDC = ScreenCapture.User32.GetWindowDC(handle);
            ScreenCapture.User32.RECT rect = default(ScreenCapture.User32.RECT);
            ScreenCapture.User32.GetWindowRect(handle, ref rect);
            int nWidth = rect.right - rect.left;
            int nHeight = rect.bottom - rect.top;
            IntPtr intPtr = ScreenCapture.GDI32.CreateCompatibleDC(windowDC);
            IntPtr intPtr2 = ScreenCapture.GDI32.CreateCompatibleBitmap(windowDC, nWidth, nHeight);
            IntPtr hObject = ScreenCapture.GDI32.SelectObject(intPtr, intPtr2);
            ScreenCapture.GDI32.BitBlt(intPtr, 0, 0, nWidth, nHeight, windowDC, 0, 0, 13369376);
            ScreenCapture.GDI32.SelectObject(intPtr, hObject);
            ScreenCapture.GDI32.DeleteDC(intPtr);
            ScreenCapture.User32.ReleaseDC(handle, windowDC);
            Image result = Image.FromHbitmap(intPtr2);
            ScreenCapture.GDI32.DeleteObject(intPtr2);
            return result;
        }

        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image image = this.CaptureWindow(handle);
            image.Save(filename, format);
        }

        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image image = this.CaptureScreen();
            image.Save(filename, format);
        }

        private class GDI32
        {
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

            public const int SRCCOPY = 13369376;
        }

        private class User32
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref ScreenCapture.User32.RECT rect);

            public struct RECT
            {
                public int left;

                public int top;

                public int right;

                public int bottom;
            }
        }
    }
}
