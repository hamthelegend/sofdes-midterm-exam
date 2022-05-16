using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SofdesMidtermExam
{
    public sealed partial class MainPage : Page
    {

        ObservableCollection<Student> students = new ObservableCollection<Student>();

        List<string> genders = new List<string>(){ "Male", "Female", "Unspecified" };

        public MainPage()
        {
            InitializeComponent();
        }

        private void UpdateAddUpdateButton(object sender, SelectionChangedEventArgs e)
        {
            if (studentGrid.SelectedIndex == -1)
            {
                addUpdateSymbol.Symbol = Symbol.Add;
                addUpdateLabel.Text = "Add";
            }
            else
            {
                addUpdateSymbol.Symbol = Symbol.Download;
                addUpdateLabel.Text = "Update";
            }
        }

        private void AddUpdate(object sender, RoutedEventArgs e)
        {
            var selectedStudentIndex = studentGrid.SelectedIndex;
            if (selectedStudentIndex == -1)
            {
                Add();
            }
            else
            {
                Update(selectedStudentIndex);
            }
        }

        private void Add()
        {
            try
            {
                var student = ParseStudent();
                students.Add(student);
                Clear();
            }
            catch (InputException exception)
            {
                Notification.Show(exception.Message, 2000);
            }
        }



        private void Update(int selectedStudentIndex)
        {
            try
            {
                var selectedStudent = students[selectedStudentIndex];
                var updatedStudent = ParseStudent();
                students.Remove(selectedStudent);
                students.Insert(selectedStudentIndex, updatedStudent);
                Clear();
            }
            catch (InputException exception)
            {
                Notification.Show(exception.Message, 2000);
            }
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(studentGrid.SelectedItem is Student selectedStudent))
                {
                    throw new InputException("Select a student to delete from the list first");
                }
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Delete Student",
                    Content = "Are you sure you want to delete this student?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    students.Remove(selectedStudent);
                    studentGrid.SelectedIndex = -1;
                }
            }
            catch (InputException exception)
            {
                Notification.Show(exception.Message, 2000);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            firstNameInput.Text = string.Empty;
            lastNameInput.Text = string.Empty;
            emailInput.Text = string.Empty;
            mobileNumberInput.Text = string.Empty;
            genderInput.SelectedIndex = -1;
            addressInput.Text = string.Empty;
            usernameInput.Text = string.Empty;
            passwordInput.Text = string.Empty;
            studentGrid.SelectedIndex = -1;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void StudentEdit(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (studentGrid.SelectedItem is Student student)
            {
                firstNameInput.Text = student.FirstName;
                lastNameInput.Text = student.LastName;
                emailInput.Text = student.Email;
                mobileNumberInput.Text = student.MobileNumber;
                genderInput.SelectedIndex = genders.IndexOf(student.Gender);
                addressInput.Text = student.Address;
                usernameInput.Text = student.Username;
                passwordInput.Text = student.Password;
            }
        }

        private Student ParseStudent()
        {
            var firstName = firstNameInput.Text;
            var lastName = lastNameInput.Text;
            var email = emailInput.Text;
            var mobileNumber = mobileNumberInput.Text;
            var gender = genderInput.SelectedItem as string;
            var address = addressInput.Text;
            var username = usernameInput.Text;
            var password = passwordInput.Text;
            if (
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(mobileNumber) ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password)
                )
            {
                throw new InputException("None of the fields can be empty,");
            }
            return new Student(firstName, lastName, email, mobileNumber, gender, address, username, password);
        }

        private void GenerateStudentGridColumns(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "FirstName":
                    e.Column.CanUserSort = false;
                    e.Column.Header = "First name";
                    e.Column.Visibility = Visibility.Visible;
                    break;
                case "LastName":
                    e.Column.CanUserSort = false;
                    e.Column.Header = "Last name";
                    e.Column.Visibility = Visibility.Visible;
                    break;
                case "Email":
                    e.Column.CanUserSort = false;
                    e.Column.Header = "Email";
                    e.Column.Visibility = Visibility.Visible;
                    break;
                case "Username":
                    e.Column.CanUserSort = false;
                    e.Column.Header = "Username";
                    e.Column.Visibility = Visibility.Visible;
                    break;
                case "Password":
                    e.Column.CanUserSort = false;
                    e.Column.Header = "Password";
                    e.Column.Visibility = Visibility.Visible;
                    break;
                default:
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
