using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_bi.Models;

public class Library
{
    [Key]
    public int id { get; set; }

    [Required]
    public string? name { get; set; }

    public string? location { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; }

}