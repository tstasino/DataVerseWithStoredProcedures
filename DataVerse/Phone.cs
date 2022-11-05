//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace DataVerse
{
    using Foolproof;
    using System;
    using System.Collections.Generic;
    
    public partial class Phone
    {
        public int Customer_id { get; set; }

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
        //[RegularExpression("True|true|true", ErrorMessage = "At least one field must be given a value")]
        //public bool one => HomePhone != null || WorkPhone != null || CellPhone != null;
        public virtual Customer Customer { get; set; }
    }
}
