using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ToDoApp.Models;
using ToDoApp.Repositories;

namespace ToDoApp.Controllers
{

    [Route("api/ToDoItems")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _ToDoRepository;

        public ToDoController(ToDoRepository toDoRepository)
        {
            _ToDoRepository = toDoRepository;
        }

        // GET: api/GetToDoItems
        [HttpGet]
        public List<ToDoItem> GetToDoItems()
        {
            return _ToDoRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ToDoItem GetToDoItem(int id)
        {
            return _ToDoRepository.GetById(id);
        }

        [HttpPost]
        public void PostToDoItem([FromForm] ToDoItem toDoItem)
        {
            _ToDoRepository.Create(toDoItem);
        }

/*
        
        
        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {

        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem([FromForm]ToDoItem toDoItem)
        {
             _context.MyToDoItems.Add(myToDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyToDoItem), new { id = myToDoItem.Id }, myToDoItem);
       


       [HttpPost]
        public IActionResult Create(Inventory inventory)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Inventory (ItemName, IsComplete, Id) Values ('{inventory.ItemName}', '{inventory.IsComplete}','{inventory.Id}')";
 
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
 
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
 
            return View();
        }
        
        }
        */
        
    }
}