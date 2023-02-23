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

[assembly: Dependency(typeof(ModelService))]
namespace Sprout.Sevices
{
    class ModelService : IModelService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyDeviceModels.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<DeviceModelTemplate>();

            await db.DeleteAllAsync<DeviceModelTemplate>();
            await AddModel("Хаб", "https://github.com/A1tor/Sprout-SmartHome/blob/main/Pic/Hub.png?raw=true");
            await AddModel("Умная лампочка", "https://github.com/A1tor/Sprout-SmartHome/blob/main/Pic/Bulb.png?raw=true");
            await AddModel("Датчик звука", "https://github.com/A1tor/Sprout-SmartHome/blob/main/Pic/Sound%20sensor.png?raw=true");

        }
        public async Task AddModel(string name, string image)
        {
            Console.WriteLine("fgdgds");
            await Init();
            var model = new DeviceModelTemplate
            {
                Name = name,
                Image = image
            };
            var id = await db.InsertAsync(model);
        }
        public async Task<IEnumerable<DeviceModelTemplate>> GetModel()
        {
            await Init();
            var model = await db.Table<DeviceModelTemplate>().ToListAsync();
            return model;
        }
        public async Task RemoveModel(int id)
        {
            await Init();
            await db.DeleteAsync<DeviceModelTemplate>(id);
        }
    }
}
