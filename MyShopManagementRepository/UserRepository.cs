using MyShopManagementBO;
using MyShopManagementDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementRepository
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAll() => UserDAO.Instance.GetAll();
        public List<Role> GetRoles() => UserDAO.Instance.GetRoles();
        public User Get(string email) => UserDAO.Instance.Get(email);
        public void Create(User entity) => UserDAO.Instance.Create(entity);
        public void Update(User entity) => UserDAO.Instance.Update(entity);
        public void Delete(string email) => UserDAO.Instance.Delete(email);
        public bool Exist(string email) => UserDAO.Instance.Exist(email);
    }
}
