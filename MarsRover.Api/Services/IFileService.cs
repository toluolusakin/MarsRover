using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Services
{
    public interface IFileService
    {
        bool SaveUpload(string image, string rootFolder, string fileName);
    }
}
