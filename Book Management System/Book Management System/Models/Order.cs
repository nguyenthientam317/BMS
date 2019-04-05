namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string IdCard { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Status { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
