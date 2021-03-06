namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Book_Management_System.Resources.Admin.User;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Carts = new HashSet<Cart>();
        }

        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "User_Name", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "IdRole", ResourceType = typeof(Resource))]
        [StringLength(10)]
        public string IdRole { get; set; }

        [Display(Name = "User_IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual User User { get; set; }
    }
}
