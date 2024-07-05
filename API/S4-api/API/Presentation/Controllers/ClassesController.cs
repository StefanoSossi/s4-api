using Microsoft.AspNetCore.Mvc;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;
using s4.Presentation.Middleware;
using System.Net;

namespace s4.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/Classes/")]
    public class ClassesController : Controller
    {
        private readonly IClassesManager _classManager;

        public ClassesController(IClassesManager ClassesManager)
        {
            _classManager = ClassesManager;
        }

        /// <summary>
        /// Returns all Classes according to the endpoint.
        /// </summary>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllCategories()
        {
            return Ok(new MiddlewareResponse<IEnumerable<ClassDto>>(await _classManager.GetAll()));
        }
        /// <summary>
        /// Returns specific Class from the Id provided.
        /// </summary>
        /// <param name="id">Id of the Class</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClassById([FromRoute] Guid id)
        {
            ClassDto response = await _classManager.GetById(id);
            return Ok(new MiddlewareResponse<ClassDto>(response));
        }

        /// <summary>
        /// Creates an Class.
        /// </summary>
        /// <remarks>
        /// When creating the request body you can ignore the value of the Id parameter, it will be generated automatically.
        /// </remarks>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPost]
        [Route("")]
        public virtual async Task<IActionResult> AddClass([FromBody] ClassDto classDto)
        {
            ClassDto response = await _classManager.Create(classDto);
            return Ok(new MiddlewareResponse<ClassDto>(response));
        }

        /// <summary>
        /// Updates the Class of the provided Id.
        /// </summary>
        /// <param name="id">Id of the Class</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] ClassDto classDto)
        {
            ClassDto response = await _classManager.Update(classDto, id);
            return Ok(new MiddlewareResponse<ClassDto>(response));
        }

        /// <summary>
        /// Deletes the Class of the provided Id.
        /// </summary>
        /// <param name="id">Id of the Class</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(new MiddlewareResponse<bool>(await _classManager.Delete(id)));
        }
    }
}
