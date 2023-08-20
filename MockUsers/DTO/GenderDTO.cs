using MockUsers.Validation;

namespace MockUsers.DTO
{
    public class GenderDTO
    {
        [GenderValidation(AllowableValues = new[] { "male", "female" }, ErrorMessage = "Gender must be either 'male' or 'female'.")]
        public string Gender { get; set; }
    }
}
