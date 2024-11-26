using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Vehicles
    {
        public int id { get; set; }
        public string description { get; set; }
        public int year { get; set; }
        public int make { get; set; }
        public int capacity { get; set; }
        public bool active { get; set; }
    }
}
