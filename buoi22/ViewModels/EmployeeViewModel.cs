using System.ComponentModel.DataAnnotations;

namespace buoi22.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter salary")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Please choose Profile Image ")]
        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }
    }
}
