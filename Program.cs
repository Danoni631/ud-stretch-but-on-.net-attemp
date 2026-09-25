using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    private const double M_PI = 3.14159265358979323846264338327950288;
    private const int PATINVERT = 0x005F0069;
    private const int SRCERASE = 0x0045003A;
    private const int SRCCOPY = 0x00CC0020;

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateSolidBrush(uint crColor);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    [DllImport("gdi32.dll")]
    private static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, uint dwRop);

    static void Main()
    {
        int w = GetSystemMetrics(0);
        int h = GetSystemMetrics(1);

        IntPtr hdc = GetDC(IntPtr.Zero);
        IntPtr dcCopy = CreateCompatibleDC(hdc);

        float radius = 0.0f;
        double angle = 0;

        while (true)
        {
            hdc = GetDC(IntPtr.Zero);

            int x = (int)(Math.Cos(angle) * radius);
            int y = (int)(Math.Sin(angle) * radius);

            StretchBlt(hdc, x, y, w - x * 2, h - y * 2, hdc, 0, 0, w, h, SRCCOPY);
            radius += 0.1f;

            Thread.Sleep(1);

            ReleaseDC(IntPtr.Zero, hdc);

            angle = (M_PI + (angle + M_PI / radius) % (M_PI * radius)) / 1.001;
        }
    }
}