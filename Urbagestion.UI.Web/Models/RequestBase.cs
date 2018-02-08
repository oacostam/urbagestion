using System;
using Urbagestion.Util;

namespace Urbagestion.UI.Web.Models
{

    [Serializable]
    public class RequestBase
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public SortOrder Order { get; set; } = SortOrder.Desc;

        public string SortField { get; set; } = string.Empty;

        public int? Id { get; set; }
    }
}