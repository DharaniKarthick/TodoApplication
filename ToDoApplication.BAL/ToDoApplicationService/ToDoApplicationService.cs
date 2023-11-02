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
        public Task<string> DeleteToDoItem(int id)
        {
            return _dal.DeleteToDoItem(id);
        }

        public Task<ToDoItem> GetToDoItem(int id)
        {
            return _dal.GetToDoItem(id);
        }

        public Task<List<ToDoItem>> GetToDoItems()
        {
            return _dal.GetToDoItems();
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
