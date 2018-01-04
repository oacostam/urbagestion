using System;

namespace Urbagestion.UI.Web.Models
{
    [Serializable]
    public class PageResult<T>
    {
        public T Result { get; set; }

        public int Total { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}