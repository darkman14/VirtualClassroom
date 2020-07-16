using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VirtualClassroom.Interfaces;
using VirtualClassroom.Model;
using VirtualClassroom.Repository;

namespace VirtualClassroom.View
{
	/// <summary>
	/// Interaction logic for UsersWindow.xaml
	/// </summary>
	public partial class PersonalInfoWindow : Window, INotifyPropertyChanged
	{
		IUserInterface _personalInfo = new UserRepo();
		bool isLogged, isAdmin, isChange;
		Visibility isVisible;
		int usId = 0;

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
				return UserId;
			}

			set
			{
				UserId = value;
				OnPropertyChanged(nameof(UserId));
			}
		}

		public PersonalInfoWindow(User user)
		{
			InitializeComponent();
			user = _personalInfo.GetByUsername(user.Username);
			isChange = false;

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
			dgPersonalInfo.ItemsSource = User;

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

