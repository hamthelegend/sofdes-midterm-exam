using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofdesMidtermExam
{
    public class Student
    {
        public Student(string firstName, string lastName, string email, string mobileNumber, string gender, string address, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            MobileNumber = mobileNumber;
            Gender = gender;
            Address = address;
            Username = username;
            Password = password;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string MobileNumber { get; }
        public string Gender { get; }
        public string Address { get; }
        public string Username { get; }
        public string Password { get; }
    }

}
