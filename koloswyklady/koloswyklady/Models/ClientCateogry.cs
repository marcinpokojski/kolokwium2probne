using System.ComponentModel.DataAnnotations;

namespace koloswyklady.Models;

public class ClientCateogry
{
    [Key] [Required] public int IdClientCategory { get; set; }
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] public int DiscountPerc { get; set; }
    
    public virtual ICollection<Client> Clients { get; set; }
    
}