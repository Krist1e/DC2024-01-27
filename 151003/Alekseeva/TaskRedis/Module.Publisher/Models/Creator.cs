using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Publisher.Models;

[Table("tbl_creator")]
[Index(nameof(Login), IsUnique = true)]
public class Creator
{
    [Column("id")]
    [Key]
    public long Id { get; set; }

    [Column("login")] 
    [MaxLength(64)]
    public string Login { get; set; } = string.Empty;

    [Column("password")]
    [MaxLength(128)]
    public string Password { get; set; } = string.Empty;

    [Column("firstname")]
    [MaxLength(64)] 
    public string FirstName { get; set; } = string.Empty;

    [Column("lastname")]
    [MaxLength(64)] 
    public string LastName { get; set; } = string.Empty;
    
    public List<Tweet> Tweets { get; } = new();
}