using MvvmHelpers;
using MvvmHelpers.Commands;
using Sprout.Models;
using Sprout.Sevices;
using Sprout.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sprout.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<SmartDevice> SmartDevices { get; set; }
        public ObservableRangeCollection<Grouping<string, SmartDevice>> SmartDevicesGroups { get; set; }
        public ObservableRangeCollection<RoomModelTemplate> RoomFilters { get; set; }
        public ObservableRangeCollection<String> SelectedRoomFilters { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<SmartDevice> SelectedCommand { get; }
        public AsyncCommand<RoomModelTemplate> SelectedRoomCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand AddRoomCommand { get; }
        public AsyncCommand<SmartDevice> RemoveCommand { get; }

        IDeviceService deviceService;
        IRoomService roomService;
        public MainPageViewModel()
        {
            SmartDevices = new ObservableRangeCollection<SmartDevice>();
            SmartDevicesGroups = new ObservableRangeCollection<Grouping<string, SmartDevice>>();
            RoomFilters = new ObservableRangeCollection<RoomModelTemplate>();
            SelectedRoomFilters = new ObservableRangeCollection<String>();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<SmartDevice>(Selected);
            SelectedRoomCommand = new AsyncCommand<RoomModelTemplate>(RoomSelected);
            AddCommand = new AsyncCommand(Add);
            AddRoomCommand = new AsyncCommand(AddRoom);
            RemoveCommand = new AsyncCommand<SmartDevice>(Remove);

            deviceService = DependencyService.Get<IDeviceService>();
            roomService = DependencyService.Get<IRoomService>();
        }

        SmartDevice selectedDevice;
        RoomModelTemplate selectedRoom;

        public SmartDevice SelectedDevice
        {
            get => selectedDevice;
            set => SetProperty(ref selectedDevice, value);
        }
        public RoomModelTemplate SelectedRoom
        {
            get => selectedRoom;
            set => SetProperty(ref selectedRoom, value);
        }
        async Task Selected(SmartDevice smartDevice)
        {
            if (smartDevice == null)
                return;
            var route = $"{nameof(ManageDevicePage)}?DeviceId={smartDevice.Id}";
            await Shell.Current.GoToAsync(route);
        }
        async Task RoomSelected(RoomModelTemplate room)
        {
            if (room == null)
                return;
            if (!SelectedRoomFilters.Contains(room.Name))
                SelectedRoomFilters.Add(room.Name);
            else
                SelectedRoomFilters.Remove(room.Name);
            await Refresh();
        }
        async Task Add()
        {
            var route = $"{nameof(AddDevicePage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task AddRoom()
        {
            var route = $"{nameof(AddRoomPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Remove(SmartDevice smartDevice)
        {
            await deviceService.RemoveDevice(smartDevice.Id);
            await Refresh();
        }
        async Task Refresh()
        {
            IsBusy = true;
            SmartDevices.Clear();
            var smartDevices = await deviceService.GetDevice();
            SmartDevices.AddRange(smartDevices);
            _ = GroupDevices();
            IsBusy = false;
            DependencyService.Get<IToast>()?.MakeToast("Refreshed");
        }
        async Task GroupDevices()
        {
            SmartDevicesGroups.Clear();
            RoomFilters.Clear();
            var rooms = await roomService.GetRoom();
            RoomFilters.AddRange(rooms);
            for (int i = 0; i < RoomFilters.Count; i++)
            {
                if (!SelectedRoomFilters.Contains(RoomFilters[i].Name))
                {
                    var roomName = RoomFilters[i].Name;
                    SmartDevicesGroups.Add(new Grouping<string, SmartDevice>(roomName, SmartDevices.Where(c => c.Room == roomName)));
                }
            }
        }
    }
}
