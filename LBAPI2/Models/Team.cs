using LBAPI2.Models;
using System.ComponentModel.DataAnnotations;
namespace LBAPI2.Models
{
    public class Team
    {
        public Team()
        {
            Squad = new List<Player>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Div { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public virtual ICollection<Player> Squad { get; set; }
        public string Description { get; set; }
    }
}
