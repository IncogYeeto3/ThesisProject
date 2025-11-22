using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Services
{
    public class Paginator
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public int TotalCount { get; set; } = 0;

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public int StartIndex => (Page - 1) * PageSize + 1;
        public int EndIndex => Math.Min(Page * PageSize, TotalCount);

        public void First() => Page = 1;
        public void Last() => Page = TotalPages;
        public void Next() { if (Page < TotalPages) Page++; }
        public void Prev() { if (Page > 1) Page--; }
    }

}
