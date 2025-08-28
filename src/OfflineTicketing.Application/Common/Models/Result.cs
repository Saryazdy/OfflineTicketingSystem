using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Models
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }

        public bool IsFailure => !Success;

        private Result(bool success, T? data, string? message)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        // Factory methods
        public static Result<T> Ok(T data, string? message = null) =>
            new Result<T>(true, data, message);

        public static Result<T> Fail(string message) =>
            new Result<T>(false, default, message);
    }
}
