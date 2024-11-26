using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Routes
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string description { get; set; }

        [ForeignKey("Driver")]
        public int driver_id { get; set; }
        public Drivers Driver { get; set; }

        [ForeignKey("Vehicle")]
        public int vehicle_id { get; set; }
        public Vehicles Vehicle { get; set; }

        public bool active { get; set; }
    }
}
