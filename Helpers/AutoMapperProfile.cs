using AutoMapper;
using ems_backend.Entities;
using ems_backend.Models;

#pragma warning disable

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Enrollment Request <=> Enrollment
        CreateMap<EnrollmentRequestModel, Enrollment>().ReverseMap();
        // Enrollment Response <=> Enrollment
        CreateMap<EnrollmentResponseModel, Enrollment>().ReverseMap();

        // Event Request <=> Event
        CreateMap<EventRequestModel, Event>().ReverseMap();
        // Event Response => Event
        CreateMap<EventResponseModel, Event>().ReverseMap();

        // User Request <=> User
        CreateMap<UserRequestModel, User>().ReverseMap();
        // User Response <=> User
        CreateMap<UserResponseModel, User>().ReverseMap();

        // Category <=> Category Request
        CreateMap<Category, CategoryRequestModel>().ReverseMap();
        // Category <=> Category Response
        CreateMap<Category, CategoryResponseModel>().ReverseMap();

        // WishList <=> WishList Request
        CreateMap<WishList, WishListRequestModel>().ReverseMap();
        // WishList <=> WishList Reponse
        CreateMap<WishList, WishListResponseModel>().ReverseMap();
    }
}