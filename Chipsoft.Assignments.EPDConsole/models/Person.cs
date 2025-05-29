namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNbr { get; set; }

        public Person() { }

        public Person(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNbr)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNbr = phoneNbr;
        }

        protected void Print()
        {
            Console.WriteLine($"Naam: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Telefoon: {PhoneNbr}");
        }
    }
}
