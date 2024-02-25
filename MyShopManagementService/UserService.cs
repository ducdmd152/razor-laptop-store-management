using MyShopManagementBO;
using MyShopManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService() {
            _userRepository = new UserRepository();
        }

        public List<User> GetAll() => _userRepository.GetAll();
        public List<Role> GetRoles() => _userRepository.GetRoles();
        public User Get(string email) => _userRepository.Get(email);
        public void Create(User entity) => _userRepository.Create(entity);
        public void Update(User entity) => _userRepository.Update(entity);
        public void Delete(string email) => _userRepository.Delete(email);
        public bool Exist(string email) => _userRepository.Exist(email);
		public bool Enabled(string email)
		{
			var user = _userRepository.Get(email);
			return user != null && user.Enabled;
		}
		public bool Authenticate(string email, string password)
        {
            var user = _userRepository.Get(email);
            return user != null && user.Enabled && user.Password == password;
        }
	}
}
