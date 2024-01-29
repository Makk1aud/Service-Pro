using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.ViewModels
{
    public class ProductHandlingViewModel
    {
        public Product Product { get; set; }		

        public ProductHandlingViewModel(Product product)
        {
            Product = product;
        }
        public bool WorkButtonEnabled => Product.pr_status_id == 2;

        public bool WaitButtonEnabled => Product.pr_status_id == 1;

        public bool CompleteButtonEnabled => Product.pr_status_id != 3;
    }
}
