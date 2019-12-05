using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Models
{
    public class MarsRoverRequest
    {
        public Coordinates MaxCoordinates { get; set; }
        public IList<DeployedRover> RoverPositions { get; set; }
    }

    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class DeployedRover
    {
        public Coordinates RoverCoordinates { get; set; }
        public string RoverOrientation { get; set; }
        public string MovementInstructions { get; set; }
    }
}
