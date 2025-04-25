using Application.Interfaces;
using Domain.DTOs.Conversation;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Infrastructure.Persistence;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConversationService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<ConversationDto>> GetAllConversationsWithUnreadMessagesForAdminsAsync()
        {
            // Step 1: Get all admin user IDs
            var admins = await _userManager.GetUsersInRoleAsync("admin");
            var adminIds = admins.Select(a => a.Id).ToList();

            // Step 2: Find conversations that contain unread messages for any admin
            var conversations = await _context.Conversations
                .Where(c => c.Messages.Any(m =>
                    adminIds.Contains(m.ReceiverID) &&
                    m.MessageStatus != MessageStatus.Read))
                .Include(c => c.Messages.Where(m =>
                    adminIds.Contains(m.ReceiverID) &&
                    m.MessageStatus != MessageStatus.Read))
                .ToListAsync();

            return _mapper.Map<List<ConversationDto>>(conversations);
        }
        public async Task<ConversationDto> CreateConversationAsync(CreateConversationDto dto)
        {
            // Create a new conversation
            var conversation = new Conversation
            {
                FirstUserID = dto.FirstUserID,
                SecondUserID = dto.SecondUserID,
                CreatedDate = DateTime.UtcNow
            };

            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            // Return the created conversation as DTO
            return _mapper.Map<ConversationDto>(conversation);
        }

        public async Task<ConversationDto> GetConversationByIdAsync(int conversationId)
        {
            // Get conversation by ID
            var conversation = await _context.Conversations
                .Include(c => c.Messages)  // Include messages in the conversation
                .FirstOrDefaultAsync(c => c.ConversationID == conversationId);

            if (conversation == null)
            {
                return null;
            }

            // Return the conversation as DTO
            return _mapper.Map<ConversationDto>(conversation);
        }
        // Get all conversations for a specific user
        public async Task<IEnumerable<ConversationDto>> GetConversationsByUserIdAsync(string userId)
        {
            var conversations = await _context.Conversations
                .Where(c => c.FirstUserID == userId || c.SecondUserID == userId)
                .Select(c => new ConversationDto
                {
                    ConversationID = c.ConversationID,
                    FirstUserID = c.FirstUserID,
                    SecondUserID = c.SecondUserID,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            return conversations;
        }
        public async Task<IEnumerable<ConversationDto>> GetAllConversationsAsync()
        {
            // Get all conversations
            var conversations = await _context.Conversations
                .Include(c => c.Messages)
                .ToListAsync();

            // Return all conversations as DTO
            return _mapper.Map<IEnumerable<ConversationDto>>(conversations);
        }
    }
}
