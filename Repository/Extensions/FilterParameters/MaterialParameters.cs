using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions.FilterParameters
{
    public class MaterialParameters : RequestParameters
    {
        public string MaterialTitle { get; set; }
        public int? MaterialTypeId { get; set; }

        private int minPrice = 0;

        public int MinPrice
        {
            get { return minPrice; }
            set { minPrice = value > 0 ? value : minPrice; }
        }

        private int maxPrice = int.MaxValue;
        public int MaxPrice
        {
            get { return maxPrice; }
            set { maxPrice = value > 0 ? value : minPrice; }
        }
    }
}
