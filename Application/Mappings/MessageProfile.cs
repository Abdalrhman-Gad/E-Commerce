using Domain.DTOs;
using AutoMapper;
using Domain.DTOs.Message;
using Domain.Enums;
using Domain.Models;

namespace Application.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            
            CreateMap<CreateMessageDto, Message>()
                .ForMember(dest => dest.MessageStatus, opt => opt.MapFrom(src => MessageStatus.Sent)) // Set default MessageStatus to Sent
                .ForMember(dest => dest.MessageDate, opt => opt.MapFrom(src => DateTime.UtcNow)); // Set the current time as MessageDate

           
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.MessageStatus, opt => opt.MapFrom(src => src.MessageStatus.ToString())); // Convert enum to string
        }
    }
}
