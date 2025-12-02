namespace Nabd.Application.DTOs.Common
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50; // حماية للسيرفر
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // إضافات مهمة للبحث والترتيب (Standard Best Practice)
        public string? SearchTerm { get; set; } // للبحث في الاسم أو الكود
        public string? SortBy { get; set; }     // اسم العمود المراد الترتيب به
        public bool IsDescending { get; set; } = false; // تصاعدي أم تنازلي
    }
}