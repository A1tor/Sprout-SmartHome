using Sprout.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sprout.ViewModels
{
    public class DeviceSelector : DataTemplateSelector
    {
        public DeviceSelector()
        {

        }
        public DataTemplate Normal { get; set; }
        public DataTemplate Empty { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //var smartDevice = (SmartDevice)item;

            //return smartDevice.Room == "Bedroom" ? Special : Normal;
            return Normal;
        }
    }
}
