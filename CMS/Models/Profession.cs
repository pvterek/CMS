using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Profession
    {
        [Key]
        public int ProfessionId { get; set; }

        [Required]
        [DisplayName("Profession name")]
        public string Name { get; set; }
    }
}
