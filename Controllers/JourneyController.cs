using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolitaAssignment.Models;

namespace SolitaAssignment.Controllers
{
    [ApiController]
    public class JourneyController : ControllerBase
    {
        public static DatabaseObject DBobject = new DatabaseObject();
        [HttpGet("pageid")]
        [Route("journeys")]
        public async Task<ActionResult<IEnumerable<string>>> GetJourneys(int pageid)
        {
            Console.WriteLine(pageid);
            List<BikeJourney> journeys = DBobject.GetJourneys(pageid);
            string data = "";
            foreach(var journey in journeys)
            {
                data += "<tr>";
                data += $"<td> {journey.departuretime} </td>";
                data += $"<td> {journey.returntime} </td>";
                data += $"<td> {journey.departurestationid} </td>";
                data += $"<td> {journey.departurestationname} </td>";
                data += $"<td> {journey.returnstationid} </td>";
                data += $"<td> {journey.returnstationname} </td>";
                data += $"<td> {journey.covereddistance} </td>";
                data += $"<td> {journey.duration} </td>";
                data += "</tr>\n";
            }
            return Ok(data);
        }
    }
}
