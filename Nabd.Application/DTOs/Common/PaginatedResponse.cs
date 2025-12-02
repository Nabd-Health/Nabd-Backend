using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Common
{
    public class PaginatedResponse<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedResponse(IReadOnlyList<T> data, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = data;
        }

        // Factory Method لتسهيل الإنشاء
        public static PaginatedResponse<T> Create(IReadOnlyList<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResponse<T>(data, count, pageNumber, pageSize);
        }
    }
}