using LMusic.Models;
using LMusic.Models.Requests;
using LMusic.Registries;

namespace LMusic.Services
{
    public class UserService : DbServiceAbstract<User>
    {
        private UserRegistry _userRegisty;

        public UserService() : base(new UserRegistry())
        {
            _userRegisty = (UserRegistry)_registry;
        }

        public User CreateUserByTg(TelegrammUser tgUser)
        {
            var user = new User(tgUser);
            _userRegisty.Add(user);
            return user;
        }

        public User? GetUserByTg(TelegrammUser tgUser, bool createIfNull)
        {
            User user = _userRegisty.GetUserByTgId(tgUser.id);
            if(user == null && createIfNull)
            {
                user = CreateUserByTg(tgUser);
            }
            return user;
        }
    }
}
