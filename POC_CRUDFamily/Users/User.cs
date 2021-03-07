using System;
using POC_CRUDFamily.App;

namespace POC_CRUDFamily.Users
{
    public class User : CRUD
    {
        //constructor defaul
        public User()
        {

        }

        //attribs
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string MotherLastname { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Residential { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }


        
    }
}
