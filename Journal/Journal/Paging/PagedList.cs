using System.Collections.Generic;
using System;

namespace Journal.Paging
{
    public class PagedList<V> : List<V>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<V> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<V> ToPagedList(int count, List<V> items, int pageNumber, int pageSize)
        {
            return new PagedList<V>(items, count, pageNumber, pageSize);
        }
    }
}
