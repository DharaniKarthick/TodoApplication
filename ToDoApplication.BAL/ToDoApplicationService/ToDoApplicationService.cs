using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Entities;
using ToDoApplication.BAL.IToDoApplicationService;
using ToDoApplication.DAL.IToDoApplicationRepository;

namespace ToDoApplication.BAL.ToDoApplicationService
{
    public class ToDoApplicationService : IToDoApplicationservice
    {
        IToDoRepo _dal;
        public ToDoApplicationService(IToDoRepo dal)
        {
            _dal = dal;
        }
        public Task<string> DeleteToDoItem(int id, string userId, string role)
        {
            return _dal.DeleteToDoItem(id,userId,role);
        }

        public Task<ToDoItem> GetToDoItem(int id, string userId, string role)
        {
            return _dal.GetToDoItem(id, userId, role);
        }

        public Task<List<ToDoItem>> GetToDoItems(string userId, string role)
        {
            return _dal.GetToDoItems(userId,role);
        }

        public bool isItemExist(int id)
        {
            return _dal.isItemExist(id);
        }

        public Task<string> SaveToDoItem(ToDoItem item)
        {
            return _dal.SaveToDoItem(item);
        }

        public Task<string> UpdateToDoItem(int id, ToDoItem item)
        {
           return _dal.UpdateToDoItem(id, item);
        }
    }
}
