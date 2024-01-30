using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions.FilterParameters
{
    public class ClientParameters : RequestParameters
    {
        public int? Id { get; set; }
        public string PhoneNum { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
