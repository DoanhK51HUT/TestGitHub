using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Keys = System.Windows.Forms.Keys;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Clipboard = System.Windows.Clipboard;
using System.Linq;
using System.IO.Ports;
using Gma.System.MouseKeyHook;// Author: g-mamaladze -> search mousekeyhook on GitHub 
using BLEM.MouseKeyboard.Models;
using System.Text.Json;
using System.IO;
using BLEM.MouseKeyboard.Constants;

namespace BLEM.MouseKeyboard.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Define
        CancellationTokenSource _cts;

        public MainWindow()
        {
            InitializeComponent();

            //
            _arduinoPort = new SerialPort("COM1", 115200);
            _arduinoPort.DataReceived += ArduinoPort_DataReceived;

            //
            _cts = new CancellationTokenSource();
        }

        private void btnStartButton_Click(object sender, RoutedEventArgs e)
        {
            // Press for starting
            // Load status of the last running
            if (btnStartButton.Content.ToString() == "START")
            {
                // Start open arduino port
                if (!_arduinoPort.IsOpen)
                {
                    try
                    {
                        _arduinoPort.PortName = cbbArduinoPort.SelectedItem.ToString();
                        _arduinoPort.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open Arduino port: {ex.Message}");
                        return;
                    }
                }
                // Start the hook status display
                btnStartButton.Content = "STOP";
                btnStartButton.Background = new SolidColorBrush(Colors.Yellow);

                // Start send to arduino


            }
            else
            {
                // Close arduino port
                if(_arduinoPort.IsOpen)
                {
                    try
                    {
                        _arduinoPort.Close();
                        // Stop the hook status display
                        btnStartButton.Content = "START";
                        btnStartButton.Background = new SolidColorBrush(SystemColors.ControlColor);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to close Arduino port: {ex.Message}");
                        return;
                    }
                }
            }

            // Save setting to file
            _setting.IsRunning = true;
            _setting.ArduinoPortName = _arduinoPort.PortName;
            SaveSetting(AppConstants._settingFile, _setting);
        }
        private void SaveSetting(string settingFile, Setting? setting)
        {
            string jsonString = JsonSerializer.Serialize(setting, new JsonSerializerOptions
            {
                    WriteIndented = true
            });
            if (!string.IsNullOrEmpty(jsonString))
            {
                File.WriteAllText(settingFile, jsonString);
            }
        }
        private Setting LoadSetting(string settingFile)
        {
            Setting setting = new Setting();
            if (File.Exists(settingFile))
            {
                // Check if file exists and load setting from file
                string jsonString = File.ReadAllText(settingFile);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    setting = JsonSerializer.Deserialize<Setting>(jsonString);
                }
            }
            return setting;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Check setting file exists, if not, create a new one
            if (!File.Exists(AppConstants._settingFile))
            {
                // Create new setting file
                _setting = new Setting();
                SaveSetting(AppConstants._settingFile, _setting);
            }

            // Load setting from file
            _setting = LoadSetting(AppConstants._settingFile);

            // Initialize setting port
            _arduinoPort = new SerialPort(_setting.ArduinoPortName, _setting.ArduinoBaudRate);
            _arduinoPort.DataReceived += ArduinoPort_DataReceived;

            // 
            if (_setting != null && _setting.IsRunning)
            {
                // Display UI as starting hook
                btnStartButton.Content = "STOP";
                btnStartButton.Background = new SolidColorBrush(Colors.Yellow);

                // TODO: Call hook function
            }   


        }

        private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Process data received from Arduino
             string data = _arduinoPort.ReadLine();
             // Handle the received data as needed
             // For example, you can update the UI or perform specific actions based on the data
             Dispatcher.Invoke(() =>
             {
                 // TODO: Process data recieved from Arduino and update UI accordingly
             });
        }

        private void cbbArduinoPort_DropDownOpened(object sender, EventArgs e)
        {
            // Select arduino port
            cbbArduinoPort.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames)
            {
                cbbArduinoPort.Items.Add(portName);
            }
        }
    }
}