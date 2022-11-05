using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DataVerse.Models
{
    public class CustomerViewModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50,ErrorMessage ="Name cannot exceeed 50 characters")]
        //[RegularExpression("^[a-zA-Z ]*$")]
        [RegularExpression(@"^\D*$", ErrorMessage = "No numbers allowed in First Name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceeed 50 characters")]
        //[RegularExpression("^[a-zA-Z ]*$")]
        [RegularExpression(@"^\D*$", ErrorMessage = "No numbers allowed in Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        public int id { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Home Phone")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Please enter valid Home Phone Number")]
        public Nullable<long> HomePhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Work Phone")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Please enter valid Work Phone Number")]
        public Nullable<long> WorkPhone { get; set; }

        [RegularExpression("[0-9]{10}", ErrorMessage = "Please enter valid Cell Phone Number")]
        [Display(Name = "Cell Phone")]
        public Nullable<long> CellPhone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(HomePhone.ToString()) && string.IsNullOrEmpty(WorkPhone.ToString()) 
                                     && string.IsNullOrEmpty(CellPhone.ToString()) )
                yield return new ValidationResult("Must provide value for either Home,Work or Cell Phone.", new [] { "HomePhone", "WorkPhone","CellPhone" });
        }
        //[RegularExpression("True|true|true", ErrorMessage = "At least one field must be given a value")]
        //public bool one => HomePhone != null || WorkPhone != null || CellPhone != null;

        //[Range(1, Double.MaxValue, ErrorMessage = "At least one field of Home, Work, Cell Phone must be given a value")]
        //public int Count
        //{
        //    get
        //    {
        //        var totalLength = 0;
        //        totalLength += HomePhone.ToString()?.Length ?? 0;
        //        totalLength += WorkPhone.ToString()?.Length ?? 0;
        //        totalLength += CellPhone.ToString()?.Length ?? 0;

        //        return totalLength;
        //    }
        //}
    }
}