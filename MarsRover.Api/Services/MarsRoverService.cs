using MarsRover.Api.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Services
{
    public class MarsRoverService : IMarsRoverService
    {
        private Coordinates _maxCoordinates;
        private readonly IDictionary<Direction, string> directionDictionary;
        private readonly IWebHostEnvironment _env;
        private readonly string _reportPath;
        private readonly IFileService _fileService;
        public MarsRoverService(IWebHostEnvironment env, IFileService fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            directionDictionary = new Dictionary<Direction, string>
            {
                 {Direction.North, "N"},
                 {Direction.South, "S"},
                 {Direction.East, "E"},
                 {Direction.West, "W"}
            };
            _env = env;
            _reportPath = Path.Combine(_env.ContentRootPath, @"Data\inputOutputHistory.csv");
        }
    public MarsRoverResponse MoveAllRovers(MarsRoverRequest request)
        {
            
            MarsRoverResponse response = new MarsRoverResponse();
            try
            {
                this._maxCoordinates = new Coordinates
                {
                    X = request.MaxCoordinates.X,
                    Y = request.MaxCoordinates.Y
                };
                foreach (DeployedRover _rover in request.RoverPositions)
                {
                    Rover outputRover = new Rover(_rover.RoverCoordinates.X, _rover.RoverCoordinates.Y, _rover.RoverOrientation, _rover.MovementInstructions);
                    outputRover.StartMovement();
                    CheckOutOfBoundary(outputRover);
                    response = SaveFinalPosition(outputRover, response);
                    LogInputOutputHistory(_rover, outputRover);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        private MarsRoverResponse SaveFinalPosition(Rover _outputRoverRover, MarsRoverResponse output)
        {
            var finalPosition = new RoverFinalPosition();
            finalPosition.RoverCoordinates.X = _outputRoverRover.X;
            finalPosition.RoverCoordinates.Y = _outputRoverRover.Y;
            finalPosition.RoverOrientation = directionDictionary[_outputRoverRover.RoverOrientation];
            output.FinalRoverPositions.Add(finalPosition);
            return output;
        }

        //Making sure the Rover object stays within the grid.
        private void CheckOutOfBoundary(Rover _outputRover)
        {
            if (_outputRover.X < 0 || _outputRover.X > _maxCoordinates.X || _outputRover.Y < 0 || _outputRover.Y > _maxCoordinates.Y)
            {
                throw new Exception("Rover has moved out off the plateau boundaries");
            }
        }

        public void LogInputOutputHistory(DeployedRover _input, Rover _output)
        {

            using (StreamWriter file = File.AppendText(_reportPath))
            {
                file.WriteLine(DateTime.Now + "," + this._maxCoordinates.X + "," +
                this._maxCoordinates.Y + "," + _input.RoverCoordinates.X + "," +
                _input.RoverCoordinates.Y + "," + _input.RoverOrientation + "," + 
                _input.MovementInstructions + "," +_output.X + "," + 
                _output.Y + "," + directionDictionary[_output.RoverOrientation]);
            }
        }

        public SavePlateauResponse SavePlateauImage(SavePlateauRequest request)
        {
            SavePlateauResponse response = new SavePlateauResponse();
            response.ResponseCode = "01";
            try
            {
                if (request.Image != null && request.Image.Length > 0)
                {
                    if(_fileService.SaveUpload(request.Image, _env.ContentRootPath, request.FileName))
                    {
                        response.ResponseCode = "00";
                        response.ResponseMessage = "Success";
                    }
                    else
                    {
                        response.ResponseMessage = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public MarsRoverHistoryResponse GetHistory()
        {
            MarsRoverHistoryResponse response = new MarsRoverHistoryResponse();
            try
            {
                List<RoverHistory> roverHistories = File.ReadAllLines(_reportPath)
                                           .Skip(1)
                                           .Select(v => RoverHistory.FromCsv(v))
                                           .OrderByDescending( m=>m.DateOfRequest)
                                           .ToList();
                response.RoverHistories = roverHistories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
