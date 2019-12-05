using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Models
{
    public class MarsRoverViewModel
    {
        [Required]
        public int? MaxCoordinatesX { get; set; }
        [Required]
        public int? MaxCoordinatesY { get; set; }

        [Required]
        public int? DeployedRoverX { get; set; }
        [Required]
        public int? DeployedRoverY { get; set; }

        [Required]
        public string RoverOrientation { get; set; }
        [Required]
        public string MovementInstructions { get; set; }

        public FinalMarRoverPosition FinalPosition { get; set; }

        public MarsRoverViewModel()
        {
            FinalPosition = new FinalMarRoverPosition();
        }

    }

    public class FinalMarRoverPosition
    {
        public int? RoverCoordinateX { get; set; }
        public int? RoverCoordinateY { get; set; }
        public string RoverOrientation { get; set; }
    }
}
