using MarsRover.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Services
{
    public interface IMarsRoverService
    {
        MarsRoverResponse MoveAllRovers(MarsRoverRequest request);
        void LogInputOutputHistory(DeployedRover _input, Rover _output);

        SavePlateauResponse SavePlateauImage(SavePlateauRequest request);
        MarsRoverHistoryResponse GetHistory();
    }
}
