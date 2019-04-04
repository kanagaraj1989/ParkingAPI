using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingAPI.Model
{
    public class AnalyticsData
    {
        public string Date { get; set; }
        public int TotalSession { get; set; }
        public int OnGoingSession { get; set; }
        public int FinishedSession { get; set; }
    }
}