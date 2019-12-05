using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Models
{
    public class MarsRoverResponse
    {
        public IList<RoverFinalPosition> FinalRoverPositions { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public MarsRoverResponse()
        {
            FinalRoverPositions = new List<RoverFinalPosition>();
        }
    }

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
