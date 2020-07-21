using System.Collections.Generic;
using VirtualClassroom.Model;

namespace VirtualClassroom.Repository
{
    public interface IUserInterface
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool IsLogged(string username, string password, out User user);
        bool Add(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User GetByUsername(string username);
        List<User> GetByRole(int index);
    }
}
