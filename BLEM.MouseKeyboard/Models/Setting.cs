using BLEM.MouseKeyboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEM.MouseKeyboard.Models
{
    internal class Setting
    {
        public bool IsRunning { get; set; } = false;
        public string ArduinoPortName { get; set; } = "COM1";
        public int ArduinoBaudRate { get; set; } = 115200;
        public string LastDeviceID { get; set; } = string.Empty;
        public DeviceType SelectedDeviceType { get; set; } = DeviceType.PC;
        public List<string> PairedDevices { get; set; } = new List<string>();
    }
}
