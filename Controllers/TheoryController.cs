using Microsoft.AspNetCore.Mvc;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Controllers
{
    [ApiController]
    [Route("api")]
    public class TheoryController : ControllerBase
    {
        [HttpGet("description")]
        public ActionResult<TheoryDescription> GetDescription()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null) { return NotFound(); }
            return Ok(root.Description);
        }
    }
}
