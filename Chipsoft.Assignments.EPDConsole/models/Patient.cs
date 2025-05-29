namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Patient : Person
    {
        public string SocialSecurityNbr { get; set; }
        public Address Address { get; set; }

        public Patient() { }

        public Patient(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNbr,
                       string socialSecurityNbr, Address address)
            : base(firstName, lastName, dateOfBirth, email, phoneNbr)
        {
            SocialSecurityNbr = socialSecurityNbr;
            Address = address;
        }

        public new void Print()
        {
            base.Print();
            Console.WriteLine($"RRN: {SocialSecurityNbr}");
            Address.Print();
        }
    }
}
