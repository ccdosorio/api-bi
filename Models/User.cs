using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bi.Models;

public class User
{
    [Key]
    public int id { get; set; }

    [Required]
    public string? name { get; set; }

    [Required]
    public string? email { get; set; }

    [Required]
    public string? password { get; set; }

    [Column("created_at")]
    public DateTime createdAt { get; set; }
}