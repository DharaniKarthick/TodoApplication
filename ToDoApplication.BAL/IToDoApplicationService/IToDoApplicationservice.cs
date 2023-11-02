using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Entities;

namespace ToDoApplication.BAL.IToDoApplicationService
{
    public interface IToDoApplicationservice
    {
        Task<List<ToDoItem>> GetToDoItems(string userId, string role);
        Task<ToDoItem> GetToDoItem(int id, string userId, string role);
        Task<string> UpdateToDoItem(int id, ToDoItem item);
        Task<string> SaveToDoItem(ToDoItem item);
        Task<string> DeleteToDoItem(int id, string userId, string role);
        bool isItemExist(int id);
    }
}
