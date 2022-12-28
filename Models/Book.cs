using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bi.Models;

public class Book
{
    [Key]
    public int id { get; set; }

    [Required]
    public string? name { get; set; }

    [Column("libraryid")]
    [ForeignKey("libraryId")]
    public int libraryId { get; set; }

    public string? category { get; set; }

    [Column("created_at")]
    public DateTime createdAt { get; set; }

    public virtual Library? Library { get; set; }

}