namespace Book_Management_System.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Book_Management_System.Resources.Admin.User;

    [Table("User")]
    public partial class User
    {
        [StringLength(10)]
        public string Id { get; set; }

        [Display(Name = "Firstname", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Lastname", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Resource))]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [StringLength(100)]
        public string Email { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public bool Gender { get; set; }

        [Display(Name = "User_IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        public virtual Account Account { get; set; }
    }
}
