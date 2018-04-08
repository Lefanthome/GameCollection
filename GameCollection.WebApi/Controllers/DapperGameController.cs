using GameCollection.Business;
using GameCollection.Contrat.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCollection.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("2")]
    [Route("api/v2/[controller]")]
    public class DapperGameController : Controller
    {
        private readonly GameService _gameSvc;
        private IConfiguration _config;

        /// <summary>
        /// 
        /// </summary>
        public DapperGameController(IConfiguration config)
        {
            _config = config;
            _gameSvc = new GameService(_config["ConnectionStrings:DefaultConnection"]);
        }

        /// <summary>
        /// Get all Elements
        /// </summary>
        /// <returns>List of GameDto</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GameDto>), 200)]
        //[ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(void), 500)]
        public IEnumerable<GameDto> Get()
        {
            return _gameSvc.GetAll();
        }

        /// <summary>
        /// Get game by Id
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        [HttpGet("{identifier}")]
        [ProducesResponseType(typeof(GameDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult Get(int identifier)
        {
            var item = _gameSvc.GetById(identifier);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        /// <summary>
        /// Add game
        /// </summary>
        /// <param name="value">game object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult Post([FromBody]GameDto value)
        {
            _gameSvc.Insert(value);
            return new ObjectResult("Employee added successfully!");
        }

        /// <summary>
        /// Modify game
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="value">game object</param>
        /// <returns></returns>
        [HttpPut("{identifier}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult IActionResult(int identifier, [FromBody]GameDto value)
        {
            _gameSvc.Update(value);
            return new ObjectResult("Game modified successfully!");
        }

        /// <summary>
        /// Delete Game by Id
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns></returns>
        [HttpDelete("{identifier}")]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult Delete(int identifier)
        {
            var item = _gameSvc.GetById(identifier);
            if (item == null)
            {
                return NotFound();
            }

            _gameSvc.Delete(identifier);
            return new NoContentResult();
        }
    }
}
