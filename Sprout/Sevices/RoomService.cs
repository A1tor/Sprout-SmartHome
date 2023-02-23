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

[assembly: Dependency(typeof(RoomService))]
namespace Sprout.Sevices
{
    class RoomService : IRoomService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyRoom.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<RoomModelTemplate>();
            await db.DeleteAllAsync<RoomModelTemplate>();
            await AddRoom("Прихожая", "https://github.com/A1tor/Sprout-SmartHome/blob/main/Pic/Armchair.png?raw=true");
            await AddRoom("Спальня", "https://t4.ftcdn.net/jpg/03/27/88/89/360_F_327888967_IlZWB3yXruqImbVhQBDaqneohlzXv4l4.jpg");
            await AddRoom("Кухня", "https://github.com/A1tor/Sprout-SmartHome/blob/main/Pic/Pot.png?raw=true");
        }
        public async Task AddRoom(string name, string image)
        {
            await Init();
            var room = new RoomModelTemplate
            {
                Name = name,
                Image = image
            };
            var id = await db.InsertAsync(room);
        }

        public async Task<IEnumerable<RoomModelTemplate>> GetRoom()
        {
            await Init();
            var rooms = await db.Table<RoomModelTemplate>().ToListAsync();
            return rooms;
        }

        public async Task RemoveRoom(int id)
        {
            await Init();
            await db.DeleteAsync<RoomModelTemplate>(id);
        }
    }
}
