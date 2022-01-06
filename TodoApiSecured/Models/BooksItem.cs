using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class BooksItem
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [StringLength(30, MinimumLength = 3)]
        public string Genre { get; set; }

        public bool IsAvailable { get; set; }
    }
}
