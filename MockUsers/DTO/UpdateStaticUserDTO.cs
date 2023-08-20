using MockUsers.Validation;
using System.ComponentModel.DataAnnotations;

namespace MockUsers.DTO
{
    public class UpdateStaticUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [GenderValidation]
        public string Gender { get; set; }
        
        public string Phone { get; set; }
        
        public string Country { get; set; }
    }
}
