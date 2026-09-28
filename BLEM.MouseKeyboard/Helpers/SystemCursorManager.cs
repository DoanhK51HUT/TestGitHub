using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLEM.MouseKeyboard.Helpers
{
    internal class SystemCursorManager
    {
        private const uint OCR_NORMAL = 32512;
        private const uint OCR_IBEAM = 32513;
        private const uint OCR_WAIT = 32514;
        private const uint OCR_CROSS = 32515;
        private const uint OCR_UPARROW = 32516;

        private const uint OCR_SIZE = 32640;
        private const uint OCR_SIZENWSE = 32642;
        private const uint OCR_SIZENESW = 32643;
        private const uint OCR_SIZEWE = 32644;
        private const uint OCR_SIZENS = 32645;
        private const uint OCR_SIZEALL = 32646;

        private const uint OCR_NO = 32648;
        private const uint OCR_HAND = 32649;
        private const uint OCR_APPSTARTING = 32650;

        private const uint SPI_SETCURSORS = 0x0057;

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        public static void ResetCursorLocation()
        {
            SetCursorPos(0, 0);
        }

        public static void SetCursorLocation(int X, int Y)
        {
            SetCursorPos(X, Y);
        }

        public static void ReplaceAllCursors(string emptyCursorPath)
        {
            IntPtr hCursor = LoadCursorFromFile(emptyCursorPath);
            if (hCursor == IntPtr.Zero)
            {
                throw new Exception("Failed to load cursor from file.");
            }
            uint[] cursorIds = new uint[]
            {
                OCR_NORMAL, OCR_IBEAM, OCR_WAIT, OCR_CROSS, OCR_UPARROW,
                OCR_SIZE, OCR_SIZENWSE, OCR_SIZENESW, OCR_SIZEWE, OCR_SIZENS, OCR_SIZEALL,
                OCR_NO, OCR_HAND, OCR_APPSTARTING
            };
            foreach (uint id in cursorIds)
            {
                if (!SetSystemCursor(hCursor, id))
                {
                    throw new Exception($"Failed to set system cursor for ID {id}.");
                }
            }
        }

        public static void RestoreDefaultCursors()
        {
            if (!SystemParametersInfo(SPI_SETCURSORS, 0, IntPtr.Zero, 0))
            {
                throw new Exception("Failed to restore default cursors.");
            }

        }
    }
}
