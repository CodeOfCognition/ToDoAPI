using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoApp.Models
{
    [Table("Items")]
        public class ToDoItem
        {
            [Key]
            public int Id {get; set;}
            public string Name {get; set;}
            public bool IsComplete {get; set;}
        }
}