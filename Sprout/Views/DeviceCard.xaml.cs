using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sprout.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceCard : Grid
	{
		public DeviceCard ()
		{
			InitializeComponent ();
		}
	}
}