using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProjectViewModel
    {
        [Display(Name = "Project Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        [MaxLength(100, ErrorMessage = "Exceeded size limit!")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Exceeded size limit!")]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Project Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Project End Date")]
        public DateTime EndDate { get; set; }
    }
}
