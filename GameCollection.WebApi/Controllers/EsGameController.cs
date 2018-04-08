using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GameCollection.Contrat.Dto;
using GameCollection.ElasticSearch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GameCollection.WebApi.Controllers
{
    /// <summary>
    /// all methods game operation 
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class EsGameController : Controller
    {
        private readonly EsGameDocumentService _esGameDocumentSvc;
        private IConfiguration _config;

        /// <summary>
        /// 
        /// </summary>
        public EsGameController(IConfiguration config)
        {
            _config = config;
            _esGameDocumentSvc = new EsGameDocumentService(_config["elasticsearch:Index"], _config["elasticsearch:Url"]);
        }

        /// <summary>
        /// Get all Elements
        /// </summary>
        /// <returns>List of GameDto</returns>
        [HttpGet]
        [ProducesResponseType(typeof(SearchResult<GameDto>), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(void), 500)]
        public SearchResult<GameDto> Get()
        {
            //var currentUser = HttpContext.User;
            //if(currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            //{
            //    DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
            //    int userAge = DateTime.Today.Year - birthDate.Year;
            //}
            return _esGameDocumentSvc.SearchAll();
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
            var item = _esGameDocumentSvc.SeachBy(identifier);

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
            _esGameDocumentSvc.Insert(value);
            return new ObjectResult("Game added successfully!");
        }

        /// <summary>
        /// Modify game
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="value">game object</param>
        /// <returns></returns>
        [HttpPut("{identifier}"), Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult IActionResult(int identifier, [FromBody]GameDto value)
        {
            _esGameDocumentSvc.Update(value);
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
            var item = _esGameDocumentSvc.SeachBy(identifier);
            if (item == null)
            {
                return NotFound();
            }

            _esGameDocumentSvc.Delete(item);
            return new NoContentResult();
        }
    }
}
