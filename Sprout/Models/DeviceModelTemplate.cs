using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Models
{
    public class DeviceModelTemplate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
