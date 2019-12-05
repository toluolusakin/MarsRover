using MarsRover.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Services
{
    public interface IMarsRoverService
    {
        MarsRoverResponse MoverRover(MarsRoverRequest payload);
        MarsRoverHistoryResponse GetHistory();
    }
}
