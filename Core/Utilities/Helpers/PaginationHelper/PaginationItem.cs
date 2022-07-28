using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.Helpers.PaginationHelper
{
    public class PaginationItem<T>
    {
        public int CurPage { get; set; } // o anki aktif sayfa numarası
        public int PageSize { get; set; } // bir sayfada kaç adet satır gösterileceği    
        public int TotalRowCount { get; set; } // toplam kayıt sayısı
        public IQueryable<T> Rows { get; set; } // gösterilen data kümesi
        public int TotalPageCount { get { return (int)Math.Ceiling((decimal)TotalRowCount / (decimal)PageSize); } } // toplam kaç sayfa olduğu

        // primeng grid için kullanılanlar

        public Dictionary<string, object> Filters { get; set; } // column bazında filtreleme yapmamızı sağlar
        public List<MultiSortMetaItem> MultiSortMeta { get; set; } // Order yapmak için kullanıyoruz

        public CustomFilterItem[] CustomFilter { get; set; } // dışardan filtreleme yapabilmek için eklendi

        // ctor
        public PaginationItem(int curPage, int pageSize = 10)
        {
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.Rows = null;
        }

        // override tostring
        public override string ToString()
        {
            return $"CurPage:{CurPage} PageSize:{PageSize} TotalRowCount:{TotalRowCount} TotalPageCount:{TotalPageCount}";
        }
    }

    public class MultiSortMetaItem
    {
        public string Field { get; set; }
        public int Order { get; set; } // 1 descending, -1 ascending
    }

    public class CustomFilterItem
    {
        public string Field { get; set; }
        public object Value { get; set; }
    }
}
