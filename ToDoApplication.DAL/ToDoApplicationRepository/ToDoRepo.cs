using Microsoft.EntityFrameworkCore;
using System.Data;
using TodoApplication.Entities;
using ToDoApplication.DAL.IToDoApplicationRepository;

namespace ToDoApplication.DAL.ToDoApplicationRepository
{
    public class ToDoRepo : IToDoRepo
    {
        private readonly ToDoContext _context;

        public ToDoRepo(ToDoContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteToDoItem(int id,string userId,string role)
        {
            if (_context.ToDoItems == null)
            {
                return null;
            }
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return null; ;
            }
            if (userId == toDoItem.UserId || role == UserRole.Admin)
            {
                _context.ToDoItems.Remove(toDoItem);
                await _context.SaveChangesAsync();

                return "ToDo Item Deleted";
            }
            return null;
        }

        public async Task<ToDoItem> GetToDoItem(int id, string userId,string role)
        {

            dynamic toDoItem=null;
            if (role == UserRole.User)
            {
                toDoItem= await _context.ToDoItems
                .Where(b => b.UserId == userId && b.ItemId == id).FirstOrDefaultAsync();
            }
            else
            {
                toDoItem = await _context.ToDoItems.FindAsync(id);
            }
            return toDoItem;
        }

        public async Task<List<ToDoItem>> GetToDoItems(string userId, string role)
        {
            dynamic res = null;
            if (role == UserRole.User)
            {
                res = await _context.ToDoItems
                .Where(b => b.UserId == userId).ToListAsync();
            }
            else
            {
                res = await _context.ToDoItems.ToListAsync();
            }
            return res;
        }

        public bool isItemExist(int id)
        {
            return (_context.ToDoItems?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }

        public async Task<string> SaveToDoItem(ToDoItem item)
        {
            if (_context.ToDoItems == null)
            {
                return null;
            }
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();

            return "item saved";
        }

        public async Task<string> UpdateToDoItem(int id, ToDoItem item)
        {
            if (id != item.ItemId)
            {
                return null;
            }
            
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!isItemExist(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return "ToDoItem Updated";
        }
    }
}
