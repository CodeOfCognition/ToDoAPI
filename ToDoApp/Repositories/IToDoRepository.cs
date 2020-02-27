using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public interface IToDoRepository 
    {
        public ToDoItem PopulateRecord(SqlDataReader reader);
        public void Create(ToDoItem item);
         public List<ToDoItem> GetAll();
        public ToDoItem GetById(int id);
        public void Update(ToDoItem item);
        public void Delete(int id);


    }
}