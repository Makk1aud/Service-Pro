using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions.FilterParameters
{
    public class ProductParameters : RequestParameters
    {
        
        public int? Id { get; set; }
        public int? ExpertId { get; set; }
        public string SearchName { get; set; }
        public int? ProductTypeId { get; set; }
        public int? ClientId { get; set; }
        public string SearchDesc { get; set; }
        public int? ProductStatusId { get; set; }
    }
}
