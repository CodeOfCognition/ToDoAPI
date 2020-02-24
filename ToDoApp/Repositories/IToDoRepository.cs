using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public interface IToDoRepository 
    {
        public void ExecuteCommand(SqlCommand command);
        public ToDoItem PopulateRecord(SqlDataReader reader);
        public IEnumerable<ToDoItem> GetRecords(SqlCommand command);
        public ToDoItem GetRecord(SqlCommand command);
        public IEnumerable<ToDoItem> ExecuteStoredProc(SqlCommand command);
    }
}