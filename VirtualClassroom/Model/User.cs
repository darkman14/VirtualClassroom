using System.ComponentModel;

namespace VirtualClassroom.Model
{
	public class User : INotifyPropertyChanged
    {
        public enum Role { Administrator, Profesor, Asistent }
        #region Fields
        private int id;
        private string name;
        private string surname;
        private string email;
        private string username;
        private string password;
        private Role userRole;
        private int profesorId;
        
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

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public int ProfesorId
        {
            get
            {
                return profesorId;
            }
            set
            {
                profesorId = value;
                OnPropertyChanged(nameof(ProfesorId));
            }
        }

        public Role UserRole
        {
            get
            {
                return userRole;
            }

            set
            {
                userRole = value;
                OnPropertyChanged(nameof(UserRole));
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