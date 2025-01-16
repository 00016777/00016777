using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Models.MealDTOs
{
    public class MealDtos
    {
        public List<MealDto> Meals { get; set; }

        public List<MealDto> MealChildren { get; set; }
        
        public List<MealDto>? Path { get; set; }
    }
}
