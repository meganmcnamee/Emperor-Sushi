namespace Emperor_Backend
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItem")]
    public partial class MenuItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuItem()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int ID { get; set; }

        public string ItemName { get; set; }

        public double? ItemPrice { get; set; }

        public string ItemDescription { get; set; }

        public bool? InStock { get; set; }

        public string Category { get; set; }

        public string ProductImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
