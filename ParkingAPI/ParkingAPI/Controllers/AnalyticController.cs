using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ParkingAPI.Model;
using System.Globalization;

namespace ParkingAPI.Controllers
{
    [RoutePrefix("api/analytic")]
    public class AnalyticController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var obj = new List<AnalyticsData>();
            using (ParkingEntities db = new ParkingEntities())
            {
                var resultSet = db.ParkingLogs
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
                    }).ToList();

                obj = GetResult(resultSet);
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        private static List<AnalyticsData> GetResult(List<NumberPlateHistory> resultSet) {
            List<string> dateList = resultSet.
                    Select(car => car.InTimeStamp?.ToString("dd/M/yyyy", CultureInfo.InvariantCulture))
                   .Distinct().ToList();
            List<AnalyticsData> analyticsList = new List<AnalyticsData>();

            foreach (var date in dateList)
            {
                var result = resultSet
                    .Where(car => date.Equals(car.InTimeStamp?.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)))
                    .ToList();
                var totalCount = result.Count();
                var onGoingSession = result.Where(car => car.Status.Equals("OnGoing")).Count();
                var finishedSession = result.Where(car => car.Status.Equals("Completed")).Count();
                analyticsList.Add(
                    new AnalyticsData {
                        Date = date,
                        TotalSession = totalCount,
                        OnGoingSession = onGoingSession,
                        FinishedSession = finishedSession
                    }
                );
            }

            return analyticsList;

        }
    }
}
