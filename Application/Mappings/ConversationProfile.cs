using AutoMapper;
using Domain.DTOs.Conversation;
using Domain.Models;

namespace Application.Mappings
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            // Mapping from Conversation entity to ConversationDto
            CreateMap<Conversation, ConversationDto>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            // Mapping from CreateConversationDto to Conversation entity (for creating a new conversation)
            CreateMap<CreateConversationDto, Conversation>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)); // Set CreatedDate to current UTC time
        }
    }
}
