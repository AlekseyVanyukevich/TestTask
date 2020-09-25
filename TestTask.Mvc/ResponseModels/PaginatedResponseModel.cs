using System;
using System.Collections.Generic;

namespace TestTask.Mvc.ResponseModels
{
    public class PaginatedResponseModel<T> where T : class
    {
        public PaginatedResponseModel(int pageIndex, int pageSize, long total, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public int TotalPages => (int) Math.Ceiling((double) Total / PageSize);

        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;
    }
}