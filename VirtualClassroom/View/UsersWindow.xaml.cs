using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VirtualClassroom.Interfaces;
using VirtualClassroom.Model;
using VirtualClassroom.Repository;
using static VirtualClassroom.Model.User;

namespace VirtualClassroom.View
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window, INotifyPropertyChanged
    {
        IUserInterface _user = new UserRepo();
        bool isLogged, isAdmin, isChange;
        Visibility isVisible;
        Dictionary<int, string> userRoles = new Dictionary<int, string>();
        int userId;

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

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsChange
        {
            get
            {
                return isChange;
            }

            set
            {
                isChange = value;
                OnPropertyChanged(nameof(IsChange));
            }
        }

        public Visibility _isVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(_isVisible));
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        public Dictionary<int, string> UserRoles
        {
            get
            {
                return userRoles;
            }
            set
            {
                userRoles = value;
                OnPropertyChanged(nameof(UserRoles));
            }
        }

        public UsersWindow(User user)
        {
            InitializeComponent();
            IEnumerable<User> users = _user.GetAll();
            isChange = false;
            userRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToDictionary(t => (int)t, t => t.ToString());

            if (user == null)
            {
                isLogged = false;
                isAdmin = false;
                isVisible = Visibility.Hidden;
            }
            else if (user.UserRole == User.Role.Administrator)
            {
                isLogged = true;
                isAdmin = true;
                isVisible = Visibility.Visible;
            }
            else
            {
                isLogged = true;
                isAdmin = false;
                isVisible = Visibility.Hidden;
            }

            DataContext = this;
            dgUser.ItemsSource = users;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)dgUser.SelectedItem;
            if (user.UserRole == Role.Administrator)
            {
                MessageBox.Show("Korisnik sa rolom administrator ne moze biti izbrisan", "Info poruka", MessageBoxButton.OK);
                return;
            }
            _user.DeleteUser(user.Id);
            dgUser.ItemsSource = _user.GetAll();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Sva polja za unos moraju biti popunjena", "Info poruka", MessageBoxButton.OK);
                return;
            }

            List<User> users = (List<User>)_user.GetAll();

            if (users.Any(x => x.Name == txtName.Text))
            {
                MessageBox.Show("Korisnik sa datim imenom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
            }
            else
            {
                User korisnik = new User() { Name = txtName.Text, Surname = txtSurname.Text, Email = txtEmail.Text, Username = txtUsername.Text, Password = txtPassword.Text };
                //bool usSuccessfull = _user.AddUser(korisnik);
            }

            txtName.Clear();
            txtSurname.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            dgUser.ItemsSource = _user.GetAll();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> searchParameters = new Dictionary<string, string>();

            searchParameters.Add("Name", txtSearchName.Text);
            searchParameters.Add("Surname", txtSearchSurname.Text);
            searchParameters.Add("Email", txtSearchEmail.Text);
            searchParameters.Add("Username", txtSearchUsername.Text);
            searchParameters.Add("Password", txtSearchPassword.Text);



            if (CheckParameters(searchParameters))
            {
                MessageBox.Show("Uneto vise od jednog parametra pretrage!", "Info poruka", MessageBoxButton.OK);
                txtSearchName.Clear();
                txtSearchSurname.Clear();
                txtSearchEmail.Clear();
                txtSearchUsername.Clear();
                txtSearchPassword.Clear();

            }
            else
            {
                //dgUser.ItemsSource = _user.GetByParameters(searchParameters);
            }
        }

        private bool CheckParameters(Dictionary<string, string> searchParameters)
        {
            int count = 0;
            foreach (string param in searchParameters.Values)
            {
                if (!string.IsNullOrEmpty(param))
                {
                    count++;
                }
            }

            if (count <= 1)
            {
                return false;
            }

            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var usHelp = (Button)sender;
            User user = usHelp.DataContext as User;

            txtChangeName.Text = user.Name;
            txtChangeSurname.Text = user.Surname.ToString();
            txtChangeEmail.Text = user.Email.ToString();
            txtChangeUsername.Text = user.Username.ToString();
            txtChangePassword.Text = user.Password.ToString();
            UserId = user.Id;
            IsChange = true;

        }


        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtChangeName.Text) || string.IsNullOrEmpty(txtChangeSurname.Text) || string.IsNullOrEmpty(txtChangeEmail.Text) || string.IsNullOrEmpty(txtChangeUsername.Text) || string.IsNullOrEmpty(txtChangePassword.Text))
            {
                MessageBox.Show("Potrebno je da sva polja budu popunjena", "Info poruka", MessageBoxButton.OK);
                return;
            }

            List<User> user = (List<User>)_user.GetAll();

            if (user.Any(x => x.Name == txtChangeName.Text))
            {
                MessageBox.Show("Korisnik sa datim imenom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
            }
            else
            {
                User users = new User() { Id = UserId, Name = txtChangeName.Text, Surname = txtChangeSurname.Text, Email = txtChangeEmail.Text, Username = txtChangeUsername.Text, Password = txtChangePassword.Text };
                _user.UpdateUser(users);
            }

            IsChange = false;
            txtChangeName.Clear();
            txtChangeSurname.Clear();
            txtChangeEmail.Clear();
            txtChangeUsername.Clear();
            txtChangePassword.Clear();
            dgUser.ItemsSource = _user.GetAll();
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


