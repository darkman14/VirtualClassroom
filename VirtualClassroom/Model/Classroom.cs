using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassroom.Model
{
    public class Classroom : INotifyPropertyChanged
    {
        public enum ClassroomType { withComp, withoutComp }

        #region Fields
        private int id;
        private string code;
        private int classroomNo;
        private int seatsNo;
        private ClassroomType typeOfClassroom;
        private Institution institution;
        #endregion Fields

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public int ClassroomNo
        {
            get
            {
                return classroomNo;
            }
            set
            {
                classroomNo = value;
                OnPropertyChanged(nameof(ClassroomNo));
            }
        }

        public int SeatsNo  
        {
            get
            {
                return seatsNo;
            }
            set
            {
                seatsNo = value;
                OnPropertyChanged(nameof(SeatsNo));
            }
        }

        public Institution Institution
        {
            get
            {
                return institution;
            }

            set
            {
                institution = value;
                OnPropertyChanged(nameof(Institution));
            }
        }

        public ClassroomType TypeOfClassroom
        {
            get
            {
                return typeOfClassroom;
            }

            set
            {
                typeOfClassroom = value;
                OnPropertyChanged(nameof(TypeOfClassroom));
            }
        }

        #endregion Properties

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
        #endregion
    }
}
