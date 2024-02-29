using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions.FilterParameters
{
    public class MetaData
    {
        public int CurrentPage { get; set; }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value > 0 ? value : 1; }
        }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1 && CurrentPage <= TotalPages;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
