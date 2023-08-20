using MockUsers.DTO;
using MockUsers.Models;

namespace MockUsers.UsersInteralSemiDb
{
    /*
     SIDE NOTE:
    the correct way would be to implement this with a model User class and with mappers but for 
    Lack of time in the exam i did it like this which works for this scenario
      */
    public class UserDb : IUserDb
    {
        private int p_counter = 0;
        private List<NewStaticUserDTO> p_users = new List<NewStaticUserDTO>();
        public UserDb()
        {
               
        }
        public int AddUser(NewStaticUserDTO user)
        {
            user.Id = p_counter++;
            p_users.Add(user);
            return p_counter;
        }
        public NewStaticUserDTO GetUser(int id)
        {
            return p_users.FirstOrDefault(user => user.Id == id);
        }
        
        public int UpdateUser(UpdateStaticUserDTO user)
        {
            var existingUser = p_users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                // Update fields that are not null in the UpdateStaticUserDTO
                if (!string.IsNullOrEmpty(user.Name))
                    existingUser.Name = user.Name;

                if (!string.IsNullOrEmpty(user.Email))
                    existingUser.Email = user.Email;

                if (!string.IsNullOrEmpty(user.Gender))
                    existingUser.Gender = user.Gender;

                if (!string.IsNullOrEmpty(user.Phone))
                    existingUser.Phone = user.Phone;

                if (!string.IsNullOrEmpty(user.Country))
                    existingUser.Country = user.Country;

                return existingUser.Id;
            }

            return -1; // Indicate that the user with the given ID was not found

        }
       

    }
}
