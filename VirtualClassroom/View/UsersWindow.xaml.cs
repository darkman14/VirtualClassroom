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
	public partial class UsersWindow : Window, INotifyPropertyChanged
    {
		IInstitutionInterface _institution = new InstitutionRepo();
		IUserInterface _user = new UserRepo();
		bool isLogged, isAdmin, isChange;
		Visibility isVisible;
		int instId = 0;

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

		public int InstId
		{
			get
			{
				return instId;
			}

			set
			{
				instId = value;
				OnPropertyChanged(nameof(InstId));
			}
		}

		public UsersWindow(User user)
		{
			InitializeComponent();
			IEnumerable<Institution> Institutions = _institution.GetAll();
			isChange = false;
			List<string> UserRoles = Enum.GetNames((typeof(User.Role))).ToList();

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
			dgInstitutions.ItemsSource = Institutions;

		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			Institution institution = (Institution)dgInstitutions.SelectedItem;
			_institution.Delete(institution.Id);
			dgInstitutions.ItemsSource = _institution.GetAll();
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			//if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAddress.Text))
			//{
			//	MessageBox.Show("Sva polja za unos moraju biti popunjena", "Info poruka", MessageBoxButton.OK);
			//	return;
			//}

			//List<Institution> institutions = (List<Institution>)_institution.GetAll();

			//if (institutions.Any(x => x.Code == txtCode.Text))
			//{
			//	MessageBox.Show("Ustanova sa datom sifrom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
			//}
			//else
			//{
			//	Institution institution = new Institution() { Code = txtCode.Text, Name = txtName.Text, Address = txtAddress.Text };
			//	bool isSuccessfull = _institution.Add(institution);
			//}

			//txtCode.Clear();
			//txtName.Clear();
			//txtAddress.Clear();
			//dgInstitutions.ItemsSource = _institution.GetAll();
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<string, string> searchParameters = new Dictionary<string, string>();

			searchParameters.Add("Code", txtSearchCode.Text);
			searchParameters.Add("Name", txtSearchName.Text);
			searchParameters.Add("Address", txtSearchAddress.Text);

			if (CheckParameters(searchParameters))
			{
				MessageBox.Show("Uneto vise od jednog parametra pretrage!", "Info poruka", MessageBoxButton.OK);
				txtSearchCode.Clear();
				txtSearchName.Clear();
				txtSearchAddress.Clear();
			}
			else
			{
				dgInstitutions.ItemsSource = _institution.GetByParameters(searchParameters);
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
			var instHelp = (Button)sender;
			Institution institution = instHelp.DataContext as Institution;

			txtChangeCode.Text = institution.Code;
			txtChangeName.Text = institution.Name;
			txtChangeAddress.Text = institution.Address;
			InstId = institution.Id;
			IsChange = true;
		}

		private void btnChange_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtChangeCode.Text) || string.IsNullOrEmpty(txtChangeName.Text) || string.IsNullOrEmpty(txtChangeAddress.Text))
			{
				MessageBox.Show("Potrebno je da sva polja budu popunjena", "Info poruka", MessageBoxButton.OK);
				return;
			}

			List<Institution> institutions = (List<Institution>)_institution.GetAll();

			if (institutions.Any(x => x.Code == txtChangeCode.Text))
			{
				MessageBox.Show("Ustanova sa datom sifrom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
			}
			else
			{
				Institution institution = new Institution() { Id = InstId, Code = txtChangeCode.Text, Name = txtChangeName.Text, Address = txtChangeAddress.Text };
				_institution.Update(institution);
			}

			IsChange = false;
			txtChangeCode.Clear();
			txtChangeName.Clear();
			txtChangeAddress.Clear();
			dgInstitutions.ItemsSource = _institution.GetAll();
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
