using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassroom.Model;

namespace VirtualClassroom
{
    class VirtualClassroomApp
    {
        public ObservableCollection<User> Users { get; set; }
        private static VirtualClassroomApp instance = new VirtualClassroomApp();

        public static VirtualClassroomApp Instance
        {
            get
            {
                return instance;
            }
        }

        private VirtualClassroomApp()
        {
            Users = new ObservableCollection<User>();
        }
    }
}
