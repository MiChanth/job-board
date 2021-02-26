using System.ComponentModel.DataAnnotations;

namespace JobBoardApi.Dtos
{
    public record UpdateJobDto{
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Country { get; set; }
    }
}