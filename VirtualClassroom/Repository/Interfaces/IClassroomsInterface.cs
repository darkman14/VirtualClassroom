using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Model;

namespace VirtualClassroom.Interfaces
{
    public interface IClassroomsInterface
    {
        IEnumerable<Classroom> GetAll();
        IEnumerable<Classroom> GetByParameters(Dictionary<string, string> searchParameters);
        bool Add(Classroom classroom);
        void Update(Classroom classroom);
        void Delete(int Id);
    }
}
