using Automobile.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Automobile.ViewModel
{
    public class CarVM
    {
        public List<Car> Cars { get; set; } = new List<Car>();

        public IEnumerable<SelectListItem> Brand { get; set; }

        public Guid? BrandId { get; set; }
    }
}
