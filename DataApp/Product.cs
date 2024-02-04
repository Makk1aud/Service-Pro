//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Coursework.DataApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Deal = new HashSet<Deal>();
            this.Expenditure = new HashSet<Expenditure>();
        }
    
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string prod_description { get; set; }
        public int pr_type_id { get; set; }
        public int client_id { get; set; }
        public int manager_id { get; set; }
        public Nullable<int> expert_id { get; set; }
        public int pr_status_id { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deal> Deal { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenditure> Expenditure { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
