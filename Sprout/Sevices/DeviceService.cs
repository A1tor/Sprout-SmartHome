using Sprout.Models;
using Sprout.Sevices;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SQLite.SQLite3;

[assembly:Dependency(typeof(DeviceService))]
namespace Sprout.Sevices
{
    public class DeviceService : IDeviceService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<SmartDevice>();
        }
        public async Task AddDevice(string name, string model, string room, string image)
        {
            await Init();
            var smartDevice = new SmartDevice
            {
                Name = name,
                Model = model,
                Room = room,
                Image = image
            };
            var id = await db.InsertAsync(smartDevice);
        }
        public async Task RemoveDevice(int id)
        {
            await Init();
            Console.WriteLine("checked");
            await db.DeleteAsync<SmartDevice>(id);
        }
        public async Task<IEnumerable<SmartDevice>> GetDevice()
        {
            await Init();
            var smartDevices = await db.Table<SmartDevice>().ToListAsync();
            return smartDevices;
        }
        public async Task<SmartDevice> GetDevice(int id)
        {
            await Init();
            var smartDevice = await db.Table<SmartDevice>().FirstOrDefaultAsync(c => c.Id == id);
            return smartDevice;
        }
    }
}
