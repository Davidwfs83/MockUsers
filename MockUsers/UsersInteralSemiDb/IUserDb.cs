using MockUsers.DTO;

namespace MockUsers.UsersInteralSemiDb
{
    public interface IUserDb
    {
        public int AddUser(NewStaticUserDTO user);
        public NewStaticUserDTO GetUser(int id);
        public int UpdateUser(UpdateStaticUserDTO user);
    }
}
