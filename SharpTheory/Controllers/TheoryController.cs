using Microsoft.AspNetCore.Mvc;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Controllers
{

    [ApiController]
    [Route("api")]
    public class TheoryController : ControllerBase
    {
        private readonly ILogger<TheoryController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
       
        public TheoryController(ILogger<TheoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("description")]
        public ActionResult<TheoryDescription> GetDescription()
        {
            _logger.LogInformation("GET api/description called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetDescription");
                return NotFound();
            }
            return Ok(root.Description);
        }

        [HttpGet("keys/all")]
        public ActionResult<IEnumerable<TheoryKey>> GetAllKeys()
        {
            _logger.LogInformation("GET api/keys/all called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllKeys");
                return NotFound();
            }
            return Ok(root.Keys.ToList());
        }

        [HttpGet("keys/sharps")]
        public ActionResult<TheoryKey> GetKeysAllSharps()
        {
            _logger.LogInformation("GET api/keys/sharps called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysAllSharps");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumSharps > 0).ToList();
            return Ok(result);
        }

        [HttpGet("keys/sharps/{numsharps}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysBySharps(int numsharps)
        {
            _logger.LogInformation("GET api/keys/sharps/{numsharps} called", numsharps);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysBySharps");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumSharps == numsharps).ToList();
            return Ok(result);
        }

        [HttpGet("keys/flats")]
        public ActionResult<TheoryKey> GetKeysAllFlats()
        {
            _logger.LogInformation("GET api/keys/flats called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysAllFlats");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumFlats > 0).ToList();
            return Ok(result);
        }

        [HttpGet("keys/flats/{numflats}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysByFlats(int numflats)
        {
            _logger.LogInformation("GET api/keys/flats/{numflats} called", numflats);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysByFlats");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumFlats == numflats).ToList();
            return Ok(result);
        }

        [HttpGet("scales/all")]
        public ActionResult<TheoryScale> GetAllScales()
        {
            _logger.LogInformation("GET api/scales/all called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllScales");
                return NotFound();
            }
            return Ok(root.Scales);
        }

        [HttpGet("scales/integer/{integer}")]
        public ActionResult<IEnumerable<TheoryScale>> GetScalesByInteger(int integer)
        {
            _logger.LogInformation("GET api/scales/integer/{integer} called", integer);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetScalesByInteger");
                return NotFound();
            }
            var result = root.Scales.Where(s => s.Integer == integer);
            return Ok(result);
        }

        [HttpGet("integers")]
        public ActionResult<TheoryInteger> GetAllIntegers()
        {
            _logger.LogInformation("GET api/integers called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllIntegers");
                return NotFound();
            }
            return Ok(root.Integers);
        }

        [HttpGet("integers/{integer}")]
        public ActionResult<TheoryInteger> GetInteger(int integer)
        {
            _logger.LogInformation("GET api/integers/{integer} called", integer);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetInteger");
                return NotFound();
            }
            var result = root.Integers.Where(i => i.Integer == integer);
            return Ok(result);
        }

        [HttpGet("intervals")]
        public ActionResult<TheoryInteger> GetIntervals()
        {
            _logger.LogInformation("GET api/intervals called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetIntervals");
                return NotFound();
            }
            return Ok(root.Intervals);
        }

        [HttpGet("interval/{interval}")]
        public ActionResult<TheoryInteger> GetIntervals(string interval)
        {
            _logger.LogInformation("GET api/interval/{interval} called", interval);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetIntervals (by abbreviation)");
                return NotFound();
            }
            var result = root.Intervals.Where(i => i.Abbreviation == interval);
            return Ok(result);
        }

        [HttpGet("nondiatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllNonDiatonic()
        {
            _logger.LogInformation("GET api/nondiatonic called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllNonDiatonic");
                return NotFound();
            }
            return Ok(root.NondiatonicScales);
        }

        [HttpGet("nondiatonic/octatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllOctatonic()
        {
            _logger.LogInformation("GET api/nondiatonic/octatonic called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllOctatonic");
                return NotFound();
            }
            return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic")));
        }

        [HttpGet("nondiatonic/octatonic/{integer}")]
        public ActionResult<NondiatonicScale> GetOctatonicByInt(int integer)
        {
            _logger.LogInformation("GET api/nondiatonic/octatonic/{integer} called", integer);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetOctatonicByInt");
                return NotFound();
            }
            var octatonic = root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic"));
            return Ok(octatonic?.Where(o => o.Name.Contains(integer.ToString())));
        }

        [HttpGet("nondiatonic/wholetone")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllWholeTone()
        {
            _logger.LogInformation("GET api/nondiatonic/wholetone called");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllWholeTone");
                return NotFound();
            }
            return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("whole_tone")));
        }

        [HttpGet("nondiatonic/wholetone/{integer}")]
        public ActionResult<NondiatonicScale> GetWholeToneByInt(int integer)
        {
            _logger.LogInformation("GET api/nondiatonic/wholetone/{integer} called", integer);
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json, _jsonOptions);
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetWholeToneByInt");
                return NotFound();
            }                                                                                                                       
            var wholeTone = root.NondiatonicScales?.Where(s => s.Name.Contains("whole_tone"));
            return Ok(wholeTone?.Where(w => w.Name.Contains(integer.ToString())));
        }
    }
}
