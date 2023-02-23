using Sprout.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Sevices
{
    interface IModelService
    {
        Task AddModel(string name, string image);
        Task<IEnumerable<DeviceModelTemplate>> GetModel();
        Task RemoveModel(int id);
    }
}
