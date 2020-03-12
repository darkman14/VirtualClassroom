using System.Collections.Generic;
using VirtualClassroom.Model;

namespace VirtualClassroom.Interfaces
{
	public interface IInstitutionInterface
    {
        IEnumerable<Institution> GetAll();
		IEnumerable<Institution> GetByParameters(Dictionary<string, string> searchParameters);
		bool Add(Institution institution);
        void Update(Institution institution);
        void Delete(int id);
    }
}
