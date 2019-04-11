namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            CartItems = new HashSet<CartItem>();
            Comments = new HashSet<Comment>();
        }

        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }


        public string ImageURL { get; set; }

        [Required]
        [StringLength(100)]
        public string ISBN { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(10)]
        public string IdAuthor { get; set; }

        [Required]
        [StringLength(10)]
        public string IdPublisher { get; set; }

        [Required]
        [StringLength(10)]
        public string IdCategory { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Author Author { get; set; }

        public virtual BookCategory BookCategory { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
