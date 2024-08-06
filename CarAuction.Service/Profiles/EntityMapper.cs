using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.DTOs.Models;
using CarAuction.Service.DTOs.Brands;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.DTOs.Fuels;
using CarAuction.Service.DTOs.Colors;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Settings;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.DTOs.Bans;
using CarAuction.Service.DTOs.Statuses;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.DTOs.Comments;

namespace CarAuction.Service.Profiles
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();
            CreateMap<BlogGetDto, BlogUpdateDto>().ReverseMap();
            CreateMap<BlogGetDto, Blog>().ReverseMap();

            CreateMap<Brand, BrandPostDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();
            CreateMap<BrandGetDto, BrandUpdateDto>().ReverseMap();
            CreateMap<BrandGetDto, Brand>().ReverseMap();

            CreateMap<Model, ModelPostDto>().ReverseMap();
            CreateMap<Model, ModelUpdateDto>().ReverseMap();
            CreateMap<ModelGetDto, ModelUpdateDto>().ReverseMap();
            CreateMap<ModelGetDto, Model>().ReverseMap();

            CreateMap<Fuel, FuelPostDto>().ReverseMap();
            CreateMap<Fuel, FuelUpdateDto>().ReverseMap();
            CreateMap<FuelGetDto, FuelUpdateDto>().ReverseMap();
            CreateMap<FuelGetDto, Fuel>().ReverseMap();

            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();
            CreateMap<BlogUpdateDto, BlogGetDto>().ReverseMap().ForMember(dto => dto.TagIds,
               opt => opt.MapFrom(src =>
               src.Tags.Select(x => x.Id).ToList()));

            CreateMap<BlogGetDto, Blog>().ReverseMap().ForMember(dto => dto.Author, opt => opt.MapFrom(src =>
               src.Admin.Name + " " + src.Admin.Surname)).ForMember(dto => dto.Tags,
               opt => opt.MapFrom(src =>
               src.BlogTags.Select(x => x.Tag).ToList()));

            CreateMap<Tag, TagPostDto>().ReverseMap();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
            CreateMap<TagGetDto, TagUpdateDto>().ReverseMap();
            CreateMap<TagGetDto, Tag>().ReverseMap();

            CreateMap<Status, StatusPostDto>().ReverseMap();
            CreateMap<Status, StatusUpdateDto>().ReverseMap();
            CreateMap<StatusGetDto, StatusUpdateDto>().ReverseMap();
            CreateMap<StatusGetDto, Status>().ReverseMap();


            CreateMap<Setting, SettingPostDto>().ReverseMap();
            CreateMap<Setting, SettingUpdateDto>().ReverseMap();
            CreateMap<SettingGetDto, SettingUpdateDto>().ReverseMap();
            CreateMap<SettingGetDto, Setting>().ReverseMap();

            CreateMap<Ban, BanPostDto>().ReverseMap();
            CreateMap<Ban, BanUpdateDto>().ReverseMap();
            CreateMap<BanGetDto, BanUpdateDto>().ReverseMap();
            CreateMap<BanGetDto, Ban>().ReverseMap();

            CreateMap<Color, ColorPostDto>().ReverseMap();
            CreateMap<Color, ColorUpdateDto>().ReverseMap();
            CreateMap<ColorGetDto, ColorUpdateDto>().ReverseMap();
            CreateMap<ColorGetDto, Color>().ReverseMap();

            CreateMap<Category, CategoryPostDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryGetDto, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryGetDto, Category>().ReverseMap();

            CreateMap<Car, CarPostDto>().ReverseMap();
            CreateMap<Car, CarUpdateDto>().ReverseMap();
            CreateMap<CarGetDto, CarUpdateDto>().ReverseMap();


            CreateMap<CarGetDto, Car>().ReverseMap().ForMember(dto => dto.Admin, opt => opt.MapFrom(src =>
               src.Admin.Name + " " + src.Admin.Surname)).ForMember(dto => dto.AuctionDate, opt => opt.MapFrom(src => src.CarAuctionDetail.AuctionDate)).ForMember(dto => dto.InitialPrice, opt => opt.MapFrom(src => src.CarAuctionDetail.InitialPrice)).ForMember(dto => dto.FinishDate, opt => opt.MapFrom(src => src.CarAuctionDetail.FinishDate)).ForMember(dto => dto.WinnerName, opt => opt.MapFrom(src => src.CarAuctionDetail.Winner.Name + " " + src.CarAuctionDetail.Winner.Surname)).ForMember(dto => dto.AuctionWinPrice, opt => opt.MapFrom(src => src.CarAuctionDetail.AuctionWinPrice));

            CreateMap<AppUser, UserGetDto>().ReverseMap();
            CreateMap<BidPostDto, Bid>().ReverseMap();
            CreateMap<BidGetDto, Bid>().ReverseMap().ForMember(dto => dto.UserName, opt => opt.MapFrom(src =>
               src.User.Name + " " + src.User.Surname));

            CreateMap<CommentPostDto, Comment>().ReverseMap();
            CreateMap<CommentGetDto, Comment>().ReverseMap().ForMember(dto => dto.UserName, opt => opt.MapFrom(src =>
               src.User.Name + " " + src.User.Surname));

        }
    }
}
