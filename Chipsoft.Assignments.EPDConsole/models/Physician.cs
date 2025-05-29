namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Physician : Person
    {
        public string RizivNbr { get; set; }
        public string? EmployeeId { get; set; }

        public Physician() { }

        public Physician(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNbr,
                         string rizivNbr, string employeeId = null)
            : base(firstName, lastName, dateOfBirth, email, phoneNbr)
        {
            RizivNbr = rizivNbr;
            EmployeeId = employeeId;
        }

        public new void Print()
        {
            base.Print();
            Console.WriteLine($"RIZIV: {RizivNbr}");
        }
    }
}