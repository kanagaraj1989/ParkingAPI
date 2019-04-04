using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using ParkingAPI.Model;

namespace ParkingAPI.Controllers
{
    [RoutePrefix("api/activity")]
    public class ActivityController : ApiController
    {
        [Route("{plateNumber}")]
        public HttpResponseMessage Get(string plateNumber) {
            var obj = new List<NumberPlateHistory>();
            using (ParkingEntities db = new ParkingEntities()) {
               obj = db.ParkingLogs
                    .Where( car => car.PlateNumber.Equals(plateNumber))
                    .OrderByDescending( car => car.InTimeStamp)
                    .Select( car => new NumberPlateHistory {
                            PlateNumber = car.PlateNumber,
                            Status = car.Status,
                            Id = car.Id,
                            Imagecdn = car.Imagecdn,
                            INAgentMACID = car.INAgentMACID,
                            OUTAgentMACID = car.OUTAgentMACID,
                            InGateSessionID = car.InGateSessionID,
                            OutGateSessionID = car.OutGateSessionID,
                            InTimeStamp = car.InTimeStamp,
                            OutTimeStamp = car.OutTimeStamp
                    })
                    .ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}
