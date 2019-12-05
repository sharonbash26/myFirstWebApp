using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Excercise3.Models
{
    public class Location
    {
        public string Lon { get; set; }
        public string Lat { get; set; }

        public string ToJSON()
        {
            return "{\"Lon\":" + Lon + ", \"Lat\":" + Lat + "}";
        }
    }
}