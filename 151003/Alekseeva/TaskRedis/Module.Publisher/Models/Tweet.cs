using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Publisher.Models;

[Table("tbl_tweet")]
[Index(nameof(Title), IsUnique = true)]
public class Tweet
{
    [Column("id")]
    [Key]
    public long Id { get; set; }

    [Column("creator_id")]
    [ForeignKey("creator")] 
    public long? CreatorId { get; set; }

    [Column("title")]
    [MaxLength(32)] 
    public string Title { get; set; } = string.Empty;
    
    [Column("content")]
    [MaxLength(2048)] 
    public string Content { get; set; } = string.Empty;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? Created { get; init; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? Modified { get; set; }

    public Creator? Creator { get; set; }
    public List<Tag> Tags { get; } = [];
}