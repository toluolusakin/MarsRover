using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Models
{
    public class MarsRoverHistoryResponse
    {
        public IList<RoverHistory> RoverHistories { get; set; }

        public MarsRoverHistoryResponse()
        {
            RoverHistories = new List<RoverHistory>();
        }
    }

    public class RoverHistory
    {
        public DateTime DateOfRequest { get; set; }
        public int MaxCoordinatesX { get; set; }
        public int MaxCoordinatesY { get; set; }
        public int RoverCoordinatesX { get; set; }
        public int RoverCoordinatesY { get; set; }
        public string RoverOrientation { get; set; }
        public string MovementIntructions { get; set; }
        public int FinalRoverCoordinatesX { get; set; }
        public int FinalRoverCoordinatesY { get; set; }
        public string FinalRoverOrientation { get; set; }
    }
}
