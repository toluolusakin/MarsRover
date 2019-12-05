using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarsRover.Portal.Models;
using MarsRover.Portal.Services;

namespace MarsRover.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMarsRoverService _clientService;
        public HomeController(ILogger<HomeController> logger, IMarsRoverService clientService)
        {
            _logger = logger;
            _clientService = clientService;

        }
        public IActionResult History()
        {
            IList<RoverHistory> model = new List<RoverHistory>();
            try
            {
                MarsRoverHistoryResponse response = _clientService.GetHistory();
                if(response != null && response.RoverHistories.Count() > 0)
                {
                    foreach(RoverHistory _rover in response.RoverHistories)
                    {
                        model.Add(_rover);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View(new MarsRoverViewModel());
        }

        [HttpPost]
        public IActionResult Index(MarsRoverViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("error", "Invalid form entry");
                    return View(model);
                }

                var roverPositions = new List<DeployedRover>();
                var roverPosition = new DeployedRover()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = model.DeployedRoverX.Value,
                        Y = model.DeployedRoverY.Value
                    },
                    RoverOrientation = model.RoverOrientation,
                    MovementInstructions = model.MovementInstructions
                };
                roverPositions.Add(roverPosition);
                var request = new MarsRoverRequest()
                {
                    MaxCoordinates = new Coordinates()
                    {
                        X = model.MaxCoordinatesX.Value,
                        Y = model.MaxCoordinatesY.Value
                    },
                    RoverPositions = roverPositions
                };
                MarsRoverResponse response = _clientService.MoverRover(request);
                if(response == null)
                {
                    ModelState.AddModelError("error", "An error occured");
                    return View(model);
                }
                
                foreach(RoverFinalPosition _finalRover in response.FinalRoverPositions)
                {
                    model.FinalPosition = new FinalMarRoverPosition()
                    {
                        RoverCoordinateX = _finalRover.RoverCoordinates.X,
                        RoverCoordinateY = _finalRover.RoverCoordinates.Y,
                        RoverOrientation = _finalRover.RoverOrientation
                    };
                }
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                ModelState.AddModelError("error", ex.Message);
            }
            return View(model);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
