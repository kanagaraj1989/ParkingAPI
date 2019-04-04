using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ParkingAPI.Model;

namespace ParkingAPI.Controllers
{
    [RoutePrefix("api/session")]
    public class SessionController : ApiController
    {
        [Route("{sessionid}")]
        public HttpResponseMessage Get(string sessionid)
        {
            var obj = new List<NumberPlateHistory>();
            using (ParkingEntities db = new ParkingEntities())
            {
                obj = db.ParkingLogs
                     .Where(car => car.InGateSessionID.Equals(sessionid)
                                  || car.OutGateSessionID.Equals(sessionid))
                     .OrderByDescending(car => car.Id)
                     .Select(car => new NumberPlateHistory
                     {
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
