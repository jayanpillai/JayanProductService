using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class UserDataModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
    }
}
