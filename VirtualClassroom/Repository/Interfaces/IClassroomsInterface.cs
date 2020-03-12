using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassroom.Model;

namespace VirtualClassroom.Interfaces
{
    public interface IClassroomsInterface
    {
        IQueryable<Classroom> GetAll();
        void Add(Classroom classroom);
        void Update(int id);
        void Delete(int Id);
    }
}
