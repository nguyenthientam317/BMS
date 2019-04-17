namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Book_Management_System.Resources.Admin.Author;

    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [StringLength(10)]
        public string Id { get; set; }

        [Display(Name = "Author_Name", ResourceType = typeof(Resource))]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Author_IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
