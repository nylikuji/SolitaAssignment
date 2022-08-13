using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolitaAssignment.Models;

namespace SolitaAssignment.Controllers
{
    [Route("stations")]
    [ApiController]
    public class StationController : ControllerBase
    {
        public static DatabaseObject DBobject = new DatabaseObject();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetStations(int pageid, string sortby)
        {
            List<BikeStation> stations = DBobject.GetStations(pageid, sortby);
            string data = "";
            foreach(var station in stations)
            {
                data += "<tr>";
                data += $"<td> {station.fid} </td>";
                data += $"<td> {station.id} </td>";
                data += $"<td> {station.nimi} </td>";
                data += $"<td> {station.namn} </td>";
                data += $"<td> {station.name} </td>";
                data += $"<td> {station.osoite} </td>";
                data += $"<td> {station.adress} </td>";
                data += $"<td> {station.kaupunki} </td>";
                data += $"<td> {station.stad} </td>";
                data += $"<td> {station.opegator} </td>";
                data += $"<td> {station.capacity} </td>";
                data += $"<td> {station.x} </td>";
                data += $"<td> {station.y} </td>";
                data += "</tr>\n";
            }
            return Ok(data);
        }
    }
}
