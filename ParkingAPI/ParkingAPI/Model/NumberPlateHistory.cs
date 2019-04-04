using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingAPI.Model
{
    public class NumberPlateHistory
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Imagecdn { get; set; }
        public string INAgentMACID { get; set; }
        public string OUTAgentMACID { get; set; }
        public string Status { get; set; }
        public string InGateSessionID { get; set; }
        public string OutGateSessionID { get; set; }
        public Nullable<System.DateTime> InTimeStamp { get; set; }
        public Nullable<System.DateTime> OutTimeStamp { get; set; }

    }
}