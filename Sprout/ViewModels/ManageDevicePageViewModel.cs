using MvvmHelpers;
using Sprout.Models;
using Sprout.Sevices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sprout.ViewModels
{
    public class ManageDevicePageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<SmartDevice> SensorDevices { get; set; }
        public ObservableRangeCollection<SmartDevice> SensorDevicesBuffer { get; set; }
        IDeviceService deviceService;
        public ManageDevicePageViewModel()
        {
            deviceService = DependencyService.Get<IDeviceService>();
            SensorDevices = new ObservableRangeCollection<SmartDevice>();
            SensorDevicesBuffer = new ObservableRangeCollection<SmartDevice>();

            _ = GetSensorDevices();
        }
        private async Task GetSensorDevices()
        {
            SensorDevices.Clear();
            var models = await deviceService.GetDevice();
            SensorDevicesBuffer.AddRange(models);
            SensorDevicesBuffer.Clear();
        }
    }
}
