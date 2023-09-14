using System;
using System.ComponentModel.DataAnnotations;

namespace atm_web.Models.Data
{

    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }

        public int PIN { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        

        // Other properties
    }


}



