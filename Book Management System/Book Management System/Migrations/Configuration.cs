namespace Book_Management_System.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Book_Management_System.Models.Model>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Book_Management_System.Models.Model context)
        {
            //  This method will be called after migrating to the latest version.
            //Add Seed Data Authors
            context.Authors.AddOrUpdate(x => x.Id,
                new Models.Author() { Id = "1", Name = "Nguyễn Nhật Ánh", IsActive = true },
                new Models.Author() { Id = "2", Name = "Hồ Biểu Chánh", IsActive = true },
                new Models.Author() { Id = "3", Name = "Hồ Anh Thái", IsActive = true },
                new Models.Author() { Id = "4", Name = "Vũ Trọng Phụng", IsActive = true },
                new Models.Author() { Id = "5", Name = "Nguyễn Ngọc Tư", IsActive = true },
                new Models.Author() { Id = "6", Name = "Nguyễn Quang Sáng", IsActive = true });
            //Add Seed Data Caterogies
            context.BookCategories.AddOrUpdate(x => x.Id,
                new Models.BookCategory()
                {
                    Id = "1",
                    CateName = "Sách ngoại văn",
                    Description = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    IsActive = true
                },
                new Models.BookCategory()
                {
                    Id = "2",
                    CateName = "Sách kinh tế",
                    Description = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    IsActive = true
                },
                new Models.BookCategory()
                {
                    Id = "3",
                    CateName = "Sách văn học trong nước",
                    Description = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    IsActive = true
                },
                new Models.BookCategory()
                {
                    Id = "4",
                    CateName = "Sách thiếu nhi",
                    Description = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    IsActive = true
                },
                new Models.BookCategory()
                {
                    Id = "5",
                    CateName = "Tiểu thuyết",
                    Description = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    IsActive = true
                });
            ////Add Seed Data Publisher
            context.Publishers.AddOrUpdate(x => x.Id,
                new Models.Publisher() { Id = "1", Name = "Nhà xuất bản Chính trị Quốc gia" },
                new Models.Publisher() { Id = "2", Name = "Nhà xuất bản Tư pháp" },
                new Models.Publisher() { Id = "3", Name = "Nhà xuất bản Hồng Đức" },
                new Models.Publisher() { Id = "4", Name = "Nhà xuất bản Quân đội" },
                new Models.Publisher() { Id = "5", Name = "Nhà xuất bản Công an nhân dân" });
            ////Add Seed Data Books
            ///
            DateTime Now = DateTime.Now;
            context.Books.AddOrUpdate(x => x.Id,
                new Models.Book()
                {
                    Id = "1",
                    Title = "Kỹ năng đi trước đam mê",
                    Summary = "Niềm tin sáo rỗng này không những sai sót ở chỗ là những đam mê tồn tại sẵn có thường hiếm hoi.",
                    ImageURL = "/Assets/images/ky-nang-di-truoc-dam-me.jpg",
                    ISBN = "1111",
                    Price = 10,
                    Quantity = 10,
                    IdAuthor = "1",
                    IdPublisher = "1",
                    IdCategory = "1",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "2",
                    Title = "Cùng con trưởng thành",
                    Summary = "Sau khi đưa ra dẫn chứng chống lại niềm tin vào đam mê, Newport bắt đầu cuộc hành trình khám phá thực tế ",
                    ImageURL = "/Assets/images/cung-con-truong-thanh.jpg",
                    ISBN = "2222",
                    Price = 20,
                    Quantity = 15,
                    IdAuthor = "2",
                    IdPublisher = "2",
                    IdCategory = "2",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "3",
                    Title = "Nghệ thuật đàm phán",
                    Summary = "Nói cách khác, cách bạn làm việc thì quan trọng hơn nhiều so với công việc bạn làm.",
                    ImageURL = "/Assets/images/nghe-thuat-dam-phan.jpg",
                    ISBN = "3333",
                    Price = 30,
                    Quantity = 20,
                    IdAuthor = "3",
                    IdPublisher = "3",
                    IdCategory = "3",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "4",
                    Title = "Biết hài lòng",
                    Summary = "Đông Tử đã cho độc giả được lắng mình chiêm nghiệm và yêu thương cha hơn qua cuốn sách Cùng con trưởng thành.",
                    ImageURL = "/Assets/images/biet-hai-long.jpg",
                    ISBN = "4444",
                    Price = 15,
                    Quantity = 25,
                    IdAuthor = "4",
                    IdPublisher = "4",
                    IdCategory = "4",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "5",
                    Title = "Quân khu Nam Đồng",
                    Summary = "Cùng con trưởng thành nói về vai trò của người cha trong từng giai đoạn phát triển của trẻ.",
                    ImageURL = "/Assets/images/quan-khu-nam-dong-binh-ca.jpg",
                    ISBN = "5555",
                    Price = 12,
                    Quantity = 30,
                    IdAuthor = "5",
                    IdPublisher = "5",
                    IdCategory = "5",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "6",
                    Title = "Kỹ năng đi trước đam mê 2",
                    Summary = "Cuộc sống có bao nhiêu niềm hạnh phúc, dù là cơm ngon áo ấm, cuộc sống như ý hay ước mơ.",
                    ImageURL = "/Assets/images/ky-nang-di-truoc-dam-me.jpg",
                    ISBN = "1111",
                    Price = 10,
                    Quantity = 10,
                    IdAuthor = "1",
                    IdPublisher = "1",
                    IdCategory = "1",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "7",
                    Title = "Cùng con trưởng thành 2",
                    Summary = "Đọc cuốn sách này, mỗi người sẽ cảm nhận được những khoảnh khắc vô cùng ý nghĩa khi người đàn ông.",
                    ImageURL = "/Assets/images/cung-con-truong-thanh.jpg",
                    ISBN = "2222",
                    Price = 20,
                    Quantity = 15,
                    IdAuthor = "2",
                    IdPublisher = "2",
                    IdCategory = "2",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "8",
                    Title = "Nghệ thuật đàm phán 2",
                    Summary = "Cuốn sách Cùng con trưởng thành người đọc là cuốn sách hay và ý nghĩa về tình cha con, ",
                    ImageURL = "/Assets/images/nghe-thuat-dam-phan.jpg",
                    ISBN = "3333",
                    Price = 30,
                    Quantity = 20,
                    IdAuthor = "3",
                    IdPublisher = "3",
                    IdCategory = "3",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "9",
                    Title = "Biết hài lòng 2",
                    Summary = "Đây là những gì mà một người cha phải làm cho đứa con yêu của mình”.",
                    ImageURL = "/Assets/images/biet-hai-long.jpg",
                    ISBN = "4444",
                    Price = 15,
                    Quantity = 25,
                    IdAuthor = "4",
                    IdPublisher = "4",
                    IdCategory = "4",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "10",
                    Title = "Quân khu Nam Đồng 2",
                    Summary = "Quyển sách cho chúng ta thấy cách Trump làm việc mỗi ngày - cách ông điều hành công việc .",
                    ImageURL = "/Assets/images/quan-khu-nam-dong-binh-ca.jpg",
                    ISBN = "5555",
                    Price = 12,
                    Quantity = 30,
                    IdAuthor = "5",
                    IdPublisher = "5",
                    IdCategory = "5",
                    IsActive = true,
                    CreateDate = Now
                },
                 new Models.Book()
                 {
                     Id = "11",
                     Title = "Kỹ năng đi trước đam mê 3",
                     Summary = "Quyển sách đi sâu vào đầu óc của một doanh nhân xuất sắc và khám phá một cách khoa học .",
                     ImageURL = "/Assets/images/ky-nang-di-truoc-dam-me.jpg",
                     ISBN = "1111",
                     Price = 10,
                     Quantity = 10,
                     IdAuthor = "1",
                     IdPublisher = "1",
                     IdCategory = "1",
                     IsActive = true,
                     CreateDate = Now
                 },
                new Models.Book()
                {
                    Id = "12",
                    Title = "Cùng con trưởng thành 3",
                    Summary = "Đây là một cuốn sách thú vị về đàm phán và kinh doanh – và là một cuốn sách nên đọc cho bất kỳ ai quan tâm đến đầu tư.",
                    ImageURL = "/Assets/images/cung-con-truong-thanh.jpg",
                    ISBN = "2222",
                    Price = 20,
                    Quantity = 15,
                    IdAuthor = "2",
                    IdPublisher = "2",
                    IdCategory = "2",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "13",
                    Title = "Nghệ thuật đàm phán 3",
                    Summary = "Biết Hài Lòng” là một quyển sách khá hay của Leo Babauta. Nếu bạn đang gặp khó khăn.",
                    ImageURL = "/Assets/images/nghe-thuat-dam-phan.jpg",
                    ISBN = "3333",
                    Price = 30,
                    Quantity = 20,
                    IdAuthor = "3",
                    IdPublisher = "3",
                    IdCategory = "3",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "14",
                    Title = "Biết hài lòng 3",
                    Summary = "Đọc trong một giờ. Không phải đọc sơ sơ rồi bỏ đó, mà là đọc thực sự.",
                    ImageURL = "/Assets/images/biet-hai-long.jpg",
                    ISBN = "4444",
                    Price = 15,
                    Quantity = 25,
                    IdAuthor = "4",
                    IdPublisher = "4",
                    IdCategory = "4",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "15",
                    Title = "Quân khu Nam Đồng 3",
                    Summary = "Dale Carnegie từng tuyên bố thu nhập một triệu đôla còn dễ dàng hơn là thêm vào ngôn ngữ của nhân loại một tựa sách được cả thế giới biết đến..",
                    ImageURL = "/Assets/images/quan-khu-nam-dong-binh-ca.jpg",
                    ISBN = "5555",
                    Price = 12,
                    Quantity = 30,
                    IdAuthor = "5",
                    IdPublisher = "5",
                    IdCategory = "5",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "16",
                    Title = "Kỹ năng đi trước đam mê 4",
                    Summary = "Vivamus suscipit tortor eget felis porttitor volutpat. Cras ultricies ligula sed magna dictum porta.",
                    ImageURL = "/Assets/images/ky-nang-di-truoc-dam-me.jpg",
                    ISBN = "1111",
                    Price = 10,
                    Quantity = 10,
                    IdAuthor = "1",
                    IdPublisher = "1",
                    IdCategory = "1",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "17",
                    Title = "Cùng con trưởng thành 4",
                    Summary = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    ImageURL = "/Assets/images/cung-con-truong-thanh.jpg",
                    ISBN = "2222",
                    Price = 20,
                    Quantity = 15,
                    IdAuthor = "2",
                    IdPublisher = "2",
                    IdCategory = "2",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "18",
                    Title = "Nghệ thuật đàm phán 4",
                    Summary = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    ImageURL = "/Assets/images/nghe-thuat-dam-phan.jpg",
                    ISBN = "3333",
                    Price = 30,
                    Quantity = 20,
                    IdAuthor = "3",
                    IdPublisher = "3",
                    IdCategory = "3",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "19",
                    Title = "Biết hài lòng 4",
                    Summary = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    ImageURL = "/Assets/images/biet-hai-long.jpg",
                    ISBN = "4444",
                    Price = 15,
                    Quantity = 25,
                    IdAuthor = "4",
                    IdPublisher = "4",
                    IdCategory = "4",
                    IsActive = true,
                    CreateDate = Now
                },
                new Models.Book()
                {
                    Id = "20",
                    Title = "Quân khu Nam Đồng 4",
                    Summary = "Những lần xuất bản tiếp theo với nhịp độ dồn dập để đáp ứng một yêu cầu tăng lên không ngừng.",
                    ImageURL = "/Assets/images/quan-khu-nam-dong-binh-ca.jpg",
                    ISBN = "5555",
                    Price = 12,
                    Quantity = 30,
                    IdAuthor = "5",
                    IdPublisher = "5",
                    IdCategory = "5",
                    IsActive = true,
                    CreateDate = Now
                });
            ///Add Seed Data Roles
            context.Roles.AddOrUpdate(x => x.Id,
                new Models.Role() { Id = "1", RoleName = "Administrator", IsActive = true },
                new Models.Role() { Id = "2", RoleName = "Moderator", IsActive = true },
                new Models.Role() { Id = "3", RoleName = "User", IsActive = true });
            ////Add Seed Data Users
            context.Users.AddOrUpdate(x => x.Id,
                new Models.User() { Id = "1", FirstName = "Nguyễn", LastName = "Thiện Tâm", Birthday = DateTime.Parse("1997/07/03"), Email = "nguyenthientam317@gmail.com", Address = "Somewhere in the world", Gender = true, Avatar = "/Assets/images/user1.jpg", IsActive = true },
                new Models.User() { Id = "2", FirstName = "Đặng", LastName = "Hiếu", Birthday = DateTime.Parse("1997/07/02"), Email = "myzusstevecc@gmail.com", Address = "Somewhere in the world", Gender = true, Avatar = "/Assets/images/user2.jpg", IsActive = true },
                new Models.User() { Id = "3", FirstName = "Nguyễn", LastName = "Ngọc Phú", Birthday = DateTime.Parse("1997/07/01"), Email = "lmhtcc@gmail.com", Address = "Somewhere in the world", Gender = true, Avatar = "/Assets/images/user3.jpg", IsActive = true });
            //Add Seed Data Accounts
            context.Accounts.AddOrUpdate(x => x.Id,
                new Models.Account() { Id = "1", UserName = "thientam123", Password = Common.Encryptor.ToMD5("Abc123"), IdRole = "1", IsActive = true },
                new Models.Account() { Id = "2", UserName = "hieucho123", Password = Common.Encryptor.ToMD5("Abc123"), IdRole = "2", IsActive = true },
                new Models.Account() { Id = "3", UserName = "phucho123", Password = Common.Encryptor.ToMD5("Abc123"), IdRole = "3", IsActive = true });
            //Add Seed Data Comments
            context.Comments.AddOrUpdate(x => x.Id,
                new Models.Comment()
                {
                    Id = "1",
                    CommenterName = "Steve",
                    IdBook = "1",
                    IsActive = true,
                    Content = "Sách hay nội dung hấp dẫn",
                    CreateDate = Now

                },
                new Models.Comment()
                {
                    Id = "2",
                    CommenterName = "Rendo",
                    IdBook = "1",
                    IsActive = true,
                    Content = "Giá rẻ thiết kế đẹp",
                    CreateDate = Now
                },
                new Models.Comment()
                {
                    Id = "3",
                    CommenterName = "PhuCai",
                    IdBook = "1",
                    IsActive = true,
                    Content = "Nội dung phong phú, nên mua",
                    CreateDate = Now
                });
            context.Carts.AddOrUpdate(l => l.Id,
                new Models.Cart()
                {
                    Id = "1",
                    IdUser = "1",
                    CreateDate = Now,
                    IsActive = true,
                    Total = 50
                }
                );
            context.CartItems.AddOrUpdate(l => l.Id,
                new Models.CartItem()
                {
                    Id = "1",
                    IdCard = "1",
                    IdBook = "1",
                    Quantity = 2,
                    IsActive = true,
                },
                 new Models.CartItem()
                 {
                     Id = "2",
                     IdCard = "1",
                     IdBook = "2",
                     Quantity = 1,
                     IsActive = true,
                 }
                );
            //context.Carts.AddOrUpdate(l => l.Id,
            //   new Models.Cart()
            //   {
            //       Id = "2",
            //       IdUser = "1",
            //       CreateDate = Now,
            //       IsActive = false,
            //       Total = 50
            //   }
            //   );
            //context.CartItems.AddOrUpdate(l => l.Id,
            //    new Models.CartItem()
            //    {
            //        Id = "3",
            //        IdCard = "2",
            //        IdBook = "1",
            //        Quantity = 2,
            //        IsActive = true,
            //    },
            //     new Models.CartItem()
            //     {
            //         Id = "4",
            //         IdCard = "2",
            //         IdBook = "2",
            //         Quantity = 1,
            //         IsActive = true,
            //     }
            //    );
            //context.Carts.AddOrUpdate(l => l.Id,
            //  new Models.Cart()
            //  {
            //      Id = "3",
            //      IdUser = "1",
            //      CreateDate = Now,
            //      IsActive = false,
            //      Total = 50
            //  }
            //  );
            //context.CartItems.AddOrUpdate(l => l.Id,
            //    new Models.CartItem()
            //    {
            //        Id = "5",
            //        IdCard = "3",
            //        IdBook = "3",
            //        Quantity = 2,
            //        IsActive = true,
            //    },
            //     new Models.CartItem()
            //     {
            //         Id = "6",
            //         IdCard = "3",
            //         IdBook = "4",
            //         Quantity = 1,
            //         IsActive = true,
            //     }
            //    );
            //context.Carts.AddOrUpdate(l => l.Id,
            //  new Models.Cart()
            //  {
            //      Id = "4",
            //      IdUser = "1",
            //      CreateDate = Now,
            //      IsActive = true,
            //      Total = 50
            //  }
            //  );
            //context.CartItems.AddOrUpdate(l => l.Id,
            //    new Models.CartItem()
            //    {
            //        Id = "7",
            //        IdCard = "4",
            //        IdBook = "3",
            //        Quantity = 2,
            //        IsActive = true,
            //    },
            //     new Models.CartItem()
            //     {
            //         Id = "8",
            //         IdCard = "4",
            //         IdBook = "4",
            //         Quantity = 7,
            //         IsActive = true,
            //     },
            //      new Models.CartItem()
            //      {
            //          Id = "9",
            //          IdCard = "4",
            //          IdBook = "5",
            //          Quantity = 3,
            //          IsActive = true,
            //      }
            //    );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

        }
    }
}

