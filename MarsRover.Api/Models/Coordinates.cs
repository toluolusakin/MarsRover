using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class Coordinates
    {
        [Required]
        public int X { get; set; }
        [Required]
        public int Y { get; set; }
    }
}
