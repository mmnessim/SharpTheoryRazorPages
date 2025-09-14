using Microsoft.AspNetCore.Mvc;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Controllers
{

    [ApiController]
    [Route("api")]
    public class TheoryController : ControllerBase
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        [HttpGet("description")]
        public ActionResult<TheoryDescription> GetDescription()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null) { return NotFound(); }
            return Ok(root.Description);
        }

        [HttpGet("keys/all")]
        public ActionResult<IEnumerable<TheoryKey>> GetAllKeys()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null) { return NotFound(); }
            return Ok(root.Keys.ToList());
        }

        [HttpGet("keys/sharps")]
        public ActionResult<TheoryKey> GetKeysAllSharps()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Keys.Where(k => k.NumSharps > 0).ToList();
            return Ok(result);
        }

        [HttpGet("keys/sharps/{numsharps}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysBySharps(int numsharps)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Keys.Where(k => k.NumSharps == numsharps).ToList();
            return Ok(result);
        }

        [HttpGet("keys/flats")]
        public ActionResult<TheoryKey> GetKeysAllFlats()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Keys.Where(k => k.NumFlats > 0).ToList();
            return Ok(result);
        }

        [HttpGet("keys/flats/{numflats}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysByFlats(int numflats)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Keys.Where(k => k.NumFlats == numflats).ToList();
            return Ok(result);
        }

        [HttpGet("scales/all")]
        public ActionResult<TheoryScale> GetAllScales()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.Scales);
        }

        [HttpGet("scales/integer/{integer}")]
        public ActionResult<IEnumerable<TheoryScale>> GetScalesByInteger(int integer)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Scales.Where(s => s.Integer == integer);
            return Ok(result);
        }

        [HttpGet("integers")]
        public ActionResult<TheoryInteger> GetAllIntegers()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.Integers);
        }

        [HttpGet("integers/{integer}")]
        public ActionResult<TheoryInteger> GetInteger(int integer)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Integers.Where(i => i.Integer == integer);
            return Ok(result);
        }

        [HttpGet("intervals")]
        public ActionResult<TheoryInteger> GetIntervals()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.Intervals);
        }

        [HttpGet("interval/{interval}")]
        public ActionResult<TheoryInteger> GetIntervals(string interval)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var result = root.Intervals.Where(i => i.Abbreviation == interval);
            return Ok(result);
        }

        [HttpGet("nondiatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllNonDiatonic()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.NondiatonicScales);
        }

        [HttpGet("nondiatonic/octatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllOctatonic()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic")));
        }

        [HttpGet("nondiatonic/octatonic/{integer}")]
        public ActionResult<NondiatonicScale> GetOctatonicByInt(int integer)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var octatonic = root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic"));
            return Ok(octatonic?.Where(o => o.Name.Contains(integer.ToString())));
        }

        [HttpGet("nondiatonic/wholetone")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllWholeTone()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("whole_tone")));
        }

        [HttpGet("nondiatonic/wholetone/{integer}")]
        public ActionResult<NondiatonicScale> GetWholeToneByInt(int integer)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null) return NotFound();
            var wholeTone = root.NondiatonicScales?.Where(s => s.Name.Contains("whole_tone"));
            return Ok(wholeTone?.Where(w => w.Name.Contains(integer.ToString())));
        }
    }
}
