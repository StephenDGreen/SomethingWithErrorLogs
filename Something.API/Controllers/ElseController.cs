using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Something.Application;
using System;
using System.Threading.Tasks;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ElseController : ControllerBase
    {
        private readonly ISomethingElseCreateInteractor createInteractor;
        private readonly ISomethingElseReadInteractor readInteractor;
        private readonly ISomethingElseUpdateInteractor updateInteractor;
        private readonly ISomethingElseDeleteInteractor deleteInteractor;
        private readonly ILogger logger;

        public ElseController(ISomethingElseCreateInteractor createInteractor, ISomethingElseReadInteractor readInteractor, ISomethingElseUpdateInteractor updateInteractor, ISomethingElseDeleteInteractor deleteInteractor, ILogger logger)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.updateInteractor = updateInteractor;
            this.deleteInteractor = deleteInteractor;
            this.logger = logger;
        }
        [HttpPost]
        [Route("api/thingselse")]
        public async Task<IActionResult> CreateElseAsync([FromForm] string name, [FromForm] string[] othername)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (name.Length < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            try
            {
                await createInteractor.CreateSomethingElseAsync(name, othername);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut]
        [Route("api/thingselse/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromForm] string othername)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (othername.Length < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

            if (id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            try
            {
                await updateInteractor.UpdateSomethingElseAddSomethingAsync(id, othername);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}/{something_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id, int something_id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (else_id < 1 || something_id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            
            try
            {
                await updateInteractor.UpdateSomethingElseDeleteSomethingAsync(else_id, something_id);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (else_id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            try
            {
                await deleteInteractor.DeleteSomethingElseAsync(else_id);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("api/thingselse")]
        public async Task<ActionResult> GetElseListAsync()
        {            
            try
            {
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        private async Task<ActionResult> GetAllSomethingElseIncludeSomethingAsync()
        {
            try
            {
                var result = await readInteractor.GetSomethingElseIncludingSomethingsListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                throw;
            }
        }
    }
}
