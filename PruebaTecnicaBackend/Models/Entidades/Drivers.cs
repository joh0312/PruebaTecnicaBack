using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Drivers
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string last_name { get; set; }

        [Required]
        [MaxLength(255)]
        public string first_name { get; set; }

        [Required]
        [MaxLength(20)]
        public string ssn { get; set; }

        [Required]
        public DateTime dob { get; set; }

        [MaxLength(255)]
        public string address { get; set; }

        [MaxLength(100)]
        public string city { get; set; }

        [MaxLength(20)]
        public string zip { get; set; }

        public long phone { get; set; }

        public bool active { get; set; }
    }
}