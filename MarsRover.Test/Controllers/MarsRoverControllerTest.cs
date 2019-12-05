using MarsRover.Api.Controllers;
using MarsRover.Api.Models;
using MarsRover.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarsRover.Test.Controllers
{
    public class MarsRoverControllerTest
    {
        protected MarsRoverController ControllerUnderTest { get; }
        protected Mock<IMarsRoverService> MarsRoverServiceMock { get; }
        protected Mock<ILogger<MarsRoverController>> ILoggerMock { get; }

        public MarsRoverControllerTest()
        {
            MarsRoverServiceMock = new Mock<IMarsRoverService>();
            ControllerUnderTest = new MarsRoverController(MarsRoverServiceMock.Object);
        }

        public class MoveRovers : MarsRoverControllerTest
        {
            [Fact]
            public void Test_Multiple_Rovers_Movement()
            {
                // Arrange
                var roverPositions = new List<DeployedRover>();
                var roverFinalPositions = new List<RoverFinalPosition>();
                var roverPosition = new DeployedRover()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 1,
                        Y = 2
                    },
                    RoverOrientation = "N",
                    MovementInstructions = "LMLMLMLMM"
                };
                roverPositions.Add(roverPosition);
                roverPosition = new DeployedRover()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 3,
                        Y = 3
                    },
                    RoverOrientation = "E",
                    MovementInstructions = "MMRMMRMRRM"
                };
                roverPositions.Add(roverPosition);

                var request = new MarsRoverRequest()
                {
                    MaxCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 5
                    },
                    RoverPositions = roverPositions
                };

                var roverFinalPosition = new RoverFinalPosition()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 1,
                        Y = 3
                    },
                    RoverOrientation = "N"
                };
                roverFinalPositions.Add(roverFinalPosition);

                roverFinalPosition = new RoverFinalPosition()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 1
                    },
                    RoverOrientation ="E"
                };
                roverFinalPositions.Add(roverFinalPosition);

                var expectedResponse = new MarsRoverResponse()
                {
                    FinalRoverPositions = roverFinalPositions
                };

                MarsRoverServiceMock.Setup(x => x.MoveAllRovers(request))
                    .Returns(expectedResponse);

                // Act
                var result = ControllerUnderTest.MoveRovers(request);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedResponse, okResult.Value);
            }

            [Fact]
            public void Test_12N_LMLMLMLMM_Rover_Movement()
            {
                // Arrange
                var roverPositions = new List<DeployedRover>();
                var roverFinalPositions = new List<RoverFinalPosition>();
                var roverPosition = new DeployedRover()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 1,
                        Y = 2
                    },
                    RoverOrientation = "N",
                    MovementInstructions = "LMLMLMLMM"
                };
                roverPositions.Add(roverPosition);
  
                var request = new MarsRoverRequest()
                {
                    MaxCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 5
                    },
                    RoverPositions = roverPositions
                };

                var roverFinalPosition = new RoverFinalPosition()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 1,
                        Y = 3
                    },
                    RoverOrientation = "N"
                };
                roverFinalPositions.Add(roverFinalPosition);

                var expectedResponse = new MarsRoverResponse()
                {
                    FinalRoverPositions = roverFinalPositions
                };

                MarsRoverServiceMock.Setup(x => x.MoveAllRovers(request))
                    .Returns(expectedResponse);

                // Act
                var result = ControllerUnderTest.MoveRovers(request);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedResponse, okResult.Value);
            }

            [Fact]
            public void Test_33E_MMRMMRMRRM_Rover_Movement()
            {
                // Arrange
                var roverPositions = new List<DeployedRover>();
                var roverFinalPositions = new List<RoverFinalPosition>();
               
                var roverPosition = new DeployedRover()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 3,
                        Y = 3
                    },
                    RoverOrientation = "E",
                    MovementInstructions = "MMRMMRMRRM"
                };
                roverPositions.Add(roverPosition);

                var request = new MarsRoverRequest()
                {
                    MaxCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 5
                    },
                    RoverPositions = roverPositions
                };

               
                var roverFinalPosition = new RoverFinalPosition()
                {
                    RoverCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 1
                    },
                    RoverOrientation = "E"
                };
                roverFinalPositions.Add(roverFinalPosition);

                var expectedResponse = new MarsRoverResponse()
                {
                    FinalRoverPositions = roverFinalPositions
                };

                MarsRoverServiceMock.Setup(x => x.MoveAllRovers(request))
                    .Returns(expectedResponse);

                // Act
                var result = ControllerUnderTest.MoveRovers(request);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedResponse, okResult.Value);
            }

            [Fact]
            public void Should_return_BadRequestResult()
            {
                // Arrange
                var roverPositions = new List<DeployedRover>();
                var request = new MarsRoverRequest()
                {
                    MaxCoordinates = new Coordinates()
                    {
                        X = 5,
                        Y = 5
                    },
                    RoverPositions = roverPositions
                };
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                // Act
                var result = ControllerUnderTest.MoveRovers(request);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }
    }
}
