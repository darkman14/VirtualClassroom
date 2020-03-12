using System;
using System.ComponentModel;

namespace VirtualClassroom.Model
{
    public class CalendarInfo : INotifyPropertyChanged
    {
        public enum DayOfWeek { Ponedeljak, Utorak, Sreda, Cetvrtak, Petak, Subota }
        public enum ClassType { Vezbe, Predavanja, Laboratorija }
        #region Fields
        private int id;
        private string code;
        private DateTime startTime;
        private DateTime endTime;
        private User assignedUser;
        private DayOfWeek day;
        private ClassType typeOfClass;

        private Classroom classroom;
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

        public User AssignedUser
        {
            get
            {
                return assignedUser;
            }

            set
            {
                assignedUser = value;
                OnPropertyChanged(nameof(AssignedUser));
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public Classroom Classroom
        {
            get
            {
                return classroom;
            }

            set
            {
                classroom = value;
                OnPropertyChanged(nameof(Classroom));
            }
        }

        public DayOfWeek Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
                OnPropertyChanged(nameof(Day));
            }
        }

        public ClassType TypeOfClass
        {
            get
            {
                return typeOfClass;
            }

            set
            {
                typeOfClass = value;
                OnPropertyChanged(nameof(TypeOfClass));
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
