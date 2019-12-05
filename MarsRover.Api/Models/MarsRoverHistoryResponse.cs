using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class MarsRoverHistoryResponse
    {
       public  IList<RoverHistory> RoverHistories { get; set; }

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

        public static RoverHistory FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            RoverHistory roverHistory = new RoverHistory ();
            roverHistory.DateOfRequest = Convert.ToDateTime(values[0]);
            roverHistory.MaxCoordinatesX = Convert.ToInt32(values[1]);
            roverHistory.MaxCoordinatesY = Convert.ToInt32(values[2]);
            roverHistory.RoverCoordinatesX = Convert.ToInt32(values[3]);
            roverHistory.RoverCoordinatesY = Convert.ToInt32(values[4]);
            roverHistory.RoverOrientation = Convert.ToString(values[5]);
            roverHistory.MovementIntructions = Convert.ToString(values[6]);
            roverHistory.FinalRoverCoordinatesX = Convert.ToInt32(values[7]);
            roverHistory.FinalRoverCoordinatesY = Convert.ToInt32(values[8]);
            roverHistory.FinalRoverOrientation = Convert.ToString(values[9]);
            return roverHistory;
        }
    }
}
