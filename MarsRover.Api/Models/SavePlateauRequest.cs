using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Api.Models
{
    public class SavePlateauRequest
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string FileName { get; set; }
    }

    public class SavePlateauResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
