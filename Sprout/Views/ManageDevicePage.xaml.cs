using MQTTnet.Client;
using Sprout.Models;
using Sprout.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sprout.Views
{
    [QueryProperty (nameof(DeviceId), nameof(DeviceId))]
    public partial class ManageDevicePage : ContentPage
    {
        public string DeviceId { get; set; }
        public string State { get; set; }
        public string Measurment { get; set; }
        public SmartDevice CurrentDevice { get; set; }
        IDeviceService deviceService;
        public ManageDevicePage()
        {
            InitializeComponent();
            deviceService = DependencyService.Get<IDeviceService>();
            _ = MQTTService.ConnectToBroker();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(DeviceId, out var result);
            string Codename;
            CurrentDevice = await deviceService.GetDevice(result);
            BindingContext = CurrentDevice;
            MQTTService.mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                if (Decode(message, 0, 5) == "state")
                    State = Decode(message, 5, 2);
                if (Decode(message, 0, 5) == "param")
                    Measurment = Decode(message, 5, 2);
                return Task.CompletedTask;
            };
            switch (CurrentDevice.Model)
            {
                case "Датчик звука":
                    Codename = "ss";
                    break;
                case "Умная лампочка":
                    Trigger_entry.IsVisible = true;
                    Codename = "sb";
                    break;
                default:
                    Measurment_Label.IsVisible = false;
                    State_Button.IsVisible = false;
                    Codename = "sh";
                    break;
            }  

            _ = MQTTService.Publish("state" + Codename);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CurrentDevice.Model == "Умная лампочка" & Trigger_entry.Text != null)
                await MQTTService.Publish("trggr" + Trigger_entry.Text.ToString());
            await MQTTService.Disconnect();
            await Shell.Current.GoToAsync("..");
        }
        private string Decode(string message, int first, int count)
        {
            string message_buffer = "";
            for (int i = first; i < first + count; i++)
                message_buffer += message[i];
            return message_buffer;
        } 
        private void State_Button_Clicked(object sender, EventArgs e)
        {
            if (State != null)
                State_Button.Text = State;
            if (Measurment != null)
                Measurment_Label.Text = Measurment;
        }
    }
}