using System.Collections.Generic;
using VirtualClassroom.Model;

namespace VirtualClassroom.Repository
{
    public interface IUserInterface
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool IsLogged(string username, string password, out User user);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User GetByUsername(string username);       
    }
}
