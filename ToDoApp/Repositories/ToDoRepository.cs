using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data;
using ToDoApp.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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

        public void ExecuteCommand(SqlCommand command)
        {
            command.Connection = _connection;
            command.CommandType = CommandType.Text;
            _connection.Open();
            try
                {
            command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
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
        public async Task<ToDoItem> GetById(int id)
        {
            using (var command = new SqlCommand("crud_ToDoDataRead", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", id));
                ToDoItem response = null;
                await _connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        response = PopulateRecord(reader);
                    }
                }

                return response;
            }
        }

        public ToDoItem PopulateRecord(SqlDataReader reader)
        {
            return new ToDoItem
            {
                //Id = int.Parse(reader.GetString(0)),
                //Name = reader.GetString(1),                
                //IsComplete = bool.Parse(reader.GetString(2))
            
                Id = (int)reader["Id"],
                Name = reader["ItemName"].ToString(),
                IsComplete = (bool)reader["IsComplete"]

            };
        }

         public ToDoItem GetRecord(SqlCommand command)
        {
            ToDoItem record = null;
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return record;
        }

         public IEnumerable<ToDoItem> GetRecords(SqlCommand command)
        {
            var list = new List<ToDoItem>();
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

       
        public IEnumerable<ToDoItem> ExecuteStoredProc(SqlCommand command)
        {
            var list = new List<ToDoItem>();
            command.Connection = _connection;
            command.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }
    }
}