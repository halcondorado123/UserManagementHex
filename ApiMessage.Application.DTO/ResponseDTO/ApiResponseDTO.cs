using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMessage.Application.DTO.ResponseDTO
{
    public class ApiResponseDTO
    {
            public int Status { get; set; }
            public string? Message { get; set; }
    }
}
