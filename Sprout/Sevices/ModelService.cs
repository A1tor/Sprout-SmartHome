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
            await AddModel("Хаб", "https://sun9-west.userapi.com/sun9-15/s/v1/ig2/OM79Scrz7dJs38nEu0UzUVAEQGJA36KUFBuNukWE7R20R1HCXO1vKX93M4TGpLweEMtV76igdhyw4CrVntpFzrzZ.jpg?size=540x540&quality=96&type=album");
            await AddModel("Умная лампочка", "https://sun9-west.userapi.com/sun9-53/s/v1/ig2/YumVoIcI_auFuiY17Q7wlxqSW6MqBDH9LZu4sGhMG9z9kjhasmJocdho_7tIl0-7vXPXvBvcT5eEmYdpsX6mH0Bk.jpg?size=540x540&quality=96&type=album");
            await AddModel("Датчик звука", "https://sun9-east.userapi.com/sun9-20/s/v1/ig2/W8IEQSb60UuEhCatzffSfxm-54AC7kOFJ4YOwipQv0MUT-4IZ8fQbEJoG3l-I4T1bD-eqIFIummcTmeQpgkO5t_q.jpg?size=540x540&quality=96&type=album");

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
