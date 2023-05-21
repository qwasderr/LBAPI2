using System.ComponentModel.DataAnnotations;
namespace LBAPI2.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
