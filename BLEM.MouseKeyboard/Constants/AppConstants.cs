using BLEM.MouseKeyboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BLEM.MouseKeyboard.Constants
{
    internal class AppConstants
    {
        public static readonly Dictionary<string,BleConnectedInfo> ConnectedBleDevices = new();
        public static readonly string _settingFile = "BLEM.SettingFile.json";
    }
}
