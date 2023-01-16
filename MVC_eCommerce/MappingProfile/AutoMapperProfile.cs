using AutoMapper;
using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Account;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Home;
using MVC_eCommerce.Models.Order;

namespace MVC_eCommerce.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tbl_Product, ProductVM>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
               .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage))
               .ForMember(dest => dest.IsFeatured, opt => opt.MapFrom(src => src.IsFeatured))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<ProductVM, Tbl_Product>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
               .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage))
               .ForMember(dest => dest.IsFeatured, opt => opt.MapFrom(src => src.IsFeatured))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Tbl_Category, CategoryVM>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
               .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
               .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CategoryVM, Tbl_Category>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                  .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                  .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<Tbl_Order, OrderVM>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                 .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => src.Tbl_Cart));

            CreateMap<OrderVM, Tbl_Order>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                 .ForMember(dest => dest.Tbl_Cart, opt => opt.MapFrom(src => src.Cart));

            CreateMap<Tbl_Cart, CartItemVM>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                      .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                      .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                      .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                      .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                      .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Tbl_Status, StatusVM>()
                           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                         .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName))
                         .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                         .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            CreateMap<StatusVM, Tbl_Status>()
                           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                         .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName))
                         .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                         .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            CreateMap<EmploeeVM, Tbl_Emploee>()
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Tbl_Emploee, EmploeeVM>()
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<EmploeeProfileVM, Tbl_Emploee>()
                         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                       .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Tbl_Emploee, EmploeeProfileVM>()
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}