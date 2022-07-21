using AutoMapper;
using ems_backend.Entities;
using ems_backend.Models;

#pragma warning disable

public class UserProfile: Profile
{
    public UserProfile()
    {
        // Enrollment Request => Enrollment
        CreateMap<EnrollmentRequestModel, Enrollment>()
        .ForMember(dest => dest.Event, act => act.MapFrom(src => (Event)null))
        .ForMember(dest => dest.User, act => act.MapFrom(src => (User)null));

        // Enrollment => Enrollment Request
        CreateMap<Enrollment, EnrollmentRequestModel>();

        // Enrollment Response => Enrollment
        CreateMap<EnrollmentResponseModel, Enrollment>()
        .ForMember(dest => dest.Event, act => act.MapFrom(src => (Event)null))
        .ForMember(dest => dest.User, act => act.MapFrom(src => (User)null));

        // Enrollment => Enrollment Response
        CreateMap<Enrollment, EnrollmentResponseModel>();



        // Event Request => Event
        CreateMap<EventRequestModel, Event>().ReverseMap();

        // Event => Event Request
        // CreateMap<Event, EventRequestModel>();

        // Event Response => Event
        CreateMap<EventResponseModel, Event>().ReverseMap();

        // Event => Event Response
        // CreateMap<Event, EventResponseModel>();


        // User Request => User
        CreateMap<UserRequestModel, User>().ReverseMap();

        // User => User Request
        // CreateMap<User, UserRequestModel>();

        // User Response <=> User
        CreateMap<UserResponseModel, User>().ReverseMap();

        // User => User Response
        // CreateMap<User, UserResponseModel>();

        CreateMap<Category, CategoryRequestModel>().ReverseMap();
        CreateMap<Category, CategoryResponseModel>().ReverseMap();

        CreateMap<WishList, WishListRequestModel>().ReverseMap();
        CreateMap<WishList, WishListResponseModel>().ReverseMap();
    }
}