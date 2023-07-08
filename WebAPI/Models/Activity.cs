using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public int InchargeId { get; set; }
        public User User { get; set; }
        public bool BolActive { get; set; }
    }
}
