using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Entities;
using ToDoApplication.BAL.IToDoApplicationService;
using ToDoApplication.Entities.Authentication;

namespace TodoApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoApplicationservice _bal;

        public ToDoItemsController(IToDoApplicationservice bal)
        {
            _bal = bal;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult> GetToDoItems()
        {
            try
            {
                var principal = HttpContext.User;
                var userId = principal?.Claims?.SingleOrDefault
                    (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")?.Value;
                var userRole = principal?.Claims?.SingleOrDefault
                (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole == null)
                {
                    userRole = UserRole.User;
                }
                return Ok(await _bal.GetToDoItems(userId,userRole));
            }
            catch(Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            try
            {
                var principal = HttpContext.User;
                var userId = principal?.Claims?.SingleOrDefault
                    (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")?.Value;
                var userRole = principal?.Claims?.SingleOrDefault
                (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole == null)
                {
                    userRole = UserRole.User;
                }
                return Ok(await _bal.GetToDoItem(id,userId,userRole));
            }
            catch (Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }

        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
        {
            try
            {
                var result = await _bal.UpdateToDoItem(id, toDoItem);
                if(result != null)
                {
                    return Ok(result);
                }
                Response response = new Response { Status = "Error", Message = "Cannot update item" };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }
        }

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            try
            {
                var principal = HttpContext.User;
                var userId = principal?.Claims?.SingleOrDefault
                    (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")?.Value;
                var userRole = principal?.Claims?.SingleOrDefault
                (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole == null)
                {
                    userRole = UserRole.User;
                }
                toDoItem.UserId = userId;
                var result = await _bal.SaveToDoItem(toDoItem);
                if (result != null)
                {
                    return CreatedAtAction("GetToDoItem", new { id = toDoItem.ItemId }, toDoItem);
                }
                Response response = new Response { Status = "Error", Message = "Cannot save the item" };
                return new JsonResult(response);

            }
            catch (Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }

            
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            try
            {
                var principal = HttpContext.User;
                var userId = principal?.Claims?.SingleOrDefault
                    (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")?.Value;
                var userRole = principal?.Claims?.SingleOrDefault
                (p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole == null)
                {
                    userRole = UserRole.User;
                }
                var result= await _bal.DeleteToDoItem(id,userId,userRole);
                if (result != null)
                {
                    return Ok(result);
                }
                Response response = new Response { Status = "Error", Message = "Cannot delete the item" };
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }

            return NoContent();
        }

        
    }
}
