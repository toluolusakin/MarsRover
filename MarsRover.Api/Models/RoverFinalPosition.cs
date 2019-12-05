
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class RoverFinalPosition
    {
        public Coordinates RoverCoordinates { get; set; }
        public string RoverOrientation { get; set; }

        public RoverFinalPosition()
        {
            RoverCoordinates = new Coordinates();
        }
    }
}
