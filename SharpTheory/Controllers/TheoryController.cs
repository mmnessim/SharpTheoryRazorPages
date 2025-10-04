using Microsoft.AspNetCore.Mvc;
using SharpTheory.Models;
using SharpTheory.Services;

namespace SharpTheory.Controllers
{

    /// <summary>
    /// TheoryController provides endpoints to access music theory data such as keys, scales, intervals, and non-diatonic scales.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class TheoryController : ControllerBase
    {
        private readonly ILogger<TheoryController> _logger;
        private readonly TheoryDataService _dataService;
        
        /// <summary>
        /// Constructor for TheoryController, injects logger and data service.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dataService"></param>
        public TheoryController(ILogger<TheoryController> logger, TheoryDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        /// <summary>
        /// /api/description endpoint to get the theory description.
        /// </summary>
        /// <returns></returns>
        [HttpGet("description")]
        public ActionResult<TheoryDescription> GetDescription()
        {
            _logger.LogInformation("GET api/description called");
            var root = _dataService.Root;
            if (root != null) return Ok(root.Description);
            _logger.LogWarning("TheoryRoot not found in GetDescription");
            return NotFound();
        }

        /// <summary>
        /// /api/keys/all endpoint to get all keys.
        /// </summary>
        /// <returns></returns>
        [HttpGet("keys/all")]
        public ActionResult<IEnumerable<TheoryKey>> GetAllKeys()
        {
            _logger.LogInformation("GET api/keys/all called");
            var root = _dataService.Root;
            if (root != null) return Ok(root.Keys.ToList());
            _logger.LogWarning("TheoryRoot not found in GetAllKeys");
            return NotFound();
        }

        /// <summary>
        /// /api/keys/sharps endpoint to get all keys with sharps.
        /// </summary>
        /// <returns></returns>
        [HttpGet("keys/sharps")]
        public ActionResult<TheoryKey> GetKeysAllSharps()
        {
            _logger.LogInformation("GET api/keys/sharps called"); 
            var root = _dataService.Root;
            if (root != null)
            {
                var result = root.Keys.Where(k => k.NumSharps > 0).ToList();
                return Ok(result);
            }

            _logger.LogWarning("TheoryRoot not found in GetKeysAllSharps");
            return NotFound();
        }

        /// <summary>
        /// /api/keys/sharps/{numsharps} endpoint to get keys by number of sharps.
        /// Valid values are 0-7
        /// </summary>
        /// <param name="numSharps"></param>
        /// <returns></returns>
        [HttpGet("keys/sharps/{numSharps}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysBySharps(int numSharps)
        {
            _logger.LogInformation("GET api/keys/sharps/{numSharps} called", numSharps);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysBySharps");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumSharps == numSharps).ToList();
            return Ok(result);
        }

        /// <summary>
        /// /api/keys/flats endpoint to get all keys with flats.
        /// </summary>
        /// <returns></returns>
        [HttpGet("keys/flats")]
        public ActionResult<TheoryKey> GetKeysAllFlats()
        {
            _logger.LogInformation("GET api/keys/flats called");
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysAllFlats");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumFlats > 0).ToList();
            return Ok(result);
        }

        /// <summary>
        /// /api/keys/flats/{numflats} endpoint to get keys by number of flats.
        /// Valid values are 0-7
        /// </summary>
        /// <param name="numflats"></param>
        /// <returns></returns>
        [HttpGet("keys/flats/{numflats}")]
        public ActionResult<IEnumerable<TheoryKey>> GetKeysByFlats(int numflats)
        {
            _logger.LogInformation("GET api/keys/flats/{numflats} called", numflats);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetKeysByFlats");
                return NotFound();
            }
            var result = root.Keys.Where(k => k.NumFlats == numflats).ToList();
            return Ok(result);
        }

        /// <summary>
        /// /api/scales/all endpoint to get all scales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("scales/all")]
        public ActionResult<TheoryScale> GetAllScales()
        {
            _logger.LogInformation("GET api/scales/all called");
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllScales");
                return NotFound();
            }
            return Ok(root.Scales);
        }

        /// <summary>
        /// /api/scales/integer/{integer} endpoint to get scales by integer representation.
        /// Valid values are 0-11
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        [HttpGet("scales/integer/{integer:int}")]
        public ActionResult<IEnumerable<TheoryScale>> GetScalesByInteger(int integer)
        {
            _logger.LogInformation("GET api/scales/integer/{integer} called", integer);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetScalesByInteger");
                return NotFound();
            }
            var result = root.Scales.Where(s => s.Integer == integer);
            return Ok(result);
        }

        /// <summary>
        /// /api/integers endpoint to get all integer representations.
        /// </summary>
        /// <returns></returns>
        [HttpGet("integers")]
        public ActionResult<TheoryInteger> GetAllIntegers()
        {
            _logger.LogInformation("GET api/integers called");
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllIntegers");
                return NotFound();
            }
            return Ok(root.Integers);
        }

        /// <summary>
        /// /api/integers/{integer} endpoint to get integer representation by integer value.
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        [HttpGet("integers/{integer:int}")]
        public ActionResult<TheoryInteger> GetInteger(int integer)
        {
            _logger.LogInformation("GET api/integers/{integer} called", integer);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetInteger");
                return NotFound();
            }
            var result = root.Integers.Where(i => i.Integer == integer);
            return Ok(result);
        }

        /// <summary>
        /// /api/intervals endpoint to get all intervals.
        /// </summary>
        /// <returns></returns>
        [HttpGet("intervals")]
        public ActionResult<TheoryInteger> GetIntervals()
        {
            _logger.LogInformation("GET api/intervals called");
            var root = _dataService.Root;
            if (root != null) return Ok(root.Intervals);
            _logger.LogWarning("TheoryRoot not found in GetIntervals");
            return NotFound();
        }

        /// <summary>
        /// /api/interval/{interval} endpoint to get interval by abbreviation.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        [HttpGet("interval/{interval}")]
        public ActionResult<TheoryInteger> GetIntervals(string interval)
        {
            _logger.LogInformation("GET api/interval/{interval} called", interval);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetIntervals (by abbreviation)");
                return NotFound();
            }
            var result = root.Intervals.Where(i => i.Abbreviation == interval);
            return Ok(result);
        }

        /// <summary>
        /// /api/nondiatonic endpoint to get all non-diatonic scales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nondiatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllNonDiatonic()
        {
            _logger.LogInformation("GET api/nondiatonic called");
            var root = _dataService.Root;
            if (root != null) return Ok(root.NondiatonicScales);
            _logger.LogWarning("TheoryRoot not found in GetAllNonDiatonic");
            return NotFound();
        }

        /// <summary>
        /// /api/nondiatonic/octatonic endpoint to get all octatonic scales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nondiatonic/octatonic")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllOctatonic()
        {
            _logger.LogInformation("GET api/nondiatonic/octatonic called");
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetAllOctatonic");
                return NotFound();
            }
            return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic")));
        }

        /// <summary>
        /// /api/nondiatonic/octatonic/{integer} endpoint to get octatonic scales by integer representation.
        /// Valid values are 0-2
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        [HttpGet("nondiatonic/octatonic/{integer:int}")]
        public ActionResult<NondiatonicScale> GetOctatonicByInt(int integer)
        {
            _logger.LogInformation("GET api/nondiatonic/octatonic/{integer} called", integer);
            var root = _dataService.Root;
            if (root == null)
            {
                _logger.LogWarning("TheoryRoot not found in GetOctatonicByInt");
                return NotFound();
            }
            var octatonic = root.NondiatonicScales?.Where(s => s.Name.Contains("octatonic"));
            return Ok(octatonic?.Where(o => o.Name.Contains(integer.ToString())));
        }

        /// <summary>
        /// /api/nondiatonic/wholetone endpoint to get all whole tone scales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nondiatonic/wholetone")]
        public ActionResult<IEnumerable<NondiatonicScale>> GetAllWholeTone()
        {
            _logger.LogInformation("GET api/nondiatonic/wholetone called");
            var root = _dataService.Root;
            if (root != null) return Ok(root.NondiatonicScales?.Where(s => s.Name.Contains("whole_tone")));
            _logger.LogWarning("TheoryRoot not found in GetAllWholeTone");
            return NotFound();
        }

        /// <summary>
        /// /api/nondiatonic/wholetone/{integer} endpoint to get whole tone scales by integer representation.
        /// Valid values are 0-1
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        [HttpGet("nondiatonic/wholetone/{integer:int}")]
        public ActionResult<NondiatonicScale> GetWholeToneByInt(int integer)
        {
            _logger.LogInformation("GET api/nondiatonic/wholetone/{integer} called", integer);
            var root = _dataService.Root;
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
