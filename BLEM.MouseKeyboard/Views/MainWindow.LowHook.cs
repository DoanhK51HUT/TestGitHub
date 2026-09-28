using BLEM.MouseKeyboard.Constants;
using BLEM.MouseKeyboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEM.MouseKeyboard.Views
{
    public partial class MainWindow
    {
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            const int WM_KEYDOWN = 0x0100; // Phát sinh sự kiện khi nhấn phím
            const int WM_KEYUP = 0x0101; // Phát sinh sự kiện khi thả phím

            const int WM_SYSKEYDOWN = 0x0104; // Phát sinh sự kiện khi nhấn phím hệ thống (Alt + phím khác)
            const int WM_SYSKEYUP = 0x0105; // Phát sinh sự kiện khi thả phím hệ thống (Alt + phím khác)

            int vkCode = Marshal.ReadInt32(lParam); // Lấy mã phím từ lParam

            KBDLLHOOKSTRUCT keyInfo = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam); // Chuyển đổi lParam thành cấu trúc KBDLLHOOKSTRUCT để xác định phím bấm đầu
            bool isFirstPress = (keyInfo.flags & 0x40000000) == 00; // Kiểm tra nếu bit 30 của flags không được đặt, đó là lần nhấn phím đầu tiên

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                // Lưu lại trạng thái phím bấm 4 lần gần nhất
                // Nếu là phím mới thì ghi vào vị trí có giá trị none
                // Nếu là phím đã có trong array thì bỏ qua
                if (!keysPressing.Contains((Keys)vkCode))
                {
                    for (int i = 0; i < keysPressing.Length; i++)
                    {
                        if (keysPressing[i] == Keys.None)
                        {
                            keysPressing[i] = (Keys)vkCode;
                            break;
                        }
                    }
                }

                // Xác định combo key
                // Combo key không bao gồm phím shift, chữ, số
                // Combo key không bao gồm chỉ có 1 key
                string combokeyStr = string.Empty;
                bool isContainShiftKey = keysPressing.Contains(Keys.LShiftKey) || keysPressing.Contains(Keys.RShiftKey);
                bool isAZCharKey = (vkCode >= (int)Keys.A) && (vkCode <= (int)Keys.Z);
                bool isNumKey = (vkCode >= (int)Keys.D0) && (vkCode <= (int)Keys.D9);
                bool isSpecialKey = (vkCode >= 186 && vkCode <= 192) || (vkCode >= 219 && vkCode <= 222); // Ký tự đặc biệt ;,{....
                bool isContainControlKey = keysPressing.Contains(Keys.LControlKey) || keysPressing.Contains(Keys.RControlKey);
                bool isContainMenuKey = keysPressing.Contains(Keys.LMenu) || keysPressing.Contains(Keys.RMenu);
                bool isContainWinKey = keysPressing.Contains(Keys.LWin) || keysPressing.Contains(Keys.RWin);
                bool isContainLKey = keysPressing.Contains(Keys.L);
                bool isContainTabKey = keysPressing.Contains(Keys.Tab);
                bool isContainDel = keysPressing.Contains(Keys.Delete);


                if ((isContainShiftKey && (isAZCharKey || isNumKey || isSpecialKey))
                    || !(isContainShiftKey || isContainControlKey || isContainMenuKey || isContainTabKey || isContainWinKey)
                    || (isContainDel && isContainControlKey && isContainMenuKey)
                    || (isContainWinKey && isContainLKey))
                {
                    // Reset tổ hợp phím
                    keysPressing = new Keys[4] { Keys.None, Keys.None, Keys.None, Keys.None };
                }

                foreach (Keys key in keysPressing)
                {
                    // Lưu 4 ký tự gõ gần nhất
                    if (key != Keys.None)
                    {
                        if (combokeyStr == string.Empty)
                        {
                            combokeyStr = key.ToString();
                        }
                        else combokeyStr += $"{key}";
                    }
                }

                // Chuyển đổi chế độ gửi tới mobile hoặc pc
                // So sánh combokeyStr với các tổ hợp phím đã được định nghĩa trước đó để xác định hành động cần thực hiện
                if (AppConstants.ConnectedBleDevices.ContainsKey(combokeyStr))
                {
                    // 
                    BleConnectedInfo selectedDeviceInfo = AppConstants.ConnectedBleDevices[combokeyStr];

                    //
                    if (selectedDeviceInfo.BLEDeviceType == Enums.DeviceType.PC)
                    {
                        // Gửi đến PC
                    }
                    else if (selectedDeviceInfo.BLEDeviceType == Enums.DeviceType.BLEMOUSEKEYBOARD)
                    {
                        // Gửi đến thiết bị BLE Mouse Keyboard
                        // Code gửi tổ hợp phím qua Bluetooth sẽ được triển khai ở đây
                    }
                }
                return (IntPtr)1;
            }
            return (IntPtr)1; // Chỉ test -> xóa
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return (IntPtr)1;
        }
    }
}
