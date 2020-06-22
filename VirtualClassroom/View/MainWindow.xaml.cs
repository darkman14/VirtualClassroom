using System.ComponentModel;
using System.Windows;
using VirtualClassroom.Model;
using VirtualClassroom.Repository;
using VirtualClassroom.View;

namespace VirtualClassroom
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool isLogged;
        private bool unknownUser;
		private User user;
        IUserInterface _user = new UserRepo();

        public bool UnknownUser
        {
            get { return unknownUser; }
            set
            {
                unknownUser = !IsLogged;
                OnPropertyChanged(nameof(UnknownUser));
            }
        }

        public bool IsLogged
        {
            get
            {
                return isLogged;
            }

            set
            {
                isLogged = value;
                OnPropertyChanged(nameof(IsLogged));
            }
        }

		public User User
		{
			get
			{
				return user;
			}

			set
			{
				user = value;
			}
		}

		public MainWindow()
        {
            InitializeComponent();
            IsLogged = false;
            UnknownUser = !IsLogged;
            DataContext = this;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            IsLogged = _user.IsLogged(txtUsername.Text, txtPassword.Password, out user);
            if (!isLogged)
            {
                MessageBox.Show("Pogresno korisnicko ime i/ili lozinka! Pokusajte ponovo.", "Info poruka", MessageBoxButton.OK);
            }
            DataContext = this;
        }

        private void btnInstitution_Click(object sender, RoutedEventArgs e)
        {
            InstitutionsWindow institutionWindow = new InstitutionsWindow(User);
            institutionWindow.ShowDialog();
        }

        private void btnClassroom_Click(object sender, RoutedEventArgs e)
        {
            ClassroomsWindow classroomWindow = new ClassroomsWindow();
            classroomWindow.ShowDialog();
        }

        private void btnCalendarInfo_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.ShowDialog();
        }

        private void btnTimetable_Click(object sender, RoutedEventArgs e)
        {
            TimetableWindow timetableWindow = new TimetableWindow();
            timetableWindow.ShowDialog();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow(User);
            usersWindow.ShowDialog();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            PersonalInfoWindow personalInfoWindow = new PersonalInfoWindow();
            personalInfoWindow.ShowDialog();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
			IsLogged = false;
			DataContext = this;
        }

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
