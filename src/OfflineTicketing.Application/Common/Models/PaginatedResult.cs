using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public PaginatedList<T> Data { get; private set; } = null!;

        private PaginatedResult(bool success, PaginatedList<T> data, string? message)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        public static PaginatedResult<T> Ok(PaginatedList<T> data, string? message = null) =>
            new PaginatedResult<T>(true, data, message);

        public static PaginatedResult<T> Fail(string message) =>
            new PaginatedResult<T>(false, new PaginatedList<T>(new List<T>(), 0, 0, 0), message);
    }
}
