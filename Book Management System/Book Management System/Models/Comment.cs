namespace Book_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(10)]
        public string CommenterName { get; set; }

        [Required]
        [StringLength(10)]
        public string IdBook { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual Book Book { get; set; }
    }
}
