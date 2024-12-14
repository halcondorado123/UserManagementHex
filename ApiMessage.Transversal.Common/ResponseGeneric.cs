using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMessage.Transversal.Common
{
    public class ResponseGeneric<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public ResponseGeneric() { }

        public ResponseGeneric(bool isSuccess, string? message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ResponseGeneric(T? data, bool isSuccess, string? message = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}


