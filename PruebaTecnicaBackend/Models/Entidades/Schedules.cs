using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Schedules
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Routes")]
        [Required]
        public int route_id { get; set; }

        [Required]
        public int week_num { get; set; }

        [Required]
        public DateTime from { get; set; }

        [Required]
        public DateTime to { get; set; }

        [Required]
        public bool active { get; set; }
    }
}
