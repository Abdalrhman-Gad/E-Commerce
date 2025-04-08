namespace Application.DTOs.User
{
    public class UserDTO
    {
        public string UserName { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public List<string> ErrorMessages { get; set; } = [];
    }
}
