using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using BLEM.MouseKeyboard.Enums;
using BLEM.MouseKeyboard.Models;

namespace BLEM.MouseKeyboard.Views
{
    public partial class MainWindow
    {
        private Setting _setting = new();
        private SerialPort _arduinoPort;

        private static IntPtr _keyHookID = IntPtr.Zero;
        private static IntPtr _mouseHookID = IntPtr.Zero;

        private static LowLevelKeyboardProc _keyboardProc = KeyboardHookCallback;
        private static LowLevelMouseProc _mouseProc = MouseHookCallback;

        private static bool interceptKeys = false;
        private static MainWindow? mainWindow;

        private static IKeyboardEvents? globalHook;

        private static POINT lastPoint, prePoint;

        private static Keys[] keysPressing = {Keys.None, Keys.None, Keys.None, Keys.None, Keys.None};

        private static bool isCapslockOn = false;

        private static bool isAltPressing = false;
        private static bool isShiftPressing = false;
        private static bool isCtrlPressing = false;
        private static bool isCapslockPressing = false;

        private string modifier = string.Empty; // Xác định tổ hợp phím
        private static List<Keys> modifiers = new()
        {
            Keys.LShiftKey, Keys.RShiftKey,
            Keys.LMenu, Keys.RMenu,
            Keys.LControlKey, Keys.RControlKey,
            Keys.Escape, Keys.Back, Keys.Home, Keys.Left, Keys.Up, Keys.Right, Keys.Down,
            Keys.End, Keys.Space, Keys.Capital, Keys.CapsLock, Keys.BrowserHome,
            Keys.BrowserBack, Keys.BrowserForward
        };

    }
}
