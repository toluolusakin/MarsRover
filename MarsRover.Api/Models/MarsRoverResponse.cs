using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class MarsRoverResponse
    {
        public IList<RoverFinalPosition> FinalRoverPositions { get; set; }
        public MarsRoverResponse()
        {
            FinalRoverPositions = new List<RoverFinalPosition>();
        }
    }

    
}
