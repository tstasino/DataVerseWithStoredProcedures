using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataVerse.Models
{
    public class CustomerVM
    {
      
        public string FirstName { get; set; }

        public string LastName { get; set; }

        
        public string Address { get; set; }

       
        public string email { get; set; }
        public int id { get; set; }

       
        public Nullable<long> HomePhone { get; set; }

       
        public Nullable<long> WorkPhone { get; set; }

       
        public Nullable<long> CellPhone { get; set; }
    }
}