namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Book_Management_System.Resources.Admin.BookCategory;

    [Table("BookCategory")]
    public partial class BookCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="CateName",ResourceType = typeof(Resources))]
        public string CateName { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources))]
        public bool IsActive { get; set; }

        [StringLength(500)]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
