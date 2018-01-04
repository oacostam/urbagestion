using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Common;


namespace Urbagestion.Model.Models
{
    public class Facility : Entity
    {
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public TimeSpan OpensAt { get; set; }

        public TimeSpan CloseAt { get; set; }
    }
}