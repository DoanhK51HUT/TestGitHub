using BLEM.MouseKeyboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEM.MouseKeyboard.Models
{
    internal class BleConnectedInfo
    {
        public string MacAddress { get; set; } = string.Empty; //  MAC: AA:BB:CC:11:22:33
        public string BondingKey { get; set; } = string.Empty; // Bonding key / Security key for BLE connection
        public DeviceType BLEDeviceType { get; set; } = DeviceType.PC;
    }
}
