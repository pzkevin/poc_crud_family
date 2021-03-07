using System;
namespace POC_CRUDFamily.Users
{
    public class Admin :User 
    {
        public Admin(string name, string lastname, string motherLastname, string cellphone, string email, string street, string houseNumber, string residential, string city, string postalCode)
        {
            this.Name = name;
            this.Lastname = lastname;
            this.MotherLastname = motherLastname;
            this.Cellphone = cellphone;
            this.Email = email;
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.Residential = residential;
            this.City = city;
            this.PostalCode = postalCode;
        }
    }
}
