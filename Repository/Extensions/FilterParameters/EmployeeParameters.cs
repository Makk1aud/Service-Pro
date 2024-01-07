using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions.FilterParameters
{
    public class EmployeeParameters
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public int? PositonId { get; set; }
        public string Lastname { get; set; }
    }
}
