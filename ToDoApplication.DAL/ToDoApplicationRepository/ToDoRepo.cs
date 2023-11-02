using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<string> DeleteToDoItem(int id)
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

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return "ToDo Item Deleted";
        }

        public async Task<ToDoItem> GetToDoItem(int id)
        {
           
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            return toDoItem;
        }

        public async Task<List<ToDoItem>> GetToDoItems()
        {            
            var res= await _context.ToDoItems.ToListAsync();
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
