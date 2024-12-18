using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Projects")] 
    public class Project
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string Name { get; set; }

        [MaxLength(500)] 
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required] 
        public DateTime EndDate { get; set; } 
    }

}
