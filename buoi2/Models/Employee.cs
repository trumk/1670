using System.ComponentModel.DataAnnotations.Schema;

namespace buoi2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public string ProfilePicture { get; set; }
        public Employee()
        {
            FullName = FirstName + " " + LastName;
        }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }
}
