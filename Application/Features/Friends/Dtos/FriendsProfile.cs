namespace Application.Features.Friends.Dtos
{
    public sealed class FriendsProfile : Profile
    {
        public FriendsProfile()
        {
            CreateMap<Friend, FriendRequestDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Student2.User.FirstName} {src.Student2.User.LastName}"))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.Student2.User.ProfilePicture))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Student2.Grade))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Student2.Id));
        }
    }
}