using MvvmHelpers;
using MvvmHelpers.Commands;
using Sprout.Models;
using Sprout.Sevices;
using Sprout.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sprout.ViewModels
{
    public class AddDeviceViewModel : ViewModelBase
    {
        string name, model, room, image;
        public ObservableRangeCollection<RoomModelTemplate> RoomTemplates { get; set; }
        public ObservableRangeCollection<DeviceModelTemplate> ModelTemplates { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        DeviceModelTemplate selectedDeviceTemplate;
        RoomModelTemplate selectedRoomTemplate;
        public DeviceModelTemplate SelectedDeviceTemplate
        {
            get { return selectedDeviceTemplate; }
            set
            {
                if (selectedDeviceTemplate != value)
                {
                    selectedDeviceTemplate = value;
                    OnPropertyChanged();
                }
            }
        }
        public RoomModelTemplate SelectedRoomTemplate
        {
            get { return selectedRoomTemplate; }
            set
            {
                if (selectedRoomTemplate != value)
                {
                    selectedRoomTemplate = value;
                    OnPropertyChanged();
                }
            }
        }
        public AsyncCommand SaveCommand { get; }
        IDeviceService deviceService;
        IRoomService roomService;
        IModelService modelService;
        public AddDeviceViewModel()
        {
            Title = "Add Coffee";

            deviceService = DependencyService.Get<IDeviceService>();
            roomService = DependencyService.Get<IRoomService>();
            modelService = DependencyService.Get<IModelService>();

            RoomTemplates = new ObservableRangeCollection<RoomModelTemplate>();
            ModelTemplates = new ObservableRangeCollection<DeviceModelTemplate>();

            _ = GetModelTemplate();
            _ = GetRoomTemplate();

            SaveCommand = new AsyncCommand(Save);
        }
        async Task Save()
        {
            if (selectedDeviceTemplate != null)
            {
                model = selectedDeviceTemplate.Name;
                image = selectedDeviceTemplate.Image;
            }

            if (selectedRoomTemplate != null)
            {
                room = selectedRoomTemplate.Name;
            }

            if (name == null | model == null | room == null)
                return;

            await deviceService.AddDevice(name, model, room, image);

            await Shell.Current.GoToAsync("..");
        }
        private async Task GetModelTemplate()
        {
            ModelTemplates.Clear();
            var models = await modelService.GetModel();
            ModelTemplates.AddRange(models);
        }
        private async Task GetRoomTemplate()
        {
            RoomTemplates.Clear();
            var rooms = await roomService.GetRoom();
            RoomTemplates.AddRange(rooms);
        }
    }
}
