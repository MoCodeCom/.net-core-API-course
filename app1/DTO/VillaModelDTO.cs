using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app1.DTO
{
    public class VillaModelDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public int sqft { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateData { get; set; }
        public DateTime UpdateData { get; set; }
    }

}
