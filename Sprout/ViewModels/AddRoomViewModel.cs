using MvvmHelpers.Commands;
using Sprout.Sevices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sprout.ViewModels
{
    public class AddRoomViewModel : ViewModelBase
    {
        string name, image;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Image { get => image; set => SetProperty(ref image, value); }
        public AsyncCommand SaveCommand { get; }
        IRoomService roomService;

        public AddRoomViewModel()
        {
            Title = "Add room";

            roomService = DependencyService.Get<IRoomService>();
            SaveCommand = new AsyncCommand(Save);
        }
        async Task Save()
        {
            await roomService.AddRoom(name, image);
            await Shell.Current.GoToAsync("..");
        }
    }
}
