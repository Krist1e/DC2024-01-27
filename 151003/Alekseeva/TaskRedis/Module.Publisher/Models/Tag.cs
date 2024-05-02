using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Publisher.Models;

[Table("tbl_tag")]
[Index(nameof(Name), IsUnique = true)]
public class Tag
{
    [Column("id")]
    [Key]
    public long Id { get; set; }
    
    [Column("name")]
    [MaxLength(32)] 
    public string Name { get; set; } = string.Empty;
    
    public List<Tweet> Tweets { get; } = [];
}