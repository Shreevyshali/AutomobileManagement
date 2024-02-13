using automobileCar.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automobile.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
