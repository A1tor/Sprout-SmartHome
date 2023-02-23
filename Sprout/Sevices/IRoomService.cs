using Sprout.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Sevices
{
    interface IRoomService
    {
        Task AddRoom(string name, string image);
        Task<IEnumerable<RoomModelTemplate>> GetRoom();
        Task RemoveRoom(int id);
    }
}
