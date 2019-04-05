namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartItem")]
    public partial class CartItem
    {
        [StringLength(10)]
        public string Id { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [Required]
        [StringLength(10)]
        public string IdBook { get; set; }

        [Required]
        [StringLength(10)]
        public string IdCard { get; set; }

        public bool IsActive { get; set; }

        public virtual Book Book { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
