using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Address
    {
        public string Street { get; set; }
        public int HouseNbr { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public Address(string street, int houseNbr, int postalCode, string city)
        {
            Street = street;
            HouseNbr = houseNbr;
            PostalCode = postalCode;
            City = city;
        }

        public void Print()
        {
            Console.WriteLine($"Adres: {Street} {HouseNbr}, {PostalCode} {City}");
        }
    }
}