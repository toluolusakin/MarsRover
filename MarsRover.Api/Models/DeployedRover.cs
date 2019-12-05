
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class DeployedRover
    {
        public Coordinates RoverCoordinates { get; set; }
        [Required]
        public string RoverOrientation { get; set; }
        [Required]
        public string MovementInstructions { get; set; }
    }

   
}
