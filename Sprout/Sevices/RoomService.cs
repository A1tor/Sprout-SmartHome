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
            await AddRoom("Прихожая", "https://sun9-east.userapi.com/sun9-32/s/v1/ig2/TKqEaqpdzLhTi1r1viYKrwZidc2B9hF-_ncLFEp2Jv_R9f-vqwDWITQSx8N4zbh9xQ6N0TmKjVwygHSF9W-j1sLW.jpg?size=540x540&quality=96&type=album");
            await AddRoom("Спальня", "https://t4.ftcdn.net/jpg/03/27/88/89/360_F_327888967_IlZWB3yXruqImbVhQBDaqneohlzXv4l4.jpg");
            await AddRoom("Кухня", "https://sun9-east.userapi.com/sun9-18/s/v1/ig2/bLI4bmKWKcikMzT1w-QcLTDeQjiTUM0rcVEN_lvAhbtTDDsOKt0NJRkFbn5sCWYN3nai9vd1eLMsPbN83XV3lo5B.jpg?size=540x540&quality=96&type=album");
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
