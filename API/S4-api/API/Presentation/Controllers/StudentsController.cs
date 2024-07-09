using Microsoft.AspNetCore.Mvc;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;
using s4.Presentation.Middleware;
using System.Net;

namespace s4.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/Students/")]
    public class StudentsController : Controller
    {
        private readonly IStudentManager _studentManager;

        public StudentsController(IStudentManager studentsManager)
        {
            _studentManager = studentsManager;
        }

        /// <summary>
        /// Returns all Students according to the endpoint.
        /// </summary>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllStudents()
        {
            return Ok(new MiddlewareResponse<IEnumerable<StudentDto>>(await _studentManager.GetAll()));
        }
        /// <summary>
        /// Returns specific Student from the Id provided.
        /// </summary>
        /// <param name="id">Id of the Student</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            StudentDto response = await _studentManager.GetById(id);
            return Ok(new MiddlewareResponse<StudentDto>(response));
        }

        /// <summary>
        /// Creates an Student.
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
        public virtual async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            StudentDto response = await _studentManager.Create(studentDto);
            return Ok(new MiddlewareResponse<StudentDto>(response));
        }

        /// <summary>
        /// Updates the Student of the provided Id.
        /// </summary>
        /// <param name="id">Id of the Student</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] StudentDto studentDto)
        {
            StudentDto response = await _studentManager.Update(studentDto, id);
            return Ok(new MiddlewareResponse<StudentDto>(response));
        }

        /// <summary>
        /// Deletes the Student of the provided Id.
        /// </summary>
        /// <param name="id">Id of the Student</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(new MiddlewareResponse<bool>(await _studentManager.Delete(id)));
        }

        /// <summary>
        /// Add Class with provided Id to the Stuednt of the provided Id.
        /// </summary>
        /// <param name="studentId">Id of the Student</param>
        /// <param name="classId">Id of the Class</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPost]
        [Route("{studentId}/class/{classId}")]
        public async Task<IActionResult> AddClassToStudent([FromRoute] Guid studentId, [FromRoute] Guid classId)
        {
            StudentDto response = await _studentManager.AddClass(classId, studentId);
            return Ok(new MiddlewareResponse<StudentDto>(response));
        }

        /// <summary>
        /// Remove Class with provided Id of the Student of the provided Id.
        /// </summary>
        /// <param name="studentId">Id of the Student</param>
        /// <param name="classId">Id of the Class</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpDelete]
        [Route("{studentId}/class/{classId}")]
        public async Task<IActionResult> RemoveClassToStudent([FromRoute] Guid studentId, [FromRoute] Guid classId)
        {
            StudentDto response = await _studentManager.RemoveClass(classId, studentId);
            return Ok(new MiddlewareResponse<StudentDto>(response));
        }
    }
}
