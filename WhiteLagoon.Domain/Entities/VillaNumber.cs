using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLagoon.Domain.Entities
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] // This is the primary key
        public int Villa_Number { get; set; } // This is the primary key from another table    
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
