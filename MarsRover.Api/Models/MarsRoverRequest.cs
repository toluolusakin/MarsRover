
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class MarsRoverRequest
    {
        public Coordinates MaxCoordinates { get; set; }
        [Required]
        public IList<DeployedRover> RoverPositions { get; set; }
    }
}
