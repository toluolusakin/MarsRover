using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Services
{
    public class FileService : IFileService
    {

        public bool SaveUpload(string image, string rootFolder, string fileName)
        {
            bool status;
            try
            {
                var ext = GetFileExtension(image);
                string path = Path.Combine(rootFolder, @"Data\", fileName + ext);         
                File.WriteAllBytes(path, Convert.FromBase64String(image));
                status = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        private static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                default:
                    return string.Empty;
            }
        }
    }
}
