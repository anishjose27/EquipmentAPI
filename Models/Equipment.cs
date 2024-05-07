using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EquipmentAPI.Models
{
    [Table("Equipment")]
    public class Equipment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public String Name { get; set; }
        [StringLength(50)]
        public String Status { get; set; }
    }
}