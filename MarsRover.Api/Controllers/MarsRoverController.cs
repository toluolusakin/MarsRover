using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsRover.Api.Exceptions;
using MarsRover.Api.Models;
using MarsRover.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarsRover.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MarsRoverController : ControllerBase
    {
        private readonly IMarsRoverService _marsRoverService;
        
        public MarsRoverController(IMarsRoverService marsRoverService)
        {
            _marsRoverService = marsRoverService ?? throw new ArgumentNullException(nameof(marsRoverService));
            
        }

        [HttpPost("MoveRovers")]
        public IActionResult MoveRovers([FromBody] MarsRoverRequest request)
        {
            MarsRoverResponse response;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                response = _marsRoverService.MoveAllRovers(request);
                if (response == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (MarsRoverException)
            {
                throw new MarsRoverException();
                
            }
        }

        [HttpPost("SavePlateauImage")]
        public IActionResult SavePlateauImage([FromBody] SavePlateauRequest request)
        {
            SavePlateauResponse response;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                response = _marsRoverService.SavePlateauImage(request);
                if (response == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (MarsRoverException)
            {
                throw new MarsRoverException();

            }
        }

        [HttpGet("GetHistory")]
        public IActionResult GetHistory()
        {
            MarsRoverHistoryResponse response;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                response = _marsRoverService.GetHistory();
                if (response == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (MarsRoverException)
            {
                throw new MarsRoverException();

            }
        }
    }
}