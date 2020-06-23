﻿using System;
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
    /// Interaction logic for ClassroomsWindow.xaml
    /// </summary>
    public partial class ClassroomsWindow : Window, INotifyPropertyChanged
	{
		IClassroomsInterface _classroom = new ClassroomRepo();
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

		public int ClassId
		{
			get
			{
				return ClassId;
			}

			set
			{
				ClassId = value;
				OnPropertyChanged(nameof(ClassId));
			}
		}
		public ClassroomsWindow(User user)
		{
			InitializeComponent();
			IEnumerable<Classroom> Classroom = _classroom.GetAll();
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
			dgClassrooms.ItemsSource = Classroom;

		}
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			Classroom classroom = (Classroom)dgClassrooms.SelectedItem;
			_classroom.Delete(classroom.Id);
			dgClassrooms.ItemsSource = _classroom.GetAll();
		}


		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtNumberClassroom.Text) || string.IsNullOrEmpty(txtNoSeats.Text) || string.IsNullOrEmpty(txtTypeOfClassroom.Text))
			{
				MessageBox.Show("Sva polja za unos moraju biti popunjena", "Info poruka", MessageBoxButton.OK);
				return;
			}

			List<Classroom> classrooms = (List<Classroom>)_classroom.GetAll();

			if (classrooms.Any(x => x.Code == txtCode.Text))
			{
				MessageBox.Show("Ustanova sa datom sifrom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
			}
			else
			{
				Classroom classroom = new Classroom() { Code = txtCode.Text, ClassroomNo = GetNo(txtNumberClassroom.Text), SeatsNo = GetNo(txtNoSeats.Text), TypeOfClassroom = txtTypeOfClassroom.Text };
				bool clSuccessfull = _classroom.Add(classroom);
			}

			txtCode.Clear();
			txtNumberClassroom.Clear();
			txtNoSeats.Clear();
			txtTypeOfClassroom.Clear();
			dgClassrooms.ItemsSource = _classroom.GetAll();
		}
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<string, string> searchParameters = new Dictionary<string, string>();

			searchParameters.Add("Code", txtSearchCode.Text);
			searchParameters.Add("ClassroomNo", txtSearchClassroomNo.Text);
			searchParameters.Add("SeatsNo", txtSearchSeatsNo.Text);
			searchParameters.Add("TypeOfClassroom", txtSearchTypeOfClassroom.Text);


			if (CheckParameters(searchParameters))
			{
				MessageBox.Show("Uneto vise od jednog parametra pretrage!", "Info poruka", MessageBoxButton.OK);
				txtSearchCode.Clear();
				txtSearchClassroomNo.Clear();
				txtSearchSeatsNo.Clear();
				txtSearchTypeOfClassroom.Clear();

			}
			else
			{
				dgClassrooms.ItemsSource = _classroom.GetByParameters(searchParameters);
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
			Classroom classroom = instHelp.DataContext as Classroom;

			txtChangeCode.Text = classroom.Code;
			txtChangeClassroomNo.Text = classroom.ClassroomNo.ToString();
			txtChangeSeatsNo.Text = classroom.SeatsNo.ToString();
			txtChangeTypeOfClassroom.Text = GetClassroomType(classroom.TypeOfClassroom);
			ClassId = classroom.Id;
			IsChange = true;

		}

		private string GetClassroomType(Classroom.ClassroomType typeOfClassroom)
		{
			if (typeOfClassroom == 0)
			{
				return "Da";
			}
			return "Ne";
		}

		private void btnChange_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtChangeCode.Text) || string.IsNullOrEmpty(txtChangeClassroomNo.Text) || string.IsNullOrEmpty(txtChangeSeatsNo.Text) || string.IsNullOrEmpty(txtChangeTypeOfClassroom.Text))
			{
				MessageBox.Show("Potrebno je da sva polja budu popunjena", "Info poruka", MessageBoxButton.OK);
				return;
			}

			List<Classroom> classrooms = (List<Classroom>)_classroom.GetAll();

			if (classrooms.Any(x => x.Code == txtChangeCode.Text))
			{
				MessageBox.Show("Ustanova sa datom sifrom vec postoji. Unesite ponovo!", "Info poruka", MessageBoxButton.OK);
			}
			else
			{
				Classroom classroom = new Classroom() { Id = ClassId, Code = txtChangeCode.Text, ClassroomNo = GetNo(txtChangeClassroomNo.Text), SeatsNo = GetNo(txtChangeSeatsNo.Text), TypeOfClassroom = txtChangeTypeOfClassroom.Text };
				_classroom.Update(classroom);
			}

			IsChange = false;
			txtChangeCode.Clear();
			txtChangeClassroomNo.Clear();
			txtChangeSeatsNo.Clear();
			txtChangeTypeOfClassroom.Clear();
			dgClassrooms.ItemsSource = _classroom.GetAll();
		}

		private int GetNo(string text)
		{
			int outNumber;
			bool isValid = Int32.TryParse(text, out outNumber);

			if (isValid)
			{
				return outNumber;
			}
			return 0;
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
