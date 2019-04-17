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
        [Display(Name = "IdCart", ResourceType = typeof(Resources.Admin.OrderResource.Resources))]
        public string IdCard { get; set; }

        [Display(Name = "CreateDate", ResourceType = typeof(Resources.Admin.OrderResource.Resources))]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "MethodPay", ResourceType = typeof(Resources.Admin.OrderResource.Resources))]
        public string MethodPayment { get; set; }   

        [Required]
        [StringLength(200)]
        [Display(Name = "Status", ResourceType = typeof(Resources.Admin.OrderResource.Resources))]
        public string Status { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
