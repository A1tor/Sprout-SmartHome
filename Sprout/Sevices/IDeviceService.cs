using Sprout.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Sevices
{
    public interface IDeviceService
    {
        Task AddDevice(string name, string model, string room, string image);
        Task<IEnumerable<SmartDevice>> GetDevice();
        Task<SmartDevice> GetDevice(int id);
        Task RemoveDevice(int id);
    }
}