using System;
using System.ComponentModel.DataAnnotations;

namespace automobileCar.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Established Year")]
        public int EstablishedYear { get; set; }

        [Display(Name="Brand Logo")]
        public string BrandLogo { get; set; }
    }
}
