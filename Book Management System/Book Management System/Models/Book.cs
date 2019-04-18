namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Book_Management_System.Resources.Admin.Book;

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
        [Display(Name = "Title", ResourceType = typeof(Resources))]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "EnTitle", ResourceType = typeof(Resources))]
        public string EnTitle { get; set; }

 
        [Display(Name = "Summary", ResourceType = typeof(Resources))]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "EnSummary", ResourceType = typeof(Resources))]
        public string EnSummary { get; set; }

        [Display(Name = "ImageURL", ResourceType = typeof(Resources))]
        public string ImageURL { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "ISBN", ResourceType = typeof(Resources))]
        public string ISBN { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resources))]
        public double Price { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Resources))]
        public int Quantity { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "IdAuthor", ResourceType = typeof(Resources))]
        public string IdAuthor { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "IdPublisher", ResourceType = typeof(Resources))]
        public string IdPublisher { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "IdCategory", ResourceType = typeof(Resources))]
        public string IdCategory { get; set; }

        [Display(Name = "Createdate", ResourceType = typeof(Resources))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Modifieddate", ResourceType = typeof(Resources))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources))]
        public bool IsActive { get; set; }

        [Display(Name = "Author", ResourceType = typeof(Resources))]
        public virtual Author Author { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Resources))]
        public virtual BookCategory BookCategory { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(Resources))]
        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
