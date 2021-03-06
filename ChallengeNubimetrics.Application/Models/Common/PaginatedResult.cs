using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Models.Common
{
    public record PaginatedResult<T> : Result
    {
        protected PaginatedResult() { }

        protected PaginatedResult(List<T> data, int totalCount, int pageSize, int totalPages)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            TotalPages = totalPages;
            Data = data;
        }

        protected PaginatedResult(List<T> data)
        {
            TotalCount = 0;
            PageSize = 0;
            TotalPages = 0;
            Data = data;
        }

        public int TotalCount { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public List<T> Data { get; init; }

        public static PaginatedResult<T> Success(int totalCount, int pageSize, int totalPages, List<T> data = default)
           => new PaginatedResult<T>(data, totalCount, pageSize, totalPages) { HasErrors = false, Data = data };

        public static PaginatedResult<T> Error(string message, List<T> data = default)
            => new PaginatedResult<T>(data) { HasErrors = true, Message = message };

        public static PaginatedResult<T> NotFound(List<T> data = default)
            => new PaginatedResult<T>(data) { HasErrors = true, Message = "No se encontró un registro con la información enviada" };
    }
}