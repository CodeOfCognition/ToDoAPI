using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data;
using ToDoApp.Models;
using Microsoft.Extensions.Configuration;

namespace ToDoApp.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public ToDoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
            var connectionString = _configuration.GetConnectionString("ToDoDataDatabase");
            _connection = new SqlConnection(connectionString);
        }


        public List<ToDoItem> GetAll()
        {
            using (var command = new SqlCommand("crud_ToDoDataReadAll", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var response = new List<ToDoItem>();
                _connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Add(PopulateRecord(reader));
                    }
                }

                return response;
            }
        }
        public ToDoItem GetById(int id)
        {
            using (var command = new SqlCommand("crud_ToDoDataRead", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", id));
                ToDoItem response = null;
                _connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response = PopulateRecord(reader);
                    }
                }

                return response;
            }
        }

        public void Create(ToDoItem item)
        {
            using (var command = new SqlCommand("crud_ToDoDataCreate", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@itemName", item.Name));
                command.Parameters.Add(new SqlParameter("@id", item.Id));
                command.Parameters.Add(new SqlParameter("@IsComplete", item.IsComplete));
                _connection.Open();
                command.ExecuteNonQuery();
                return;
            }
        }

        public void Update(ToDoItem item)
        {
            using (var command = new SqlCommand("crud_ToDoDataUpdate", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@itemName", item.Name));
                command.Parameters.Add(new SqlParameter("@id", item.Id));
                command.Parameters.Add(new SqlParameter("@IsComplete", item.IsComplete));
                _connection.Open();
                command.ExecuteNonQuery();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var command = new SqlCommand ("crud_ToDoDataDelete", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", id));
                _connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ToDoItem PopulateRecord(SqlDataReader reader)
        {
            return new ToDoItem
            {
                Id = (int)reader["Id"],
                Name = reader["ItemName"].ToString(),
                IsComplete = (bool)reader["IsComplete"]
            };
        }

    }
}