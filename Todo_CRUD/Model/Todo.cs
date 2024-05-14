using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_CRUD.Model
{
    public partial class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TaskName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsDone { get; set; } = false;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        DbSet<User>? Users { get; set; }
    }
}
