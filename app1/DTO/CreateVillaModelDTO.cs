using System.ComponentModel.DataAnnotations;

namespace app1.DTO
{
    public class CreateVillaModelDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public int sqft { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateData { get; set; }
        public DateTime UpdateData { get; set; }
    }
}
