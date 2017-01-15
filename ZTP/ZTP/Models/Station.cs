using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models
{
    public class Station
    {
        public int StationID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }


        public virtual ICollection<Train> TrainsFrom { get; set; }
        public virtual ICollection<Train> TrainsTo { get; set; }
    }
}