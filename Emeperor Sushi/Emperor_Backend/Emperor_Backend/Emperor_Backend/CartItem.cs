namespace Emperor_Backend
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartItem")]
    public partial class CartItem
    {
        public int ID { get; set; }

        public int Item { get; set; }

        public int Quantity { get; set; }

        public int Cart { get; set; }

        [JsonIgnore]
        public virtual Cart Cart1 { get; set; }

        public virtual MenuItem MenuItem { get; set; }
    }
}
