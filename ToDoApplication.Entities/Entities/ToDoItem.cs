using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoApplication.Entities;

namespace TodoApplication.Entities
{
    public class ToDoItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "ItemStatus is required")]
        [Column(TypeName = "bit")]
        public bool ItemStatus { get; set; }
        public string? UserId { get; set; }
    }


}
