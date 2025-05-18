using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class ProductDataModel
    {
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [Required]
        [MaxLength(5)]
        public string colour { get; set; }
    }
}
