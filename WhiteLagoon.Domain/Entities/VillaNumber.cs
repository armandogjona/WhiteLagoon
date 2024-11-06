using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLagoon.Domain.Entities
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] // This is the primary key
        [Display(Name = "Villa Number")]
        public int Villa_Number { get; set; } // This is the primary key from another table    
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]//doesnt validate the navigation property
        public Villa Villa { get; set; }
        [Display(Name = "Special Details")]
        public string? SpecialDetails { get; set; }
    }
}
