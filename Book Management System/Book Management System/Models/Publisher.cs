namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Publisher")]
    public partial class Publisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name_Pulisher", ResourceType = typeof(Resources.Admin.PulisherResource.Resources))]
        public string Name { get; set; }
        [Display(Name = "IsActive_Pulisher", ResourceType = typeof(Resources.Admin.PulisherResource.Resources))]
        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
