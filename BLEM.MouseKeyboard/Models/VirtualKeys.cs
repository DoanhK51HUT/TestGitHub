using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEM.MouseKeyboard.Models
{
    internal class VirtualKeys
    {
        /*
         * Define virtual keys
         */
        // Normal keys
        public const int VK_SHIFT = 0x10;
        public const int vk_LSHIFT = 0xA0;
        public const int vk_RSHIFT = 0xA1;

        public const int VK_CONTROL = 0x11;
        public const int vk_LCONTROL = 0xA2;
        public const int vk_RCONTROL = 0xA3;

        public const int VK_MENU = 0x12;
        public const int vk_LMENU = 0xA4;
        public const int vk_RMENU = 0xA5;

        // Control keys
        public const int VK_CAPITAL = 0x14;
        public const int VK_TAB = 0x09;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_BACK = 0x08;
        public const int VK_RETURN = 0x0D;
        public const int VK_SPACE = 0x20;
        public const int VK_DeLETE = 0x2E;
        public const int VK_INSERT = 0x2D;
        public const int VK_ENTER = 0x0D;

        // Arrow keys and Navigation keys
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_HOME = 0x24;
        public const int VK_END = 0x23;
        public const int VK_PRIOR = 0x21; // Page Up
        public const int VK_NEXT = 0x22; // Page Down

        // Function keys (F1-F12)
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;

        // Numpad keys
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;

        /*
         * Functions
         */
        // Check if a key is a modifier key (Shift, Control, Alt)
        public static bool IsModifierKey(int keyCode)
        {
            return keyCode == VK_SHIFT || keyCode == VK_CONTROL || keyCode == VK_MENU ||
                   keyCode == vk_LSHIFT || keyCode == vk_RSHIFT ||
                   keyCode == vk_LCONTROL || keyCode == vk_RCONTROL ||
                   keyCode == vk_LMENU || keyCode == vk_RMENU;
        }

        // Check if a key is a shift key
        public static bool IsShiftKey(int keyCode)
        {
            return keyCode == VK_SHIFT || keyCode == vk_LSHIFT || keyCode == vk_RSHIFT;
        }

        // Check if a key is a control key
        public static bool IsControlKey(int keyCode)
        {
            return keyCode == VK_CONTROL || keyCode == vk_LCONTROL || keyCode == vk_RCONTROL;
        }

        // Check if a key is an alt key
        public static bool IsAltKey(int keyCode)
        {
            return keyCode == VK_MENU || keyCode == vk_LMENU || keyCode == vk_RMENU;
        }

        // Check if a key is a capital key
        public static bool IsCapitalKey(int keyCode)
        {
            return keyCode == VK_CAPITAL;
        }
    }
}
