using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Authentication;
using TodoApplication.Entities;
using ToDoApplication.BAL.IToDoApplicationService;

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
                return Ok(await _bal.GetToDoItems());
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
                return Ok(await _bal.GetToDoItem(id));
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
                var result= await _bal.DeleteToDoItem(id);
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
