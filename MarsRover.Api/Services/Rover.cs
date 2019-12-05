using MarsRover.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Services
{
    ///
    /// Rover class maintains the state of of deployed rover and processsing of the movement intruction
    ///
    public class Rover : IRover
    {
        //private objects : Coordinates, Direction, Instruction Array
        private readonly Coordinates _roverCoordinates;
        private readonly string _roverInstructions;

        public int X { get { return _roverCoordinates.X; } }
        public int Y { get { return _roverCoordinates.Y; } }
        public Direction RoverOrientation { get; private set; }


        ///
        /// Constructor. Passes to Struct coorinates for getting coordinate object
        ///
        public Rover(int _xCor, int _yCor, string roverOrientation, string roverInstructions)
        {
            _roverCoordinates = new Coordinates
            {
                X = _xCor,
                Y = _yCor
            };
            RoverOrientation = GetDirection(roverOrientation);
            _roverInstructions = roverInstructions;
        }

        //Parses The Start Position into Direction Enum
        private Direction GetDirection(string _startDir)
        {
            switch (_startDir)
            {
                case "N":
                    return Direction.North;
                case "S":
                    return Direction.South;
                case "E":
                    return Direction.East;
                case "W":
                    return Direction.West;
                default:
                    throw new Exception("Invalid Direction. Accepted values are N,S,E,W");
            }
        }

        //Processes the Instruction Array
        public void StartMovement()
        {
            foreach (char _instruction in _roverInstructions)
            {
                if ((_instruction == 'L') || (_instruction == 'R'))
                {
                    Rotate90Degrees(_instruction);
                }
                else if (_instruction == 'M')
                    MoveInSameDirection();
                else
                    throw new Exception("Invalid Instruction Processed");
            }
        }

        //This Processes the direction of the Rover. Instructions L and R
        //are processed by this method.
        private void Rotate90Degrees(char _instruction)
        {
            int _rovDirInt = (int)this.RoverOrientation;

            if (_instruction == 'L')
            {
                if (_rovDirInt == 0)
                    _rovDirInt = 4;
                this.RoverOrientation = (Direction)(_rovDirInt - 1);
            }
            else
            {
                if (_rovDirInt == 3)
                    _rovDirInt = -1;
                this.RoverOrientation = (Direction)(_rovDirInt + 1);
            }

        }

        private void MoveInSameDirection()
        {
            switch (RoverOrientation)
            {
                case Direction.North:
                    _roverCoordinates.Y++;
                    break;
                case Direction.South:
                    _roverCoordinates.Y--;
                    break;
                case Direction.East:
                    _roverCoordinates.X++;
                    break;
                case Direction.West:
                    _roverCoordinates.X--;
                    break;
            }
        }
    }
}
